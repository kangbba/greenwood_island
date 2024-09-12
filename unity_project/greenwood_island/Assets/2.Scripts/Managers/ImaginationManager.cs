using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// ImaginationManager는 활성화된 상상의 이미지를 관리하는 싱글톤 클래스입니다.
/// </summary>
public static class ImaginationManager
{
    private static string CurrentStoryName => StoryManager.GetCurrentStoryName();
    // 활성화된 상상 이미지를 관리하는 딕셔너리
    private static Dictionary<string, Image> _activeImaginations = new Dictionary<string, Image>();

    // 활성화된 상상 이미지들을 반환
    public static Dictionary<string, Image> ActiveImaginations => _activeImaginations;

    // 상상 이미지를 생성하여 등록하는 메서드
    public static Image CreateImagination(string imaginationID, float scaleFactor)
    {
        // 이미 해당 ID가 등록되어 있다면 바로 반환
        if (_activeImaginations.ContainsKey(imaginationID))
        {
            Debug.LogWarning($"Imagination with ID '{imaginationID}' already exists.");
            return _activeImaginations[imaginationID];
        }

        Transform parent = UIManager.Instance.SystemCanvas.ImaginationLayer.transform;
        // 새로운 GameObject 생성
        GameObject imaginationObject = new GameObject(imaginationID);
        imaginationObject.transform.SetParent(parent, false);

        // Image 컴포넌트 추가 및 초기화
        Image img = imaginationObject.AddComponent<Image>();
        img.rectTransform.sizeDelta = new Vector2(1920, 1080);
        img.rectTransform.anchoredPosition = Vector2.zero;
        img.transform.localScale = Vector2.one * scaleFactor;

        // Imagination에 해당하는 Sprite 로드
        string resourcePath = ResourcePathManager.GetResourcePath(imaginationID, CurrentStoryName, ResourceType.Imagination, false);
        Sprite imaginationSprite = Resources.Load<Sprite>(resourcePath);

        // 스토리 리소스에서 로드 실패 시 공유 리소스에서 재시도
        if (imaginationSprite == null)
        {
            resourcePath = ResourcePathManager.GetResourcePath(imaginationID, CurrentStoryName, ResourceType.Imagination, true);
            imaginationSprite = Resources.Load<Sprite>(resourcePath);
        }

        if (imaginationSprite != null)
        {
            img.sprite = imaginationSprite;
            img.color = new Color(img.color.r, img.color.g, img.color.b, 0f); // 초기 알파값을 0으로 설정
        }
        else
        {
            Debug.LogError($"Failed to load sprite for Imagination ID: {imaginationID} from path '{resourcePath}'");
            Object.Destroy(imaginationObject); // 이미지 로드 실패 시 오브젝트 제거
            return null;
        }

        // 딕셔너리에 등록
        _activeImaginations.Add(imaginationID, img);

        return img;
    }

    // 상상 이미지를 imaginationID로 찾아 페이드 인하는 메서드
    public static void FadeColor(string imaginationID, Color targetColor, float duration, Ease easeType)
    {
        // imaginationID에 해당하는 이미지 찾기
        if (_activeImaginations.TryGetValue(imaginationID, out Image img))
        {
            img.DOColor(targetColor, duration).SetEase(easeType);
        }
        else
        {
            Debug.LogWarning($"Imagination with ID '{imaginationID}' not found in active imaginations.");
        }
    }

    // 상상 이미지를 페이드 아웃 후 제거하는 메서드
    public static void DestroyImagination(string imaginationID, float duration, Ease easeType)
    {
        // imaginationID에 해당하는 이미지 찾기
        if (_activeImaginations.TryGetValue(imaginationID, out Image img))
        {
            // 페이드 아웃 애니메이션 후 제거
            img.DOColor(new Color(img.color.r, img.color.g, img.color.b, 0f), duration).SetEase(easeType).OnComplete(() =>
            {
                _activeImaginations.Remove(imaginationID);
                Object.Destroy(img.gameObject);
            });
        }
        else
        {
            Debug.LogWarning($"Imagination with ID '{imaginationID}' not found for destruction.");
        }
    }

    // 모든 상상 이미지를 페이드 아웃 후 제거하는 메서드
    public static void DestroyAllImaginations(float duration, Ease easeType)
    {
        foreach (var imaginationID in new List<string>(_activeImaginations.Keys))
        {
            DestroyImagination(imaginationID, duration, easeType);
        }
    }
}
