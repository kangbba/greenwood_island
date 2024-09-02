using System.Collections.Generic;
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

    // 프리팹 초기화 및 Animator 검증
    private void InitializeFXPrefabs()
    {
        foreach (var entry in fxPrefabEntries)
        {
            if (entry.fxPrefab != null)
            {
                // Animator가 포함되어 있는지 검사
                if (entry.fxPrefab.GetComponent<Animator>() == null)
                {
                    Debug.LogWarning($"FX Prefab '{entry.fxType}' does not contain an Animator component and will not be registered.");
                    continue; // Animator가 없으면 등록하지 않음
                }

                // 중복 등록 방지
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
    // 특정 FX 프리팹 생성 메서드 (localPosition으로 생성하도록 설정)
    public GameObject SpawnFX(FXType fxType, Vector3 localPos)
    {
        if (_fxPrefabs.TryGetValue(fxType, out GameObject fxPrefab))
        {
            GameObject fxInstance = Instantiate(fxPrefab, UIManager.Instance.WorldCanvas.FXLayer);
            fxInstance.transform.localPosition = localPos; // localPosition으로 설정

            // 생성된 FX를 _activeFXs에 추가
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

    // 특정 FXType에 대한 활성화된 FX 리스트 반환
    public List<GameObject> GetActiveFXs(FXType fxType)
    {
        if (_activeFXs.TryGetValue(fxType, out List<GameObject> activeFXList))
        {
            return activeFXList;
        }
        return new List<GameObject>();
    }

    // 특정 FXType에 대한 FX 제거
    public void RemoveFX(GameObject fxInstance)
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

    // 모든 FX 제거 (선택적)
    public void RemoveAllFX(FXType fxType)
    {
        if (_activeFXs.TryGetValue(fxType, out List<GameObject> activeFXList))
        {
            foreach (var fxInstance in activeFXList)
            {
                Destroy(fxInstance);
            }
            activeFXList.Clear();
        }
    }
}
