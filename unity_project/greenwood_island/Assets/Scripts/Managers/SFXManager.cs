using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SFXManager : SingletonManager<SFXManager>
{
    // 람다식으로 현재 실행 중인 스토리 이름을 자동으로 가져옴

    private Dictionary<string, List<AudioSource>> _activeBGMs = new Dictionary<string, List<AudioSource>>(); // 활성화된 SFX들
    private Dictionary<string, List<AudioSource>> _activeSFXs = new Dictionary<string, List<AudioSource>>(); // 활성화된 SFX들
    private Dictionary<AudioSource, Coroutine> _activeLoops = new Dictionary<AudioSource, Coroutine>(); // 활성화된 루프 코루틴들

    // 특정 SFX ID의 오디오 클립 단일 재생 코루틴 메서드
    public void PlaySFXOnce(string sfxID, float volume)
    {
        CoroutineUtils.StartCoroutine(PlaySFXOnceCoroutine(sfxID, volume));
    }

    public void PlayBGM(string bgmID, float volume){
        
        AudioClip sfxClip = LoadSFXClip(bgmID);
        if (sfxClip == null)
        {
            Debug.LogError($"SFX Clip '{bgmID}' could not be loaded from storyme or shared resources.");
            return;
        }

        // 새로운 GameObject를 생성하여 AudioSource 추가
        GameObject audioObject = new GameObject($"SFX_{bgmID}");
        audioObject.transform.parent = UIManager.SystemCanvas.SFXLayer;
        audioObject.transform.localPosition = Vector2.zero;

        // AudioSource 컴포넌트를 추가하고 설정
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        audioSource.volume = volume; // 볼륨 설정
        audioSource.clip = sfxClip;
        audioSource.loop = true; // 반복 재생 없음
        audioSource.Play(); // 오디오 재생

        if (!_activeBGMs.ContainsKey(bgmID))
        {
            _activeBGMs[bgmID] = new List<AudioSource>();
        }
        _activeBGMs[bgmID].Add(audioSource);
    }

    // 특정 SFX ID의 오디오 클립 단일 재생 코루틴 메서드
    private IEnumerator PlaySFXOnceCoroutine(string sfxID, float volume)
    {
        AudioClip sfxClip = LoadSFXClip(sfxID);
        if (sfxClip == null)
        {
            Debug.LogError($"SFX Clip '{sfxID}' could not be loaded from storyme or shared resources.");
            yield break;
        }

        // 새로운 GameObject를 생성하여 AudioSource 추가
        GameObject audioObject = new GameObject($"SFX_{sfxID}");
        audioObject.transform.parent = UIManager.SystemCanvas.SFXLayer;
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

        // 오디오 클립이 재생되는 동안 대기
        yield return CoroutineUtils.WaitForSeconds(sfxClip.length);

        // 재생이 끝난 후 AudioSource를 제거
        _activeSFXs[sfxID].Remove(audioSource);
        Destroy(audioObject); // AudioSource가 포함된 GameObject를 파괴
    }

    // 특정 시간 간격으로 SFX 반복 재생하는 메서드
    public AudioSource PlaySFXLoop(string sfxID, float volume, float term)
    {
        AudioClip sfxClip = LoadSFXClip(sfxID);

        if (sfxClip == null)
        {
            Debug.LogError($"SFX Clip '{sfxID}' could not be loaded from storyme or shared resources.");
            return null;
        }

        // 새로운 GameObject를 생성하여 AudioSource 추가
        GameObject audioObject = new GameObject($"SFX_{sfxID}");
        audioObject.transform.parent = UIManager.SystemCanvas.SFXLayer;
        audioObject.transform.localPosition = Vector2.zero;

        // AudioSource 컴포넌트를 추가하고 설정
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        audioSource.clip = sfxClip;
        audioSource.volume = volume; // 볼륨 설정
        audioSource.loop = false; // 직접 제어하기 때문에 루프 설정 없음
        audioSource.Play(); // 오디오 재생

        // 생성된 AudioSource를 _activeSFXs에 추가
        if (!_activeSFXs.ContainsKey(sfxID))
        {
            _activeSFXs[sfxID] = new List<AudioSource>();
        }
        _activeSFXs[sfxID].Add(audioSource);

        // 기존에 실행 중인 코루틴이 있다면 중복 방지
        if (_activeLoops.ContainsKey(audioSource) && _activeLoops[audioSource] != null)
        {
            CoroutineUtils.StopCoroutine(_activeLoops[audioSource]);
        }

        // 새로운 루프 코루틴 실행
        Coroutine loopCoroutine = CoroutineUtils.StartCoroutine(LoopWithTerm(audioSource, term));
        _activeLoops[audioSource] = loopCoroutine;

        return audioSource;
    }

    // 특정 시간 간격으로 반복 재생하는 코루틴
    private IEnumerator LoopWithTerm(AudioSource audioSource, float term)
    {
        int playCount = 0; // 재생 횟수를 카운트하는 변수

        while (audioSource != null)
        {
            if (audioSource.clip == null) yield break; // 클립이 없으면 종료
            
            // 재생 횟수를 10으로 나눈 나머지가 0일 때만 로그를 출력
            if (playCount % 10 == 0)
            {
                Debug.Log($"SFXManager :: 음원이 반복 재생중입니다. AudioSource: {audioSource.name}, Term: {term}. 의도된 동작인지 확인하세요.");
            }
            
            audioSource.Play(); // 오디오 재생
            yield return CoroutineUtils.WaitForSeconds(audioSource.clip.length + term); // 클립 재생 시간과 간격만큼 대기
            
            playCount++; // 재생 횟수 증가
        }
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
            GameObject.Destroy(audioSource.gameObject);
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

    // 모든 SFX를 페이드 아웃하고 제거하는 메서드
    public void FadeOutAndDestroyAllBGM(float duration)
    {
        foreach (var bgmList in _activeBGMs.Values)
        {
            foreach (var audioSource in bgmList)
            {
                if (audioSource != null)
                {
                    FadeOutAndDestroy(audioSource, duration); // 개별 오디오 소스를 페이드 아웃 후 제거
                }
            }
        }
        _activeBGMs.Clear();
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
                    CoroutineUtils.StopCoroutine(_activeLoops[audioSource]); // 코루틴 중지
                    _activeLoops.Remove(audioSource);
                }

                sfxList.Remove(audioSource);
                Object.Destroy(audioSource.gameObject); // AudioSource를 가진 GameObject 파괴
                break;
            }
        }
    }

    // 스토리 이름과 SFX ID를 사용하여 오디오 클립을 로드하는 메서드
    private AudioClip LoadSFXClip(string sfxID)
    {
        string storySFXPath = ResourcePathManager.GetCurrentStoryResourcePath(sfxID, ResourceType.SFX);
        AudioClip sfxClip = Resources.Load<AudioClip>(storySFXPath);

        if (sfxClip != null)
        {
            Debug.Log($"SFX Clip '{sfxID}' loaded from Story path: '{storySFXPath}'.");
            return sfxClip;
        }

        string sharedSFXPath = ResourcePathManager.GetSharedResourcePath(sfxID, ResourceType.SFX);
        sfxClip = Resources.Load<AudioClip>(sharedSFXPath);

        if (sfxClip != null)
        {
            Debug.Log($"SFX Clip '{sfxID}' loaded from Shared path: '{sharedSFXPath}'.");
        }

        return sfxClip;
    }
}
