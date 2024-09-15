using System.Collections;
using UnityEngine;

public class StoryTransition : Element
{
    private string _storyID;

    // 생성자를 통해 string storyID만 받도록 설정
    public StoryTransition(string storyID)
    {
        _storyID = storyID;
    }

    public override IEnumerator ExecuteRoutine()
    {
        // StoryManager를 통해 스토리 실행
        StoryManager.PlayStory(_storyID);

        yield return null;
    }
}
