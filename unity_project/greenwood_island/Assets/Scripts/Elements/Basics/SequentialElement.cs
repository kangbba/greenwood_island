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
    public SequentialElement(params Element[] elements)
    {
        _elements = new List<Element>(elements);
    }
    public SequentialElement(List<Element> elements)
    {
        _elements = elements ?? new List<Element>();
    }

    public override void ExecuteInstantly()
    {
        for (int i = 0; i < _elements.Count; i++)
        {
            _elements[i].ExecuteInstantly();
        }
    }
    // // 콜백을 받아서 각 Element의 실행을 알리는 메서드
    public override IEnumerator ExecuteRoutine()
    {
        for (int i = 0; i < _elements.Count; i++)
        {
            Element element = _elements[i];
            if (element != null)
            {
                yield return element.ExecuteRoutine();
            }
        }
    }
}
