using System;
using System.Collections;
using System.Reflection;
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
        StartCoroutine(DelayStart());
    }

    IEnumerator DelayStart(){
        yield return new WaitForEndOfFrame();
        PlayStory("OpeningStory"); // 스토리 이름을 전달하여 실행
    }

    // 현재 스토리의 이름을 가져오는 메서드
    public string GetCurrentStoryName()
    {
        return _currentStory != null ? _currentStory.GetType().Name : string.Empty;
    }

    // 스토리를 실행하는 메서드
      // 스토리를 실행하는 메서드
    public void PlayStory(string storyName)
    {
        _previousStory = _currentStory;

        // 스토리 타입을 현재 어셈블리에서 찾음
        Type storyType = FindStoryType(storyName);
        if (storyType == null)
        {
            Debug.LogError($"Story class '{storyName}' could not be found in the current assembly.");
            return;
        }

        try
        {
            // Reflection을 사용해 스토리 클래스 동적 인스턴스화 시도
            _currentStory = (Story)Activator.CreateInstance(storyType);
            StartCoroutine(StoryStartRoutine());
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to instantiate story '{storyName}': {e.Message}");
        }
    }

    // 현재 어셈블리에서 스토리 타입을 찾는 메서드
    private Type FindStoryType(string storyName)
    {
        // 현재 실행 중인 어셈블리를 가져옴
        Assembly assembly = Assembly.GetExecutingAssembly();

        // 어셈블리에서 storyName에 해당하는 타입을 찾음
        Type storyType = assembly.GetType(storyName);

        return storyType;
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
            Debug.Log("스토리 끝");
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
