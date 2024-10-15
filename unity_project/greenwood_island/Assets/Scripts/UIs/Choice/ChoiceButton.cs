using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class ChoiceButton : DesignedIconButton
{
    [SerializeField] private TextMeshProUGUI _choiceText;
    [SerializeField] private CanvasGroup _canvasGroup;

    public TextMeshProUGUI ChoiceText => _choiceText;
    public CanvasGroup CanvasGroup => _canvasGroup;


    public void SetText(string text)
    {
        _choiceText.text = text;
    }

    public void SetOnClickListener(UnityEngine.Events.UnityAction action)
    {
        _button.onClick.AddListener(action);
    }

    public void FadeIn(float duration)
    {
        _canvasGroup.alpha = 0f;
        _canvasGroup.DOFade(1f, duration);
    }

    public void FadeOut(float duration, float delay, System.Action onComplete)
    {
        _canvasGroup.DOFade(0f, duration).SetDelay(delay).OnComplete(() => onComplete?.Invoke());
    }
}
