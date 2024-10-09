using UnityEngine;

[CreateAssetMenu(fileName = "NewStoryData", menuName = "Story/StoryData")]
public class StoryData : ScriptableObject
{
    // Inspector에서 접근 가능하지만, 외부 코드에서는 수정할 수 없도록 캡슐화
    [SerializeField] private string _storyName_KO;
    [SerializeField] private string _storyDesc;

    [SerializeField] private Sprite _storyThumbnail;

    // 읽기 전용 프로퍼티를 제공하여 외부에서 필드 값을 읽을 수만 있게 설정
    public string StoryDesc
    {
        get { return _storyDesc; }  // 읽기 전용, 외부에서는 수정 불가
    }

    public Sprite StoryThumbnail
    {
        get { return _storyThumbnail; }  // 읽기 전용, 외부에서는 수정 불가
    }

    public string StoryName_KO { get => _storyName_KO; }
}
