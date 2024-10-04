using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public enum EmotionType
{
    // 긍정적인 감정
    Neutral,
    Happy,              // 행복 (약한 행복)
    VeryHappy,          // 매우 행복 (강한 행복)

    // 슬픔 감정
    Sad,                // 슬픔 (약한 슬픔)
    VerySad,            // 매우 슬픔 (강한 슬픔)

    // 분노 감정
    Angry,              // 화남 (약한 분노)
    VeryAngry,          // 매우 화남 (강한 분노)

    // 공포 감정
    Scared,             // 두려움 (약한 공포)
    VeryScared,         // 매우 두려움 (강한 공포)

    // 의심 및 불안 감정
    Worried,            // 불안 (약한 불안)
    VeryWorried,        // 매우 불안 (강한 불안)

    // 충격 감정
    Surprised,          // 놀람 (약한 충격)
    VerySurprised,      // 매우 놀람 (강한 충격)

    // 결의 및 광기
    Determined,         // 결의 (강한 의지)
    Crazy               // 광기 (이성을 잃은 상태)
}


// 캐릭터의 감정을 관리하는 클래스
[Serializable]
public class CharacterEmotion
{
    public EmotionType emotionType;   // 감정의 enum (예: EmotionType.Smile)
    public Emotion emotion;           // 실제 Emotion 객체
}

public class Character : MonoBehaviour
{
    public enum AnchorType
    {
        Left,
        Right,
        Top,
        Bottom
    }

    [SerializeField] private string _characterName;  // 캐릭터 이름 (예: "Kate", "Lisa")
    [SerializeField] private RectTransform _rectTr;
    private RectMask2D _rectMask;

    // 감정 목록 (각 캐릭터의 감정을 관리)
    [SerializeField] private List<CharacterEmotion> _emotionsList;

    private Emotion _currentEmotion;

    public Emotion CurrentEmotion { get => _currentEmotion; }

    private void Awake()
    {
        _rectMask = gameObject.AddComponent<RectMask2D>();
        AllEmotionsFadeOut(0f);
    }

    // 모든 감정 비활성화
    public void AllEmotionsFadeOut(float duration)
    {
        _rectTr.sizeDelta = Vector2.zero;
        _currentEmotion = null;

        // 모든 감정을 비활성화
        foreach (var characterEmotion in _emotionsList)
        {
            if (characterEmotion.emotion != null)
            {
                characterEmotion.emotion.gameObject.SetActive(true);
                characterEmotion.emotion.Activate(false, duration);
            }
        }
    }

    // 감정 변경 함수
    public void ChangeEmotion(EmotionType emotionType, float fadeDuration = 1f)
    {
        // 현재 감정을 emotionType을 기반으로 찾기
        var emotion = FindEmotion(emotionType);
        if (emotion == null) return;

        // 현재 활성화된 감정을 비활성화
        if (_currentEmotion != null)
        {
            _currentEmotion.Activate(false, fadeDuration);
        }

        // 새로운 감정을 활성화
        _currentEmotion = emotion;
        _currentEmotion.Activate(true, fadeDuration);
    }

    // 특정 감정을 emotionType으로 찾는 함수
    private Emotion FindEmotion(EmotionType emotionType)
    {
        foreach (var characterEmotion in _emotionsList)
        {
            if (characterEmotion.emotionType == emotionType)
            {
                return characterEmotion.emotion;
            }
        }
        Debug.LogWarning($"Emotion '{emotionType}' not found for character '{_characterName}'");
        return null;
    }

    // RectMask2D 마스크 조절 함수 (EmotionType과 AnchorType을 인풋으로 받도록 수정)
    public void SetRectMask(EmotionType emotionType, AnchorType anchorType, bool isShow, float duration, Ease easeType)
    {
        // 현재 감정을 emotionType을 기반으로 찾기
        var emotion = FindEmotion(emotionType);
        if (emotion == null) return;

        // RectTransform을 가져옴
        RectTransform emotionRectTr = emotion.RectTr;
        if (emotionRectTr == null)
        {
            Debug.LogWarning("Emotion has no RectTransform.");
            return;
        }

        // 감정의 크기를 기준으로 RectMask 크기 설정
        _rectTr.sizeDelta = emotionRectTr.sizeDelta;
        Vector2 paddingSize = new Vector2(700, 1200);  // 필요시 조정

        // 현재 RectMask2D의 padding을 가져옴
        Vector4 currentPadding = _rectMask.padding;
        Vector4 targetPadding = currentPadding; // 기존 값을 기준으로 필요한 축만 수정

        // 각 앵커 타입에 따라 초기 패딩 값 설정 및 수정
        switch (anchorType)
        {
            case AnchorType.Left:
                targetPadding.x = isShow ? 0 : paddingSize.x;
                break;
            case AnchorType.Right:
                targetPadding.z = isShow ? 0 : paddingSize.x;
                break;
            case AnchorType.Top:
                targetPadding.w = isShow ? 0 : paddingSize.y;
                break;
            case AnchorType.Bottom:
                targetPadding.y = isShow ? 0 : paddingSize.y;
                break;
        }

        // DOTween 애니메이션을 사용하여 해당 축의 padding 값을 변경
        DOTween.To(() => _rectMask.padding, x => _rectMask.padding = x, targetPadding, duration).SetEase(easeType);
    }

    // 딜레이를 추가하여 RectMask 설정 (EmotionType과 AnchorType을 인풋으로 받도록 수정)
    public void SetRectMaskWithDelay(EmotionType emotionType, AnchorType anchorType, bool isShow, float duration, Ease easeType, float delay = 0f)
    {
        // 딜레이 후에 SetRectMask 실행
        DOVirtual.DelayedCall(delay, () =>
        {
            SetRectMask(emotionType, anchorType, isShow, duration, easeType);
        });
    }
}
