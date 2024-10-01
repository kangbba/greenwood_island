using System.Collections;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// ImaginationEnter 클래스는 특정 상상의 장면을 화면에 보여주는 역할을 합니다.
/// </summary>
public class ImaginationEnter : Element
{
    private string _imaginationID; // 상상의 이미지 ID
    private float _duration;       // 페이드 인 지속 시간
    private float _scaleFactor;
    private Ease _easeType;        // 페이드 인 애니메이션 이징 타입
    private Color _targetColor;    // 페이드 인 목표 색상

    // 생성자: 상상 ID, 스케일 팩터, 색상, 지속 시간, 이징 타입을 받아서 초기화
    public ImaginationEnter(string imaginationID, float scaleFactor, Color targetColor, float duration = 1f, Ease easeType = Ease.OutQuad)
    {
        _imaginationID = imaginationID;
        _scaleFactor = scaleFactor;
        _targetColor = targetColor;
        _duration = duration;
        _easeType = easeType;
    }

    public string ImaginationID { get => _imaginationID; }

    // 상상을 화면에 보여주는 코루틴
    public override IEnumerator ExecuteRoutine()
    {
        Debug.Log($"ImaginationEnter :: 상상의 장면 '{_imaginationID}'을 인스턴스화 시도합니다.");

        // ImaginationManager를 통해 상상 이미지 생성
        ImaginationManager.CreateImagination(_imaginationID, _scaleFactor);

        // ImaginationManager를 통해 페이드 인 애니메이션 적용
        ImaginationManager.FadeColor(_imaginationID, _targetColor, _duration, _easeType);

        // 애니메이션이 끝날 때까지 기다림
        yield return new WaitForSeconds(_duration);
    }
}
