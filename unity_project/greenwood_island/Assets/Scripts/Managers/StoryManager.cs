using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

public static class StoryManager
{
    private static Story _currentStory; // 현재 실행 중인 스토리
    private static Story _previousStory; // 이전에 실행된 스토리

    // StoryManager 초기화 메서드
    public static void Init()
    {
      //       PlayStory("FirstJosephStory");
   //     PlayStory("OpeningStory");
            PlayStory(new FirstKateStory());
    }

    // 현재 스토리의 이름을 가져오는 메서드
    public static string GetCurrentStoryName()
    {
        return _currentStory != null ? _currentStory.GetType().Name : string.Empty;
    }

    // 스토리를 실행하는 메서드
    public static void PlayStory(Story storyInstance)
    {
        _previousStory = _currentStory;

        // 스토리 인스턴스가 유효한지 확인
        if (storyInstance == null)
        {
            Debug.LogError("Invalid or null story instance.");
            return;
        }

        // 현재 스토리를 인스턴스로 설정
        _currentStory = storyInstance;

        try
        {
            // 스토리 시작 루틴 실행
            CoroutineUtils.StartCoroutine(StoryStartRoutine());
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to start story: {e.Message}");
        }
    }

    // 현재 어셈블리에서 스토리 타입을 찾는 메서드
    private static Type FindStoryType(string storyName)
    {
        // 현재 실행 중인 어셈블리를 가져옴
        Assembly assembly = Assembly.GetExecutingAssembly();

        // 어셈블리에서 storyName에 해당하는 타입을 찾음
        Type storyType = assembly.GetType(storyName);

        return storyType;
    }

    // 스토리 시작, 업데이트, 종료 루틴을 관리하는 코루틴
    private static IEnumerator StoryStartRoutine()
    {
        UIManager.Instance.SystemCanvas.LetterBox.SetOn(false, 2f);
        yield return _currentStory.ClearRoutine(2f);
        UIManager.Instance.SystemCanvas.LetterBox.SetOn(true, 2f);
        yield return new WaitForSeconds(2f);

        if (_currentStory != null)
        {
            yield return _currentStory.StartRoutine();
            yield return _currentStory.UpdateRoutine();
            yield return _currentStory.ExitRoutine();
            Debug.Log("스토리 끝");
        }
    }
}
