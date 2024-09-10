using System.Collections;
using UnityEngine;

[System.Serializable]
public class CameraShake : Element
{
    private float _duration;
    private float _strength;
    private int _vibrato;
    private float _randomness;

    public CameraShake(float duration = 1f, float strength = 10f, int vibrato = 10, float randomness = 90f)
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
