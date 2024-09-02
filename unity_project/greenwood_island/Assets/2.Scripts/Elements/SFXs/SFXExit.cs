using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // DOTween을 사용하여 페이드 아웃 효과 적용

[System.Serializable]
public class SFXExit : Element
{
    private SFXType _sfxType;
    private float _fadeDuration;

    public SFXExit(SFXType sfxType, float fadeDuration = 1f) // 페이드 아웃 시간을 인자로 받음
    {
        _sfxType = sfxType;
        _fadeDuration = fadeDuration;
    }

    public override IEnumerator ExecuteRoutine()
    {
        // 활성화된 SFX 오디오 소스를 가져옴
        List<AudioSource> activeSFXs = SFXManager.Instance.GetActiveSFXs(_sfxType);

        // 리스트를 역순으로 순회하며 안전하게 제거
        for (int i = activeSFXs.Count - 1; i >= 0; i--)
        {
            AudioSource audioSource = activeSFXs[i];
            if (audioSource != null)
            {
                // 볼륨을 페이드 아웃 시키고 완료될 때까지 대기
                yield return audioSource.DOFade(0f, _fadeDuration).WaitForCompletion();

                // 페이드 아웃 완료 후 SFX 제거
                SFXManager.Instance.RemoveSFX(audioSource);
            }
        }

        yield return null;
    }
}
