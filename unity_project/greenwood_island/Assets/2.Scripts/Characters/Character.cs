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
    CryingHappy,

}

public class Character : MonoBehaviour
{
    [SerializeField] private CanvasGroup _graphic;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Image _img; // Image component for the character sprite
    [SerializeField] private EmotionPlansData _emotionPlansData; // ScriptableObject to manage emotion plans


    private List<EmotionPlan> EmotionPlans => _emotionPlansData != null ? _emotionPlansData.EmotionPlans : new List<EmotionPlan>();

    public EmotionPlansData EmotionPlansData { get => _emotionPlansData; }

    public void SetVisibility(bool visible, float duration, Ease easeType = Ease.OutQuad)
    {
        float targetAlpha = visible ? 1f : 0f;

        _graphic.DOFade(targetAlpha, duration).SetEase(easeType);
    }
    public void SetVisibility(float targetAlpha, float duration, Ease easeType = Ease.OutQuad)
    {
        _graphic.DOFade(targetAlpha, duration).SetEase(easeType);
    }

    public void ChangeEmotion(EEmotionID emotionID, int index, float duration = 1f)
    {
        // 해당 감정을 EmotionPlans에서 찾음
        EmotionPlan selectedPlan = EmotionPlans.Find(plan => plan.EmotionID == emotionID);

        if (selectedPlan == null)
        {
            Debug.LogWarning($"{nameof(Character)} :: 이모션 '{emotionID}' 을 플랜에서 찾을 수 없음");
            return;
        }

        if (index < 0 || index >= selectedPlan.EmotionSprites.Length)
        {
            Debug.LogWarning($"{nameof(Character)} :: 유효하지 않은 인덱스 '{emotionID}' '{index}' 을 할당하려는 시도.");
            return;
        }

        // 새로운 스프라이트 설정
        Sprite newSprite = selectedPlan.EmotionSprites[index];

        // duration이 0일 때: 즉시 이미지 변경
        if (duration <= 0f)
        {
            _img.sprite = newSprite;
            _img.color = new Color(_img.color.r, _img.color.g, _img.color.b, 1f); // 알파 값을 1로 설정
            Debug.Log($"Emotion changed to '{emotionID} {index}' instantly.");
            return;
        }

        // 현재 이미지의 투명도 조정 및 새로운 스프라이트 설정 후 등장
        Image tempImage = Instantiate(_img, _img.transform.parent); // 현재 이미지의 복제본을 만듦
        tempImage.sprite = _img.sprite; // 이전 스프라이트를 할당

        // 새로운 스프라이트를 투명하게 설정
        _img.sprite = newSprite;
        _img.color = new Color(_img.color.r, _img.color.g, _img.color.b, 0);

        // 시퀀스로 애니메이션 처리: 이전 스프라이트는 사라지고, 새로운 스프라이트는 나타남
        Sequence transitionSequence = DOTween.Sequence();

        transitionSequence.Append(tempImage.DOFade(0f, duration * 0.5f)) // 이전 스프라이트 사라짐
                        .Join(_img.DOFade(1f, duration * 0.5f)) // 새로운 스프라이트 등장
                        .OnComplete(() =>
                        {
                            Destroy(tempImage.gameObject); // 이전 스프라이트 제거
                        });

        transitionSequence.Play();

        // 로그 메시지 간소화 및 스크립트 이름 추가
        Debug.Log($"Emotion changed to '{emotionID} {index}' (duration: {duration}s).");
    }

}
