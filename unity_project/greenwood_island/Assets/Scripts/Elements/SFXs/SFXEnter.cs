using System.Collections;
using UnityEngine;

[System.Serializable]
public class SFXEnter : Element
{
    private string _sfxID;
    private float _volume;
    private bool _isLoop;
    private float _loopTerm;
    private bool _waitForEnd;

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
        AudioSource sfxSource;

        if (_isLoop && _loopTerm >= 0f)
        {
            // 반복 재생 시 SFXManager의 PlaySFXLoop 사용
            sfxSource = SFXManager.PlaySFXLoop(_sfxID, _volume, _loopTerm); // 볼륨과 반복 간격을 전달
        }
        else
        {
            // 단일 재생 시 코루틴을 기다리면서 실행
            SFXManager.PlaySFXOnce(_sfxID, _volume); // SFXManager에서 PlaySFXOnceCoroutine 호출
        }
        yield return null;
    }
}
