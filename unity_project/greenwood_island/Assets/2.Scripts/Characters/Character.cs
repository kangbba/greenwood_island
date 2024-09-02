using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum EEmotionID
{
    Happy,
    Smile,
    Normal,
    Sad,
    Crying,
    Angry,
    Panic,
    Stumped,
}
public class Character : MonoBehaviour
{
    [System.Serializable]
    public class EmotionPlan
    {
        public EEmotionID _emotionID; // Emotion ID (e.g., "Happy", "Sad")
        public Sprite[] _emotionSprites; // Array of sprites corresponding to the emotion
    }

    [SerializeField] private CanvasGroup _graphic;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private List<EmotionPlan> _emotionPlans; // List of emotion plans
    [SerializeField] private Image _img; // Image component for the character sprite

    private Vector3 _originalPos = Vector3.zero; // Define the original position as (0, 0, 0)

    protected List<EmotionPlan> EmotionPlans { get => _emotionPlans; }

    private void Start()
    {
        SetVisibility(false, 0f);
    }
    public void SetVisibility(bool visible, float duration)
    {
        float targetAlpha = visible ? 1f : 0f;

        // Animate the alpha value
        _graphic.DOFade(targetAlpha, duration).SetEase(Ease.OutQuad);
    }

    public void ChangeEmotion(EEmotionID emotionID, int index, float duration)
    {
        EmotionPlan selectedPlan = EmotionPlans.Find(plan => plan._emotionID == emotionID);

        if (selectedPlan == null)
        {
            Debug.LogWarning($"Emotion '{emotionID}' not found in the emotion plans.");
            return;
        }

        if (index < 0 || index >= selectedPlan._emotionSprites.Length)
        {
            Debug.LogWarning($"Invalid sprite index '{index}' for emotion '{emotionID}'.");
            return;
        }

        if (duration <= 0f)
        {
            // Change the sprite immediately
            _img.sprite = selectedPlan._emotionSprites[index];
            _graphic.alpha = 1f;
        }
        else
        {
            // Fade to 0.3, then change sprite, and fade back to 1
            Sequence transitionSequence = DOTween.Sequence();

            transitionSequence.Append(_graphic.DOFade(0.3f, duration * 0.4f).SetEase(Ease.InQuad)) // Fade from 1 -> 0.3
                              .AppendInterval(0.1f) // Wait for 0.1 seconds for a smooth transition
                              .AppendCallback(() =>
                              {
                                  _img.sprite = selectedPlan._emotionSprites[index]; // Change the sprite
                              })
                              .Append(_graphic.DOFade(1f, duration * 0.6f).SetEase(Ease.OutQuad)); // Fade from 0.3 -> 1

            transitionSequence.Play();
        }

        Debug.Log($"Emotion changed to '{emotionID}' with sprite index {index}, duration: {duration}s.");
    }

    public void ApplyJumpEffect()
    {
        float jumpHeight = 30f;

        _rectTransform.DOAnchorPosY(_originalPos.y + jumpHeight, 0.2f).SetEase(Ease.OutQuad)
            .OnComplete(() => _rectTransform.DOAnchorPosY(_originalPos.y, 0.2f).SetEase(Ease.InQuad));
    }

    public void ApplyShakeEffect(float duration, float strength)
    {
        _rectTransform.DOShakePosition(duration, strength, 10, 90, false, true);
    }

    public void Highlight(float duration = 0.5f)
    {
        Sequence highlightSequence = DOTween.Sequence();

        highlightSequence.Append(_graphic.DOFade(1.2f, duration / 2).SetEase(Ease.OutQuad)) 
                         .Join(_rectTransform.DOScale(1.1f, duration / 2).SetEase(Ease.OutQuad))
                         .Append(_graphic.DOFade(1f, duration / 2).SetEase(Ease.InQuad))
                         .Join(_rectTransform.DOScale(1f, duration / 2).SetEase(Ease.InQuad));

        highlightSequence.Play();
    }
}
