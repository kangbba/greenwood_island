using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// ScreenOverlayFilmEffect 클래스는 Unity 프로젝트에서 화면 오버레이 필름 효과를 제어하는 Element입니다.
/// 이 클래스는 화면 전환 또는 특정 연출 시 화면을 덮는 필름 효과를 실행하고 제어하는 데 사용됩니다.
/// 화면의 원래 색상에서 타겟 색상으로의 전환을 제공하여 장면 전환을 부드럽게 만들어 줍니다.
/// </summary>
public class ScreenOverlayFilm : Element
{
    private Color _overlayColor;
    private float _duration;
    private Ease _easeType;

    // ScreenOverlayFilm을 참조
    private Image ScreenOverlayFilmImg => UIManager.Instance.WorldCanvas.ScreenOverlayFilm;

    public ScreenOverlayFilm(Color overlayColor, float duration = 1f, Ease easeType = Ease.Linear)
    {
        _overlayColor = overlayColor;
        _duration = duration;
        _easeType = easeType;
    }

    public override IEnumerator ExecuteRoutine()
    {
        // ScreenOverlayFilm이 null인 경우 예외 처리 및 얼리 리턴
        if (ScreenOverlayFilmImg == null)
        {
            Debug.LogError("ScreenOverlayFilm is not assigned or found.");
            yield break;
        }

        // 현재 ScreenOverlayFilm의 색상에서 타겟 색상으로 전환
        yield return ScreenOverlayFilmImg.DOColor(_overlayColor, _duration).SetEase(_easeType).WaitForCompletion();
    }
}
