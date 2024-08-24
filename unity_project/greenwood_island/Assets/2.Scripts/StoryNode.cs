using UnityEngine;

[CreateAssetMenu(fileName = "NewStoryNode", menuName = "Story/StoryNode")]
public class StoryNode : ScriptableObject
{
    public string storyName;
    public StoryNode nextStoryNode;  // 다음에 이어질 스토리 노드를 참조
}
