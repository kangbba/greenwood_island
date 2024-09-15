using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Place : MonoBehaviour
{
    [SerializeField] private Image _imageComponent;

    // 로드된 스프라이트를 이미지에 설정하는 메서드
    public void SetImage(Sprite sprite)
    {
        if (_imageComponent != null)
        {
            _imageComponent.sprite = sprite;
        }
        else
        {
            Debug.LogWarning("No Image component found to set the sprite.");
        }
    }

    // 이미지의 색상을 변경하는 애니메이션 메서드
    public Tween SetColor(Color color, float duration, Ease easeType)
    {
        if (_imageComponent != null)
        {
            // DOTween을 사용하여 이미지의 색상을 변경하고 애니메이션 적용
            return _imageComponent.DOColor(color, duration).SetEase(easeType);
        }
        else
        {
            Debug.LogWarning("Place :: No Image component found to set the color.");
            return null;
        }
    }
}
