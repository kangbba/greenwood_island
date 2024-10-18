using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// PlaceOverlayFilmClear 클래스는 특정 장소에 덮이는 모든 오버레이 필름 효과를 복원하고 제거하는 역할을 합니다.
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
        // PlaceOverlayFilmLayer의 자식 오브젝트들을 모두 찾기
        var overlayFilmLayer = UIManager.SystemCanvas.PlaceOverlayFilmLayer;
        if (overlayFilmLayer == null)
        {
            Debug.LogError("PlaceOverlayFilmLayer is missing in UIManager.");
            yield break;
        }

        // 자식 오브젝트 중 Image 컴포넌트를 가진 모든 오브젝트에 대해 FadeOutAndDestroyImage 수행
        foreach (Transform child in overlayFilmLayer.transform)
        {
            Image overlayFilm = child.GetComponent<Image>();
            if (overlayFilm != null)
            {
                // ImageController의 FadeOutAndDestroyImage 메서드를 사용하여 페이드 아웃 후 제거
                overlayFilm.FadeOutAndDestroyImage(_duration, _easeType);
            }
        }

        // 애니메이션이 끝날 때까지 대기
        yield return new WaitForSeconds(_duration);
    }
}
