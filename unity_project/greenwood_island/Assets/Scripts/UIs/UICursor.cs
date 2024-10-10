using UnityEngine;
using DG.Tweening;

public class UICursor : MonoBehaviour
{
    [SerializeField] private RectTransform _parentObject; // 상위 부모 RectTransform
    [SerializeField] private RectTransform _graphic; // 클릭 시 효과를 줄 그래픽
    [SerializeField] private float _clickEffectDuration = 0.3f; // 클릭 시 발생할 도트윈 효과 지속 시간
    [SerializeField] private Vector3 _clickEffectScale = new Vector3(1.2f, 1.2f, 1.2f); // 클릭 시 스케일

    private void Update()
    {
        // 마우스 포인터 위치를 가져와 상위 부모의 위치를 바로 설정
        Vector3 mousePosition = Input.mousePosition;
        _parentObject.position = mousePosition; // 마우스 위치로 상위 부모의 위치를 이동
    }

    private void OnMouseDown()
    {
        // 클릭 시 그래픽에 도트윈 효과 적용
        _graphic.DOKill(); // 기존 트윈 중지
        _graphic.DOScale(_clickEffectScale, _clickEffectDuration).SetEase(Ease.OutQuad).SetLoops(2, LoopType.Yoyo);
    }
}
