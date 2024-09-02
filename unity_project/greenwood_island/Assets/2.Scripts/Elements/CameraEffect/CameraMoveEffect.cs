using System.Collections;
using DG.Tweening;
using UnityEngine;

[System.Serializable]
public class CameraMoveEffect : Element
{
    private Vector3 _targetPosition;
    private float _duration;
    private Ease _easeType;

    public CameraMoveEffect(Vector3 targetPosition, float duration = 1f, Ease easeType = Ease.Linear)
    {
        _targetPosition = targetPosition;
        _duration = duration;
        _easeType = easeType;
    }

    public override IEnumerator Execute()
    {
        if (CameraController.Instance == null)
        {
            Debug.LogWarning("CameraController instance is not available.");
            yield break;
        }

        CameraController.Instance.MoveTo(_targetPosition, _duration, _easeType);
        yield return new WaitForSeconds(_duration);
    }
}
