using System.Collections;
using UnityEngine;

[System.Serializable]
public class CameraShakeEffect : Element
{
    private float _duration;
    private float _strength;
    private int _vibrato;
    private float _randomness;

    public CameraShakeEffect(float duration = 1f, float strength = 3f, int vibrato = 10, float randomness = 90f)
    {
        _duration = duration;
        _strength = strength;
        _vibrato = vibrato;
        _randomness = randomness;
    }

    public override IEnumerator ExecuteRoutine()
    {
        if (CameraController.Instance == null)
        {
            Debug.LogWarning("CameraController instance is not available.");
            yield break;
        }

        CameraController.Instance.Shake(_duration, _strength, _vibrato, _randomness);
        yield return new WaitForSeconds(_duration);
    }
}
