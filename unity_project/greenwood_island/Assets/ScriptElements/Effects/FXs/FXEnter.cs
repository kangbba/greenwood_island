using System.Collections;
using UnityEngine;

public class FXEnter : Element
{
    private string _fxID; // FX의 ID
    private Vector3 _localPosition; // 로컬 포지션을 직접 사용

    // _localPosition의 기본값을 Vector3.zero로 설정
    public FXEnter(string fxID, Vector3 localPosition = default)
    {
        _fxID = fxID;
        _localPosition = localPosition == default ? Vector3.zero : localPosition; // 기본값을 Vector3.zero로 설정
    }

    public override void ExecuteInstantly()
    {
        Execute();
    }

    public override IEnumerator ExecuteRoutine()
    {
        // FX 생성 시 지정된 부모로 생성 및 localPosition 적용
        GameObject fxInstance = FXManager.Instance.SpawnFX(_fxID, Vector3.zero);

        if (fxInstance != null)
        {
            fxInstance.transform.localPosition = _localPosition; // 로컬 포지션 설정
        }

        yield return null; // 즉시 실행 후 반환
    }
}
