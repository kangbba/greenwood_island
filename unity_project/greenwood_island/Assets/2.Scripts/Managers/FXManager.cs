using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public static class FXManager
{
    // 람다식으로 현재 실행 중인 스토리 이름을 자동으로 가져옴
    private static string CurrentStoryName => StoryManager.GetCurrentStoryName();

    private static Dictionary<string, List<GameObject>> _activeFXs = new Dictionary<string, List<GameObject>>(); // 활성화된 FX들

    // 특정 FX를 스폰하는 메서드
    public static GameObject SpawnFX(string fxID, Vector3 localPos)
    {
        // 현재 스토리 이름과 FX ID를 사용하여 FX 프리팹 로드
        GameObject fxPrefab = LoadFXPrefab(fxID, CurrentStoryName);

        if (fxPrefab == null)
        {
            Debug.LogError($"FX Prefab '{fxID}' could not be loaded from story '{CurrentStoryName}' or shared resources.");
            return null;
        }

        // 로드된 프리팹을 인스턴스화하여 위치 설정
        GameObject fxInstance = Object.Instantiate(fxPrefab, UIManager.Instance.SystemCanvas.FXLayer);
        fxInstance.transform.localPosition = localPos;

        // 활성화된 FX 리스트에 추가
        if (!_activeFXs.ContainsKey(fxID))
        {
            _activeFXs[fxID] = new List<GameObject>();
        }
        _activeFXs[fxID].Add(fxInstance);

        return fxInstance;
    }

    // 특정 FX ID에 대한 활성화된 FX 리스트 반환
    public static List<GameObject> GetActiveFXs(string fxID)
    {
        if (_activeFXs.TryGetValue(fxID, out List<GameObject> activeFXList))
        {
            return activeFXList;
        }
        return new List<GameObject>();
    }

    // 특정 FX 인스턴스를 파괴하는 메서드
    public static void DestroyFX(GameObject fxInstance)
    {
        foreach (var fxList in _activeFXs.Values)
        {
            if (fxList.Contains(fxInstance))
            {
                fxList.Remove(fxInstance);
                Object.Destroy(fxInstance);
                break;
            }
        }
    }

    // CanvasGroup이 없으면 추가한 후 페이드 아웃 적용 및 FX 제거
    public static void FadeAndDestroyFX(GameObject fxInstance, float duration)
    {
        if (fxInstance == null) return;

        CanvasGroup canvasGroup = fxInstance.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = fxInstance.AddComponent<CanvasGroup>();
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        canvasGroup.DOFade(0, duration).OnComplete(() => DestroyFX(fxInstance));
    }

    // 모든 FX를 페이드 아웃하고 제거하는 메서드
    public static void FadeOutAndDestroyAllFX(float duration)
    {
        foreach (var fxList in _activeFXs.Values)
        {
            foreach (var fxInstance in fxList)
            {
                FadeAndDestroyFX(fxInstance, duration); // 개별 FX를 페이드 아웃 후 제거
            }
        }

        // 모든 FX 리스트를 클리어하여 딕셔너리를 초기화
        _activeFXs.Clear();
    }

    // FX 프리팹을 로드하는 메서드 (스토리 자원 우선, 실패 시 공유 자원 로드)
    private static GameObject LoadFXPrefab(string fxID, string storyName)
    {
        // 스토리 경로에서 FX 로드 시도
        string storyFXPath = ResourcePathManager.GetResourcePath(fxID, storyName, ResourceType.FX, false);
        GameObject fxPrefab = Resources.Load<GameObject>(storyFXPath);

        if (fxPrefab != null)
        {
            Debug.Log($"FX Prefab '{fxID}' loaded from Story path: '{storyFXPath}'.");
            return fxPrefab;
        }

        // 스토리 경로에서 로드 실패 시, 공유 경로에서 로드 시도
        string sharedFXPath = ResourcePathManager.GetResourcePath(fxID, storyName, ResourceType.FX, true);
        GameObject sharedPrefab = Resources.Load<GameObject>(sharedFXPath);

        if (sharedPrefab != null)
        {
            Debug.Log($"FX Prefab '{fxID}' loaded from Shared path: '{sharedFXPath}'.");
        }

        return sharedPrefab;
    }
}
