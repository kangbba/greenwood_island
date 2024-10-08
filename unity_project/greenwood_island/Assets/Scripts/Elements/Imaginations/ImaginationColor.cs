using System.Collections;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// ImaginationColor 클래스는 현재 활성화된 상상의 이미지를 화면에서 색상 변경하는 역할을 합니다.
/// </summary>
public class ImaginationColor : Element
{
    private Color _targetColor;    // 목표 색상
    private float _duration;       // 색상 전환 시간
    private Ease _easeType;        // 색상 전환 애니메이션 이징 타입

    // 생성자: 목표 색상, 지속 시간, 이징 타입을 받아서 초기화
    public ImaginationColor(Color targetColor, float duration = 1f, Ease easeType = Ease.OutQuad)
    {
        _targetColor = targetColor;
        _duration = duration;
        _easeType = easeType;
    }

    // 상상의 색상을 변경하는 코루틴
    public override IEnumerator ExecuteRoutine()
    {
        string currentImaginationID = ImaginationManager.Instance.CurrentImaginationID;

        if (string.IsNullOrEmpty(currentImaginationID))
        {
            Debug.LogError("No active imagination to change color.");
            yield break;
        }

        Debug.Log($"ImaginationColor :: 상상의 장면 '{currentImaginationID}'의 색상을 변경 시도합니다.");

        // ImaginationManager를 통해 색상 변경 애니메이션 적용
        ImaginationManager.Instance.FadeColor(_targetColor, _duration, _easeType);

        // 애니메이션이 끝날 때까지 기다림
        yield return new WaitForSeconds(_duration);
    }
}
