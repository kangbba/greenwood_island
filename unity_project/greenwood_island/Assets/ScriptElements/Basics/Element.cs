using System.Collections;
using UnityEngine;

public abstract class Element
{
    // 조건이 충족될 때까지 루틴을 실행하는 추상 메서드
    public abstract IEnumerator ExecuteRoutine();

    // Skip 메서드도 추상 메서드로 정의
    public abstract void ExecuteInstantly();

    // Execute 메서드도 추상 메서드로 정의
    public virtual void Execute()
    {
        CoroutineUtils.StartCoroutine(ExecuteRoutine());
    }
}
