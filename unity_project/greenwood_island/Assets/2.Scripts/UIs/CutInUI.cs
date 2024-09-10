using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CutInUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CanvasGroup _canvasGroup;     // 최상위 CanvasGroup for fade effect
    [SerializeField] private RectMask2D _rectMask;        // 최상위 RectMask2D for masking
    [SerializeField] private Image _highlightImage;       // 강조할 이미지 (자식)

    private void Awake()
    {
        // 초기화 - 캔버스 그룹 알파와 스케일을 설정합니다.
        _canvasGroup.alpha = 1f;
        transform.localScale = new Vector3(1, 0, 1); // X 스케일은 1, Y 스케일은 0으로 설정
    }

    /// <summary>
    /// Cut-in UI 초기화
    /// </summary>
    /// <param name="highlightSprite">강조할 이미지 스프라이트</param>
    /// <param name="bgColor">배경 색상</param>
    /// <param name="highlightOffset">강조할 이미지의 오프셋</param>
    public void Init(Sprite highlightSprite, float scaleFactor, Vector2 highlightOffset)
    {

        // 강조할 이미지 설정 및 위치 조정
        _highlightImage.sprite = highlightSprite;
        _highlightImage.SetNativeSize();
        _highlightImage.transform.localScale = Vector3.one * scaleFactor;
        _highlightImage.rectTransform.anchoredPosition = highlightOffset;
        _highlightImage.gameObject.SetActive(true);
    }

    /// <summary>
    /// Cut-in UI 등장 및 퇴장 애니메이션
    /// </summary>
    /// <param name="show">true면 등장, false면 퇴장</param>
    /// <param name="duration">애니메이션 지속 시간</param>
    public void Show(bool show, float duration)
    {
        if (show)
        {
            // Cut-in 등장 애니메이션
            transform.localScale = new Vector3(1, 0, 1); // X 스케일은 1로 유지, Y 스케일은 0에서 시작
            _canvasGroup.alpha = 1f; // CanvasGroup의 알파 값을 1로 설정
            Sequence showSequence = DOTween.Sequence();

            showSequence.Append(transform.DOScaleY(1.2f, duration * 0.6f).SetEase(Ease.OutBack)) // Y 스케일이 1.2까지 급격히 늘어남
                        .Append(transform.DOScaleY(1f, duration * 0.4f).SetEase(Ease.InOutSine)); // Y 스케일이 1로 조정됨
        }
        else
        {
            // Cut-in 퇴장 애니메이션
            _canvasGroup.DOFade(0f, duration).SetEase(Ease.InOutQuad); // CanvasGroup의 알파 값을 0으로 줄임
        }
    }
}
