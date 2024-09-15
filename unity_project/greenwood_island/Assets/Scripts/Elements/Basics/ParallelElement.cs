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

    public override IEnumerator ExecuteRoutine()
    {
        // 각 요소의 완료 여부를 추적할 리스트
        List<bool> isCompleted = new List<bool>(_elements.Count);

        // 초기 완료 여부 리스트를 false로 초기화
        for (int i = 0; i < _elements.Count; i++)
        {
            isCompleted.Add(false);
        }

        // 각 Element의 ExecuteRoutine 코루틴을 동시에 실행
        for (int i = 0; i < _elements.Count; i++)
        {
            int index = i; // 클로저 문제를 피하기 위해 별도의 변수 사용
            CoroutineUtils.StartCoroutine(ExecuteRoutineElement(_elements[index], index, isCompleted));
        }

        // 모든 실행된 코루틴이 완료될 때까지 대기
        while (isCompleted.Contains(false))
        {
            yield return null; // 모든 코루틴이 완료될 때까지 매 프레임마다 대기
        }
    }

    // 개별 Element의 ExecuteRoutine를 수행하고 완료 여부를 추적하는 코루틴
    private IEnumerator ExecuteRoutineElement(Element element, int index, List<bool> isCompleted)
    {
        yield return element.ExecuteRoutine();
        isCompleted[index] = true; // 해당 인덱스의 완료 여부를 true로 설정
    }
}
