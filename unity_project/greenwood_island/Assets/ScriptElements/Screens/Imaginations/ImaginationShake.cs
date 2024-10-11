using System.Collections;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// ImaginationShake 클래스는 상상 레이어를 흔드는 Element입니다.
/// </summary>
public class ImaginationShake : Element
{
    private float _duration;
    private float _strength;
    private int _vibrato;
    private float _randomness;

    public ImaginationShake(float duration = 1f, float strength = 40f, int vibrato = 10, float randomness = 5) 
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
        Transform imaginationLayer = UIManager.SystemCanvas.ImaginationLayer;

        // ImaginationLayer 흔들기
        yield return imaginationLayer.DOShakePosition(_duration, _strength, _vibrato, _randomness).WaitForCompletion();
    }
}
