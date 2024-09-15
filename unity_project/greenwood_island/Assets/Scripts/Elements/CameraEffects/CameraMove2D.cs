using System.Collections;
using DG.Tweening;
using UnityEngine;

[System.Serializable]
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

    public override IEnumerator ExecuteRoutine()
    {
        // CameraController의 MovePlane 메서드를 사용하여 평면 이동을 실행
        CameraController.MovePlane(_targetLocalPos, _duration, _easeType);
        yield return new WaitForSeconds(_duration);
    }
}
