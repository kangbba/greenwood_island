using System.Collections;
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

    // 스토리 ID는 자동으로 클래스의 이름을 사용
    public virtual string StoryId => GetType().Name; // 클래스의 이름을 ID로 사용

    // Start 단계의 Elements를 실행하는 메서드
    public IEnumerator ClearRoutine(float duration)
    {
        Element element = new AllClear(duration);
        yield return element.ExecuteRoutine();
    }

    public IEnumerator StartRoutine()
    {
        yield return StartElements.ExecuteRoutine();
    }

    // Update 단계의 Elements를 실행하는 메서드, 콜백 추가
    public IEnumerator UpdateRoutine(System.Action<Element, int, int> onElementStartCallback)
    {
        yield return UpdateElements.ExecuteRoutine(onElementStartCallback);
    }

    // 기존 UpdateRoutine을 유지, 콜백 없이도 실행 가능
    public IEnumerator UpdateRoutine()
    {
        yield return UpdateRoutine(null); // 콜백 없이 실행
    }


    // Exit 단계의 Elements를 실행하는 메서드 (필요할 경우)
    public IEnumerator ExitRoutine()
    {
        yield return ExitElements.ExecuteRoutine();
    }
}
