using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    [SerializeField] private Story _currentStory;

    private void Start()
    {
        if (_currentStory != null)
        {
            StartCoroutine(StoryRoutine(_currentStory.GetElements()));
        }
        else
        {
            Debug.LogError("No Story assigned to the StoryManager.");
        }
    }

    private IEnumerator StoryRoutine(List<Element> elements)
    {
        foreach (var element in elements)
        {
            yield return element.Execute();
        }

        Debug.Log("모든 스토리 요소가 완료되었습니다.");
    }
}
