using UnityEngine;
using DG.Tweening;

public class SingletonManager<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    public static void Initialize()
    {
        if (Instance == null)
        {
            // 매니저 오브젝트가 파괴되었을 경우, 새로운 오브젝트를 생성하여 씬에 배치
            GameObject managerObject = new GameObject(typeof(T).Name);
            Instance = managerObject.AddComponent<T>();
        }

        // 타입 검증 - T가 자신의 타입인지 확인
        if (Instance.GetType() != typeof(T))
        {
            Debug.LogError($"잘못된 타입이 사용되었습니다: {Instance.GetType().Name}는 {typeof(T).Name}이어야 합니다.");
            throw new System.Exception($"SingletonManager<T>의 제네릭 타입이 올바르지 않습니다. {typeof(T).Name}이어야 하지만 {Instance.GetType().Name}이(가) 할당되었습니다.");
        }
    }

    // 파괴될 때 인스턴스를 null로 설정하여 재생성 가능하도록
    protected virtual void OnDestroy()
    {
        if (Instance == this)
        {
            // DOTween 애니메이션을 모두 종료
            DOTween.KillAll();

            // 이 MonoBehaviour에서 실행 중인 모든 코루틴 중지
            StopAllCoroutines();

            Instance = null;
        }
    }
}
