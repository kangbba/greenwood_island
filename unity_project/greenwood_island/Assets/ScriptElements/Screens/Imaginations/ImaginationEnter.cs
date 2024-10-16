using System.Collections;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// ImaginationEnter 클래스는 특정 상상의 장면을 화면에 보여주는 역할을 합니다.
/// </summary>
public class ImaginationEnter : Element
{
    private string _imaginationID; // 상상의 이미지 ID
    private Vector2 _localScale;
    private Vector2 _anchoredPos;
    private float _duration;       // 페이드 인 지속 시간
    private Color _color;    // 페이드 인 목표 색상
    private Ease _easeType;        // 페이드 인 애니메이션 이징 타입

    // 생성자: 상상 ID, 스케일 팩터, 색상, 지속 시간, 이징 타입을 받아서 초기화
    public ImaginationEnter(string imaginationID, float duration = 1f, Vector2 localScale = default, Vector2 anchoredPos = default, Color color = default, Ease easeType = Ease.OutQuad)
    {
        _imaginationID = imaginationID;
        _duration = duration;
        _localScale = localScale == default ? Vector2.one : localScale;
        _anchoredPos = anchoredPos == default ? Vector2.zero : anchoredPos;
        _color = color == default ? Color.white : color;
        _easeType = easeType;
    }

    public override void ExecuteInstantly()
    {
        _duration = 0;
        Execute();
    }

    public string ImaginationID { get => _imaginationID; }

    // 상상을 화면에 보여주는 코루틴
    public override IEnumerator ExecuteRoutine()
    {
        Debug.Log($"ImaginationEnter :: 상상의 장면 '{_imaginationID}'을 인스턴스화 시도합니다.");

        // ImaginationManager를 통해 상상 이미지 생성
        ImaginationManager.Instance.CreateImagination(_imaginationID);

        // ImaginationColor를 이용해 페이드 인 애니메이션 적용
        new ImaginationColor(_color, _duration, _easeType).Execute();

        // 이미지의 위치와 크기 조정
        new ImaginationMove(_anchoredPos, _duration, _easeType).Execute();
        new ImaginationScale(_localScale, _duration, _easeType).Execute();

        // 애니메이션이 끝날 때까지 기다림
        yield return new WaitForSeconds(_duration);
    }
}
