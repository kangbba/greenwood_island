using System.Collections;
using UnityEngine;

public class StoryTransition : Element
{
    private string _nextStoryName;

    public StoryTransition(string nextStoryName)
    {
        _nextStoryName = nextStoryName;
    }

    public override IEnumerator Execute()
    {
        // 다음 스토리로의 전환 처리
        StoryManager.Instance.StartStory(_nextStoryName);
        yield return null; // 전환 시에는 특별한 대기 없이 바로 넘어가도록 처리
    }
}
