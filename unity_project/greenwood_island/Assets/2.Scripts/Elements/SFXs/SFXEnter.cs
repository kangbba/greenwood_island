using System.Collections;
using UnityEngine;

[System.Serializable]
public class SFXEnter : Element
{
    private string _sfxID;
    private Story _story; // Story 객체를 저장
    private Vector3 _localPos;
    private bool _isLoop;
    private float _loopTerm;

    // Story 객체를 인자로 받아 스토리 정보를 활용
    public SFXEnter(string sfxID, Story story, bool isLoop = true, float loopTerm = 0f, Vector3 localPos = default)
    {
        _sfxID = sfxID;
        _story = story; // 스토리 객체 초기화
        _localPos = localPos == default ? Vector3.zero : localPos; // 기본값을 Vector3.zero로 설정
        _isLoop = isLoop;
        _loopTerm = loopTerm;
    }

    public override IEnumerator ExecuteRoutine()
    {
        // SFX 생성 시 SFXManager를 통해 재생
        AudioSource sfxSource;
        string storyName = _story.StoryId; // Story 객체에서 스토리 이름을 추출

        if (_isLoop && _loopTerm >= 0f)
        {
            sfxSource = SFXManager.Instance.PlaySFXLoop(_sfxID, storyName, _localPos, _loopTerm); // 스토리 이름 전달
        }
        else
        {
            sfxSource = SFXManager.Instance.PlaySFXOnce(_sfxID, storyName, _localPos); // 스토리 이름 전달
        }

        if (sfxSource != null)
        {
            yield return null; // SFX는 자동으로 재생되며 반복되므로 대기 상태는 필요하지 않음
        }
    }
}
