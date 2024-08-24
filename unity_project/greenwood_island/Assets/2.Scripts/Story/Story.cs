using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Story
{
    protected List<Element> _elements = new List<Element>();

    public abstract string StoryId { get; }

    public IEnumerator Execute()
    {
        foreach (var element in _elements)
        {
            yield return element.Execute();
        }
        Debug.Log($"Story {StoryId} completed.");
    }
}
