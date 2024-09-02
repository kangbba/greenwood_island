using System.Collections;
using UnityEngine;

public class StoryTransition : Element
{
    private EStoryID _storyID;

    // 생성자를 통해 EStoryID만 받도록 설정
    public StoryTransition(EStoryID storyID)
    {
        _storyID = storyID;
    }

    public override IEnumerator ExecuteRoutine()
    {
        // StoryManager를 통해 스토리 실행
        StoryManager.Instance.StartStory(_storyID);

        yield return null;
    }
}
