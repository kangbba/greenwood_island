using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SFXType
{
    CreepyWhisper,
    Footsteps,
    DoorCreak,
    HeavyBreathing,
    // 필요한 SFX 타입들을 추가
}

[System.Serializable]
public class SFXClipEntry
{
    public SFXType sfxType;        // SFX의 타입 (enum 기반)
    public AudioClip sfxClip;      // SFX에 사용할 오디오 클립
}

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance { get; private set; }

    [SerializeField]
    private List<SFXClipEntry> sfxClipEntries = new List<SFXClipEntry>(); // Inspector에서 등록할 오디오 클립 리스트

    private Dictionary<SFXType, AudioClip> _sfxClips = new Dictionary<SFXType, AudioClip>();
    private Dictionary<SFXType, List<AudioSource>> _activeSFXs = new Dictionary<SFXType, List<AudioSource>>(); // 활성화된 SFX들
    private Dictionary<AudioSource, Coroutine> _activeLoops = new Dictionary<AudioSource, Coroutine>(); // 활성화된 루프 코루틴들

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeSFXClips();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 오디오 클립 초기화
    private void InitializeSFXClips()
    {
        foreach (var entry in sfxClipEntries)
        {
            if (entry.sfxClip != null)
            {
                // 중복 등록 방지
                if (!_sfxClips.ContainsKey(entry.sfxType))
                {
                    _sfxClips.Add(entry.sfxType, entry.sfxClip);
                }
                else
                {
                    Debug.LogWarning($"SFX Clip '{entry.sfxType}' is already registered.");
                }
            }
            else
            {
                Debug.LogWarning($"SFX Clip entry with type '{entry.sfxType}' is null.");
            }
        }
    }

    // 특정 SFXType의 오디오 클립 단일 재생 메서드
    public AudioSource PlaySFXOnce(SFXType sfxType, Vector3 position)
    {
        if (_sfxClips.TryGetValue(sfxType, out AudioClip sfxClip))
        {
            // 새로운 GameObject를 생성하여 AudioSource 추가
            GameObject audioObject = new GameObject($"SFX_{sfxType}");
            audioObject.transform.position = position;
            audioObject.transform.parent = UIManager.Instance.WorldCanvas.SFXLayer;

            // AudioSource 컴포넌트를 추가하고 설정
            AudioSource audioSource = audioObject.AddComponent<AudioSource>();
            audioSource.clip = sfxClip;
            audioSource.loop = false; // 반복 재생 없음
            audioSource.Play(); // 오디오 재생

            // 생성된 AudioSource를 _activeSFXs에 추가
            if (!_activeSFXs.ContainsKey(sfxType))
            {
                _activeSFXs[sfxType] = new List<AudioSource>();
            }
            _activeSFXs[sfxType].Add(audioSource);

            return audioSource;
        }
        else
        {
            Debug.LogError($"SFX Clip '{sfxType}' not found.");
            return null;
        }
    }

    // 특정 시간 간격으로 SFX 반복 재생하는 메서드
    public AudioSource PlaySFXLoop(SFXType sfxType, Vector3 position, float term = 1f)
    {
        AudioSource audioSource = PlaySFXOnce(sfxType, position);
        if (audioSource != null)
        {
            audioSource.loop = false; // 루프 설정 없음, 직접 제어
            // 기존에 실행 중인 코루틴이 있다면 중복 방지
            if (_activeLoops.ContainsKey(audioSource) && _activeLoops[audioSource] != null)
            {
                StopCoroutine(_activeLoops[audioSource]);
            }
            // 새로운 루프 코루틴 실행
            Coroutine loopCoroutine = StartCoroutine(LoopWithTerm(audioSource, term));
            _activeLoops[audioSource] = loopCoroutine;
        }
        return audioSource;
    }

    // 특정 시간 간격으로 반복 재생하는 코루틴
    private IEnumerator LoopWithTerm(AudioSource audioSource, float term)
    {
        while (audioSource != null)
        {
            if (audioSource.clip == null) yield break; // 클립이 없으면 종료
            audioSource.Play(); // 오디오 재생
            yield return new WaitForSeconds(audioSource.clip.length + term); // 클립 재생 시간과 간격만큼 대기
        }
        yield break; // AudioSource가 null이면 종료
    }

    // 특정 SFXType에 대한 활성화된 SFX 리스트 반환
    public List<AudioSource> GetActiveSFXs(SFXType sfxType)
    {
        if (_activeSFXs.TryGetValue(sfxType, out List<AudioSource> activeSFXList))
        {
            return activeSFXList;
        }
        return new List<AudioSource>();
    }

    // 특정 SFXType에 대한 SFX 제거
    public void RemoveSFX(AudioSource audioSource)
    {
        foreach (var sfxList in _activeSFXs.Values)
        {
            if (sfxList.Contains(audioSource))
            {
                if (_activeLoops.ContainsKey(audioSource) && _activeLoops[audioSource] != null)
                {
                    StopCoroutine(_activeLoops[audioSource]); // 코루틴 중지
                    _activeLoops.Remove(audioSource);
                }

                sfxList.Remove(audioSource);
                Destroy(audioSource.gameObject); // AudioSource를 가진 GameObject 파괴
                break;
            }
        }
    }

    // 모든 SFX 제거
    public void RemoveAllSFX(SFXType sfxType)
    {
        if (_activeSFXs.TryGetValue(sfxType, out List<AudioSource> activeSFXList))
        {
            foreach (var audioSource in activeSFXList)
            {
                if (_activeLoops.ContainsKey(audioSource) && _activeLoops[audioSource] != null)
                {
                    StopCoroutine(_activeLoops[audioSource]); // 코루틴 중지
                    _activeLoops.Remove(audioSource);
                }

                Destroy(audioSource.gameObject); // AudioSource를 가진 GameObject 파괴
            }
            activeSFXList.Clear();
        }
    }
}