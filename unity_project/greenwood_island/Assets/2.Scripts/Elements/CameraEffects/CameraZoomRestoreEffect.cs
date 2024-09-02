using System.Collections;
using DG.Tweening;
using UnityEngine;

[System.Serializable]
public class CameraZoomRestoreEffect : Element
{
    private float _duration;
    private Ease _easeType;

    public CameraZoomRestoreEffect(float duration = 1f, Ease easeType = Ease.Linear)
    {
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

        CameraController.Instance.ZoomRestore(_duration, _easeType);
        yield return new WaitForSeconds(_duration);
    }
}
