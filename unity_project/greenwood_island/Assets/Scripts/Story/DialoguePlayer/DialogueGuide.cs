using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public enum GuideState
{
    Hidden,
    Ongoing,
    Ended
}

public class DialogueGuide : MonoBehaviour
{
    [SerializeField] private Image _guideImage;  // Guide 이미지
    [SerializeField] private Transform _graphic; // 그래픽 오브젝트 (예: 아이콘, 캐릭터 등)

    private GuideState _state = GuideState.Hidden;
    private RectTransform _guideRectTransform;

    void Start()
    {
        _guideRectTransform = _guideImage.GetComponent<RectTransform>();
        SetState(GuideState.Hidden); // 시작 시에는 가이드가 숨겨진 상태
    }

    public void SetState(GuideState state)
    {
        _state = state;

        switch (_state)
        {
            case GuideState.Hidden:
                _guideImage.gameObject.SetActive(false);
                _graphic.DOKill(); // _graphic 애니메이션 중지
                _graphic.localRotation = Quaternion.identity; // 회전을 초기화 (0도)
                break;

            case GuideState.Ongoing:
                _guideImage.gameObject.SetActive(true);
                _graphic.DOKill(); // 모든 애니메이션 중지 후 새로운 애니메이션 시작

                // _graphic을 좌우로 작은 범위로 흔들림
                _graphic.DOLocalMoveX(5f, 0.3f) // 흔들림 범위를 10에서 5로 줄임
                        .SetRelative()
                        .SetLoops(-1, LoopType.Yoyo);

                // 왼쪽으로 90도 회전
                _graphic.localRotation = Quaternion.Euler(0f, 0f, 270f);
                break;

            case GuideState.Ended:
                _guideImage.gameObject.SetActive(true);
                _graphic.DOKill(); // 모든 애니메이션 중지 후 새로운 애니메이션 시작

                // _graphic을 위아래로 작은 범위로 흔들림
                _graphic.DOLocalMoveY(5f, 0.3f) // 흔들림 범위를 10에서 5로 줄임
                        .SetRelative()
                        .SetLoops(-1, LoopType.Yoyo);

                // 회전을 0도로 설정
                _graphic.localRotation = Quaternion.Euler(0f, 0f, 180f);
                break;
        }
    }

    public void SetGraphicRotation(float angle)
    {
        _graphic.localRotation = Quaternion.Euler(0f, 0f, angle);
    }
}
