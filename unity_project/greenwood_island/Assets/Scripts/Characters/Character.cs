using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public enum KateEmotionID
{
    ArmCrossed_Smile,
    ArmCrossed_Energetic,
    ArmCrossed_YeahRight,           
    ArmCrossed_Sad,  
    ArmCrossed_Angry,  
    OneHandRaised_Shy,  
}
public enum CommonEmotionID{
    Default,
}
public enum AmalianEmotionID
{
    HandsTogether_Smile,
    HandsTogether_Satisfied,
}
public class Character : MonoBehaviour
{
    private string _characterID;  // 캐릭터 이름 (예: "Kate", "Lisa")
    private bool _isJumping = false;  // 점프 중복 방지 플래그


    // 감정 목록 (각 캐릭터의 감정을 관리)
    private Dictionary<string, Emotion> _emotionDictionary;

    private Emotion _currentEmotion;

    public void Highlight(bool b, float duration){
        if(_currentEmotion == null){
            Debug.LogWarning("_currentEmotion is null");
            return;
        }
        _currentEmotion.Highlight(b, duration);
    }
    
    public void Init(string characterID, string initialEmotionID)
    {
        // 감정 데이터를 초기화
        _emotionDictionary = new Dictionary<string, Emotion>();

        // 해당 폴더에 있는 모든 Emotion 프리팹 로드
        string resourceFolderPath = $"CharacterManager/Emotions/{characterID}";
        Emotion[] emotionPrefabs = Resources.LoadAll<Emotion>(resourceFolderPath);

        // 불러온 Emotion 프리팹들을 순회하여 딕셔너리에 추가
        foreach (Emotion emotionPrefab in emotionPrefabs)
        {
            string emotionID = emotionPrefab.name; // Emotion 프리팹의 이름을 emotionID로 사용
            _emotionDictionary[emotionID] = emotionPrefab; // 딕셔너리에 저장
        }

        if (_emotionDictionary.Count == 0)
        {
            Debug.LogWarning($"No Emotion prefabs found in path: {resourceFolderPath}");
        }

        _characterID = characterID;
        ChangeEmotion(initialEmotionID); // 전달된 emotionID로 변경
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
    public void ChangeEmotion(string emotionID, float fadeDuration = .3f)
    {
        // 현재 활성화된 감정을 비활성화
        if (_currentEmotion != null)
        {
            _currentEmotion.FadeOutAndDestroy(fadeDuration);
        }

        // 딕셔너리에서 emotionID를 기반으로 감정 프리팹을 찾기
        if (_emotionDictionary.TryGetValue(emotionID, out Emotion emotionPrefab))
        {
            // 새로운 감정을 활성화
            var emotion = GameObject.Instantiate(emotionPrefab.gameObject, transform).GetComponent<Emotion>();
            _currentEmotion = emotion;
            _currentEmotion.FadeInThenActivate(fadeDuration);
        }
        else
        {
            Debug.LogWarning($"Emotion ID {emotionID} not found in the dictionary.");
        }
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
