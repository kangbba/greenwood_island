using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;  // DoTween 사용

public class DesignedIconButton : DesignedBtn
{
    [SerializeField] private Image _iconImg;        // 아이콘 이미지
    [SerializeField] private Image _backgroundImg;  // 배경 이미지
    [SerializeField] private Transform _graphicParent;  // 그래픽 부모 트랜스폼

    [SerializeField] private float hoverScale = 1.1f;    // Hover 시 스케일
    [SerializeField] private float pressedScale = 0.95f; // 눌렀을 때 스케일
    [SerializeField] private float animationDuration = 0.1f; // 애니메이션 지속 시간

    private bool _isOn = true;  // 버튼 상태 (켜짐/꺼짐)
    private bool _isAnimating = false;  // 애니메이션 중복 방지 플래그

    protected override void Awake()
    {
        base.Awake();  // 부모 클래스의 Awake 호출
        ResetGraphicParent();  // 초기 상태로 설정
    }

    // 마우스가 버튼 위에 올라갈 때 실행
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (!_isAnimating)  // 애니메이션 중이 아니면 실행
        {
            base.OnPointerEnter(eventData);  // Hover 사운드 재생
            AnimateGraphicParent(hoverScale);  // Hover 애니메이션
        }
    }

    // 마우스가 버튼 위에서 나갈 때 실행
    public override void OnPointerExit(PointerEventData eventData)
    {
        if (!_isAnimating)  // 애니메이션 중이 아니면 실행
        {
            ResetGraphicParent();  // 원래 스케일로 복귀
        }
    }

    // 버튼을 누를 때 실행
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (!_isAnimating)  // 애니메이션 중이 아니면 실행
        {
            base.OnPointerDown(eventData);  // Pressed 사운드 재생
            AnimateGraphicParent(pressedScale);  // 눌렀을 때 애니메이션
        }
    }

    // 버튼에서 손을 뗄 때 실행
    public override void OnPointerUp(PointerEventData eventData)
    {
        if (!_isAnimating)  // 애니메이션 중이 아니면 실행
        {
            _isOn = !_isOn;  // 버튼 상태 토글
            StartCoroutine(HandleButtonClickAnimation());  // 애니메이션 처리 시작
        }
    }

    // 애니메이션 중 중복 클릭 방지 및 애니메이션 처리
    private IEnumerator HandleButtonClickAnimation()
    {
        _isAnimating = true;  // 애니메이션 플래그 활성화

        // 켜진 상태면 Hover로 복귀, 꺼진 상태면 축소
        float targetScale = _isOn ? hoverScale : 0.9f;
        AnimateGraphicParent(targetScale);

        yield return new WaitForSeconds(animationDuration);  // 애니메이션 시간 대기

        ResetGraphicParent();  // 애니메이션 종료 후 원래 상태로 복귀

        _isAnimating = false;  // 애니메이션 플래그 비활성화
    }

    // 그래픽 부모에 애니메이션 적용
    private void AnimateGraphicParent(float targetScale)
    {
        if (_graphicParent != null)
        {
            _graphicParent.DOKill();  // 기존 애니메이션 정지
            _graphicParent.DOScale(Vector3.one * targetScale, animationDuration)
                          .SetEase(Ease.OutQuad);  // DoTween으로 애니메이션 설정
        }
    }

    // 그래픽 부모를 원래 크기로 복원
    private void ResetGraphicParent()
    {
        if (_graphicParent != null)
        {
            _graphicParent.DOKill();  // 기존 애니메이션 정지
            _graphicParent.DOScale(Vector3.one, animationDuration)
                          .SetEase(Ease.OutQuad);  // 원래 크기로 복귀
        }
    }
}
