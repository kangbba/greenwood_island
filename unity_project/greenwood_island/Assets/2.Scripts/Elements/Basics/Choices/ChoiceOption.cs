using System;
using System.Collections;
using System.Collections.Generic;

public class ChoiceOption
{
    public string Title { get; private set; }
    public List<Element> Elements { get; private set; }
    public Action OnSelected { get; private set; } // 선택된 후 실행할 콜백

    public ChoiceOption(string title, List<Element> elements, Action onSelected = null)
    {
        Title = title;
        Elements = elements ?? new List<Element>(); // null 방지를 위해 리스트 초기화
        OnSelected = onSelected;
    }



    public IEnumerator ExecuteRoutine()
    {
        foreach (var element in Elements)
        {
            yield return CoroutineRunner.Instance.StartCoroutine(element.ExecuteRoutine());
        }
    }
}