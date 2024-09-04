using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public enum FXType
{
    BloodDrip,
    SmokePuff,
    Sparkle,
    FireExplosion,
    // 필요한 FX 타입들을 추가
}

[System.Serializable]
public class FXPrefabEntry
{
    public FXType fxType;          // FX의 타입 (enum 기반)
    public GameObject fxPrefab;    // FX에 사용할 프리팹
}

public class FXManager : MonoBehaviour
{
    public static FXManager Instance { get; private set; }

    [SerializeField]
    private List<FXPrefabEntry> fxPrefabEntries = new List<FXPrefabEntry>(); // Inspector에서 등록할 프리팹 리스트

    private Dictionary<FXType, GameObject> _fxPrefabs = new Dictionary<FXType, GameObject>();
    private Dictionary<FXType, List<GameObject>> _activeFXs = new Dictionary<FXType, List<GameObject>>(); // 활성화된 FX들

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeFXPrefabs();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeFXPrefabs()
    {
        foreach (var entry in fxPrefabEntries)
        {
            if (entry.fxPrefab != null)
            {
                if (entry.fxPrefab.GetComponent<Animator>() == null)
                {
                    Debug.LogWarning($"FX Prefab '{entry.fxType}' does not contain an Animator component and will not be registered.");
                    continue;
                }

                if (!_fxPrefabs.ContainsKey(entry.fxType))
                {
                    _fxPrefabs.Add(entry.fxType, entry.fxPrefab);
                }
                else
                {
                    Debug.LogWarning($"FX Prefab '{entry.fxType}' is already registered.");
                }
            }
            else
            {
                Debug.LogWarning($"FX Prefab entry with type '{entry.fxType}' is null.");
            }
        }
    }

    public GameObject SpawnFX(FXType fxType, Vector3 localPos)
    {
        if (_fxPrefabs.TryGetValue(fxType, out GameObject fxPrefab))
        {
            GameObject fxInstance = Instantiate(fxPrefab, UIManager.Instance.WorldCanvas.FXLayer);
            fxInstance.transform.localPosition = localPos;

            if (!_activeFXs.ContainsKey(fxType))
            {
                _activeFXs[fxType] = new List<GameObject>();
            }
            _activeFXs[fxType].Add(fxInstance);

            return fxInstance;
        }
        else
        {
            Debug.LogError($"FX Prefab '{fxType}' not found.");
            return null;
        }
    }

    public List<GameObject> GetActiveFXs(FXType fxType)
    {
        if (_activeFXs.TryGetValue(fxType, out List<GameObject> activeFXList))
        {
            return activeFXList;
        }
        return new List<GameObject>();
    }

    public void DestroyFX(GameObject fxInstance)
    {
        foreach (var fxList in _activeFXs.Values)
        {
            if (fxList.Contains(fxInstance))
            {
                fxList.Remove(fxInstance);
                Destroy(fxInstance);
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
}
