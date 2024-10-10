using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ImaginationManager : SingletonManager<ImaginationManager>
{
    private Material _invertColorMaterial;

    private string _currentImaginationID; // 현재 활성화된 상상 이미지 ID
    private Image _currentImaginationImage; // 현재 활성화된 상상 이미지 객체

    public string CurrentImaginationID { get => _currentImaginationID; }
    public Image CurrentImaginationImage { get => _currentImaginationImage; }

    // 상상 이미지를 생성하여 등록하는 메서드
    public Image CreateImagination(string imaginationID)
    {
        // 현재 활성화된 상상이 있으면 제거
        if (!string.IsNullOrEmpty(_currentImaginationID))
        {
            Debug.Log($"Destroying existing imagination with ID: {_currentImaginationID}");
            DestroyImagination(_currentImaginationID, 0.5f, Ease.OutQuad);
        }

        Transform parent = UIManager.SystemCanvas.ImaginationLayer.transform;

        // 새로운 GameObject 생성
        GameObject imaginationObject = new GameObject(imaginationID);
        imaginationObject.transform.SetParent(parent, false);

        // Image 컴포넌트 추가 및 초기화
        Image img = imaginationObject.AddComponent<Image>();
        img.rectTransform.anchoredPosition = Vector2.zero;
        img.transform.localScale = Vector2.one;

        // Imagination에 해당하는 Sprite 로드
        string resourcePath = ResourcePathManager.GetCurrentStoryResourcePath(imaginationID, ResourceType.Imagination);
        Sprite imaginationSprite = Resources.Load<Sprite>(resourcePath);

        // 스토리 리소스에서 로드 실패 시 공유 리소스에서 재시도
        if (imaginationSprite == null)
        {
            resourcePath = ResourcePathManager.GetSharedResourcePath(imaginationID, ResourceType.Imagination);
            imaginationSprite = Resources.Load<Sprite>(resourcePath);
        }

        if (imaginationSprite == null)
        {
            Debug.LogError($"Failed to load sprite for Imagination ID: {imaginationID} from path '{resourcePath}'");
            Object.Destroy(imaginationObject); // 이미지 로드 실패 시 오브젝트 제거
            return null;
        }

        // 이미지 초기화
        img.sprite = imaginationSprite;
        img.SetNativeSize();
        Vector2 nativeImgSize = img.rectTransform.sizeDelta;
        float heightRatio = 1080 / nativeImgSize.y;
        img.rectTransform.sizeDelta = new Vector2(nativeImgSize.x * heightRatio, 1080);
        img.color = new Color(img.color.r, img.color.g, img.color.b, 0f); // 초기 알파값을 0으로 설정

        // 현재 상상 이미지 업데이트
        _currentImaginationID = imaginationID;
        _currentImaginationImage = img;

        return img;
    }

    // 상상 이미지를 imaginationID로 찾아 페이드 인하는 메서드
    public void FadeColor(Color targetColor, float duration, Ease easeType)
    {
        // 현재 상상 이미지에 대해 페이드 인
        if (_currentImaginationImage != null)
        {
            _currentImaginationImage.DOColor(targetColor, duration).SetEase(easeType);
        }
        else
        {
            Debug.LogWarning("No active imagination to fade color.");
        }
    }

    // 상상 이미지를 페이드 아웃 후 제거하는 메서드
    public void Move(Vector2 anchoredPos, float duration, Ease ease)
    {
        // 현재 상상 이미지가 없으면 경고
        if (_currentImaginationImage == null)
        {
            Debug.LogWarning("No active imagination found for movement.");
            return;
        }

        // anchoredPosition으로 이동하는 애니메이션 실행
        RectTransform rectTransform = _currentImaginationImage.rectTransform;
        rectTransform.DOAnchorPos(anchoredPos, duration).SetEase(ease);
    }

    // 상상 이미지를 특정 크기로 조정하는 메서드
    public void Scale(Vector3 targetScale, float duration, Ease ease)
    {
        // 현재 상상 이미지가 없으면 경고
        if (_currentImaginationImage == null)
        {
            Debug.LogWarning("No active imagination found for scaling.");
            return;
        }

        // localScale을 변경하는 애니메이션 실행
        _currentImaginationImage.transform.DOScale(targetScale, duration).SetEase(ease);
    }

    // 색상 반전 효과를 적용하는 함수
    public void InvertColorEffect(bool isInverted)
    {
        // 현재 상상 이미지가 없으면 경고
        if (_currentImaginationImage == null)
        {
            Debug.LogWarning("No active imagination found for color inversion.");
            return;
        }

        // _invertColorMaterial이 로드되지 않았으면 그때 로드 (Lazy Loading)
        if (_invertColorMaterial == null)
        {
            _invertColorMaterial = Resources.Load<Material>("ImaginationManager/InvertColorMat");
            if (_invertColorMaterial == null)
            {
                Debug.LogError("InvertColorMat not found in Resources/Shaders.");
                return;
            }
            else
            {
                Debug.Log("_invertColorMaterial successfully loaded.");
            }
        }

        // isInverted가 true일 경우 반전 효과 적용
        if (isInverted)
        {
            Debug.Log("Applying inverted color effect.");

            // 새로운 Material 인스턴스화
            Material instantiatedMaterial = new Material(_invertColorMaterial);
            _currentImaginationImage.material = instantiatedMaterial;

            // _InvertEffect 프로퍼티가 있는지 확인
            if (_currentImaginationImage.material.HasProperty("_InvertEffect"))
            {
                Debug.Log("_InvertEffect property found. Setting it to 1 (inverted).");
                _currentImaginationImage.material.SetFloat("_InvertEffect", 1);
            }
            else
            {
                Debug.LogError("_InvertEffect property not found in the material.");
            }
        }
        else
        {
            Debug.Log("Removing inverted color effect.");

            // _InvertEffect 프로퍼티가 있는지 확인
            if (_currentImaginationImage.material.HasProperty("_InvertEffect"))
            {
                Debug.Log("_InvertEffect property found. Setting it to 0 (normal).");
                _currentImaginationImage.material.SetFloat("_InvertEffect", 0);
            }
            else
            {
                Debug.LogError("_InvertEffect property not found in the material.");
            }
        }
    }


    // 상상 이미지를 페이드 아웃 후 제거하는 메서드
    public void DestroyImagination(string imaginationID, float duration, Ease easeType)
    {
        if (_currentImaginationImage != null && _currentImaginationID == imaginationID)
        {
            _currentImaginationImage.DOColor(new Color(_currentImaginationImage.color.r, _currentImaginationImage.color.g, _currentImaginationImage.color.b, 0f), duration).SetEase(easeType).OnComplete(() =>
            {
                Object.Destroy(_currentImaginationImage.gameObject);
                _currentImaginationID = null;
                _currentImaginationImage = null;
                Debug.Log($"Imagination with ID '{imaginationID}' destroyed.");
            });
        }
        else
        {
            Debug.LogWarning($"Imagination with ID '{imaginationID}' not found for destruction.");
        }
    }
}
