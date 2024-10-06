using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameSaveData
{
    public string saveMemo;   // 저장 메모
    public string storyID;
    public int elementIndex;
    public Dictionary<string, int> trustLevels;

    // 저장된 시간을 문자열로 저장
    public string saveTimeString;

    // 생성자
    public GameSaveData(string storyID, Dictionary<string, int> trustLevels, string saveMemo, DateTime saveTime = default)
    {
        this.storyID = storyID;
        this.trustLevels = new Dictionary<string, int>(trustLevels);
        this.saveMemo = saveMemo;

        // saveTime이 기본값이면 현재 시간을 저장
        this.saveTimeString = (saveTime == default ? DateTime.Now : saveTime).ToString("yyyy-MM-dd HH:mm:ss");
    }
    // 얕은 복사 메서드 (MemberwiseClone을 사용)
    public GameSaveData DeepClone()
    {
        // 딕셔너리의 키와 값을 새로 복사
        Dictionary<string, int> copiedTrustLevels = new Dictionary<string, int>(this.trustLevels);

        // 새롭게 GameSaveData 객체를 생성하여 반환 (딕셔너리와 값이 독립적)
        return new GameSaveData(this.storyID, copiedTrustLevels, this.saveMemo, DateTime.Parse(this.saveTimeString));
    }


    // 저장 시간 문자열을 DateTime으로 변환해 반환하는 프로퍼티
    public DateTime SaveTime
    {
        get
        {
            return DateTime.TryParse(saveTimeString, out DateTime result) ? result : DateTime.MinValue;
        }
    }

    // storyID 변경 함수
    public GameSaveData GetModifiedStoryID(string newStoryID)
    {
        if (!string.IsNullOrEmpty(newStoryID))
        {
            storyID = newStoryID;
            Debug.Log($"StoryID가 {newStoryID}로 변경되었습니다.");
        }
        else
        {
            Debug.LogError("유효하지 않은 StoryID입니다.");
        }

        return this; // 변경된 객체 반환
    }
}
