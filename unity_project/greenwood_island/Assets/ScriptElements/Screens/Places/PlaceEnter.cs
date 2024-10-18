using System.Collections;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// PlaceEnter 클래스는 특정 장소의 장면을 화면에 보여주는 역할을 합니다.
/// </summary>
public class PlaceEnter : Element
{
    private string _placeID;       // 장소의 이미지 ID
    private Color _initialColor;   // 페이드 인 초기 색상
    private Vector2 _initialLocalPos;  // 초기 로컬 위치
    private Vector2 _initialLocalScale; // 초기 스케일
    private Ease _easeType;        // 페이드 인 애니메이션 이징 타입

    // 생성자: 장소 ID, 초기 색상, 초기 로컬 위치, 초기 스케일, 이징 타입을 받아서 초기화
    public PlaceEnter(string placeID, Color initialColor = default, Vector2 initialLocalPos = default, Vector2 initialLocalScale = default, Ease easeType = Ease.OutQuad)
    {
        _placeID = placeID;
        _initialColor = initialColor == default ? Color.white : initialColor;
        _initialLocalPos = initialLocalPos == default ? Vector2.zero : initialLocalPos;
        _initialLocalScale = initialLocalScale == default ? Vector2.one : initialLocalScale;
        _easeType = easeType;
    }


    public override void ExecuteInstantly()
    {
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
        new PlaceColor(_initialColor, 0f, _easeType).Execute();
        new PlaceMove(_initialLocalPos, 0f, _easeType).Execute();
        new PlaceScale(_initialLocalScale, 0f, _easeType).Execute();

        // 애니메이션이 끝날 때까지 기다림
        yield break;
    }
}
