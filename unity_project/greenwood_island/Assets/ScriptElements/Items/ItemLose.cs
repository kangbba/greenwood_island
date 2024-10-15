using System.Collections;
using UnityEngine;

public class ItemLose : Element
{
    private string _itemID;  // 잃게 될 아이템의 ID

    // 생성자: 아이템 ID와 스토리 데이터를 초기화
    public ItemLose(string itemID)
    {
        _itemID = itemID;
    }

    // ExecuteRoutine: 아이템을 잃고 인벤토리 창을 열어 확인
    public override IEnumerator ExecuteRoutine()
    {
        // 아이템 잃기 처리
        if (ItemManager.HasItem(_itemID))
        {
            ItemManager.RemoveItem(_itemID, GameDataManager.CurrentStorySavedData);
            Debug.Log($"아이템 '{_itemID}'이(가) 제거되었습니다.");
        }
        else
        {
            Debug.LogWarning($"아이템 '{_itemID}'이(가) 존재하지 않습니다.");
        }
        yield break;
    }

    // 즉시 실행: ExecuteRoutine을 코루틴 없이 바로 실행
    public override void ExecuteInstantly()
    {
        if (ItemManager.HasItem(_itemID))
        {
            ItemManager.RemoveItem(_itemID, GameDataManager.CurrentStorySavedData);
            Debug.Log($"아이템 '{_itemID}'이(가) 즉시 제거되었습니다.");
        }
        else
        {
            Debug.LogWarning($"아이템 '{_itemID}'이(가) 존재하지 않습니다.");
        }
    }
}
