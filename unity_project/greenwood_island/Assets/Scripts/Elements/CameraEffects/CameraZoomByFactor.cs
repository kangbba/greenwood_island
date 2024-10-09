using System.Collections;
using DG.Tweening;
using UnityEngine;

public class CameraZoomByFactor : Element
{
    private float _zoomFactor;  // 줌 비율 (0: 최대 줌, 1: 최소 줌)
    private float _duration;    // 줌 시간
    private Ease _easeType;     // Ease 타입

    public CameraZoomByFactor(float zoomFactor, float duration = 1f, Ease easeType = Ease.InOutQuad)
    {
        _zoomFactor = Mathf.Clamp01(zoomFactor);  // 줌 비율을 0 ~ 1 사이로 제한
        _duration = duration;
        _easeType = easeType;
    }

    public override void ExecuteInstantly()
    {
        _duration = 0f;
        Execute();
    }

    public override IEnumerator ExecuteRoutine()
    {
        CameraController.ZoomByFactor(_zoomFactor, _duration, _easeType);

        yield return new WaitForSeconds(_duration);
    }
}
