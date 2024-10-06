using System.Collections;
using UnityEngine;

public class StoryTransition : Element
{
    private Story _storyInstance;

    // 생성자를 통해 Story 인스턴스를 받도록 설정
    public StoryTransition(Story storyInstance)
    {
        _storyInstance = storyInstance;
    }

    public override IEnumerator ExecuteRoutine()
    {
        // StoryManager를 통해 스토리 실행
        if (_storyInstance != null)
        {
            StoryManager.Instance.PlayStory(_storyInstance);
        }
        else
        {
            Debug.LogError("Story instance is null.");
        }

        yield return null;
    }
}
