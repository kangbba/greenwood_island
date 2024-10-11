using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public static class ImageController
{
    // 이미지를 생성하는 메서드 (Sprite를 전달받음)
    public static Image CreateImage(Sprite sprite, Transform parent)
    {
        if (sprite == null)
        {
            Debug.LogError("Sprite is null. Cannot create image.");
            return null;
        }

        GameObject imageObject = new GameObject("ImageObject");
        imageObject.transform.SetParent(parent, false);

        Image img = imageObject.AddComponent<Image>();
        img.transform.localScale = Vector2.one;
        img.rectTransform.anchoredPosition = Vector2.zero;

        // 스프라이트 설정
        img.sprite = sprite;
        img.rectTransform.sizeDelta = UIManager.SystemCanvas.GetComponent<Canvas>().renderingDisplaySize;

        // 초기 알파값 설정
        img.color = new Color(img.color.r, img.color.g, img.color.b, 0f);

        return img;
    }

    // 이미지 페이드 메서드 (Tween 반환)
    public static Tween FadeColor(Image image, Color targetColor, float duration, Ease easeType)
    {
        return image.DOColor(targetColor, duration).SetEase(easeType);
    }

    // 이미지 이동 메서드 (Tween 반환)
    public static Tween MoveImage(Image image, Vector2 anchoredPos, float duration, Ease ease)
    {
        RectTransform rectTransform = image.rectTransform;
        return rectTransform.DOAnchorPos(anchoredPos, duration).SetEase(ease);
    }

    // 이미지 크기 조정 메서드 (Tween 반환)
    public static Tween ScaleImage(Image image, Vector3 targetScale, float duration, Ease ease)
    {
        return image.transform.DOScale(targetScale, duration).SetEase(ease);
    }

    // 이미지 흔들기 메서드 (Tween 반환)
    public static Tween ShakeImage(Image image, float duration, float strength = 3f, int vibrato = 10, float randomness = 90f)
    {
        RectTransform rectTransform = image.rectTransform;
        return rectTransform.DOShakeAnchorPos(duration, strength, vibrato, randomness);
    }

    // 이미지 제거 메서드 (Tween 반환)
    public static Tween DestroyImage(Image image, float duration, Ease easeType)
    {
        return image.DOColor(new Color(image.color.r, image.color.g, image.color.b, 0f), duration).SetEase(easeType).OnComplete(() =>
        {
            Object.Destroy(image.gameObject);
        });
    }
}
