using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Unity 에디터 내에서 `Resources` 폴더 구조를 시각적으로 관리할 수 있도록 돕는 `StoryEditor` 창.
/// 스토리 폴더 내의 파일을 관리하며, 드래그 앤 드롭으로 특정 폴더에 파일을 추가할 수 있도록 함.
/// </summary>
public class StoryEditor : EditorWindow
{
    private Vector2 _leftScrollPos;
    private Vector2 _rightScrollPos;
    private string[] _storyFolders;
    private string _selectedFolder;
    private Dictionary<string, List<StoryFileInfo>> _folderContents;

    [MenuItem("GreenwoodIsland/스토리 관리")]
    public static void ShowWindow()
    {
        GetWindow<StoryEditor>("스토리 관리");
    }

    private void OnEnable()
    {
        LoadStoryFolders();
        StoryEditorFileHandler.SetupFileWatcher(LoadStoryFolders, LoadFolderContents, Repaint);
    }

    private void OnDisable()
    {
        StoryEditorFileHandler.DisposeFileWatcher();
    }
    private void OnGUI()
    {
        // 우측 상단에 Refresh 버튼 추가
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        
        if (GUILayout.Button("Refresh", GUILayout.Width(100)))
        {
            LoadStoryFolders(); // 폴더 현황을 다시 로드
        }
        
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.Space(20);
        EditorGUILayout.BeginHorizontal();

        DrawLeftPanel();
        DrawRightPanel();

        EditorGUILayout.EndHorizontal();

        StoryEditorFileHandler.HandleDragAndDrop(_selectedFolder, _folderContents, LoadFolderContents, Repaint);
    }


    private void DrawLeftPanel()
    {
        EditorGUILayout.BeginVertical(GUILayout.Width(220));
        _leftScrollPos = EditorGUILayout.BeginScrollView(_leftScrollPos, GUILayout.Width(220));
        DrawStoryFolders();
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }

    private void DrawRightPanel()
    {
        EditorGUILayout.BeginVertical(GUILayout.ExpandWidth(true));
        _rightScrollPos = EditorGUILayout.BeginScrollView(_rightScrollPos);
        DrawFolderContents();
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }

    private void LoadStoryFolders()
    {
        _storyFolders = StoryEditorFileHandler.GetStoryFolders();
    }
    private void DrawStoryFolders()
    {
        foreach (var folder in _storyFolders)
        {
            EditorGUILayout.BeginHorizontal();

            // 스토리 버튼
            if (GUILayout.Button(folder, GUILayout.Height(60), GUILayout.Width(160)))
            {
                _selectedFolder = folder;
                LoadFolderContents();
            }

            // Rename 버튼
            if (GUILayout.Button("Rename", GUILayout.Height(30), GUILayout.Width(50)))
            {
                StoryRenamePopUp.ShowWindow(this, folder);  // 팝업창을 띄움
            }

            // 삭제 버튼
            if (GUILayout.Button("X", GUILayout.Height(30), GUILayout.Width(30)))
            {
                if (EditorUtility.DisplayDialog("폴더 삭제", $"정말 '{folder}' 폴더를 삭제하시겠습니까?", "삭제", "취소"))
                {
                    DeleteStoryFolder(folder);
                }
            }

            EditorGUILayout.EndHorizontal();
        }

        if (GUILayout.Button("+", GUILayout.Height(60), GUILayout.Width(240))) // 폭을 넓혀줌
        {
            StoryCreationWindow.ShowWindow(this);
        }
    }
    public void RenameStoryFolder(string oldFolderName, string newFolderName)
    {
        string oldFolderPath = Path.Combine(ResourcePathManager.GetStoryResourcesBasePath(), oldFolderName);
        string newFolderPath = Path.Combine(ResourcePathManager.GetStoryResourcesBasePath(), newFolderName);

        // 폴더 이름 변경
        if (Directory.Exists(oldFolderPath) && !Directory.Exists(newFolderPath))
        {
            Directory.Move(oldFolderPath, newFolderPath);
            Debug.Log($"{oldFolderName} 폴더가 {newFolderName}으로 변경되었습니다.");
        }
        else
        {
            Debug.LogError($"'{newFolderName}'이라는 이름의 폴더가 이미 존재합니다.");
            return;
        }

        // .cs 파일 이름 변경 및 클래스 이름 변경
        string oldScriptPath = Path.Combine(ResourcePathManager.GetStoryResourcesBasePath(), newFolderName, "Scripts", $"{oldFolderName}.cs");
        string newScriptPath = Path.Combine(ResourcePathManager.GetStoryResourcesBasePath(), newFolderName, "Scripts", $"{newFolderName}.cs");

        if (File.Exists(oldScriptPath) && !File.Exists(newScriptPath))
        {
            // 파일 이름 변경
            File.Move(oldScriptPath, newScriptPath);

            // 파일 내용에서 클래스 이름 변경
            string scriptContent = File.ReadAllText(newScriptPath);
            scriptContent = scriptContent.Replace($"class {oldFolderName}", $"class {newFolderName}");
            File.WriteAllText(newScriptPath, scriptContent);

            Debug.Log($"{oldFolderName}.cs 스크립트가 {newFolderName}.cs로 변경되었고, 클래스 이름이 {newFolderName}으로 변경되었습니다.");
        }
        else
        {
            Debug.LogError($"{newFolderName}.cs 스크립트가 이미 존재하거나 변경할 수 없습니다.");
        }

        // 변경 후 AssetDatabase 갱신
        AssetDatabase.Refresh();

        // 스토리 폴더 다시 로드
        LoadStoryFolders();
    }


