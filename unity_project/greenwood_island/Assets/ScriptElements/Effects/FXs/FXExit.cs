using System.Collections;
using UnityEngine;

public class FXExit : Element
{
    private string _fxID; // FX의 ID
    private float _duration;

    public FXExit(string fxID, float duration = 1f)
    {
        _fxID = fxID;
        _duration = duration;
    }

    public override void ExecuteInstantly()
    {
        _duration = 0;
        Execute();
    }

    public override IEnumerator ExecuteRoutine()
    {
        // FXManager에서 활성화된 FX를 가져옴
        GameObject activeFX = FXManager.Instance.GetActiveFX(_fxID);

        // FX가 활성화되어 있으면 페이드 아웃 후 제거
        if (activeFX != null)
        {
            FXManager.Instance.FadeAndDestroyFX(activeFX, _duration);
            yield return new WaitForSeconds(_duration);
        }
    }
}
