using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

public class StoryManager : SingletonManager<StoryManager>
{
    private Story _currentStory; // 현재 실행 중인 스토리
    private Story _previousStory; // 이전에 실행된 스토리

    private int _currentElementIndex;

    public int CurrentElementIndex { get => _currentElementIndex; }


    public Story CurrentStory { get => _currentStory; }

    // 현재 스토리의 이름을 가져오는 메서드
    public string CurrentStoryName
    {
        get =>  _currentStory != null ? _currentStory.GetType().Name : string.Empty;
    }   


    // 스토리를 실행하는 메서드 (elementIndex를 받아 스토리를 특정 인덱스에서 시작)
    public void PlayStory(Story storyInstance, int elementIndexToPlay)
    {
        _currentElementIndex = 0;
        _previousStory = _currentStory;

        // 스토리 인스턴스가 유효한지 확인
        if (storyInstance == null)
        {
            Debug.LogError("Invalid or null story instance.");
            return;
        }

        // 현재 스토리를 인스턴스로 설정
        _currentStory = storyInstance;
        CoroutineUtils.StartCoroutine(StoryStartRoutine(elementIndexToPlay));
    }

    // string storyID로 스토리 실행하는 메서드
    public void PlayStory(string storyID, int elementIndexToPlay)
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

        // 스토리 실행 (elementIndex도 전달)
        PlayStory(storyInstance, elementIndexToPlay);
    }

    // 스토리 시작, 업데이트, 종료 루틴을 관리하는 코루틴
    private IEnumerator StoryStartRoutine(int elementIndexToPlay)
    {
        _currentElementIndex = elementIndexToPlay;

        UIManager.SystemCanvas.LetterBox.gameObject.SetActive(true);
        UIManager.SystemCanvas.LetterBox.SetOn(false, 2f);
        yield return new AllClear(0f).ExecuteRoutine();
        UIManager.SystemCanvas.LetterBox.SetOn(true, 2f);
        
        for(int i = 0 ; i < elementIndexToPlay ; i++){
            Element element = _currentStory.UpdateElements[i];
            Debug.Log($"{i}번째 엘리먼트인 {element.GetType().Name}은 스킵");
            element.ExecuteInstantly();
        }
        for(int i = elementIndexToPlay ; i < _currentStory.UpdateElements.Count ; i++){
            _currentElementIndex = i;
            Element element = _currentStory.UpdateElements[i];
            yield return element.ExecuteRoutine();
            Debug.Log("스토리 끝");
        }
    }
}
