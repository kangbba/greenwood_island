using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BlurPanel : MonoBehaviour
{
    [SerializeField] private Image _img; // BlurPanel의 Image 컴포넌트
    [SerializeField] private Material _blurMaterial; // 블러가 적용된 Material

    private const string BlurSizeProperty = "_Size"; // 블러 값으로 사용할 상수

    private void Awake()
    {
        // Blur Material의 인스턴스를 생성하여 Image에 적용
        _img.material = Instantiate(_blurMaterial);

        // 초기화 시 블러 값을 0으로 설정
        SetBlur(1f, 1f);
    }

    // 블러 값을 설정하는 애니메이션 메서드
    public void SetBlur(float targetValue, float duration)
    {
        // 예외 처리: 이미지와 머티리얼이 null이 아닌지 확인
        if (_img == null || _img.material == null)
        {
            Debug.LogError("BlurPanel: Image or Material is missing.");
            return;
        }

        _img.color = Color.white.ModifiedAlpha(targetValue);

        // Blur 값 애니메이션
        DOTween.To(() => _img.material.GetFloat(BlurSizeProperty), x => _img.material.SetFloat(BlurSizeProperty, x), targetValue, duration)
            .SetEase(Ease.InOutQuad);
    }

    // 블러 값 페이드 아웃 후 패널 제거
    public void FadeOutAndDestroy(float duration)
    {
        // 예외 처리: 이미지와 머티리얼이 null이 아닌지 확인
        if (_img == null || _img.material == null)
        {
            Debug.LogError("BlurPanel: Image or Material is missing.");
            return;
        }

        // Blur 값을 0으로 애니메이션 후 제거
        SetBlur(0f, duration);
        DOTween.To(() => _img.material.GetFloat(BlurSizeProperty), x => _img.material.SetFloat(BlurSizeProperty, x), 0f, duration)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() => Destroy(gameObject)); // 애니메이션 완료 후 패널 제거
    }
}
