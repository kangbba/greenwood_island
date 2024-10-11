using System;
using System.Collections;
using System.Collections.Generic;

public class ChoiceContent
{
    private string _title;
    private SequentialElement _sequentialElement;

    public string Title => _title;
    public SequentialElement SequentialElement => _sequentialElement;

    public ChoiceContent(string title, SequentialElement sequentialElement)
    {
        _title = title;
        _sequentialElement = sequentialElement ?? new SequentialElement(new List<Element>()); // null 방지를 위해 SequentialElement 초기화
    }
}
