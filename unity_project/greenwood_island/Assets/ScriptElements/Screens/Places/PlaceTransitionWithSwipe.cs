using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// PlaceTransitionWithSwipe 클래스는 장소 간의 전환을 관리하는 Element입니다.
/// </summary>
public enum SwipeMode
{
    OnlyFade,
    SwipeUp,
    SwipeDown,
    SwipeLeft,
    SwipeRight
}

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
        _duration = 0; // 즉시 실행
        Execute();
    }

    public override IEnumerator ExecuteRoutine()
    {
        // 현재 활성화된 장소 이미지 가져오기
        Image previousPlace = PlaceManager.Instance.CurrentPlaceImage;

        // 새로운 장소 생성
        PlaceManager.Instance.CreatePlace(_newPlaceID);
        Image currentPlaceImg = PlaceManager.Instance.CurrentPlaceImage;

        // 새로운 장소의 위치를 지정된 방향에서 시작
        Vector2 startingPosition = Vector2.zero;
        switch (_swipeMode)
        {
            case SwipeMode.SwipeUp:
                startingPosition = new Vector2(0, -Screen.height); // 아래에서 시작
                break;
            case SwipeMode.SwipeDown:
                startingPosition = new Vector2(0, Screen.height); // 위에서 시작
                break;
            case SwipeMode.SwipeLeft:
                startingPosition = new Vector2(Screen.width, 0); // 오른쪽에서 시작
                break;
            case SwipeMode.SwipeRight:
                startingPosition = new Vector2(-Screen.width, 0); // 왼쪽에서 시작
                break;
            case SwipeMode.OnlyFade:
                startingPosition = Vector2.zero; // 현재 위치로 설정
                break;
        }

        // 새로운 장소의 위치 설정
        currentPlaceImg.rectTransform.anchoredPosition = startingPosition;
        currentPlaceImg.color = new Color(1, 1, 1, 0); // 시작 색상 투명으로 설정

        // 전환 시퀀스 생성
        Sequence transitionSequence = DOTween.Sequence();

        if (previousPlace != null)
        {
            // 이전 이미지를 지정된 방향으로 이동하고 페이드 아웃 후 제거
            Vector2 targetPosition = previousPlace.rectTransform.anchoredPosition;

            switch (_swipeMode)
            {
                case SwipeMode.SwipeUp:
                    targetPosition += new Vector2(0, Screen.height);
                    break;
                case SwipeMode.SwipeDown:
                    targetPosition -= new Vector2(0, Screen.height);
                    break;
                case SwipeMode.SwipeLeft:
                    targetPosition -= new Vector2(Screen.width, 0);
                    break;
                case SwipeMode.SwipeRight:
                    targetPosition += new Vector2(Screen.width, 0);
                    break;
                case SwipeMode.OnlyFade:
                    targetPosition = previousPlace.rectTransform.anchoredPosition; // 현재 위치로 설정
                    break;
            }

            // 이전 이미지를 지정된 방향으로 이동
            transitionSequence.Append(previousPlace.rectTransform.DOAnchorPos(targetPosition, _duration).SetEase(_easeType));

            // 페이드 아웃 후 이전 이미지 파괴
            transitionSequence.Join(ImageController.FadeColor(previousPlace, new Color(1, 1, 1, 0), _duration, _easeType)
                .OnComplete(() => Object.Destroy(previousPlace.gameObject))); // 페이드 아웃 완료 후 이전 이미지 파괴
        }

        // 새로운 장소 올라오기와 페이드 인
        transitionSequence.Join(currentPlaceImg.rectTransform
            .DOAnchorPos(Vector2.zero, _duration) // 새로운 장소 올라오기
            .SetEase(_easeType));
        transitionSequence.Join(ImageController.FadeColor(currentPlaceImg, Color.white, _duration, _easeType)); // 새로운 장소 페이드 인

        // 애니메이션 실행 대기
        yield return transitionSequence.WaitForCompletion();
    }
}
