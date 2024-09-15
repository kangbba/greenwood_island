using UnityEngine;
using System.Collections;

public static class CoroutineUtils
{
    private static GameObject _coroutineHost;
    private static MonoBehaviour _coroutineHandler;

    // Coroutine을 실행할 임시 오브젝트와 컴포넌트 초기화
    static CoroutineUtils()
    {
        _coroutineHost = new GameObject("CoroutineUtilsHost");
        _coroutineHandler = _coroutineHost.AddComponent<CoroutineHandler>();
        Object.DontDestroyOnLoad(_coroutineHost);
    }

    // 코루틴 시작
    public static Coroutine StartCoroutine(IEnumerator coroutine)
    {
        return _coroutineHandler.StartCoroutine(coroutine);
    }

    // 코루틴 정지
    public static void StopCoroutine(Coroutine coroutine)
    {
        if (coroutine != null)
        {
            _coroutineHandler.StopCoroutine(coroutine);
        }
    }

    // 모든 코루틴 정지
    public static void StopAllCoroutines()
    {
        _coroutineHandler.StopAllCoroutines();
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
    private class CoroutineHandler : MonoBehaviour { }
}
