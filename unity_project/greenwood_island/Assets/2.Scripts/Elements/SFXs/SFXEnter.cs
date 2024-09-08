using System.Collections;
using UnityEngine;

[System.Serializable]
public class SFXEnter : Element
{
    private string _sfxID;
    private float _volume;
    private bool _isLoop;
    private float _loopTerm;

    // 생성자: SFX ID, 볼륨, 반복 여부, 반복 간격을 설정
    public SFXEnter(string sfxID, float volume = 1f, bool isLoop = true, float loopTerm = 0f)
    {
        _sfxID = sfxID;
        _volume = Mathf.Clamp(volume, 0f, 1f); // 볼륨을 0에서 1 사이로 제한
        _isLoop = isLoop;
        _loopTerm = loopTerm;
    }

    public override IEnumerator ExecuteRoutine()
    {
        // SFX 생성 시 SFXManager를 통해 재생
        AudioSource sfxSource;

        if (_isLoop && _loopTerm >= 0f)
        {
            sfxSource = SFXManager.Instance.PlaySFXLoop(_sfxID, _volume, _loopTerm); // 볼륨과 반복 간격을 전달
        }
        else
        {
            sfxSource = SFXManager.Instance.PlaySFXOnce(_sfxID, _volume); // 볼륨 전달
        }

        if (sfxSource != null)
        {
            yield return null; // SFX는 자동으로 재생되며 반복되므로 대기 상태는 필요하지 않음
        }
    }
}
