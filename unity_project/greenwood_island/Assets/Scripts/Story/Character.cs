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
        public EEmotionID _emotionID; // 감정 키 (예: "happy", "sad")
        public Sprite[] _emotionSprites; // 감정에 해당하는 스프라이트 배열
    }

    [SerializeField] private CanvasGroup _graphic;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private List<EmotionPlan> _emotionPlans; // 감정 플랜 리스트
    [SerializeField] private Image _img; // 캐릭터의 SpriteRenderer

    private Vector3 _originalPos = Vector3.zero; // 기준 위치를 (0, 0, 0)으로 정의

    protected List<EmotionPlan> EmotionPlans { get => _emotionPlans; }

    public void Init(EEmotionID initialEmotionID, int initialIndex, float duration)
    {
        // 초기 감정 상태를 설정
        ChangeEmotion(initialEmotionID, initialIndex, 0f); // 이미지 즉시 변경
        
        // 캐릭터를 부드럽게 나타냄
        ShowCharacter(false, 0f);
        ShowCharacter(true, duration);

        Debug.Log($"Character initialized with emotion '{initialEmotionID}' and duration {duration}s.");
    }

    public void ShowCharacter(bool visible, float duration)
    {
        float targetAlpha = visible ? 1f : 0f;

        // 투명도를 애니메이션으로 조정
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
        // 즉각적으로 변경
        _img.sprite = selectedPlan._emotionSprites[index];
        _graphic.alpha = 1f;
    }
    else
    {
        // 투명도 1에서 0.3으로 감소 후 0.7에서 스프라이트를 변경, 그리고 다시 1로 증가
        Sequence transitionSequence = DOTween.Sequence();

        transitionSequence.Append(_graphic.DOFade(0.3f, duration * 0.4f).SetEase(Ease.InQuad)) // 투명도 1 -> 0.3
                          .AppendInterval(0.1f) // 0.1초 대기 (부드러운 전환을 위해)
                          .AppendCallback(() =>
                          {
                              _img.sprite = selectedPlan._emotionSprites[index]; // 스프라이트 변경
                          })
                          .Append(_graphic.DOFade(1f, duration * 0.6f).SetEase(Ease.OutQuad)); // 투명도 0.7 -> 1

        transitionSequence.Play();
    }

    Debug.Log($"Emotion changed to '{emotionID}' with sprite index {index}, duration: {duration}s.");
}


    public void JumpEffect()
    {
        float jumpHeight = 30f;

        _rectTransform.DOAnchorPosY(_originalPos.y + jumpHeight, 0.2f).SetEase(Ease.OutQuad)
            .OnComplete(() => _rectTransform.DOAnchorPosY(_originalPos.y, 0.2f).SetEase(Ease.InQuad));
    }

    public void ShakeEffect(float duration, float strength)
    {
        _rectTransform.DOShakePosition(duration, strength, 10, 90, false, true);
    }

    public void HighlightCharacter(float duration = 0.5f)
    {
        Sequence highlightSequence = DOTween.Sequence();

        highlightSequence.Append(_graphic.DOFade(1.2f, duration / 2).SetEase(Ease.OutQuad)) 
                        .Join(_rectTransform.DOScale(1.1f, duration / 2).SetEase(Ease.OutQuad))
                        .Append(_graphic.DOFade(1f, duration / 2).SetEase(Ease.InQuad))
                        .Join(_rectTransform.DOScale(1f, duration / 2).SetEase(Ease.InQuad));

        highlightSequence.Play();
    }
}
