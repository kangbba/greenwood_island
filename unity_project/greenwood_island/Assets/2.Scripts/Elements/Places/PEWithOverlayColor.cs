using System.Collections;
using UnityEngine;

/// <summary>
/// PEScreenOverlay 클래스는 장소 전환 시 화면을 지정된 색상으로 페이드 인/아웃하는 효과를 제공합니다.
/// </summary>
public class PEWithOverlayColor : Element
{
    private EPlaceID _newPlaceID; // 이동할 새로운 장소의 ID
    private Color _overlayColor; // 페이드 효과의 색상
    private float _fadeDuration; // 페이드 효과의 지속 시간

    public PEWithOverlayColor(EPlaceID newPlaceID, Color overlayColor, float fadeDuration = 1f)
    {
        _newPlaceID = newPlaceID;
        _overlayColor = overlayColor;
        _fadeDuration = fadeDuration;
    }

    public override IEnumerator ExecuteRoutine()
    {
        Debug.Log($"Executing PEScreenOverlay Effect with color: {_overlayColor}");

        // 화면을 지정된 색상으로 페이드 인하고, 장소 전환 후 페이드 아웃
        yield return new PlaceEnter(
            new SequentialElement(new ScreenOverlayFilm(_overlayColor, _fadeDuration)), // 전환 전 효과
            _newPlaceID,
            new SequentialElement(new ScreenOverlayFilmClear(_fadeDuration)) // 전환 후 효과
        ).ExecuteRoutine();
    }
}
