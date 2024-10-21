using System.Collections;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// PlaceShake 클래스는 장소 레이어를 흔드는 Element입니다.
/// </summary>
public class PlaceShake : Element
{
    private float _duration;
    private float _strength;
    private int _vibrato;
    private float _randomness;

    public PlaceShake(float duration = 1f, float strength = 20f, int vibrato = 10, float randomness = 5) 
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
        Transform placeLayer = UIManager.SystemCanvas.PlaceLayer;

        yield return placeLayer.DOShakePosition(_duration, _strength, _vibrato, _randomness).WaitForCompletion();
    }
}