    private void LoadFolderContents()
    {
        _folderContents = StoryEditorFileHandler.LoadFolderContents(_selectedFolder);
    }

    private void DrawFolderContents()
    {
        if (_folderContents != null && _folderContents.Count > 0)
        {
            foreach (var folder in _folderContents)
            {
                StoryEditorUIHelper.DrawFolderHeader(folder.Key); // 시각적으로 구분된 폴더 헤더 그리기
                EditorGUILayout.BeginVertical("box");
                foreach (var content in folder.Value)
                {
                    StoryEditorUIHelper.DrawFileTile(content); // 파일 타일 그리기
                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.Space(10);
            }
        }
        else
        {
            GUILayout.Label("선택된 폴더에 파일이 없습니다.");
        }
    }

    // 스토리 폴더와 스토리 스크립트를 생성하는 메서드
    public void CreateStoryFolders(string storyName)
    {
        string basePath = ResourcePathManager.GetStoryResourcesBasePath();

        foreach (ResourceType resourceType in (ResourceType[])Enum.GetValues(typeof(ResourceType)))
        {
            string folderPath = ResourcePathManager.GetResourcePath("", storyName, resourceType, false);
            string fullFolderPath = Path.GetDirectoryName(Path.Combine(Application.dataPath, "Resources", folderPath));

            if (!Directory.Exists(fullFolderPath))
            {
                Directory.CreateDirectory(fullFolderPath);
            }
        }

        // 스토리 스크립트 생성
        CreateStoryScript(storyName);

        AssetDatabase.Refresh();
        Debug.Log($"새로운 스토리 '{storyName}' 생성이 완료되었습니다.");
        LoadStoryFolders();
    }

    // 스토리 스크립트를 생성하고 추상 클래스 구현을 수행하는 메서드
    private void CreateStoryScript(string storyName)
    {
        string scriptPath = Path.Combine(ResourcePathManager.GetStoryResourcesBasePath(), storyName, "Scripts");
        string scriptFilePath = Path.Combine(scriptPath, $"{storyName}.cs");

        if (!Directory.Exists(scriptPath))
        {
            Directory.CreateDirectory(scriptPath);
        }

        if (!File.Exists(scriptFilePath))
        {
            string scriptContent = $@"
using UnityEngine;
using System.Collections.Generic;

public class {storyName} : Story
{{
    // {storyName} 스토리의 스크립트 로직을 여기에 작성하세요.
    protected override SequentialElement StartElements => new ();

    protected override SequentialElement UpdateElements => new ();

    protected override SequentialElement ExitElements => new ();

    protected override string StoryDesc => "";
}}
";
            File.WriteAllText(scriptFilePath, scriptContent);
            Debug.Log($"{storyName}.cs 스크립트가 생성되었습니다.");

            // 스크립트 생성 후 AssetDatabase 업데이트
            AssetDatabase.Refresh();
        }
        else
        {
            Debug.LogWarning($"{storyName}.cs 스크립트는 이미 존재합니다.");
        }
    }

    private void DeleteStoryFolder(string folderName)
    {
        string folderPath = Path.Combine(ResourcePathManager.GetStoryResourcesBasePath(), folderName);

        if (Directory.Exists(folderPath))
        {
            try
            {
                // 폴더가 사용 중이거나 잠겨있는 경우를 대비해 폴더 삭제를 몇 번 시도
                DeleteFolderWithRetries(folderPath, 5); // 최대 5회 시도
                
                AssetDatabase.Refresh(); // 자산 데이터베이스 갱신
                
                EditorApplication.delayCall += () =>
                {
                    LoadStoryFolders(); // 변경된 폴더 내용 다시 로드
                    Repaint(); // 에디터 창 다시 그리기
                };
                
                Debug.Log($"{folderName} 폴더가 삭제되었습니다.");
            }
            catch (Exception e)
            {
                Debug.LogError($"폴더 삭제 중 오류가 발생했습니다: {e.Message}");
                EditorApplication.delayCall += () => AssetDatabase.Refresh();
            }
        }
        else
        {
            Debug.LogWarning("삭제할 폴더를 찾을 수 없습니다.");
        }
    }

    // 폴더를 삭제하며 최대 시도 횟수를 설정하는 메서드
    private void DeleteFolderWithRetries(string folderPath, int maxRetries)
    {
        for (int attempt = 0; attempt < maxRetries; attempt++)
        {
            try
            {
                Directory.Delete(folderPath, false); // 폴더와 하위 내용 모두 삭제
                if (!Directory.Exists(folderPath)) // 삭제가 성공했는지 확인
                {
                    return;
                }
            }
            catch (IOException)
            {
                System.Threading.Thread.Sleep(500); // 0.5초 대기 후 재시도
            }
            catch (UnauthorizedAccessException)
            {
                System.Threading.Thread.Sleep(500); // 0.5초 대기 후 재시도
            }
        }

        // 삭제 실패 시 예외 발생
        throw new IOException($"폴더를 삭제할 수 없습니다: {folderPath}");
    }
}
