using System.Collections;
using UnityEngine;

public class ItemDemand : Element
{
    private string _itemID;
    private SequentialElement _onMatchElement;     // 일치할 때 실행할 요소
    private SequentialElement _onMismatchElement;  // 일치하지 않을 때 실행할 요소

    // 생성자: 아이템 ID와 SequentialElement를 전달받음
    public ItemDemand(string itemID, SequentialElement onMatchElement, SequentialElement onMismatchElement)
    {
        _itemID = itemID;
        _onMatchElement = onMatchElement;
        _onMismatchElement = onMismatchElement;
    }
    // 생성자: 아이템 ID와 SequentialElement를 전달받음
    public ItemDemand(string itemID, Element onMatchElement, Element onMismatchElement)
    {
        _itemID = itemID;
        _onMatchElement = new SequentialElement(onMatchElement);
        _onMismatchElement = new SequentialElement(onMismatchElement);
    }

    public override void ExecuteInstantly()
    {
    }

    public override IEnumerator ExecuteRoutine()
    {
        // 팝업 생성 및 초기화
        ItemInventoryWindow itemInventory = UIManager.PopupCanvas.ShowItemInventoryWindow(
            ItemInventoryWindow.InventoryMode.SubmitMode
        );

        // 제출된 아이템이 설정될 때까지 대기
        yield return new WaitUntil(() => itemInventory.SubmittedItem != null);

        // 제출된 아이템과 기대한 아이템 비교
        bool isMatch = itemInventory.SubmittedItem.ItemID == _itemID;

        if (isMatch)
        {
            Debug.Log($"정확한 아이템 제출됨: {itemInventory.SubmittedItem.ItemID}");
            yield return _onMatchElement.ExecuteRoutine();  // 일치할 때 실행
        }
        else
        {
            Debug.Log($"잘못된 아이템 제출됨. 기대한 ID: {_itemID}, 제출된 ID: {itemInventory.SubmittedItem.ItemID}");
            yield return _onMismatchElement.ExecuteRoutine();  // 일치하지 않을 때 실행
        }

        // 팝업 파괴
        GameObject.Destroy(itemInventory.gameObject);
    }
}
