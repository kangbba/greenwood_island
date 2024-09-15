using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Element
{
    public abstract IEnumerator ExecuteRoutine();
    // Execute 메서드는 CoroutineRunner를 통해 코루틴을 실행
    public void Execute()
    {
        CoroutineUtils.StartCoroutine(ExecuteRoutine());
    }
}
