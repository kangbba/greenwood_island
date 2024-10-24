using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlaceManager : SingletonManager<PlaceManager>
{
    private Image _preivousPlaceImage;
    private Image _currentPlaceImage;
    private Material _invertColorMaterial;

    public Image CurrentPlaceImage { get => _currentPlaceImage; }
    public Image PreivousPlaceImage { get => _preivousPlaceImage; }

    private void Awake(){
        _invertColorMaterial = Resources.Load<Material>("ImaginationManager/InvertColorMat");
    }
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
    // 색상 반전 효과를 적용하는 메서드
    public void InvertColorEffect(bool isInverted)
    {
        if (_currentPlaceImage == null)
        {
            Debug.LogError("No active imagination to apply invert effect.");
            return;
        }

        // _invertColorMaterial이 로드되지 않았으면 로드 (Lazy Loading)
        if (_invertColorMaterial == null)
        {
            Debug.LogError("InvertColorMat not found in Resources.");
            return;
        }

        // 반전 효과 적용
        if (isInverted)
        {
            Debug.Log("Applying inverted color effect.");

            // Instantiate를 사용하여 새로운 머티리얼 인스턴스 생성
            Material instantiatedMaterial = Object.Instantiate(_invertColorMaterial);
            _currentPlaceImage.material = instantiatedMaterial; // 인스턴스화된 머티리얼 적용
            _currentPlaceImage.material.SetFloat("_InvertEffect", 1);
        }
        else
        {
            Debug.Log("Removing inverted color effect.");

            // 머티리얼을 비움
            if (_currentPlaceImage.material != null)
            {
                _currentPlaceImage.material.SetFloat("_InvertEffect", 0); // 반전 효과 제거
                _currentPlaceImage.material = null; // 머티리얼 비우기
            }
        }
    }
}
