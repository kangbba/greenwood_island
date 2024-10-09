using System.Collections;
using UnityEngine;

public class CameraShake : Element
{
    private float _duration;
    private float _strength;
    private int _vibrato;
    private float _randomness;


    // TimeElement의 생성자에서 duration을 받아 처리
    public CameraShake(float duration = 1f, float strength = 10f, int vibrato = 10, float randomness = 90f) 
    {
        _duration = duration;
        _strength = strength;
        _vibrato = vibrato;
        _randomness = randomness;
    }

    public override void ExecuteInstantly()
    {
        _duration = 0;
        Execute();
    }

    public override IEnumerator ExecuteRoutine()
    {
        CameraController.Shake(_duration, _strength, _vibrato, _randomness);
        yield return new WaitForSeconds(_duration);
    }
}
