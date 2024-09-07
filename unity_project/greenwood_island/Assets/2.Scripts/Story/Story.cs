using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Story
{
    /// <summary>
    /// Start, Update, Exit 단계에서 실행될 Elements를 상속받은 클래스에서 구현하도록 합니다.
    /// SequentialElement를 사용하여 각 단계의 Elements를 순차적으로 실행합니다.
    /// </summary>

    // 각 단계의 Elements를 SequentialElement로 정의
    protected abstract SequentialElement StartElements { get; }
    protected abstract SequentialElement UpdateElements { get; }
    protected abstract SequentialElement ExitElements { get; }

    public abstract EStoryID StoryId { get; }
    protected abstract string StoryDesc { get; }

    // Start 단계의 Elements를 실행하는 메서드
    public IEnumerator StartRoutine()
    {
        yield return StartElements.ExecuteRoutine();
    }

    // Update 단계의 Elements를 실행하는 메서드
    public IEnumerator UpdateRoutine()
    {
        yield return UpdateElements.ExecuteRoutine();
    }

    // Exit 단계의 Elements를 실행하는 메서드 (필요할 경우)
    public IEnumerator ExitRoutine()
    {
        yield return ExitElements.ExecuteRoutine();
    }
}
