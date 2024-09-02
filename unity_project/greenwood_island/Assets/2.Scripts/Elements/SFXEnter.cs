using System.Collections;
using UnityEngine;

[System.Serializable]
public class SFXEnter : Element
{
    private SFXType _sfxType;
    private Vector3 _localPos;
    private bool _isLoop;
    private float _loopTerm;

    public SFXEnter(SFXType sfxType, bool isLoop = true, float loopTerm = 0f, Vector3 localPos = default)
    {
        _sfxType = sfxType;
        _localPos = localPos == default ? Vector3.zero : localPos; // 기본값을 Vector3.zero로 설정
        _isLoop = isLoop;
        _loopTerm = loopTerm;
    }

    public override IEnumerator Execute()
    {
        // SFX 생성 시 SFXManager를 통해 재생
        AudioSource sfxSource;
        if (_isLoop && _loopTerm > 0f)
        {
            sfxSource = SFXManager.Instance.PlaySFXLoop(_sfxType, _localPos, _loopTerm);
        }
        else
        {
            sfxSource = SFXManager.Instance.PlaySFXOnce(_sfxType, _localPos);
        }

        if (sfxSource != null)
        {
            yield return null; // SFX는 자동으로 재생되며 반복되므로 대기 상태는 필요하지 않음
        }
    }
}
