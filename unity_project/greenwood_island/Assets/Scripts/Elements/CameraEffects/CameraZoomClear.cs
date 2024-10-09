using System.Collections;
using DG.Tweening;
using UnityEngine;

public class CameraZoomClear : Element
{
    private float _duration;
    private Ease _easeType;

    public CameraZoomClear(float duration = 1f, Ease easeType = Ease.OutQuad)
    {
        _duration = duration;
        _easeType = easeType;
    }

    public override void ExecuteInstantly()
    {
        _duration = 0;
        Execute();
    }

    public override IEnumerator ExecuteRoutine()
    { // 카메라 줌을 기본값(0)으로 복원
        CameraController.ZoomByFactor(0f, _duration, _easeType);
        yield return new WaitForSeconds(_duration);
    }
}
