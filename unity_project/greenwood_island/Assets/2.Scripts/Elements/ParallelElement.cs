using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ParallelElement : Element
{
    private List<Element> _elements;

    // 생성자를 통해 여러 Element를 받을 수 있도록 설정
    public ParallelElement(params Element[] elements)
    {
        _elements = new List<Element>(elements);
    }

    public override IEnumerator Execute()
    {
        // 각 요소의 코루틴을 저장할 리스트
        List<Coroutine> runningCoroutines = new List<Coroutine>();

        // 각 Element의 Execute 코루틴을 동시에 실행
        foreach (var element in _elements)
        {
            Coroutine coroutine = CoroutineRunner.Instance.StartCoroutine(element.Execute());
            runningCoroutines.Add(coroutine);
        }

        // 모든 실행된 코루틴이 완료될 때까지 대기
        foreach (var coroutine in runningCoroutines)
        {
            yield return coroutine;
        }
    }
}
