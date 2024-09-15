using System;
using System.Collections;
using System.Collections.Generic;

public class ChoiceContent
{
    public string Title { get; private set; }
    public SequentialElement SequentialElement { get; private set; }
    public Action OnSelected { get; private set; } // 선택된 후 실행할 콜백

    public ChoiceContent(string title, SequentialElement sequentialElement, Action onSelected = null)
    {
        Title = title;
        SequentialElement = sequentialElement ?? new SequentialElement(new List<Element>()); // null 방지를 위해 SequentialElement 초기화
        OnSelected = onSelected;
    }

    public IEnumerator ExecuteRoutine()
    {
        // SequentialElement를 사용하여 요소들을 순차적으로 실행
        yield return CoroutineUtils.StartCoroutine(SequentialElement.ExecuteRoutine());
    }
}
