using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 이 스크립트는 Unity 에디터 내에서 복잡한 `Resources` 폴더 구조를 보다 직관적으로 관리할 수 있도록 돕는 `StoryEditor` 창을 제공합니다. 
/// 사용자는 폴더와 파일을 시각적으로 탐색하고 관리할 수 있으며, 특정 규칙을 따른 폴더 구조를 이해하지 않아도 쉽게 자산을 추가하거나 삭제할 수 있습니다.
/// </summary>

public class StoryEditor : EditorWindow
{
    private Vector2 _leftScrollPos;
    private Vector2 _rightScrollPos;
    private string[] _storyFolders;
    private string _selectedFolder;
    private Dictionary<string, List<StoryFileInfo>> _folderContents;
    private FileSystemWatcher _fileWatcher; // 파일 변경 감지를 위한 FileSystemWatcher

    [MenuItem("GreenwoodIsland/스토리 관리")]
    public static void ShowWindow()
    {
        GetWindow<StoryEditor>("스토리 관리");
    }

    private void OnEnable()
    {
        LoadStoryFolders();
        SetupFileWatcher(); // 파일 시스템 감시자 설정
    }

    private void OnDisable()
    {
        if (_fileWatcher != null)
        {
            _fileWatcher.EnableRaisingEvents = false; // 비활성화 시 이벤트 발생 중지
            _fileWatcher.Dispose(); // 리소스 해제
        }
    }

    private void OnGUI()
    {
        EditorGUILayout.Space(20);
        EditorGUILayout.BeginHorizontal();

        DrawLeftPanel();
        DrawRightPanel();

        EditorGUILayout.EndHorizontal();
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
    }

    private void LoadStoryFolders()
    {
        string path = ResourcePathManager.GetStoryResourcesBasePath();
        if (Directory.Exists(path))
        {
            _storyFolders = Directory.GetDirectories(path);
            for (int i = 0; i < _storyFolders.Length; i++)
            {
                _storyFolders[i] = Path.GetFileName(_storyFolders[i]);
            }
        }
        else
        {
            _storyFolders = new string[0];
        }
    }

    private void DrawStoryFolders()
    {
        foreach (var folder in _storyFolders)
        {
            if (GUILayout.Button(folder, GUILayout.Height(60), GUILayout.Width(200)))
            {
                _selectedFolder = folder;
                LoadFolderContents();
            }
        }

        if (GUILayout.Button("+", GUILayout.Height(60), GUILayout.Width(200)))
        {
            StoryCreationWindow.ShowWindow(this);
        }
    }

    private void LoadFolderContents()
    {
        _folderContents = new Dictionary<string, List<StoryFileInfo>>();

        foreach (ResourceType resourceType in (ResourceType[])Enum.GetValues(typeof(ResourceType)))
        {
            string path = ResourcePathManager.GetResourcePath("*", _selectedFolder, resourceType, false);
            string resourcesPath = path.Replace("Assets/Resources/", "");
            string fullPath = Path.Combine(Application.dataPath, "Resources", resourcesPath);

            string folderHeader = $"{resourceType} 폴더";
            if (!Directory.Exists(Path.GetDirectoryName(fullPath))) continue;

            if (!_folderContents.ContainsKey(folderHeader))
            {
                _folderContents[folderHeader] = new List<StoryFileInfo>();
            }

            string[] files = Directory.GetFiles(Path.GetDirectoryName(fullPath));
            foreach (var file in files)
            {
                if (!file.EndsWith(".meta"))
                {
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
                    string relativePath = file.Replace(Application.dataPath, "Assets");
                    var fileInfo = new StoryFileInfo(fileNameWithoutExtension, relativePath);
                    _folderContents[folderHeader].Add(fileInfo);
                }
            }
        }
    }

