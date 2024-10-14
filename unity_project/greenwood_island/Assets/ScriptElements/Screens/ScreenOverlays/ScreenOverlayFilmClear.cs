using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// ScreenOverlayFilmClear 클래스는 화면 오버레이 필름 효과를 복원하고 제거하는 역할을 합니다.
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

    public override void ExecuteInstantly()
    {
        _duration = 0;
        Execute();
    }

    public override IEnumerator ExecuteRoutine()
    {
        Image _overlayFilm = UIManager.SystemCanvas.ScreenOverlayFilm;

        if (_overlayFilm == null)
        {
            yield break;
            // 오버레이 필름 색상을 투명하게 페이드 아웃
        }
        Color targetColor =  _isBlackClear ? Color.black : Color.clear;
        _overlayFilm.FadeImage(targetColor, _duration, _easeType);
        yield return new WaitForSeconds(_duration);
    }
}
