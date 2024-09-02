using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class Story
{
    /// <summary>
    /// Elements에 할당 해두면,
    /// StoryManager가 자동으로 이 엘리먼츠들을 순서대로 호출할것입니다.
    /// </summary>

    protected List<Element> _elements = new List<Element>();

    public abstract EStoryID StoryId { get; }

    /// <summary>
    /// 스토리의 장소 초기화를 담당하는 가상 코루틴 메서드.
    /// 상속받은 클래스에서 구체적으로 구현해야 합니다.
    /// </summary>
    
    protected virtual IEnumerator OnStory()
    {
        foreach (var element in _elements)
        {
            yield return element.ExecuteRoutine();
        }
        Debug.Log($"Story {StoryId} OnStory completed.");
    }

    public virtual IEnumerator ExecuteRoutine()
    {
        yield return OnStory();             // 스토리 진행
        Debug.Log($"Story {StoryId} completed.");
    }
}
