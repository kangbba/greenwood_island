using System.Collections.Generic;
using UnityEngine;

public static class ItemManager
{
    private static List<ItemData> _allItems;
    private const string ResourcesFolderPath = "ItemManager"; // Resources 폴더 경로

    // 정적 생성자 (ItemManager 클래스가 처음 사용될 때 호출)
    static ItemManager()
    {
        // 첫 사용 시 아이템 데이터를 로드
        if (_allItems == null)
        {
            LoadAllItems(); // _allItems가 null인 경우에만 로드
        }
    }

    // 모든 아이템을 첫 호출 시 Resources 폴더에서 불러옵니다.
    private static void LoadAllItems()
    {
        if (_allItems == null)
        {
            Debug.Log("ItemManager: 처음으로 아이템 데이터를 로드합니다.");

            // Resources 폴더에서 아이템 데이터를 불러오기 (스크립터블 오브젝트)
            _allItems = new List<ItemData>(Resources.LoadAll<ItemData>(ResourcesFolderPath));

            if (_allItems == null || _allItems.Count == 0)
            {
                Debug.LogError("ItemManager: Resources 폴더에서 아이템 데이터를 불러오지 못했습니다.");
            }
            else
            {
                Debug.Log($"ItemManager: {_allItems.Count}개의 아이템 데이터가 성공적으로 로드되었습니다.");
            }
        }
        else
        {
            Debug.Log("ItemManager: 이미 아이템 데이터가 로드되어 있습니다.");
        }
    }

    // 특정 itemID를 가진 ItemData를 반환하는 메서드 (string으로 변경)
    public static ItemData GetItemData(string itemID)
    {
        // _allItems는 처음 클래스 초기화 시 이미 로드됨
        ItemData item = _allItems.Find(item => item.ItemID == itemID);
        if (item != null)
        {
            Debug.Log($"ItemManager: 아이템 '{itemID}'를 찾았습니다.");
            return item;
        }
        else
        {
            Debug.LogWarning($"ItemManager: 아이템 '{itemID}'를 찾을 수 없습니다.");
            return null;
        }
    }

    // 모든 아이템 리스트를 반환하는 메서드 (외부에서 수정하지 못하도록 Read-Only 리스트 반환)
    public static IReadOnlyList<ItemData> GetAllItems()
    {
        return _allItems.AsReadOnly(); // Read-Only 리스트로 반환
    }

    // 해당 itemID가 StorySavedData의 OwnItems에 존재하는지 확인하는 메서드
    public static bool HasItem(string itemID, StorySavedData storyData)
    {
        return storyData.OwnItems.TryGetValue(itemID, out bool isOwned) && isOwned;
    }

    // 아이템을 획득하고 StorySavedData의 OwnItems에 저장
    public static void AddItem(string itemID, StorySavedData storyData)
    {
        if (!HasItem(itemID, storyData))
        {
            storyData.OwnItems[itemID] = true;
            Debug.Log($"ItemManager: 아이템 ID {itemID}가 추가되었습니다.");
        }
    }

    // 아이템을 제거하고 StorySavedData의 OwnItems에서 제거
    public static void RemoveItem(string itemID, StorySavedData storyData)
    {
        if (HasItem(itemID, storyData))
        {
            storyData.OwnItems.Remove(itemID);
            Debug.Log($"ItemManager: 아이템 ID {itemID}가 제거되었습니다.");
        }
    }
}
