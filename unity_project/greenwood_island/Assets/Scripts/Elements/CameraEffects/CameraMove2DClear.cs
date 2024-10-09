using System.Collections;
using DG.Tweening;
using UnityEngine;

public class CameraMove2DClear : Element
{
    private float _duration;
    private Ease _easeType;

    public CameraMove2DClear(float duration = 1f, Ease easeType = Ease.InOutQuad)
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
    {
        yield return new CameraMove2D(Vector2.zero, _duration, _easeType).ExecuteRoutine();
    }

}
