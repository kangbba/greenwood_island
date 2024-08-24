using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public static StoryManager Instance { get; private set; }

    [SerializeField] private string storyFolderPath = "Stories"; // "Stories" 폴더 경로 설정
    private Dictionary<string, Story> _stories;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadStories();
    }

    private void Start()
    {
        // 첫 번째 스토리를 자동으로 시작
        if (_stories != null && _stories.Count > 0)
        {
            var firstStory = _stories.Values.First();
            StartCoroutine(firstStory.Execute());
        }
        else
        {
            Debug.LogError("No stories available to start.");
        }
    }

    private void LoadStories()
    {
        _stories = new Dictionary<string, Story>();

        // "storyFolderPath" 폴더 내의 모든 .cs 파일을 가져옴
        string[] files = Directory.GetFiles(Application.dataPath + "/" + storyFolderPath, "*.cs", SearchOption.AllDirectories);

        foreach (string file in files)
        {
            string fileName = Path.GetFileNameWithoutExtension(file);
            Type storyType = GetTypeFromAssembly(fileName);

            if (storyType != null && storyType.IsSubclassOf(typeof(Story)))
            {
                Story storyInstance = (Story)Activator.CreateInstance(storyType);

                if (!_stories.ContainsKey(storyInstance.StoryId))
                {
                    _stories.Add(storyInstance.StoryId, storyInstance);
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

    public void StartStory(string storyId)
    {
        if (_stories.TryGetValue(storyId, out var story))
        {
            StartCoroutine(story.Execute());
        }
        else
        {
            Debug.LogError($"Story with ID {storyId} not found.");
        }
    }
}
