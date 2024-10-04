using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

public static class StoryManager
{
    private static Story _currentStory; // 현재 실행 중인 스토리
    private static Story _previousStory; // 이전에 실행된 스토리

    private static int _currentElementIndex;

    // 현재 스토리의 이름을 가져오는 메서드
    public static string CurrentStoryName
    {
        get{
            return _currentStory != null ? _currentStory.GetType().Name : string.Empty;
        }
    }

    public static int CurrentElementIndex { get => _currentElementIndex;  }


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
        _currentElementIndex = 0;

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
    // string storyID로 스토리 실행하는 메서드
    public static void PlayStory(string storyID)
    {
        Type storyType = Type.GetType(storyID);

        // 스토리 타입이 존재하는지 확인
        if (storyType == null || !typeof(Story).IsAssignableFrom(storyType))
        {
            Debug.LogError($"Story class '{storyID}' not found or not a valid Story type.");
            return;
        }

        // 스토리 인스턴스 생성
        Story storyInstance = (Story)Activator.CreateInstance(storyType);

        // 스토리 실행
        PlayStory(storyInstance);
    }

    // 스토리 시작, 업데이트, 종료 루틴을 관리하는 코루틴
    // 스토리 시작, 업데이트, 종료 루틴을 관리하는 코루틴
    private static IEnumerator StoryStartRoutine()
    {
        UIManager.SystemCanvas.LetterBox.gameObject.SetActive(true);
        UIManager.SystemCanvas.LetterBox.SetOn(false, 2f);
        yield return _currentStory.ClearRoutine(2f);
        UIManager.SystemCanvas.LetterBox.SetOn(true, 2f);

        if (_currentStory != null)
        {
            yield return _currentStory.StartRoutine();

            // UpdateRoutine에서 콜백 전달 (인덱스와 총 개수도 함께 전달)
            yield return _currentStory.UpdateRoutine((element, index, totalCount) => {
                Debug.Log($"총 {totalCount}개의 Elements 중 {index + 1}번째 Element ({element.GetType().Name})가 시작됩니다.");
            });

            yield return _currentStory.ExitRoutine();
            Debug.Log("스토리 끝");
        }
    }




}
