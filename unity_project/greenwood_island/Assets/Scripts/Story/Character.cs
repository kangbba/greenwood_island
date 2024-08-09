using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[System.Serializable]
public class EmotionPlan
{
    public string _emotionKey;            // 감정 키 (예: "happy", "sad")
    public Sprite[] _emotionSprites;      // 감정에 해당하는 스프라이트 배열
}

public class Character : MonoBehaviour
{
    [SerializeField] private CanvasGroup _graphic;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private List<EmotionPlan> _emotionPlans; // 감정 플랜 리스트
    [SerializeField] private Image _img;  // 캐릭터의 SpriteRenderer

    private Vector3 _originalPos = Vector3.zero; // 기준 위치를 (0, 0, 0)으로 정의

    protected List<EmotionPlan> EmotionPlans { get => _emotionPlans; }

    private void Start(){
        ShowCharacter(false, 0f);
    }
    public void ShowCharacter(bool b, float totalSec)
    {
        // 투명도를 0 (사라지게) 또는 1 (불투명하게) 설정
        float targetAlpha = b ? 1f : 0f;

        // 두트윈을 사용하여 alpha 값을 애니메이션으로 조정
        _graphic.DOFade(targetAlpha, totalSec).SetEase(Ease.OutQuad);
    }

    public void JumpEffect()
    {
        // 점프 높이
        float jumpHeight = 30f;

        // 점프 애니메이션 (위로 점프했다가 다시 내려옴)
        _rectTransform.DOAnchorPosY(_originalPos.y + jumpHeight, 0.2f).SetEase(Ease.OutQuad)
            .OnComplete(() => _rectTransform.DOAnchorPosY(_originalPos.y, 0.2f).SetEase(Ease.InQuad));
    }

    public void ShakeEffect(float duration, float strength)
    {
        // 흔들림 효과
        _rectTransform.DOShakePosition(duration, strength, 10, 90, false, true);
    }

    public void ChangeEmotion(string emotionID, int index)
    {
        // 감정 키에 해당하는 EmotionPlan 찾기
        EmotionPlan selectedPlan = EmotionPlans.Find(plan => plan._emotionKey == emotionID);

        if (selectedPlan == null)
        {
            Debug.LogWarning($"Emotion '{emotionID}' not found in the emotion plans.");
            return;
        }

        // 인덱스가 유효한지 확인
        if (index < 0 || index >= selectedPlan._emotionSprites.Length)
        {
            Debug.LogWarning($"Invalid sprite index '{index}' for emotion '{emotionID}'.");
            return;
        }

        // SpriteRenderer에 새로운 스프라이트 할당
        _img.sprite = selectedPlan._emotionSprites[index];
        Debug.Log($"Emotion changed to '{emotionID}' with sprite index {index}.");
    }
}
