using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SequentialElement 클래스는 주어진 리스트의 Element들을 순차적으로 실행하는 Element입니다.
/// 여러 효과를 연속적으로 실행할 때 사용됩니다.
/// </summary>
public class SequentialElement : Element
{
    private List<Element> _elements; // 실행할 Element 리스트

    // 생성자: Element 리스트를 params로 받아 설정합니다.
    public SequentialElement(params Element[] elements)
    {
        _elements = new List<Element>(elements);
    }

    // 생성자: 리스트 형태로 Element들을 받아 설정합니다.
    public SequentialElement(List<Element> elements)
    {
        _elements = elements ?? new List<Element>();
    }

    // 콜백을 받아서 각 Element의 실행을 알리는 메서드
    public IEnumerator ExecuteRoutine(System.Action<Element, int, int> onElementStartCallback)
    {
        for (int i = 0; i < _elements.Count; i++)
        {
            Element element = _elements[i];
            if (element != null)
            {
                // Element 실행 직전에 콜백 호출 (Element, 인덱스, 총 개수 전달)
                onElementStartCallback?.Invoke(element, i, _elements.Count);

                // 각 Element 실행
                yield return element.ExecuteRoutine();
            }
        }
    }

    // 기존의 ExecuteRoutine을 유지, 콜백 없이 동작
    public override IEnumerator ExecuteRoutine()
    {
        yield return ExecuteRoutine(null); // 기본적으로 콜백 없이 실행
    }
}
