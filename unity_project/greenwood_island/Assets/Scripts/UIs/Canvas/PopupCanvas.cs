using UnityEngine;
using System.Collections.Generic;

public class PopupCanvas : MonoBehaviour
{
    [SerializeField] private Transform popupParent;
    [SerializeField] private YesNoPopup _yesNoPopupPrefab;
    [SerializeField] private OkPopup _okPopupPrefab;
    [SerializeField] private SaveLoadWindow _saveLoadWindowPrefab;
    [SerializeField] private ItemInventoryWindow _itemInventoryWindowPrefab;

    public bool IsPoppedUp => popupParent.childCount > 0;
    private SaveLoadWindow _loadGameWindow;  // 인스턴스화된 불러오기 창

    public void OpenInventorySubmitMode(string targetItemID)
    {
        // 인벤토리 창을 제출 모드로 열고, 제출된 아이템을 콜백으로 받음
        ItemInventoryWindow itemInventory = Instantiate(_itemInventoryWindowPrefab, popupParent);

        // 콜백에 itemID와 제출된 아이템의 ID를 비교하는 로직을 넣음
        itemInventory.Init(ItemManager.GetAllItems(), ItemInventoryWindow.InventoryMode.SubmitMode, (submittedItem) => CompareWithItemID(submittedItem, targetItemID));
    }

    public void OpenInventoryViewMode()
    {
        // 인벤토리 창을 확인 모드(ViewMode)로 열기
        ItemInventoryWindow itemInventory = Instantiate(_itemInventoryWindowPrefab, popupParent);

        // 확인 모드에서는 콜백이 필요 없으므로 null 전달
        itemInventory.Init(ItemManager.GetAllItems(), ItemInventoryWindow.InventoryMode.ViewMode, null);
    }

    // 제출된 아이템을 검증하는 콜백 함수
    void CompareWithItemID(ItemData submittedItem, string targetItemID)
    {
        if (submittedItem.ItemID == targetItemID)
        {
            // 아이템 ID가 일치할 때의 처리
            Debug.Log($"정확한 아이템이 제출되었습니다! ID: {submittedItem.ItemID}, 설명: {submittedItem.Description}");
        }
        else
        {
            // 아이템 ID가 일치하지 않을 때의 처리
            Debug.Log($"다른 아이템이 제출되었습니다! 제출된 ID: {submittedItem.ItemID}, 기대한 ID: {targetItemID}");
        }
    }

    // 예/아니오 팝업 호출 메서드
    public void ShowYesNoPopup(string message, string yesText, string noText, System.Action onYesAction)
    {
        YesNoPopup popupInstance = Instantiate(_yesNoPopupPrefab, popupParent);
        popupInstance.Init(message, yesText, noText, onYesAction);
    }

    // 확인 팝업 호출 메서드
    public void ShowOkPopup(string message, string okText, System.Action onOkAction)
    {
        OkPopup popupInstance = Instantiate(_okPopupPrefab, popupParent);
        popupInstance.Init(message, okText, onOkAction);
    }

    // Save/Load 팝업 호출 메서드 (단일 인스턴스만 허용)
    public void ShowLoadWindow()
    {
        // 이미 인스턴스화된 창이 없으면 새로 인스턴스화
        if (_loadGameWindow == null)
        {
        }
        _loadGameWindow = Instantiate(_saveLoadWindowPrefab, popupParent);
        _loadGameWindow.Init(isSaveMode: false, null);  // 로드 모드
    }
    // Save/Load 팝업 호출 메서드 (단일 인스턴스만 허용)
    public void ShowSaveWindow(StorySavedData storySavedData)
    {
        // 이미 인스턴스화된 창이 없으면 새로 인스턴스화
        if (_loadGameWindow == null)
        {
        }
        _loadGameWindow = Instantiate(_saveLoadWindowPrefab, popupParent);
        _loadGameWindow.Init(isSaveMode: true, storySavedData);  // 로드 모드
    }

    public void Clear(){
        popupParent.DestroyAllChildren();
    }
}