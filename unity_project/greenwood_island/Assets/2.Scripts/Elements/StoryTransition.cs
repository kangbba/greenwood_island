using System.Collections;
using UnityEngine;

public class StoryTransition : Element
{
    private Story _nextStory;

    public StoryTransition(Story nextStory)
    {
        _nextStory = nextStory;
    }

    public override IEnumerator Execute()
    {
        if (_nextStory != null)
        {
            // StoryManager를 통해 다음 스토리 시작
            StoryManager.Instance.StartStory(_nextStory);
        }
        else
        {
            Debug.LogWarning("Next story is null in StoryTransition.");
        }
        yield return null;
    }
}
