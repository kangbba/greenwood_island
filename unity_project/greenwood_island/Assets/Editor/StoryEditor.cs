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
    // StoryEditor.cs에서의 스토리 폴더 생성 메서드
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
