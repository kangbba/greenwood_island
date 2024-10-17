using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FXManager : SingletonManager<FXManager>
{
    private Dictionary<string, GameObject> _activeFXs = new Dictionary<string, GameObject>(); // 활성화된 FX들

    // 특정 FX를 스폰하는 메서드
    public GameObject SpawnFX(string fxID, Vector3 localPos)
    {
        // 현재 스토리 이름과 FX ID를 사용하여 FX 프리팹 로드
        GameObject fxPrefab = LoadFXPrefab(fxID);

        if (fxPrefab == null)
        {
            Debug.LogError($"FX Prefab '{fxID}' could not be loaded from story or shared resources.");
            return null;
        }

        // 이미 활성화된 FX가 있을 경우 제거 후 새로 생성
        if (_activeFXs.ContainsKey(fxID))
        {
            DestroyFX(_activeFXs[fxID]);
        }

        // 로드된 프리팹을 인스턴스화하여 위치 설정
        GameObject fxInstance = Object.Instantiate(fxPrefab, UIManager.SystemCanvas.FXLayer);
        fxInstance.transform.localPosition = localPos;

        // 활성화된 FX에 추가
        _activeFXs[fxID] = fxInstance;

        return fxInstance;
    }

    // 특정 FX ID에 대한 활성화된 FX 반환
    public GameObject GetActiveFX(string fxID)
    {
        if (_activeFXs.TryGetValue(fxID, out GameObject activeFX))
        {
            return activeFX;
        }
        return null;
    }

    // 특정 FX 인스턴스를 파괴하는 메서드
    public void DestroyFX(GameObject fxInstance)
    {
        if (fxInstance != null)
        {
            Object.Destroy(fxInstance);
        }

        // FX 리스트에서 제거
        foreach (var fxID in _activeFXs.Keys)
        {
            if (_activeFXs[fxID] == fxInstance)
            {
                _activeFXs.Remove(fxID);
                break;
            }
        }
    }

    // CanvasGroup이 없으면 추가한 후 페이드 아웃 적용 및 FX 제거
    public void FadeAndDestroyFX(GameObject fxInstance, float duration)
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
    public void FadeOutAndDestroyAllFX(float duration)
    {
        foreach (var fxInstance in _activeFXs.Values)
        {
            if (fxInstance != null)
            {
                FadeAndDestroyFX(fxInstance, duration); // 개별 FX를 페이드 아웃 후 제거
            }
        }

        // 모든 FX 리스트를 클리어하여 딕셔너리를 초기화
        _activeFXs.Clear();
    }

    // FX 프리팹을 로드하는 메서드 (스토리 자원 우선, 실패 시 공유 자원 로드)
    private GameObject LoadFXPrefab(string fxID)
    {
        // 스토리 경로에서 FX 로드 시도
        string storyFXPath = ResourcePathManager.GetCurrentStoryResourcePath(fxID, ResourceType.FX);
        GameObject fxPrefab = Resources.Load<GameObject>(storyFXPath);

        if (fxPrefab != null)
        {
            Debug.Log($"FX Prefab '{fxID}' loaded from Story path: '{storyFXPath}'.");
            return fxPrefab;
        }

        // 스토리 경로에서 로드 실패 시, 공유 경로에서 로드 시도
        string sharedFXPath = ResourcePathManager.GetSharedResourcePath(fxID, ResourceType.FX);
        GameObject sharedPrefab = Resources.Load<GameObject>(sharedFXPath);

        if (sharedPrefab != null)
        {
            Debug.Log($"FX Prefab '{fxID}' loaded from Shared path: '{sharedFXPath}'.");
        }

        return sharedPrefab;
    }
}
