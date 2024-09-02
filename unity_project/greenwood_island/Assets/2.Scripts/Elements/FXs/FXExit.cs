using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

[System.Serializable]
public class FXExit : Element
{
    private FXType _fxType;
    private float _duration;

    public FXExit(FXType fxType, float duration = 1f)
    {
        _fxType = fxType;
        _duration = duration;
    }

    public override IEnumerator Execute()
    {
        List<GameObject> activeFXs = FXManager.Instance.GetActiveFXs(_fxType);

        foreach (var fxInstance in activeFXs)
        {
            if (fxInstance != null)
            {
                // DOTween을 이용한 페이드 아웃 예제
                CanvasGroup canvasGroup = fxInstance.GetComponent<CanvasGroup>();
                if (canvasGroup != null)
                {
                    canvasGroup.DOFade(0, _duration).OnComplete(() =>
                    {
                        FXManager.Instance.RemoveFX(fxInstance);
                    });
                }
                else
                {
                    // CanvasGroup이 없으면 단순히 제거
                    FXManager.Instance.RemoveFX(fxInstance);
                }
            }
        }

        yield return new WaitForSeconds(_duration);
    }
}
