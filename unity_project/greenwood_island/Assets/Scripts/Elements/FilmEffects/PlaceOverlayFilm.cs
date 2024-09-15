using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// PlaceOverlayFilmEffect 클래스는 특정 장소에 덮이는 오버레이 필름 효과를 제어하는 Element입니다.
/// 이 클래스는 장면 전환 또는 연출 시 특정 장소에 컬러 필름을 덮어 분위기를 조성하는 효과를 제공합니다.
/// 원래 색상으로 복원하는 기능은 포함되지 않으며, 외부에서 수동으로 처리할 수 있습니다.
/// </summary>
public class PlaceOverlayFilm : Element
{
    private Color _targetColor;
    private float _duration;
    private Ease _easeType;

    Image PlaceOverlayFilmImg => UIManager.Instance.WorldCanvas.PlaceOverlayFilm;

    public PlaceOverlayFilm(Color targetColor, float duration = 1f, Ease easeType = Ease.OutQuad)
    {
        _targetColor = targetColor;
        _duration = duration;
        _easeType = easeType;
    }

    public override IEnumerator ExecuteRoutine()
    {
        if (PlaceOverlayFilmImg == null)
        {
            Debug.LogWarning("PlaceOverlayFilm not found on WorldCanvas.");
            yield break;
        }
        yield return PlaceOverlayFilmImg.DOColor(_targetColor, _duration).SetEase(_easeType).WaitForCompletion();
    }
}
