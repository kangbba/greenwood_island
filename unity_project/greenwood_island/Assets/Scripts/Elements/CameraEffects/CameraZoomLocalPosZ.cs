using System.Collections;
using DG.Tweening;
using UnityEngine;

[System.Serializable]
public class CameraZoomLocalPosZ : Element
{
    private float _localPosZ;  // Z 위치 값
    private float _duration;   // 줌 시간
    private Ease _easeType;    // Ease 타입

    public CameraZoomLocalPosZ(float localPosZ, float duration = 1f, Ease easeType = Ease.InOutQuad)
    {
        _localPosZ = localPosZ;  // 입력된 Z 값을 그대로 사용
        _duration = duration;
        _easeType = easeType;
    }

    public override IEnumerator ExecuteRoutine()
    {
        CameraController.ZoomLocalPosZ(_localPosZ, _duration, _easeType);
        yield return new WaitForSeconds(_duration);
    }
}
