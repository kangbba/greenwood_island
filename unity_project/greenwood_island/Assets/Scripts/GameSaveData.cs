using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class GameSaveData
{
    // 유저가 지정한 저장 이름
    public string saveName;

    // 현재 진행 중인 스토리의 ID (예: "StoryID_001")
    public string storyID;

    public int elementIndex;

    // 각 캐릭터별 신뢰도 상태
    public Dictionary<string, int> trustLevels;

    // 생성자
    public GameSaveData(string saveName, string storyID, Dictionary<string, int> trustLevels)
    {
        this.saveName = saveName;
        this.storyID = storyID;
        this.trustLevels = new Dictionary<string, int>(trustLevels);
    }

    // storyID 변경 함수, 변경된 객체 반환
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
