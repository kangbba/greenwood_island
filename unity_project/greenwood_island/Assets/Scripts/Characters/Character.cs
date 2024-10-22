using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public enum EmotionType
{
    Default,
    ArmCrossed_Smile,
    ArmCrossed_Energetic,
    ArmCrossed_YeahRight,           
    ArmCrossed_Sad,  
    ArmCrossed_Angry,  
    OneHandRaised_Shy,  
}


// 캐릭터의 감정을 관리하는 클래스
[Serializable]
public class EmotionData
{
    public EmotionType emotionType;   // 감정의 enum (예: EmotionType.Smile)
    public Emotion emotionPrefab;           // 실제 Emotion 객체
}

public class Character : MonoBehaviour
{
    private string _characterID;  // 캐릭터 이름 (예: "Kate", "Lisa")
    private bool _isJumping = false;  // 점프 중복 방지 플래그


    // 감정 목록 (각 캐릭터의 감정을 관리)
    private List<EmotionData> _emotionDatas;

    private Emotion _currentEmotion;

    public void Init(string characterID, EmotionType initialEmotionType)
    {
        // 감정 데이터를 초기화
        _emotionDatas = new List<EmotionData>();

        // EmotionType에 해당하는 프리팹들을 로드하여 EmotionData 리스트에 추가
        foreach (EmotionType emotionType in Enum.GetValues(typeof(EmotionType)))
        {
            // ResourceID는 characterID와 emotionType을 조합하여 생성
            // 경로 생성: characterID/EmotionType 경로에 해당하는 프리팹을 불러옴
            string resourcePath = $"CharacterManager/Emotions/{characterID}/{emotionType.ToString()}";
            Emotion emotionPrefab = Resources.Load<Emotion>(resourcePath);

            if (emotionPrefab != null)
            {
                // EmotionData 생성 및 리스트에 추가
                var emotionData = new EmotionData
                {
                    emotionType = emotionType,
                    emotionPrefab = emotionPrefab
                };
                _emotionDatas.Add(emotionData);
            }
            else
            {
                Debug.LogWarning($"Emotion prefab not found at path: {resourcePath}");
            }
        }

        _characterID = characterID;
        ChangeEmotion(initialEmotionType);
    }
    public void Jump(float jumpHeight, float duration)
    {
        // 중복 방지 플래그 설정
        if(_isJumping){
            Debug.LogWarning("점프중 점프 호출");
            return;
        }
        _isJumping = true;

        // 기존 트윈 종료
        transform.DOKill();

        // 원래의 localPosition
        Vector3 originalPos = transform.localPosition;

        // localPosition.y를 jumpHeight만큼 증가시키고 다시 원래 위치로 돌아오는 커스텀 점프 애니메이션
        transform.DOLocalMoveY(originalPos.y + jumpHeight, duration / 2)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                transform.DOLocalMoveY(originalPos.y, duration / 2).SetEase(Ease.InQuad)
                    .OnComplete(() =>
                    {
                        // 점프 완료 후 플래그 해제
                        _isJumping = false;
                    });
            });
    }
    // 감정 변경 함수
    public void ChangeEmotion(EmotionType? emotionType, float fadeDuration = .3f)
    {
        // 현재 활성화된 감정을 비활성화
        if (_currentEmotion != null)
        {
            _currentEmotion.FadeOutAndDestroy(fadeDuration);
        }

        // 현재 감정을 emotionType을 기반으로 찾기
        var emotionData = GetEmotionData(emotionType);
        // 새로운 감정을 활성화
        var emotion = GameObject.Instantiate(emotionData.emotionPrefab.gameObject, transform).GetComponent<Emotion>();
        _currentEmotion = emotion;
        _currentEmotion.FadeInThenActivate(fadeDuration);
    }

    // 특정 감정을 emotionType으로 찾는 함수
    private EmotionData GetEmotionData(EmotionType? emotionType)
    {
        foreach (var characterEmotion in _emotionDatas)
        {
            if (characterEmotion.emotionType == emotionType)
            {
                return characterEmotion;
            }
        }
        Debug.LogWarning($"Emotion '{emotionType}' not found for character '{_characterID}'");
        return null;
    }
    public void FadeOutAndDestroy(float duration){
        _currentEmotion.FadeOutAndDestroy(duration);
        Destroy(gameObject, duration);
    }
    public void StartTalking(){
        _currentEmotion.StartTalking();
    }
    public void StopTalking(){
        _currentEmotion.StopTalking();
    }

}
