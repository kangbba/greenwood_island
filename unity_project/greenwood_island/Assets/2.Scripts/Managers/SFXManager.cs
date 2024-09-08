using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance { get; private set; }

    // 람다식으로 현재 실행 중인 스토리 이름을 자동으로 가져옴
    private string CurrentStoryName => StoryManager.Instance.GetCurrentStoryName();

    private Dictionary<string, List<AudioSource>> _activeSFXs = new Dictionary<string, List<AudioSource>>(); // 활성화된 SFX들
    private Dictionary<AudioSource, Coroutine> _activeLoops = new Dictionary<AudioSource, Coroutine>(); // 활성화된 루프 코루틴들

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 특정 SFX ID의 오디오 클립 단일 재생 메서드
    public AudioSource PlaySFXOnce(string sfxID, float volume)
    {
        // 현재 스토리 이름을 자동으로 가져옴
        AudioClip sfxClip = LoadSFXClip(sfxID, CurrentStoryName);

        if (sfxClip == null)
        {
            Debug.LogError($"SFX Clip '{sfxID}' could not be loaded from story '{CurrentStoryName}' or shared resources.");
            return null;
        }

        // 새로운 GameObject를 생성하여 AudioSource 추가
        GameObject audioObject = new GameObject($"SFX_{sfxID}");
        audioObject.transform.parent = UIManager.Instance.SystemCanvas.SFXLayer;
        audioObject.transform.localPosition = Vector2.zero;

        // AudioSource 컴포넌트를 추가하고 설정
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        audioSource.volume = volume; // 볼륨 설정
        audioSource.clip = sfxClip;
        audioSource.loop = false; // 반복 재생 없음
        audioSource.Play(); // 오디오 재생

        // 생성된 AudioSource를 _activeSFXs에 추가
        if (!_activeSFXs.ContainsKey(sfxID))
        {
            _activeSFXs[sfxID] = new List<AudioSource>();
        }
        _activeSFXs[sfxID].Add(audioSource);

        return audioSource;
    }

    // 특정 시간 간격으로 SFX 반복 재생하는 메서드
    public AudioSource PlaySFXLoop(string sfxID, float volume, float term)
    {
        AudioSource audioSource = PlaySFXOnce(sfxID, volume);
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
            Debug.Log($"SFXManager :: 음원이 반복 재생중입니다. 의도된 동작인지 확인하세요 term : {term}");
            audioSource.Play(); // 오디오 재생
            yield return new WaitForSeconds(audioSource.clip.length + term); // 클립 재생 시간과 간격만큼 대기
        }
        yield break; // AudioSource가 null이면 종료
    }

    // 특정 SFX ID에 대한 활성화된 SFX 리스트 반환
    public List<AudioSource> GetActiveSFXs(string sfxID)
    {
        if (_activeSFXs.TryGetValue(sfxID, out List<AudioSource> activeSFXList))
        {
            return activeSFXList;
        }
        return new List<AudioSource>();
    }

    // 특정 AudioSource를 페이드 아웃하고 제거하는 메서드
    public void FadeOutAndDestroy(AudioSource audioSource, float fadeDuration)
    {
        if (audioSource == null) return;

        // 페이드 아웃과 제거를 DOTween으로 수행
        audioSource.DOFade(0f, fadeDuration).OnComplete(() =>
        {
            RemoveSFX(audioSource);
        });
    }

    // 모든 SFX를 페이드 아웃하고 제거하는 메서드
    public void FadeOutAndDestroyAllSFX(float duration)
    {
        foreach (var sfxList in _activeSFXs.Values)
        {
            foreach (var audioSource in sfxList)
            {
                if (audioSource != null)
                {
                    FadeOutAndDestroy(audioSource, duration); // 개별 오디오 소스를 페이드 아웃 후 제거
                }
            }
        }
        _activeSFXs.Clear();
    }

    // 특정 AudioSource에 대한 SFX 제거
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

    // 스토리 이름과 SFX ID를 사용하여 오디오 클립을 로드하는 메서드
    private AudioClip LoadSFXClip(string sfxID, string storyName)
    {
        string storySFXPath = ResourcePathManager.GetResourcePath(sfxID, storyName, ResourceType.SFX, false);
        AudioClip sfxClip = Resources.Load<AudioClip>(storySFXPath);

        if (sfxClip != null)
        {
            Debug.Log($"SFX Clip '{sfxID}' loaded from Story path: '{storySFXPath}'.");
            return sfxClip;
        }

        string sharedSFXPath = ResourcePathManager.GetResourcePath(sfxID, storyName, ResourceType.SFX, true);
        sfxClip = Resources.Load<AudioClip>(sharedSFXPath);

        if (sfxClip != null)
        {
            Debug.Log($"SFX Clip '{sfxID}' loaded from Shared path: '{sharedSFXPath}'.");
        }

        return sfxClip;
    }
}
