using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Character : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Image _img; // 캐릭터 스프라이트용 이미지 컴포넌트
    private CharacterData _characterData; // 캐릭터 정보 관리용 ScriptableObject
    private string _previousEmotionID; // 이전 이모션 ID 저장
    private int _previousEmotionIndex; // 이전 이모션 인덱스 저장

    // 감정 계획을 관리하는 속성
    protected List<EmotionPlan> EmotionPlans => _characterData != null ? _characterData.EmotionPlans : new List<EmotionPlan>();

    public Color MainColor => _characterData != null ? _characterData.MainColor : Color.white;
    public string CharacterName_KO => _characterData != null ? _characterData.CharacterName_KO : "Unknown";

    // 캐릭터의 ID를 바인딩
    protected string CharacterID => _characterData != null ? _characterData.name : GetType().Name;

    // RectTransform은 _img에서 가져옴
    private RectTransform RectTransform => _img.GetComponent<RectTransform>();

    // CharacterData를 초기화하는 메서드
    public void Init(CharacterData characterData)
    {
        _characterData = characterData;
        Debug.Log($"Character initialized with data: {CharacterName_KO}");
    }

    public void SetVisibility(bool visible, float duration, Ease easeType = Ease.OutQuad)
    {
        float targetAlpha = visible ? 1f : 0f;
        _canvasGroup.DOFade(targetAlpha, duration).SetEase(easeType);
    }

    public void SetVisibility(float targetAlpha, float duration, Ease easeType = Ease.OutQuad)
    {
        _canvasGroup.DOFade(targetAlpha, duration).SetEase(easeType);
    }

    // 감정 변경 메서드
    public void ChangeEmotion(string emotionID, int index, float duration = 1f)
    {
        // 같은 이모션과 인덱스가 요청된 경우 변경하지 않음
        if (_previousEmotionID == emotionID && _previousEmotionIndex == index)
        {
            Debug.Log($"Emotion '{emotionID} {index}' is already active. No changes made.");
            return;
        }

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

        // 스프라이트와 설정 가져오기
        SpriteWithSettings spriteSettings = selectedPlan.EmotionSprites[index];
        Sprite newSprite = spriteSettings.Sprite;

        // 현재 이미지의 투명도 조정 및 새로운 스프라이트 설정 후 등장
        Image tempImage = Instantiate(_img, _img.transform.parent); // 현재 이미지의 복제본을 만듦
        tempImage.sprite = _img.sprite; // 이전 스프라이트를 할당

        // 새로운 스프라이트를 투명하게 설정
        _img.sprite = newSprite;
        _img.color = new Color(_img.color.r, _img.color.g, _img.color.b, 0);

        // 스프라이트 피벗에 맞춰 RectTransform 피벗 조정
        if (newSprite != null)
        {
            RectTransform.pivot = newSprite.pivot / newSprite.rect.size;
            RectTransform.anchoredPosition = spriteSettings.Offset; // 오프셋 적용
        }

        // 시퀀스로 애니메이션 처리: 이전 스프라이트는 사라지고, 새로운 스프라이트는 나타남
        Sequence transitionSequence = DOTween.Sequence();

        transitionSequence.Append(tempImage.DOFade(0f, duration * 0.5f)) // 이전 스프라이트 사라짐
                        .Join(_img.DOFade(1f, duration * 0.5f)) // 새로운 스프라이트 등장
                        .OnComplete(() =>
                        {
                            Destroy(tempImage.gameObject); // 이전 스프라이트 제거
                        });

        transitionSequence.Play();

        // 이전 이모션과 인덱스를 저장
        _previousEmotionID = emotionID;
        _previousEmotionIndex = index;

        // 로그 메시지 간소화 및 스크립트 이름 추가
        Debug.Log($"Emotion changed to '{emotionID} {index}' (duration: {duration}s).");
    }
}
