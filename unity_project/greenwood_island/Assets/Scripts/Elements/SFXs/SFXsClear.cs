using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // DOTween을 사용하여 페이드 아웃 효과 적용

public class SFXsClear : Element
{
    private float _fadeDuration;

    public SFXsClear(float fadeDuration = 1f) // 페이드 아웃 시간을 인자로 받음
    {
        _fadeDuration = fadeDuration;
    }

    public override void ExecuteInstantly()
    {
        _fadeDuration = 0;
        Execute();
    }

    public override IEnumerator ExecuteRoutine()
    {
        SFXManager.Instance.FadeOutAndDestroyAllSFX(_fadeDuration);
        // 모든 페이드 아웃이 완료될 때까지 기다림
        yield return new WaitForSeconds(_fadeDuration);
    }

}
