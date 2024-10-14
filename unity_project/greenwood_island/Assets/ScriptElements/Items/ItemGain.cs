using System.Collections;
using UnityEngine;

public class ItemGain : Element
{
    private string _itemID;  // 획득할 아이템의 ID

    // 생성자: 아이템 ID와 스토리 데이터를 초기화
    public ItemGain(string itemID)
    {
        _itemID = itemID;
    }

    // ExecuteRoutine: 아이템을 추가하고 인벤토리 창을 열어 확인
    public override IEnumerator ExecuteRoutine()
    {
        // 아이템 추가
        ItemManager.AddItem(_itemID, GameDataManager.CurrentStorySavedData);
        Debug.Log($"아이템 '{_itemID}'이(가) 추가되었습니다.");
        // 인벤토리 창 열기 및 닫히기 대기
        yield return CoroutineUtils.StartCoroutine(UIManager.PopupCanvas.OpenInventoryViewModeCoroutine(() =>
        {
            Debug.Log("인벤토리 창 닫힘 이후 실행되는 코드");
        }));

    }

    // 즉시 실행: ExecuteRoutine을 코루틴 없이 바로 실행
    public override void ExecuteInstantly()
    {
        ItemManager.AddItem(_itemID, GameDataManager.CurrentStorySavedData);
        Debug.Log($"아이템 '{_itemID}'이(가) 즉시 추가되었습니다.");
    }
}
