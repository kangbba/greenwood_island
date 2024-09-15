using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[System.Serializable]
public class FXExit : Element
{
    private string _fxID; // FX의 ID
    private float _duration;

    public FXExit(string fxID, float duration = 1f)
    {
        _fxID = fxID;
        _duration = duration;
    }

    public override IEnumerator ExecuteRoutine()
    {
        // FXManager에서 활성화된 FX 리스트를 가져옴
        List<GameObject> activeFXs = FXManager.GetActiveFXs(_fxID);

        // 리스트를 역순으로 순회하여 안전하게 제거
        for (int i = activeFXs.Count - 1; i >= 0; i--)
        {
            GameObject fxInstance = activeFXs[i];
            if (fxInstance != null)
            {
                // FXManager를 통해 CanvasGroup 확인 및 페이드 아웃 적용
                FXManager.FadeAndDestroyFX(fxInstance, _duration);
            }
        }
        yield return new WaitForSeconds(_duration);
    }
}
