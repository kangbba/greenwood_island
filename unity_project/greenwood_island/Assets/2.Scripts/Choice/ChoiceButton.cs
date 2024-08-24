using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class ChoiceButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _choiceText;
    [SerializeField] private Button _button;
    [SerializeField] private CanvasGroup _canvasGroup;

    public TextMeshProUGUI ChoiceText => _choiceText;
    public Button Button => _button;
    public CanvasGroup CanvasGroup => _canvasGroup;

    private void Awake()
    {
        // 버튼이나 텍스트가 null일 경우 에러 로그를 출력
        if (_choiceText == null || _button == null || _canvasGroup == null)
        {
            Debug.LogError("ChoiceButton is missing required components.");
        }
    }

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