    private void DrawFolderContents()
    {
        if (_folderContents != null && _folderContents.Count > 0)
        {
            foreach (var folder in _folderContents)
            {
                EditorGUILayout.LabelField(folder.Key, EditorStyles.boldLabel);
                Rect dropArea = EditorGUILayout.BeginVertical("box");
                
                // 드래그 앤 드롭 기능 처리
                HandleDragAndDrop(folder.Key, dropArea);

                foreach (var content in folder.Value)
                {
                    DrawFileTile(content);
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

    private void DrawFileTile(StoryFileInfo fileInfo)
    {
        EditorGUILayout.BeginHorizontal("box", GUILayout.Height(80));

        GUILayout.Label(fileInfo.Thumbnail, GUILayout.Width(70), GUILayout.Height(70));
        GUILayout.Label(fileInfo.FileName, EditorStyles.centeredGreyMiniLabel, GUILayout.Width(200));

        GUILayout.Label(fileInfo.IsSprite ? "✔️" : "❌", GUILayout.Width(30));

        if (GUILayout.Button("자세히", GUILayout.Width(60)))
        {
            StoryFileDetailWindow.ShowDetail(fileInfo);
        }

        if (GUILayout.Button("삭제", GUILayout.Width(60)))
        {
            // 삭제 경고 메시지 표시
            if (EditorUtility.DisplayDialog("삭제 확인", $"'{fileInfo.FileName}'을(를) 삭제하시겠습니까?", "확인", "취소"))
            {
                File.Delete(fileInfo.RelativePath);
                AssetDatabase.Refresh();
                LoadFolderContents();
                Repaint();
            }
        }

        EditorGUILayout.EndHorizontal();
    }

    // 폴더에 파일을 추가하는 메서드
    private void AddFileToFolder(string folderHeader)
    {
        string path = EditorUtility.OpenFilePanel("파일 추가", "", "");
        if (!string.IsNullOrEmpty(path))
        {
            string targetFolderPath = Path.Combine(ResourcePathManager.GetStoryResourcesBasePath(), _selectedFolder, folderHeader.Split(' ')[0]);

            if (Directory.Exists(targetFolderPath))
            {
                string targetFilePath = Path.Combine(targetFolderPath, Path.GetFileName(path));
                File.Copy(path, targetFilePath, true);
                AssetDatabase.Refresh();
                LoadFolderContents();
            }
            else
            {
                Debug.LogError("대상 폴더가 존재하지 않습니다.");
            }
        }
    }

    // 드래그 앤 드롭을 처리하는 메서드
    private void HandleDragAndDrop(string folderHeader, Rect dropArea)
    {
        Event evt = Event.current;

        // 드래그 시작
        if (evt.type == EventType.DragUpdated && dropArea.Contains(evt.mousePosition))
        {
            DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
            evt.Use();
        }

        // 드래그 종료 시 파일 추가
        if (evt.type == EventType.DragPerform && dropArea.Contains(evt.mousePosition))
        {
            DragAndDrop.AcceptDrag();

            foreach (string draggedPath in DragAndDrop.paths)
            {
                string fileName = Path.GetFileName(draggedPath);
                string folderType = folderHeader.Split(' ')[0];
                string targetPath = Path.Combine(ResourcePathManager.GetStoryResourcesBasePath(), _selectedFolder, folderType, fileName);

                try
                {
                    File.Copy(draggedPath, targetPath, true);
                    AssetDatabase.Refresh();
                    LoadFolderContents();
                }
                catch (Exception e)
                {
                    Debug.LogError($"파일을 추가하는 중 오류가 발생했습니다: {e.Message}");
                }
            }

            evt.Use();
        }
    }

    // FileSystemWatcher를 설정하여 파일 변경 감지
    private void SetupFileWatcher()
    {
        string path = ResourcePathManager.GetStoryResourcesBasePath();

        if (!Directory.Exists(path)) return;

        _fileWatcher = new FileSystemWatcher(path)
        {
            IncludeSubdirectories = true, // 하위 폴더 포함
            NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName
        };

        // 이벤트 핸들러 설정
        _fileWatcher.Changed += OnFolderChanged;
        _fileWatcher.Created += OnFolderChanged;
        _fileWatcher.Deleted += OnFolderChanged;
        _fileWatcher.Renamed += OnFolderChanged;

        _fileWatcher.EnableRaisingEvents = true; // 이벤트 활성화
    }

    // 폴더에 변화가 발생했을 때 호출되는 메서드
    private void OnFolderChanged(object sender, FileSystemEventArgs e)
    {
        EditorApplication.delayCall += () =>
        {
            AssetDatabase.Refresh(); // AssetDatabase 갱신하여 변경 사항 반영
            LoadStoryFolders(); // 변경된 폴더 내용 다시 로드
            LoadFolderContents(); // 선택된 폴더가 있다면 해당 폴더의 내용도 다시 로드
            Repaint(); // 에디터 창 다시 그리기
        };
    }

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

        AssetDatabase.Refresh();
        Debug.Log($"새로운 스토리 '{storyName}' 생성이 완료되었습니다.");
        LoadStoryFolders();
    }
}
