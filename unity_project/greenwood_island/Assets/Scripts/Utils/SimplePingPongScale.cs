using UnityEngine;
using DG.Tweening;

public class SimplePingPongScale : MonoBehaviour
{
    [SerializeField] private Vector2 _scaleFactorRange = new Vector2(0.8f, 1.2f); // 최소 및 최대 스케일 범위
    [SerializeField] private float _duration = 1f; // 스케일 변화에 걸리는 시간

    private void Start()
    {
        // 기본 스케일을 _scaleFactorRange의 최소값으로 설정
        transform.localScale = Vector3.one * _scaleFactorRange.x;

        // DOScale을 사용하여 PingPong 효과
        transform.DOScale(Vector3.one * _scaleFactorRange.y, _duration)
            .SetEase(Ease.InOutSine) // 부드러운 Sine Ease 적용
            .SetLoops(-1, LoopType.Yoyo); // 무한 반복하면서 Yoyo(PingPong) 형태로 진행
    }
}
