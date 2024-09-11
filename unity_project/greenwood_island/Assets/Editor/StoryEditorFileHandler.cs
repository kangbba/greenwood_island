using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class StoryEditorFileHandler
{
    private static FileSystemWatcher _fileWatcher; // 파일 변경 감지를 위한 FileSystemWatcher

    public static void SetupFileWatcher(Action onFoldersChanged, Action onFilesChanged, Action onRepaint)
    {
        string path = ResourcePathManager.GetStoryResourcesBasePath();

        if (!Directory.Exists(path)) return;

        _fileWatcher = new FileSystemWatcher(path)
        {
            IncludeSubdirectories = true,
            NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName
        };

        _fileWatcher.Changed += (sender, e) => OnFolderChanged(onFoldersChanged, onFilesChanged, onRepaint);
        _fileWatcher.Created += (sender, e) => OnFolderChanged(onFoldersChanged, onFilesChanged, onRepaint);
        _fileWatcher.Deleted += (sender, e) => OnFolderChanged(onFoldersChanged, onFilesChanged, onRepaint);
        _fileWatcher.Renamed += (sender, e) => OnFolderChanged(onFoldersChanged, onFilesChanged, onRepaint);

        _fileWatcher.EnableRaisingEvents = true;
    }

    public static void DisposeFileWatcher()
    {
        if (_fileWatcher != null)
        {
            _fileWatcher.EnableRaisingEvents = false;
            _fileWatcher.Dispose();
        }
    }

    private static void OnFolderChanged(Action onFoldersChanged, Action onFilesChanged, Action onRepaint)
    {
        EditorApplication.delayCall += () =>
        {
            AssetDatabase.Refresh();
            onFoldersChanged();
            onFilesChanged();
            onRepaint();
        };
    }

    public static string[] GetStoryFolders()
    {
        string path = ResourcePathManager.GetStoryResourcesBasePath();
        if (Directory.Exists(path))
        {
            string[] folders = Directory.GetDirectories(path);
            for (int i = 0; i < folders.Length; i++)
            {
                folders[i] = Path.GetFileName(folders[i]);
            }
            return folders;
        }
        return new string[0];
    }

    public static Dictionary<string, List<StoryFileInfo>> LoadFolderContents(string selectedFolder)
    {
        var folderContents = new Dictionary<string, List<StoryFileInfo>>();

        foreach (ResourceType resourceType in (ResourceType[])Enum.GetValues(typeof(ResourceType)))
        {
            string path = ResourcePathManager.GetResourcePath("*", selectedFolder, resourceType, false);
            string resourcesPath = path.Replace("Assets/Resources/", "");
            string fullPath = Path.Combine(Application.dataPath, "Resources", resourcesPath);

            string folderHeader = $"{resourceType} 폴더";
            if (!Directory.Exists(Path.GetDirectoryName(fullPath))) continue;

            if (!folderContents.ContainsKey(folderHeader))
            {
                folderContents[folderHeader] = new List<StoryFileInfo>();
            }

            string[] files = Directory.GetFiles(Path.GetDirectoryName(fullPath));
            foreach (var file in files)
            {
                if (!file.EndsWith(".meta"))
                {
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
                    string relativePath = file.Replace(Application.dataPath, "Assets");
                    var fileInfo = new StoryFileInfo(fileNameWithoutExtension, relativePath);
                    folderContents[folderHeader].Add(fileInfo);
                }
            }
        }

        return folderContents;
    }

    public static void HandleDragAndDrop(string selectedFolder, Dictionary<string, List<StoryFileInfo>> folderContents, Action onFilesChanged, Action onRepaint)
    {
        // 드래그 앤 드롭 이벤트 처리
        Event evt = Event.current;
        if ((evt.type == EventType.DragUpdated || evt.type == EventType.DragPerform) && DragAndDrop.paths.Length > 0)
        {
            DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

            if (evt.type == EventType.DragPerform)
            {
                DragAndDrop.AcceptDrag();
                foreach (var path in DragAndDrop.paths)
                {
                    string targetPath = ResourcePathManager.GetResourcePath(Path.GetFileName(path), selectedFolder, GetTargetResourceType(folderContents), false);
                    string fullTargetPath = Path.Combine(Application.dataPath, "Resources", targetPath);

                    if (!Directory.Exists(Path.GetDirectoryName(fullTargetPath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(fullTargetPath));
                    }

                    File.Copy(path, fullTargetPath, true);
                    AssetDatabase.Refresh();
                }
                onFilesChanged();
                onRepaint();
            }

            evt.Use();
        }
    }

    // 파일 삭제 메서드
    public static void DeleteFile(StoryFileInfo fileInfo)
    {
        string relativePath = fileInfo.RelativePath;

        if (File.Exists(relativePath))
        {
            File.Delete(relativePath);
            File.Delete(relativePath + ".meta"); // 메타 파일도 삭제
            AssetDatabase.Refresh(); // 자산 데이터베이스 갱신
            Debug.Log($"{fileInfo.FileName}이(가) 삭제되었습니다.");
        }
        else
        {
            Debug.LogWarning("파일을 찾을 수 없습니다.");
        }
    }
    private static ResourceType GetTargetResourceType(Dictionary<string, List<StoryFileInfo>> folderContents)
    {
        // 각 폴더에 드래그 앤 드롭할 때 ResourceType을 결정하는 로직을 작성
        // 드래그 중인 위치에 따라 적절한 ResourceType을 반환
        // 예: 현재 마우스가 위치한 폴더가 Places라면 ResourceType.Place 반환
        return ResourceType.Place; // 임시로 Place로 설정, 실제 로직에 따라 수정 필요
    }
}
