using System.Collections;
using DG.Tweening;
using UnityEngine;

[System.Serializable]
public class CameraZoomEffect : Element
{
    private float _targetFOV;
    private float _duration;
    private Ease _easeType;

    public CameraZoomEffect(float targetFOV = 60f, float duration = 1f, Ease easeType = Ease.Linear)
    {
        _targetFOV = targetFOV;
        _duration = duration;
        _easeType = easeType;
    }

    public override IEnumerator ExecuteRoutine()
    {
        if (CameraController.Instance == null)
        {
            Debug.LogWarning("CameraController instance is not available.");
            yield break;
        }

        CameraController.Instance.ZoomTo(_targetFOV, _duration, _easeType);
        yield return new WaitForSeconds(_duration);
    }
}
