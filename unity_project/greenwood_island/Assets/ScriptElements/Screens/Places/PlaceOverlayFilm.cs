using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// PlaceOverlayFilm 클래스는 특정 장소에 덮이는 오버레이 필름 효과를 제어하는 Element입니다.
/// </summary>
public class PlaceOverlayFilm : Element
{
    private Color _targetColor;
    private float _duration;
    private Ease _easeType;

    public PlaceOverlayFilm(Color targetColor, float duration = 1f, Ease easeType = Ease.OutQuad)
    {
        _targetColor = targetColor;
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
        Image _overlayFilm = UIManager.SystemCanvas.PlaceOverlayFilm;

        // ImageController를 통해 색상 변경 애니메이션 적용
        ImageController.FadeColor(_overlayFilm, _targetColor, _duration, _easeType);

        // 애니메이션이 끝날 때까지 기다림
        yield return new WaitForSeconds(_duration);
    }
}
