using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlaceManager : SingletonManager<PlaceManager>
{
    private Image _preivousPlaceImage;
    private Image _currentPlaceImage;

    public Image CurrentPlaceImage { get => _currentPlaceImage; }
    public Image PreivousPlaceImage { get => _preivousPlaceImage; }

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
        _preivousPlaceImage = _currentPlaceImage;
        _currentPlaceImage = ImageUtils.CreateImage(placeSprite, UIManager.SystemCanvas.PlaceLayer.transform);

        return _currentPlaceImage;
    }
}
