using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;  // UI 요소를 사용하기 위해 추가

public class Place : MonoBehaviour
{
    public EPlaceID PlaceID;  // 장소의 ID를 할당할 필드
    private Image _image;  // UI 이미지 컴포넌트

    public Image Image { get => _image; }

    private void Awake()
    {
        _image = GetComponent<Image>();

        if (_image == null)
        {
            Debug.LogError("Image component is missing.");
        }

        // 초기화 시 투명하게 설정
        SetVisibility(true, 1f);
    }

    // 가시성을 설정하는 메서드 (페이드 인/아웃)
    public Tween SetVisibility(bool visible, float duration)
    {
        if (_image == null) return null;

        float targetAlpha = visible ? 1f : 0f;
        Color targetColor = new Color(_image.color.r, _image.color.g, _image.color.b, targetAlpha);

        // 알파 값을 애니메이션화하여 색상 변경
        return _image.DOColor(targetColor, duration).SetEase(Ease.OutQuad);
    }

    // 색상을 변경하는 유틸리티 함수
    public Tween SetColor(Color color, float duration)
    {
        if (_image != null)
        {
            return _image.DOColor(color, duration).SetEase(Ease.OutQuad);
        }

        return null;
    }
}
