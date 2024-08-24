using UnityEngine;
using System.Collections;

public class CoroutineRunner : MonoBehaviour
{
    private static CoroutineRunner _instance;

    public static CoroutineRunner Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("CoroutineRunner");
                _instance = go.AddComponent<CoroutineRunner>();

                // 이 오브젝트는 씬 전환 시 파괴되지 않도록 설정
                DontDestroyOnLoad(go);
            }

            return _instance;
        }
    }

    // Static 메서드를 통해 코루틴 실행
    public static void RunCoroutine(IEnumerator coroutine)
    {
        Instance.StartCoroutine(coroutine);
    }

    // Static 메서드를 통해 코루틴 정지
    public static void StopRunningCoroutine(IEnumerator coroutine)
    {
        Instance.StopCoroutine(coroutine);
    }

    // 인스턴스에서 코루틴을 정지할 때는 `this.StopCoroutine`을 사용
    public void StopCoroutineInstance(IEnumerator coroutine)
    {
        this.StopCoroutine(coroutine);
    }

    public IEnumerator WaitForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    // Future 클래스 정의
    public class Future<T>
    {
        public T Result { get; private set; }
        public IEnumerator Coroutine { get; private set; }

        private IEnumerator _targetCoroutine;

        public Future(MonoBehaviour owner, IEnumerator target)
        {
            _targetCoroutine = target;
            Coroutine = Run();
            owner.StartCoroutine(Coroutine);
        }

        private IEnumerator Run()
        {
            while (_targetCoroutine.MoveNext())
            {
                if (_targetCoroutine.Current is T result)
                {
                    Result = result;
                }
                else
                {
                    Result = default(T);
                }
                yield return _targetCoroutine.Current;
            }
        }
    }
}
