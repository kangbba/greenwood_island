using System.Collections;
using DG.Tweening;
using UnityEngine;

public class CameraMove2D : Element
{
    private Vector2 _targetLocalPos;
    private float _duration;
    private Ease _easeType;

    public CameraMove2D(Vector2 targetLocalPos, float duration = 1f, Ease easeType = Ease.InOutQuad)
    {
        _targetLocalPos = targetLocalPos;
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
         CameraController.MovePlane(_targetLocalPos, _duration, _easeType);
         yield return new WaitForSeconds(_duration);
    }
}
