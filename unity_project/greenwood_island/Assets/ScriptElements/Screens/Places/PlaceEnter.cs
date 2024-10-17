using System.Collections;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// PlaceEnter 클래스는 특정 장소의 장면을 화면에 보여주는 역할을 합니다.
/// </summary>
public class PlaceEnter : Element
{
    private string _placeID;       // 장소의 이미지 ID
    private Vector2 _anchoredPos;  // 앵커 위치
    private Vector2 _localScale;   // 스케일
    private Color _targetColor;    // 페이드 인 목표 색상
    private float _duration;       // 페이드 인 지속 시간
    private Ease _easeType;        // 페이드 인 애니메이션 이징 타입

    // 생성자: 장소 ID, 앵커 위치, 스케일, 색상, 지속 시간, 이징 타입을 받아서 초기화
    public PlaceEnter(string placeID, float duration = 1f, Vector2 anchoredPos = default, Vector2 localScale = default, Color targetColor = default, Ease easeType = Ease.OutQuad)
    {
        _placeID = placeID;
        _duration = duration;
        _anchoredPos = anchoredPos == default ? Vector2.zero : anchoredPos;
        _localScale = localScale == default ? Vector2.one : localScale;
        _targetColor = targetColor == default ? Color.white : targetColor;
        _easeType = easeType;
    }

    public override void ExecuteInstantly()
    {
        _duration = 0;
        Execute();
    }

    public string PlaceID { get => _placeID; }

    // 장소를 화면에 보여주는 코루틴
    public override IEnumerator ExecuteRoutine()
    {
        Debug.Log($"PlaceEnter :: 장소 '{_placeID}'을 인스턴스화 시도합니다.");

        // PlaceManager를 통해 장소 이미지 생성
        PlaceManager.Instance.CreatePlace(_placeID);

        // PlaceColor를 이용해 페이드 인 애니메이션 적용
        new PlaceColor(_targetColor, _duration, _easeType).Execute();
        new PlaceMove(_anchoredPos, _duration, _easeType).Execute();
        new PlaceScale(_localScale, _duration, _easeType).Execute();

        // 애니메이션이 끝날 때까지 기다림
        yield return new WaitForSeconds(_duration);
        yield return new WaitForEndOfFrame();
    }
}
