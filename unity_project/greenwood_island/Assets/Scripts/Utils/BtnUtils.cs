using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public static class ButtonUtils
{

    public static void SetActiveWithScale(this Button button, bool active, float duration = .5f)
    {
        button.interactable = false;
        button.image.ScaleImage(active ? Vector2.one : Vector2.zero, duration, Ease.OutQuad)
            .OnComplete(() => button.interactable = active);
    }
}