using System.Collections;
using TMPro;
using UnityEngine;

public class DesignedTextBtn : DesignedBtn
{
    [SerializeField] private TextMeshProUGUI _contentText;  // 텍스트의 내용을 담는 TextMeshProUGUI
    [SerializeField] private RectTransform _background;     // 버튼 배경의 RectTransform
    [SerializeField] private bool _autoBackgroundWidth = true;   // 자동으로 버튼 너비를 조정할지 여부
    [SerializeField] private float _padding = 20;               // 여유 패딩 값
    [SerializeField] private float _extraSize = 0;               // 여유 패딩 값

    protected override void Awake()
    {
        base.Awake();  // 부모 클래스의 Awake() 호출

        // _autoBackgroundWidth 또는 _autoBackgroundHeight 옵션이 활성화된 경우
        if (_contentText != null && _background != null)
        {
            Vector2 newSize = _background.sizeDelta;

            // _autoBackgroundWidth가 true일 때 버튼 너비 조정 및 앵커 조정
            if (_autoBackgroundWidth)
            {
                float preferredWidth = _contentText.preferredWidth;
                newSize.x = preferredWidth + _extraSize + _padding * 2;  // 너비에 패딩 추가
                // 텍스트 정렬에 따른 앵커 조정
                switch (_contentText.alignment)
                {
                    case TextAlignmentOptions.Center:
                    case TextAlignmentOptions.Midline:
                    case TextAlignmentOptions.Baseline:
                        _background.anchorMin = new Vector2(0.5f, _background.anchorMin.y);
                        _background.anchorMax = new Vector2(0.5f, _background.anchorMax.y);
                        _background.pivot = new Vector2(0.5f, _background.pivot.y);
                        break;

                    case TextAlignmentOptions.Left:
                    case TextAlignmentOptions.MidlineLeft:
                    case TextAlignmentOptions.BaselineLeft:
                        _background.anchorMin = new Vector2(0f, _background.anchorMin.y);
                        _background.anchorMax = new Vector2(0f, _background.anchorMax.y);
                        _background.pivot = new Vector2(0f, _background.pivot.y);
                        _background.anchoredPosition = _background.anchoredPosition.ModifiedX(_background.anchoredPosition.x - _padding);
                        break;

                    case TextAlignmentOptions.Right:
                    case TextAlignmentOptions.MidlineRight:
                    case TextAlignmentOptions.BaselineRight:
                        _background.anchorMin = new Vector2(1f, _background.anchorMin.y);
                        _background.anchorMax = new Vector2(1f, _background.anchorMax.y);
                        _background.pivot = new Vector2(1f, _background.pivot.y);
                        _background.anchoredPosition = _background.anchoredPosition.ModifiedX(_background.anchoredPosition.x +_padding);
                        break;
                }
            }
            _background.sizeDelta = newSize;  // 배경 크기 조정
        }
    }
}
