using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class Story
{
    protected List<Element> _elements = new List<Element>();
    protected List<ECharacterID> _activeCharacters = new List<ECharacterID>(); // 현재 스토리에 등장한 캐릭터 목록

    public abstract string StoryId { get; }

    /// <summary>
    /// 스토리의 장소 초기화를 담당하는 가상 코루틴 메서드.
    /// 상속받은 클래스에서 구체적으로 구현해야 합니다.
    /// </summary>
    protected virtual IEnumerator OnEnterPlace()
    {
        // 장소 초기화는 상속받은 클래스에서 구현
        yield break;
    }

    /// <summary>
    /// 스토리의 메인 실행 부분을 담당하는 가상 코루틴 메서드.
    /// 기본적으로 _elements의 요소를 실행합니다.
    /// </summary>
    protected virtual IEnumerator OnStory()
    {
        foreach (var element in _elements)
        {
            yield return element.Execute();
        }
        Debug.Log($"Story {StoryId} OnStory completed.");
    }

    /// <summary>
    /// 스토리의 캐릭터 퇴장을 담당하는 메서드.
    /// _activeCharacters를 이용해 CharactersExit을 자동 호출합니다.
    /// </summary>
    private IEnumerator ExitCharacters()
    {
        if (_activeCharacters.Count > 0)
        {
            CharactersExit charactersExit = new CharactersExit(_activeCharacters, 1f, Ease.InQuad);
            yield return charactersExit.Execute();
            Debug.Log($"Story {StoryId}: Characters exited.");
        }
        _activeCharacters.Clear();
    }

    /// <summary>
    /// 스토리의 종료 작업을 담당하는 가상 코루틴 메서드.
    /// 상속받은 클래스에서 구체적으로 구현해야 합니다.
    /// </summary>
    protected virtual IEnumerator OnExitStory()
    {
        // 스토리 종료는 상속받은 클래스에서 구현
        yield break;
    }

    /// <summary>
    /// 전체 스토리를 실행하는 메인 코루틴
    /// </summary>
    public IEnumerator Execute()
    {
        yield return OnEnterPlace();        // 장소 초기화
        yield return OnStory();             // 스토리 진행
        yield return ExitCharacters();      // 캐릭터 퇴장
        yield return OnExitStory();         // 스토리 종료
        Debug.Log($"Story {StoryId} completed.");
    }
}
