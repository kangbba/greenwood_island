using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CoroutineUtils
{
    private static GameObject _coroutineHost;
    private static MonoBehaviour _coroutineHandler;

    // Static 생성자: 클래스가 처음 호출될 때 한 번 실행되어 Init 호출
    static CoroutineUtils()
    {
        Init();  // 클래스가 처음 사용될 때 Init 메서드 호출
        SceneManager.sceneLoaded += OnSceneLoaded; // 씬 로드 이벤트 등록
    }

    // CoroutineHost를 생성하는 Init 메서드
    public static void Init()
    {
        // 이미 존재하지 않으면 생성
        if (_coroutineHost == null)
        {
            _coroutineHost = new GameObject("[CoroutineUtilsHost]");
            _coroutineHandler = _coroutineHost.AddComponent<CoroutineHandler>();
            Object.DontDestroyOnLoad(_coroutineHost); // 씬 전환 시 유지 (원하는 경우 제거 가능)
        }
    }

    // 씬이 로드될 때 호출되는 메서드
    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬이 로드될 때 기존 코루틴을 정지하고 호스트를 재생성
        StopAllCoroutines(); // 모든 코루틴 정지
        if (_coroutineHost != null)
        {
            Object.Destroy(_coroutineHost); // 기존 오브젝트 파괴
        }
        _coroutineHost = null; // 호스트 초기화
        Init(); // 새로 Init 호출하여 호스트 재생성
    }

    // 코루틴 시작
    public static Coroutine StartCoroutine(IEnumerator coroutine)
    {
        Init(); // 오브젝트가 없으면 생성
        return _coroutineHandler.StartCoroutine(coroutine);
    }

    // 코루틴 정지
    public static void StopCoroutine(Coroutine coroutine)
    {
        if (coroutine != null && _coroutineHandler != null)
        {
            _coroutineHandler.StopCoroutine(coroutine);
        }
    }

    // 모든 코루틴 정지
    public static void StopAllCoroutines()
    {
        if (_coroutineHandler != null)
        {
            _coroutineHandler.StopAllCoroutines();
        }
    }

    // WaitForSeconds를 사용하여 일정 시간 대기하는 코루틴
    public static IEnumerator WaitForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    // WaitForSecondsRealtime를 사용하여 실제 시간 기준 대기하는 코루틴
    public static IEnumerator WaitForSecondsRealtime(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
    }

    // 특정 조건이 충족될 때까지 대기하는 코루틴
    public static IEnumerator WaitUntil(System.Func<bool> condition)
    {
        yield return new WaitUntil(condition);
    }

    // 특정 조건이 거짓이 될 때까지 대기하는 코루틴
    public static IEnumerator WaitWhile(System.Func<bool> condition)
    {
        yield return new WaitWhile(condition);
    }

    // 다음 프레임까지 대기하는 코루틴
    public static IEnumerator WaitForEndOfFrame()
    {
        yield return new WaitForEndOfFrame();
    }

    // 특정 FixedUpdate가 호출될 때까지 대기하는 코루틴
    public static IEnumerator WaitForFixedUpdate()
    {
        yield return new WaitForFixedUpdate();
    }

    // MonoBehaviour를 상속한 임시 핸들러 클래스
    private class CoroutineHandler : MonoBehaviour
    {
        // 씬 언로드 시 모든 코루틴을 중지하는 메서드
        public void InitStopOnDestroy()
        {
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        private void OnSceneUnloaded(Scene scene)
        {
            StopAllCoroutines(); // 씬이 언로드될 때 코루틴 정지
        }
        
        private void OnDestroy()
        {
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }
    }
}
