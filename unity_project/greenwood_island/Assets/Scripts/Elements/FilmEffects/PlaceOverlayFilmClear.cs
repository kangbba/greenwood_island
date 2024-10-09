using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// PlaceOverlayFilmEffect 클래스는 특정 장소에 덮이는 오버레이 필름 효과를 복원하는 Element입니다.
/// </summary>
public class PlaceOverlayFilmClear : Element
{
    private float _duration;
    private Ease _easeType;


    public PlaceOverlayFilmClear(float duration = 1f, Ease easeType = Ease.OutQuad)
    {
        _duration = duration;
        _easeType = easeType;
    }

    public override void ExecuteInstantly()
    {
        _duration = 0;
        Execute();
    }
    
    public override IEnumerator ExecuteRoutine()
    {
        new PlaceOverlayFilm(Color.clear, _duration, _easeType).Execute();
        yield return _duration;
    }

}
