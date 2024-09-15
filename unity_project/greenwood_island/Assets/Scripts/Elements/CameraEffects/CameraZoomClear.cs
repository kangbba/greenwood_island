using System.Collections;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// CameraZoomClear 클래스는 카메라의 줌 레벨을 기본 상태(가장 멀리)로 복원하는 Element입니다.
/// </summary>
[System.Serializable]
public class CameraZoomClear : Element
{
    private float _duration;
    private Ease _easeType;

    public CameraZoomClear(float duration = 1f, Ease easeType = Ease.OutQuad)
    {
        _duration = duration;
        _easeType = easeType;
    }

    public override IEnumerator ExecuteRoutine()
    { // 카메라 줌을 기본값(0)으로 복원
        CameraController.ZoomByFactor(0f, _duration, _easeType);
        yield return new WaitForSeconds(_duration);
    }
}
