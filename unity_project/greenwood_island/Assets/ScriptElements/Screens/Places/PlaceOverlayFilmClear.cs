using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// PlaceOverlayFilmClear 클래스는 특정 장소에 덮이는 오버레이 필름 효과를 복원하고 제거하는 역할을 합니다.
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
        // PlaceOverlayFilmLayer의 자식으로 있는 이미지 찾기
        Image overlayFilm = UIManager.SystemCanvas.PlaceOverlayFilm;


        if (overlayFilm != null)
        {
            // 투명하게 페이드 아웃한 후 오버레이 필름 제거
            yield return ImageController.FadeColor(overlayFilm, Color.clear, _duration, _easeType).WaitForCompletion();

            // 애니메이션이 끝난 후 이미지 파괴
            Object.Destroy(overlayFilm.gameObject);
        }
        else
        {
            Debug.LogWarning("No overlay film image found in PlaceOverlayFilmLayer.");
        }
    }
}
