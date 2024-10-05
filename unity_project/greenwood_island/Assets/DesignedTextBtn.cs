using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DesignedTextBtn : DesignedBtn
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;  // TextMeshProUGUI를 캡슐화하여 바인딩
    [SerializeField] private RectTransform _background;     // 버튼 배경의 RectTransform
    [SerializeField] private bool _autoBackgroundWidth = true;   // 자동으로 버튼 너비를 조정할지 여부
    [SerializeField] private bool _autoBackgroundHeight = true;  // 자동으로 버튼 높이를 조정할지 여부
    [SerializeField] private float _padding = 50f;               // 여유 패딩 값

    protected override void Awake()
    {
        base.Awake();  // 부모 클래스의 Awake() 호출

        // _autoBackgroundWidth 또는 _autoBackgroundHeight 옵션이 활성화된 경우
        if (_textMeshPro != null && _background != null)
        {
            Vector2 newSize = _background.sizeDelta;

            // _autoBackgroundWidth가 true면 텍스트의 preferredWidth에 패딩을 더해 버튼 너비를 조정
            if (_autoBackgroundWidth)
            {
                float preferredWidth = _textMeshPro.preferredWidth;
                newSize.x = preferredWidth + _padding;  // 너비에 패딩 추가
            }

            // _autoBackgroundHeight가 true면 텍스트의 preferredHeight에 패딩을 더해 버튼 높이를 조정
            if (_autoBackgroundHeight)
            {
                float preferredHeight = _textMeshPro.preferredHeight;
                newSize.y = preferredHeight + _padding;  // 높이에 패딩 추가
            }

            _background.sizeDelta = newSize;  // 배경 크기 조정
        }
    }
}
