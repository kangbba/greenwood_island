using System.Collections.Generic;
using System.Collections;

public class ChoiceOption
{
    public string Title { get; private set; }
    public List<Element> Elements { get; private set; }

    public ChoiceOption(string title, List<Element> elements)
    {
        Title = title;
        Elements = elements ?? new List<Element>();
    }

    public IEnumerator ExecuteRoutine()
    {
        foreach (var element in Elements)
        {
            yield return CoroutineRunner.Instance.StartCoroutine(element.ExecuteRoutine());
        }
    }
}
