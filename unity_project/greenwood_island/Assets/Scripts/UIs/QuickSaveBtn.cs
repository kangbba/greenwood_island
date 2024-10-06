using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSaveBtn : MonoBehaviour
{
    [SerializeField] private Button _btn;

    private void Start()
    {
        _btn.onClick.AddListener(() =>
        {

            // GameSaveData가 없으면 SaveLoadWindowPrefab을 로드하여 세이브 모드로 창을 띄움
            if (GameDataManager.CurrentGameSaveData == null)
            {
                // Resources 폴더에서 SaveLoadWindowPrefab을 로드
                var _saveLoadWindowPrefab = UIManager.SaveLoadWindowPrefab;

                if (_saveLoadWindowPrefab != null)
                {
                    // SaveLoadWindowPrefab 인스턴스화
                    SaveLoadWindow saveLoadWindowInstance = Instantiate(_saveLoadWindowPrefab, UIManager.PopupCanvas.transform);

                    // 세이브 모드로 초기화하여 세이브할 데이터를 넘겨줌
                    GameSaveData tempSaveData = new GameSaveData(StoryManager.Instance.CurrentStoryName, new Dictionary<string, int>(), "퀵 세이브");
                    saveLoadWindowInstance.Init(true, tempSaveData); // 세이브 모드로 init
                }
                else
                {
                    Debug.LogError("SaveLoadWindowPrefab을 찾을 수 없습니다.");
                }
            }
            else
            {
                GameSaveData newGameSaveData = GameDataManager.CurrentGameSaveData.DeepClone();
                // 스토리 이름과 elementIndex를 사용하여 GameSaveData를 업데이트
                newGameSaveData.storyID = StoryManager.Instance.CurrentStoryName;
                newGameSaveData.elementIndex = StoryManager.Instance.CurrentElementIndex;

                // 현재 슬롯 인덱스를 사용하여 게임 데이터를 저장
                GameDataManager.SaveGameData(newGameSaveData, GameDataManager.CurrentSlotIndex);
            }
        });
    }
}
