using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class StoryLoader
{
    private readonly string _storyFolderPath;
    private readonly Dictionary<EStoryID, Story> _loadedStories;

    public StoryLoader(string storyFolderPath)
    {
        _storyFolderPath = storyFolderPath;
        _loadedStories = new Dictionary<EStoryID, Story>();
        LoadStories();
    }

    public Dictionary<EStoryID, Story> GetLoadedStories()
    {
        return _loadedStories;
    }

    private void LoadStories()
    {
        // "storyFolderPath" 폴더 내의 모든 .cs 파일을 가져옴
        string[] files = Directory.GetFiles(Path.Combine(Application.dataPath, _storyFolderPath), "*.cs", SearchOption.AllDirectories);

        foreach (string file in files)
        {
            string fileName = Path.GetFileNameWithoutExtension(file);
            Type storyType = GetTypeFromAssembly(fileName);

            if (storyType != null && storyType.IsSubclassOf(typeof(Story)))
            {
                Story storyInstance = (Story)Activator.CreateInstance(storyType);

                if (!_loadedStories.ContainsKey(storyInstance.StoryId))
                {
                    _loadedStories.Add(storyInstance.StoryId, storyInstance);
                    Debug.Log($"Story {storyInstance.StoryId} registered.");
                }
                else
                {
                    Debug.LogWarning($"Story {storyInstance.StoryId} is already registered.");
                }
            }
        }
    }

    private Type GetTypeFromAssembly(string typeName)
    {
        // 현재 어셈블리에서 typeName과 일치하는 타입을 찾음
        return Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.Name == typeName);
    }
}
