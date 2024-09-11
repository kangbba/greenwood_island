using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start(){
        StartCoroutine(DelayStart());
    }

    // 스토리 시작을 지연시키는 코루틴
    private static IEnumerator DelayStart()
    {
        yield return new WaitForEndOfFrame();
        StoryManager.Init();
    }
}
