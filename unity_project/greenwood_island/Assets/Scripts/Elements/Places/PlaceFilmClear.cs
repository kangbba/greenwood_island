using System.Collections;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// PlaceEffectRestore 클래스는 현재 활성화된 장소의 색상을 원래 상태로 복원하는 Element입니다.
/// PlaceEffect로 적용된 색상을 기본적으로 Color.white로 되돌리며, 지정된 시간과 이징 타입을 사용합니다.
/// </summary>
public class PlaceFilmClear : Element
{
    private float _duration;
    private Ease _easeType;

    public PlaceFilmClear(float duration = 1f, Ease easeType = Ease.OutQuad)
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
       new PlaceFilm(Color.white, _duration, _easeType).Execute();
       yield return new WaitForSeconds(_duration);
    }
}
