using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
public struct EmotionKeyPair
{
    public string key;            // 감정의 키 (예: "Smile", "Angry")
    public Emotion[] emotions;    // 해당 감정의 배열 (예: 여러 단계의 smile 감정)
}

public class Character : MonoBehaviour
{
    [SerializeField] private string _characterName_KO;
    [SerializeField] private Color _mainColor;
    [SerializeField] private Image _standardImg;            // 기본 이미지 (눈을 감고 입을 닫은 상태)
    // 감정과 키를 묶은 배열 (Inspector에서 설정 가능)
    [SerializeField] private EmotionKeyPair[] _emotionKeyPairs;

    private Dictionary<string, Emotion[]> _emotions = new Dictionary<string, Emotion[]>();
    private Emotion _currentEmotion;

    private string _recentEmotionID;
    private int _recentEmotionIndex;

    public string CharacterName_KO { get => _characterName_KO; }
    public Color MainColor { get => _mainColor; }
    public Emotion CurrentEmotion { get => _currentEmotion; }

    private void Awake()
    {
        // EmotionKeyPair 배열에서 Dictionary로 변환하여 키로 감정을 관리
        foreach (var pair in _emotionKeyPairs)
        {
            if (pair.emotions != null && !string.IsNullOrEmpty(pair.key))
            {
                _emotions[pair.key] = pair.emotions;
                foreach (var emotion in pair.emotions)
                {
                    emotion.gameObject.SetActive(true);
                    emotion.Activate(false, 0f);
                }
            }
        }
    }

        // 감정 변경 함수 (키와 인덱스를 통해 감정 변경)
    public void ChangeEmotion(string emotionID, int emotionIndex)
    {
        // 해당 키에 감정 배열이 없으면 바로 리턴
        if (!_emotions.TryGetValue(emotionID, out Emotion[] emotionArray))
        {
            Debug.LogWarning($"Emotion with key '{emotionID}' not found.");
            return;
        }

        // 인덱스 범위가 유효하지 않으면 바로 리턴
        if (emotionIndex < 0 || emotionIndex >= emotionArray.Length)
        {
            Debug.LogWarning($"Invalid emotion index '{emotionIndex}' for key '{emotionID}'.");
            return;
        }

        // 동일한 감정이면 바로 리턴
        bool isIdenticalEmotion = (_recentEmotionID == emotionID) && (_recentEmotionIndex == emotionIndex);
        if (isIdenticalEmotion)
        {
            Debug.Log($"이미 해당 감정 '{emotionIndex}' for key '{emotionID}'가 활성화되어 있습니다.");
            return;
        }

        // 이전 감정이 존재하면 해제 (깜박임, 말하기 중지)
        if (_currentEmotion != null)
        {
            _currentEmotion.Activate(false, .5f);
        }

        // 새로운 감정을 활성화
        _currentEmotion = emotionArray[emotionIndex];
        _currentEmotion.Activate(true, .5f);

        // 감정 변경 상태 갱신
        _recentEmotionID = emotionID;
        _recentEmotionIndex = emotionIndex;

        Debug.Log($"{_characterName_KO}의 감정이 '{emotionID}'에서 '{emotionIndex}'로 변경되었습니다.");
    }




    // 캐릭터 투명도 조정 함수
    public void SetVisibility(bool visible, float duration)
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.alpha = visible ? 1f : 0f;
        }
    }
}
