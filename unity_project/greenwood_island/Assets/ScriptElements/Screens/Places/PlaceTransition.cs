using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// PlaceTransitionWithOverlayColor 클래스는 장소 간의 전환 시 오버레이 색상을 사용하는 Element입니다.
/// </summary>
public class PlaceTransition : Element
{
    private string _newPlaceID; // 새로운 장소 ID
    private float _duration; // 전환 지속 시간
    private Ease _easeType; // 이징 타입
    private Color _overlayColor; // 오버레이 색상
    private PlaceEffect _placeEffect; // 전역 변수: 장소 효과

    // 생성자
    public PlaceTransition(string newPlaceID, float duration, Color overlayColor = default, PlaceEffect placeEffect = null, Ease easeType = Ease.OutQuad)
    {
        _newPlaceID = newPlaceID;
        _duration = duration;
        _overlayColor = overlayColor == default ? Color.black : overlayColor; // 기본값은 Color.black
        _easeType = easeType;
        _placeEffect = placeEffect; // placeEffect 할당, 기본값은 null
    }

    public override void ExecuteInstantly()
    {
        _duration = 0; // 즉시 실행
        Execute();
    }

    public override IEnumerator ExecuteRoutine()
    {
        // 현재 활성화된 장소 이미지 가져오기
        Image previousPlace = PlaceManager.Instance.CurrentPlaceImage;

        // 오버레이 생성
        yield return CoroutineUtils.StartCoroutine(new ScreenOverlayFilm(_overlayColor, _duration / 2f, _easeType).ExecuteRoutine());

        // 새로운 장소 생성
        new PlaceEnter(_newPlaceID, 0f).Execute();

        // 이전 장소 제거
        if (previousPlace != null)
        {
            Object.Destroy(previousPlace.gameObject);
        }

        // PlaceEffect가 null이더라도 ParallelElement에서 처리됨
        ParallelElement parallelElement = new ParallelElement(
            new ScreenOverlayFilmClear(_duration / 2f, _easeType),
            _placeEffect // null일 경우 자동 처리됨
        );
        yield return CoroutineUtils.StartCoroutine(parallelElement.ExecuteRoutine());

        // duration / 2f 동안 대기
        yield return new WaitForSeconds(_duration / 2f);
    }
}
