using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// ScreenOverlayFilm 클래스는 화면 오버레이 필름 효과를 제어하는 Element입니다.
/// </summary>
public class ScreenOverlayFilm : Element
{
    private Color _overlayColor;
    private float _duration;
    private Ease _easeType;

    public ScreenOverlayFilm(Color overlayColor, float duration = 1f, Ease easeType = Ease.OutQuad)
    {
        _overlayColor = overlayColor;
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
        // PlaceOverlayFilmLayer의 자식에서 이미지 찾기
        Image _overlayFilm = UIManager.SystemCanvas.ScreenOverlayFilm;
        // ImageController를 통해 오버레이 이미지의 색상 전환 애니메이션 적용
        yield return ImageController.FadeColor(_overlayFilm, _overlayColor, _duration, _easeType).WaitForCompletion();
    }
}
