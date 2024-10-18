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
        // PlaceOverlayFilmLayer의 자식으로 기존 Image를 찾기
        var overlayFilmLayer = UIManager.SystemCanvas.PlaceOverlayFilmLayer;
        if (overlayFilmLayer == null)
        {
            Debug.LogError("PlaceOverlayFilmLayer is missing in UIManager.");
            yield break;
        }

        // 자식 오브젝트 중 'OverlayFilm'이라는 이름의 오브젝트를 찾음
        Transform existingOverlay = overlayFilmLayer.transform.Find("OverlayFilm");
        Image overlayFilmImage;

        if (existingOverlay != null)
        {
            // 기존 오브젝트가 있으면 해당 이미지를 사용
            overlayFilmImage = existingOverlay.GetComponent<Image>();
        }
        else
        {
            Debug.Log("PlaceOverlay");
            // 기존 오브젝트가 없으면 새로 생성
            GameObject overlayFilmObject = new GameObject("OverlayFilm");
            overlayFilmImage = overlayFilmObject.AddComponent<Image>();

            // 새로 생성된 Image의 부모를 PlaceOverlayFilmLayer로 설정
            overlayFilmObject.transform.SetParent(overlayFilmLayer.transform, false);

            // 앵커를 사방으로 stretched로 설정
            RectTransform overlayFilmRect = overlayFilmObject.GetComponent<RectTransform>();
            overlayFilmRect.anchorMin = Vector2.zero; // 왼쪽 아래 (0, 0)
            overlayFilmRect.anchorMax = Vector2.one;  // 오른쪽 위 (1, 1)
            overlayFilmRect.offsetMin = Vector2.zero; // 경계 최소값 설정
            overlayFilmRect.offsetMax = Vector2.zero; // 경계 최대값 설정
        }

        // ImageController를 통해 색상 변경 애니메이션 적용 (투명 -> 타겟 컬러)
        overlayFilmImage.color = _targetColor.ModifiedAlpha(0f);
        overlayFilmImage.FadeImage(_targetColor, _duration, _easeType);

        // 애니메이션이 끝날 때까지 대기
        yield return new WaitForSeconds(_duration);
    }
}
