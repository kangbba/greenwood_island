using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Story
{
    /// <summary>
    /// Start, Update, Exit 단계에서 실행될 Elements를 상속받은 클래스에서 구현하도록 합니다.
    /// </summary>

    // 각 단계의 Elements를 상속받은 클래스에서 정의하도록 강제
    protected abstract List<Element> StartElements { get; }
    protected abstract List<Element> UpdateElements { get; }

    public abstract EStoryID StoryId { get; }

    // StartRoutine: Start 단계의 Elements를 순차적으로 실행
    public virtual IEnumerator StartRoutine()
    {
        foreach (var element in StartElements)
        {
            yield return element.ExecuteRoutine();
        }
    }

    // UpdateRoutine: Update 단계의 Elements를 순차적으로 실행
    public virtual IEnumerator UpdateRoutine()
    {
        foreach (var element in UpdateElements)
        {
            yield return element.ExecuteRoutine();
        }
    }
}
