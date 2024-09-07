using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum EStoryID
{
    TestStory_KateKillLisa,
    TestStory_PlaceEnters,
    TestStoryTime,
    // 다른 스토리 ID를 여기에 추가
    TestStory_BirthdayParty
}

public class StoryManager : MonoBehaviour
{
    public static StoryManager Instance { get; private set; }

    [SerializeField] private string storyFolderPath = "Stories"; // "Stories" 폴더 경로 설정
    private Dictionary<EStoryID, Story> _stories;
    private StoryLoader _storyLoader; // StoryLoader 인스턴스

    private Story _currentStory; // 현재 실행 중인 스토리
    private Story _previousStory; // 이전에 실행된 스토리

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

        _storyLoader = new StoryLoader(storyFolderPath);
        _stories = _storyLoader.GetLoadedStories();
    }

    private void Start()
    {
        // 첫 번째 스토리를 자동으로 시작
        if (_stories != null && _stories.Count > 0)
        {
            var firstStory = _stories.Values.First();
            PlayStory(EStoryID.TestStory_BirthdayParty);
        }
        else
        {
            Debug.LogError("No stories available to start.");
        }
    }

    public void PlayStory(EStoryID storyID)
    {
        // 현재 스토리를 이전 스토리로 설정
        _previousStory = _currentStory;

        // 새로운 스토리를 찾고 실행
        if (_stories.TryGetValue(storyID, out Story story))
        {
            _currentStory = story;
            StartCoroutine(StoryStartRoutine());
        }
        else
        {
            Debug.LogError($"Story with ID {storyID} not found.");
        }
    }

    private IEnumerator StoryStartRoutine()
    {
        // 모든 클리어 작업 수행
        float clearDuration = 1f;
        yield return new WaitForSeconds(clearDuration);

        // 현재 스토리의 StartRoutine과 UpdateRoutine을 실행
        if (_currentStory != null)
        {
            yield return _currentStory.StartRoutine();
            yield return _currentStory.UpdateRoutine();
            yield return _currentStory.ExitRoutine();
            RestoreAll(clearDuration);
        }
    }

    private void RestoreAll(float duration)
    {
        Debug.Log("StoryManager :: RestoreAll Started");
        new FXsClear(duration).Execute();
        new SFXsClear(duration).Execute();
        new PlaceFilmClear(duration).Execute();
        new CameraMove2DClear(duration).Execute();
        new CameraZoomClear(duration).Execute();
        new AllCharactersClear(duration).Execute();
        new CutInClear(duration).Execute();
        new PlaceOverlayFilmClear(duration).Execute();
        new ScreenOverlayFilmClear(duration).Execute();
        Debug.Log("StoryManager :: RestoreAll Completed");
    }
}
