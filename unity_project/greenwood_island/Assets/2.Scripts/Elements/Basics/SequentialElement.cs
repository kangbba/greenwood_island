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

    public override IEnumerator ExecuteRoutine()
    {
        foreach (var element in _elements)
        {
            if (element != null)
            {
                yield return element.ExecuteRoutine(); // 각 Element를 순차적으로 실행
            }
        }
    }
}
