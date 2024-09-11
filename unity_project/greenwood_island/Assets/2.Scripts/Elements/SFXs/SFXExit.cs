using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // DOTween을 사용하여 페이드 아웃 효과 적용

[System.Serializable]
public class SFXExit : Element
{
    private string _sfxID;
    private float _fadeDuration;

    public SFXExit(string sfxID, float fadeDuration = 1f) // 페이드 아웃 시간을 인자로 받음
    {
        _sfxID = sfxID;
        _fadeDuration = fadeDuration;
    }

    public override IEnumerator ExecuteRoutine()
    {
        // 활성화된 SFX 오디오 소스를 가져옴
        List<AudioSource> activeSFXs = SFXManager.GetActiveSFXs(_sfxID);

        // 리스트를 역순으로 순회하여 안전하게 페이드 아웃 후 제거
        for (int i = activeSFXs.Count - 1; i >= 0; i--)
        {
            AudioSource audioSource = activeSFXs[i];
            if (audioSource != null)
            {
                // SFXManager를 통해 페이드 아웃과 제거를 동시에 처리
                SFXManager.FadeOutAndDestroy(audioSource, _fadeDuration);
            }
        }

        // 모든 페이드 아웃이 완료될 때까지 대기
        yield return new WaitForSeconds(_fadeDuration);
    }
}
