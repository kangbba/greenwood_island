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

    private GuideState _state = GuideState.Hidden;
    private RectTransform _guideRectTransform;

    void Start()
    {
        _guideRectTransform = _guideImage.GetComponent<RectTransform>();
        SetState(GuideState.Hidden); // 시작 시에는 가이드가 숨겨진 상태
    }

    void Update()
    {
        // Update에서 애니메이션을 처리하는 대신, SetState에서만 애니메이션 처리
    }

    public void SetState(GuideState state)
    {
        _state = state;

        switch (_state)
        {
            case GuideState.Hidden:
                _guideImage.gameObject.SetActive(false);
                _guideRectTransform.DOKill();  // 모든 애니메이션 중지
                break;

            case GuideState.Ongoing:
                _guideImage.gameObject.SetActive(true);
                _guideRectTransform.DOKill();  // 모든 애니메이션 중지 후 새로운 애니메이션 시작
                _guideRectTransform.DOAnchorPosX(_guideRectTransform.anchoredPosition.x + 10f, 0.3f)
                                   .SetLoops(-1, LoopType.Yoyo); // 좌우로 흔들림
                break;

            case GuideState.Ended:
                _guideImage.gameObject.SetActive(true);
                _guideRectTransform.DOKill();  // 모든 애니메이션 중지 후 새로운 애니메이션 시작
                _guideRectTransform.DOAnchorPosY(_guideRectTransform.anchoredPosition.y + 10f, 0.3f)
                                   .SetLoops(-1, LoopType.Yoyo); // 위아래로 흔들림
                break;
        }
    }

    public void MoveToPosition(Vector2 position)
    {
        _guideRectTransform.anchoredPosition = position;
    }
}
