using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// ScreenOverlayFilmEffect 클래스는 Unity 프로젝트에서 화면 오버레이 필름 효과를 복원하는 Element입니다.
/// </summary>
public class ScreenOverlayFilmClear : Element
{
    private bool _isBlackClear = false;
    private float _duration;
    private Ease _easeType;


    public ScreenOverlayFilmClear(float duration = 1f, Ease easeType = Ease.OutQuad, bool isBlackClear = false)
    {
        _isBlackClear = isBlackClear;
        _duration = duration;
        _easeType = easeType;
    }

    public override IEnumerator ExecuteRoutine()
    {
        new ScreenOverlayFilm(_isBlackClear ? Color.black : Color.clear, _duration, _easeType).Execute();
        yield return new WaitForSeconds(_duration);
    }
}
