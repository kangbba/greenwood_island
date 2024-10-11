using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlaceManager : SingletonManager<PlaceManager>
{
    private Image _currentPlaceImage;
    private Tween _fadeTween;

    public Image CurrentPlaceImage { get => _currentPlaceImage; }

    // 장소 이미지를 생성하는 메서드
    public Image CreatePlace(string placeID)
    {
        string resourcePath = ResourcePathManager.GetCurrentStoryResourcePath(placeID, ResourceType.Place);
        Sprite placeSprite = Resources.Load<Sprite>(resourcePath);

        if (placeSprite == null)
        {
            resourcePath = ResourcePathManager.GetSharedResourcePath(placeID, ResourceType.Place);
            placeSprite = Resources.Load<Sprite>(resourcePath);
        }

        if (placeSprite == null)
        {
            Debug.LogError($"Failed to load sprite for Place ID: {placeID} from path '{resourcePath}'");
            return null;
        }

        // 스프라이트를 이용해 이미지 생성
        _currentPlaceImage = ImageController.CreateImage(placeSprite, UIManager.SystemCanvas.PlaceLayer.transform);

        return _currentPlaceImage;
    }

    // 장소 이미지를 페이드 아웃 후 파괴하는 메서드
    public void FadeOutAndDestroyPlace(float duration, Ease easeType)
    {
        _fadeTween?.Kill();  // _fadeTween 취소
        if (_currentPlaceImage != null)
        {
            _fadeTween = ImageController.DestroyImage(_currentPlaceImage, duration, easeType);
        }
    }
}
