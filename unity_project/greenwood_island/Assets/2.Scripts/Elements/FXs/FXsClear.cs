using System.Collections;
using UnityEngine;

[System.Serializable]
public class FXsClear : Element
{
    private float _duration;

    public FXsClear(float duration = 1f)
    {
        _duration = duration;
    }

    public override IEnumerator ExecuteRoutine()
    {
        // FXManager를 통해 모든 활성화된 FX를 페이드 아웃 후 제거
        FXManager.Instance.FadeOutAndDestroyAllFX(_duration);

        // 페이드 아웃 완료까지 대기
        yield return new WaitForSeconds(_duration);
    }
}
