using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveBtn : MonoBehaviour
{
    [SerializeField] private Button _btn;

    private void Start()
    {
        _btn.onClick.AddListener(() =>
        {

            StorySavedData tempSaveData = new StorySavedData(StoryManager.Instance.CurrentStoryName, "퀵 세이브", StoryManager.Instance.CurrentElementIndex, StoryManager.Instance.CurrentStory.UpdateElements.Count);

            UIManager.PopupCanvas.ShowSaveWindow(tempSaveData);

            // // StorySavedData가 없으면 SaveLoadWindowPrefab을 로드하여 세이브 모드로 창을 띄움
            // if (GameDataManager.CurrentStorySavedData == null)
            // {
            // }
            // else
            // {
            //     StorySavedData newStorySavedData = GameDataManager.CurrentStorySavedData;
            //     // 스토리 이름과 elementIndex를 사용하여 StorySavedData를 업데이트
            //     newStorySavedData.storyID = StoryManager.Instance.CurrentStoryName;
            //     newStorySavedData.recentPlayedElementIndex = StoryManager.Instance.CurrentElementIndex;

            //     // 현재 슬롯 인덱스를 사용하여 게임 데이터를 저장
            //     GameDataManager.SaveGameData(newStorySavedData, GameDataManager.CurrentSlotIndex);
            // }
        });
    }
}
