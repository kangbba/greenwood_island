using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using static ImageUtils;

public class PlaceTransitionWithSwipe : Element
{
    private string _newPlaceID; // 새로운 장소 ID
    private float _duration; // 전환 지속 시간
    private Ease _easeType; // 이징 타입
    private SwipeMode _swipeMode; // 스와이프 모드

    public PlaceTransitionWithSwipe(string newPlaceID, float duration, SwipeMode swipeMode, Ease easeType = Ease.OutQuad)
    {
        _newPlaceID = newPlaceID;
        _swipeMode = swipeMode;
        _duration = duration;
        _easeType = easeType;
    }

    public override void ExecuteInstantly()
    {
        _duration = 0;
        Execute();
    }

    public override IEnumerator ExecuteRoutine()
    {
        // 새로운 장소 생성
        PlaceManager.Instance.CreatePlace(_newPlaceID);
        Image currentPlaceImg = PlaceManager.Instance.CurrentPlaceImage;
        Image previousPlaceImg = PlaceManager.Instance.PreivousPlaceImage;

        // SwipeImage 메서드를 사용해 전환 처리
        ImageUtils.SwipeImage(currentPlaceImg, previousPlaceImg, _duration, _swipeMode, _easeType, previousDestroy: true);

        // 모든 애니메이션 완료 대기
        yield return new WaitForSeconds(_duration);
    }
}
