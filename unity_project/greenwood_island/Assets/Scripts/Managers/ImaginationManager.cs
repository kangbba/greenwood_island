using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ImaginationManager : SingletonManager<ImaginationManager>
{
    private Image _currentImaginationImage;
    private Image _previousImaginationImage;
    private Material _invertColorMaterial;

    public Image CurrentImaginationImage { get => _currentImaginationImage; }
    public Image PreviousImaginationImage { get => _previousImaginationImage; }

    // 상상 이미지를 생성하는 메서드
    public Image CreateImagination(string imaginationID)
    {
        // 리소스 경로 처리
        string resourcePath = ResourcePathManager.GetCurrentStoryResourcePath(imaginationID, ResourceType.Imagination);
        Sprite imaginationSprite = Resources.Load<Sprite>(resourcePath);

        if (imaginationSprite == null)
        {
            resourcePath = ResourcePathManager.GetSharedResourcePath(imaginationID, ResourceType.Imagination);
            imaginationSprite = Resources.Load<Sprite>(resourcePath);
        }

        if (imaginationSprite == null)
        {
            Debug.LogError($"Failed to load sprite for Imagination ID: {imaginationID} from path '{resourcePath}'");
            return null;
        }

        // 스프라이트를 이용해 이미지 생성
        _previousImaginationImage = _currentImaginationImage;
        _currentImaginationImage = ImageUtils.CreateImage(imaginationSprite, UIManager.SystemCanvas.ImaginationLayer.transform);

        return _currentImaginationImage;
    }

    // 색상 반전 효과를 적용하는 메서드
    // 색상 반전 효과를 적용하는 메서드
    // 색상 반전 효과를 적용하는 메서드
    public void InvertColorEffect(bool isInverted)
    {
        if (_currentImaginationImage == null)
        {
            Debug.LogError("No active imagination to apply invert effect.");
            return;
        }

        // _invertColorMaterial이 로드되지 않았으면 로드 (Lazy Loading)
        if (_invertColorMaterial == null)
        {
            _invertColorMaterial = Resources.Load<Material>("ImaginationManager/InvertColorMat");
            if (_invertColorMaterial == null)
            {
                Debug.LogError("InvertColorMat not found in Resources.");
                return;
            }
            else
            {
                Debug.Log("_invertColorMaterial successfully loaded.");
            }
        }

        // 반전 효과 적용
        if (isInverted)
        {
            Debug.Log("Applying inverted color effect.");

            // Instantiate를 사용하여 새로운 머티리얼 인스턴스 생성
            Material instantiatedMaterial = Object.Instantiate(_invertColorMaterial);
            _currentImaginationImage.material = instantiatedMaterial; // 인스턴스화된 머티리얼 적용
            _currentImaginationImage.material.SetFloat("_InvertEffect", 1);
        }
        else
        {
            Debug.Log("Removing inverted color effect.");

            // 머티리얼을 비움
            if (_currentImaginationImage.material != null)
            {
                _currentImaginationImage.material.SetFloat("_InvertEffect", 0); // 반전 효과 제거
                _currentImaginationImage.material = null; // 머티리얼 비우기
            }
        }
    }
}
