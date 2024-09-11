using System.Collections;
using DG.Tweening;
using UnityEngine;

[System.Serializable]
public class CameraMove2DClear : Element
{
    private float _duration;
    private Ease _easeType;

    public CameraMove2DClear(float duration = 1f, Ease easeType = Ease.InOutQuad)
    {
        _duration = duration;
        _easeType = easeType;
    }

    public override IEnumerator ExecuteRoutine()
    { // 평면 이동을 원래 위치(Vector2.zero)로 복원
        yield return new CameraMove2D(Vector2.zero, _duration, _easeType).ExecuteRoutine();
    }
}
