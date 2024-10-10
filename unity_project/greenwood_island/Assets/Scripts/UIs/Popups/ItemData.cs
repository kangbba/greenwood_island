using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "Inventory/ItemData")]
public class ItemData : ScriptableObject
{
    [SerializeField] private string _itemID;             // 아이템 고유 식별 ID
    [SerializeField] private string _itemName_KO;        // 아이템 이름 (한글)
    [SerializeField] private string _description;        // 아이템 설명
    [SerializeField] private Sprite _spr;                // 아이템 대표 이미지

    // 프로퍼티를 통해 외부에서 접근 가능하게 설정
    public string ItemID => _itemID;
    public string ItemName_KO => _itemName_KO;
    public string Description => _description;
    public Sprite Spr => _spr;
}
