using System.Collections;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// ImaginationScale 클래스는 현재 활성화된 상상의 이미지를 화면에서 크기를 조정하는 역할을 합니다.
/// </summary>
public class ImaginationScale : Element
{
    private Vector3 _targetScale;  // 목표 크기
    private float _duration;       // 애니메이션 지속 시간
    private Ease _easeType;        // 애니메이션 이징 타입

    // 생성자: 목표 크기, 지속 시간, 이징 타입을 받아서 초기화
    public ImaginationScale(Vector3 targetScale, float duration = 1f, Ease easeType = Ease.OutQuad)
    {
        _targetScale = targetScale;
        _duration = duration;
        _easeType = easeType;
    }

    // 상상을 크기 조정하는 코루틴
    public override IEnumerator ExecuteRoutine()
    {
        string currentImaginationID = ImaginationManager.Instance.CurrentImaginationID;

        if (string.IsNullOrEmpty(currentImaginationID))
        {
            Debug.LogError("No active imagination to scale.");
            yield break;
        }

        Debug.Log($"ImaginationScale :: 상상의 장면 '{currentImaginationID}'의 크기를 {_targetScale}로 조정 시도합니다.");

        // ImaginationManager를 통해 크기 조정 애니메이션 적용
        ImaginationManager.Instance.Scale(_targetScale, _duration, _easeType);

        // 애니메이션이 끝날 때까지 기다림
        yield return new WaitForSeconds(_duration);
    }
}
