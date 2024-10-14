using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public static class ImageUtils
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
        img.transform.localScale = Vector3.one;
        img.rectTransform.anchoredPosition = Vector2.zero;

        // 스프라이트 설정
        img.sprite = sprite;
        img.rectTransform.sizeDelta = UIManager.SystemCanvas.GetComponent<Canvas>().renderingDisplaySize;

        // 초기 알파값 설정
        img.color = new Color(img.color.r, img.color.g, img.color.b, 0f);

        return img;
    }

    // 이미지 페이드 메서드 (this 사용)
    public static Tween FadeImage(this Image image, Color targetColor, float duration, Ease easeType)
    {
        if (image == null) return null;
        return image.DOColor(targetColor, duration).SetEase(easeType);
    }
    // 초기 색상을 설정하는 페이드 메서드
    public static Tween FadeImageFrom(this Image image, Color initialColor, Color targetColor, float duration, Ease easeType)
    {
        if (image == null) return null;
        image.color = initialColor; // 초기 색상 설정
        return image.DOColor(targetColor, duration).SetEase(easeType);
    }


    // 이미지 이동 메서드 (this 사용)
    public static Tween MoveImage(this Image image, Vector2 anchoredPos, float duration, Ease easeType)
    {
        if (image == null) return null;
        return image.rectTransform.DOAnchorPos(anchoredPos, duration).SetEase(easeType);
    }
    // 초기 위치를 설정하는 이동 메서드
    public static Tween MoveImageFrom(this Image image, Vector2 initialPos, Vector2 targetPos, float duration, Ease easeType)
    {
        if (image == null) return null;
        image.rectTransform.anchoredPosition = initialPos; // 초기 위치 설정
        return image.rectTransform.DOAnchorPos(targetPos, duration).SetEase(easeType);
    }

    // 이미지 크기 조정 메서드 (this 사용)
    public static Tween ScaleImage(this Image image, Vector3 targetScale, float duration, Ease easeType, float delay = 0f) // 딜레이 추가
    {
        if (image == null) return null;
        return image.transform
                    .DOScale(targetScale, duration)  // 목표 크기로 애니메이션
                    .SetEase(easeType)  // 이징 설정
                    .SetDelay(delay);  // 딜레이 설정
    }

    // 초기 스케일을 설정하는 크기 조정 메서드
    public static Tween ScaleImageFrom(this Image image, Vector3 initialScale, Vector3 targetScale, float duration, Ease easeType)
    {
        if (image == null) return null;
        image.transform.localScale = initialScale; // 초기 스케일 설정
        return image.transform.DOScale(targetScale, duration).SetEase(easeType);
    }

    // 이미지 흔들기 메서드 (this 사용)
    public static Tween ShakeImage(this Image image, float duration, float strength = 3f, int vibrato = 10, float randomness = 90f)
    {
        if (image == null) return null;
        return image.rectTransform.DOShakeAnchorPos(duration, strength, vibrato, randomness);
    }

    // 이미지 제거 메서드 (this 사용)
    public static Tween FadeOutAndDestroyImage(this Image image, float duration, Ease easeType)
    {
        if (image == null) return null;
        return image.DOColor(new Color(image.color.r, image.color.g, image.color.b, 0f), duration)
                    .SetEase(easeType)
                    .OnComplete(() =>
                    {
                        if (image != null)
                        {
                            Object.Destroy(image.gameObject);
                        }
                    });
    }


    /// <summary>
    /// PlaceTransitionWithSwipe 클래스는 장소 간의 전환을 관리하는 Element입니다.
    /// </summary>
    public enum SwipeMode
    {
        OnlyFade,
        SwipeUp,
        SwipeDown,
        SwipeLeft,
        SwipeRight
    }

    // SwipeImage: 이전 이미지와 새 이미지를 전환하는 메서드
    public static void SwipeImage(Image currentImage, Image previousImage, float duration, SwipeMode swipeMode, Ease easeType = Ease.OutQuad, bool previousDestroy = true)
    {
        if (previousImage != null)
        {
            // 이전 이미지 타겟 위치 계산 및 애니메이션 실행
            Vector2 targetPosition = GetExitSwipePosition(swipeMode);
            previousImage.MoveImage(targetPosition, duration, easeType);
            previousImage.FadeOutAndDestroyImage(duration, easeType).OnComplete(() =>
            {
                if (previousDestroy && previousImage != null)
                    Object.Destroy(previousImage.gameObject);
            });
        }

        // 새 이미지의 시작 위치 설정 및 애니메이션 실행
        Vector2 startingPosition = GetEnterSwipePosition(swipeMode);
        currentImage.MoveImageFrom(startingPosition, Vector2.zero, duration, easeType);
        currentImage.FadeImageFrom(new Color(1, 1, 1, 0), Color.white, duration, easeType);
    }
    
    /// <summary>
    /// 스와이프 모드에 따른 이전 장소의 타겟 위치 반환 (이전 이미지 위치와 무관)
    /// </summary>
    public static Vector2 GetExitSwipePosition(SwipeMode swipeMode)
    {
        return swipeMode switch
        {
            SwipeMode.SwipeUp => new Vector2(0, Screen.height),    // 화면 위로 나감
            SwipeMode.SwipeDown => new Vector2(0, -Screen.height),  // 화면 아래로 나감
            SwipeMode.SwipeLeft => new Vector2(-Screen.width, 0),   // 화면 왼쪽으로 나감
            SwipeMode.SwipeRight => new Vector2(Screen.width, 0),   // 화면 오른쪽으로 나감
            SwipeMode.OnlyFade => Vector2.zero,                     // 페이드만 수행
            _ => Vector2.zero
        };
    }
    public static Vector2 GetEnterSwipePosition(SwipeMode swipeMode)
    {
        return swipeMode switch
        {
            SwipeMode.SwipeUp => new Vector2(0, -Screen.height),    // 화면 위로 나감
            SwipeMode.SwipeDown => new Vector2(0, Screen.height),  // 화면 아래로 나감
            SwipeMode.SwipeLeft => new Vector2(Screen.width, 0),   // 화면 왼쪽으로 나감
            SwipeMode.SwipeRight => new Vector2(-Screen.width, 0),   // 화면 오른쪽으로 나감
            SwipeMode.OnlyFade => Vector2.zero,                     // 페이드만 수행
            _ => Vector2.zero
        };
    }

}
