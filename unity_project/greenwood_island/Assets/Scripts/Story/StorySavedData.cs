using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StorySavedData
{
    // 자동 구현 프로퍼티로 변경
    public string StoryID { get; set; }    // 스토리 ID
    public int RecentPlayedElementIndex { get; set; } // 최근 플레이한 Element 인덱스
    public int ElementTotalCount { get; set; } // 최근 플레이한 Element 카운트
    public string SaveMemo { get; set; }   // 저장 메모
    public string SaveTimeString { get; set; } // 저장된 시간을 문자열로 저장

    // 아이템 보유 여부를 저장하는 Dictionary (아이템 ID -> 보유 여부, string ID로 변경)
    public Dictionary<string, bool> OwnItems { get; set; } = new Dictionary<string, bool>();

    // 저장 시간 문자열을 DateTime으로 변환해 반환하는 프로퍼티
    public DateTime SaveTime => DateTime.TryParse(SaveTimeString, out DateTime result) ? result : DateTime.MinValue;

    // 매개변수가 있는 생성자
    public StorySavedData(string storyID, string saveMemo, int recentPlayedElementIndex, int elementTotalCount, DateTime saveTime = default)
    {
        this.StoryID = storyID;
        this.SaveMemo = saveMemo;
        this.RecentPlayedElementIndex = recentPlayedElementIndex;  
        this.ElementTotalCount = elementTotalCount;  
        this.SaveTimeString = (saveTime == default ? DateTime.Now : saveTime).ToString("yyyy-MM-dd HH:mm:ss");
        this.OwnItems = new Dictionary<string, bool>(); // 아이템 초기화
    }
}
