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
    [SerializeField] private Image _standardImg;            // 기본 이미지 (눈을 감고 입을 닫은 상태)
    // 감정과 키를 묶은 배열 (Inspector에서 설정 가능)
    [SerializeField] private EmotionKeyPair[] _emotionKeyPairs;

    private Dictionary<string, Emotion[]> _emotions = new Dictionary<string, Emotion[]>();
    private Emotion _currentEmotion;

    public Emotion CurrentEmotion { get => _currentEmotion; }

    private void Awake()
    {
        AllEmotionsFadeOut(0f);
    }

    public void AllEmotionsFadeOut(float duration){

        // EmotionKeyPair 배열에서 Dictionary로 변환하여 키로 감정을 관리
        foreach (var pair in _emotionKeyPairs)
        {
            if (pair.emotions != null && !string.IsNullOrEmpty(pair.key))
            {
                _emotions[pair.key] = pair.emotions;
                foreach (var emotion in pair.emotions)
                {
                    emotion.gameObject.SetActive(true);
                    emotion.Activate(false, duration);
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
        // 이전 감정이 존재하면 해제 (깜박임, 말하기 중지)
        if (_currentEmotion == emotionArray[emotionIndex])
        {
            Debug.LogWarning("동일 감정이므로 생략");
            return;
        }
        if(_currentEmotion != null){
            _currentEmotion.Activate(false, .5f);
        }
          
        // 새로운 감정을 활성화
        _currentEmotion = emotionArray[emotionIndex];
        _currentEmotion.Activate(true, .5f);

    }
}
