using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class StorySavedData
{
    public string storyID;    // 스토리 ID
    public int recentPlayedElementIndex; // 최근 플레이한 Element 인덱스
    public int elementTotalCount; // 최근 플레이한 Element 카운트
    public string saveMemo;   // 저장 메모
    public Dictionary<string, int> savedChoiceResult =  new Dictionary<string, int>(); // ChoiceSet에서 선택한 인덱스를 저장하기 위한 딕셔너리
    // UserActionID에 해당하는 수행된 ActionType 리스트를 저장
    public Dictionary<string, List<UserActionType>> userActionHistory = new Dictionary<string, List<UserActionType>>();
    public string saveTimeString; // 저장된 시간을 문자열로 저장

    // 저장 시간 문자열을 DateTime으로 변환해 반환하는 프로퍼티
    public DateTime SaveTime => DateTime.TryParse(saveTimeString, out DateTime result) ? result : DateTime.MinValue;

   // 매개변수가 있는 생성자
    public StorySavedData(string storyID, string saveMemo, int recentPlayedElementIndex, int elementTotalCount, DateTime saveTime = default)
    {
        this.storyID = storyID;
        this.saveMemo = saveMemo;
        this.recentPlayedElementIndex = recentPlayedElementIndex;  // 생성자 인자로 받은 값
        this.elementTotalCount = elementTotalCount;  // 생성자 인자로 받은 값
        this.savedChoiceResult = new Dictionary<string, int>();
        this.userActionHistory = new Dictionary<string, List<UserActionType>>();
        this.saveTimeString = (saveTime == default ? DateTime.Now : saveTime).ToString("yyyy-MM-dd HH:mm:ss");
    }


    // storyID 변경 함수
    public StorySavedData GetModifiedStoryID(string newStoryID)
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

    // 선택된 인덱스를 저장하는 메서드
    public void SaveChoiceSetIndex(string choiceSetID, int chosenIndex)
    {
        if (!savedChoiceResult.ContainsKey(choiceSetID))
        {
            savedChoiceResult.Add(choiceSetID, chosenIndex);
        }
        else
        {
            savedChoiceResult[choiceSetID] = chosenIndex;
        }

        // 저장된 상태를 출력
        Debug.Log($"*** {choiceSetID}에 대한 선택지 {chosenIndex} 저장되었습니다.");
        Debug.Log($"저장된 데이터: {string.Join(", ", savedChoiceResult.Select(kv => kv.Key + ": " + kv.Value))}");
    }

    // 특정 ChoiceSet의 저장된 선택 인덱스를 불러오는 메서드
    public int LoadChoiceSetIndex(string choiceSetID)
    {
        return savedChoiceResult.TryGetValue(choiceSetID, out int chosenIndex) ? chosenIndex : -1;
    }

    // 특정 UserActionID에 ActionType을 추가하며, 해당 기록을 남기는 메서드
    public void AddUserActionTypeToHistory(string userActionID, UserActionType actionType)
    {
        if (!userActionHistory.ContainsKey(userActionID))
        {
            userActionHistory[userActionID] = new List<UserActionType>();
        }

        // 이미 해당 ActionType이 추가되지 않았다면 추가
        if (!userActionHistory[userActionID].Contains(actionType))
        {
            userActionHistory[userActionID].Add(actionType);
        }
    }

    // 특정 UserActionID에 해당하는 ActionType 기록을 반환하는 메서드
    public List<UserActionType> GetUserActionTypeFromHistory(string userActionID)
    {
        if (userActionHistory.ContainsKey(userActionID))
        {
            return userActionHistory[userActionID];
        }

        return new List<UserActionType>(); // 없다면 빈 리스트 반환
    }
}
