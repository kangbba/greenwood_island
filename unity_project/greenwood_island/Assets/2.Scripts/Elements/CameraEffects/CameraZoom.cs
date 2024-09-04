using System.Collections;
using DG.Tweening;
using UnityEngine;

[System.Serializable]
public class CameraZoom : Element
{
    private float _zoomFactor;
    private float _duration;
    private Ease _easeType;

    public CameraZoom(float zoomFactor, float duration = 1f, Ease easeType = Ease.InOutQuad)
    {
        _zoomFactor = Mathf.Clamp01(zoomFactor); // zoomFactor는 0과 1 사이의 값으로 제한
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

        // CameraController의 Zoom 메서드를 사용하여 줌을 조절
        CameraController.Instance.Zoom(_zoomFactor, _duration, _easeType);
        yield return new WaitForSeconds(_duration);
    }
}
