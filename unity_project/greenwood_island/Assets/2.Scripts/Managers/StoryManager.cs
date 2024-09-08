using System;
using System.Collections;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public static StoryManager Instance { get; private set; }

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
    }

    private void Start()
    {
        PlayStory("Story1"); // 스토리 이름을 전달하여 실행
    }

    // 현재 스토리의 이름을 가져오는 메서드
    public string GetCurrentStoryName()
    {
        return _currentStory != null ? _currentStory.GetType().Name : string.Empty;
    }

    // 스토리를 실행하는 메서드
    public void PlayStory(string storyName)
    {
        _previousStory = _currentStory;

        // Reflection을 사용해 스토리 클래스 동적 인스턴스화 시도
        Type storyType = Type.GetType(storyName);
        if (storyType == null)
        {
            Debug.LogError($"Story class '{storyName}' could not be found.");
            return;
        }

        try
        {
            _currentStory = (Story)Activator.CreateInstance(storyType);
            StartCoroutine(StoryStartRoutine());
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to instantiate story '{storyName}': {e.Message}");
        }
    }

    private IEnumerator StoryStartRoutine()
    {
        float clearDuration = 1f;
        yield return new WaitForSeconds(clearDuration);

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
