using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemBtn : MonoBehaviour
{
    [SerializeField] private Image _itemImage;               // 아이템 이미지
    [SerializeField] private TextMeshProUGUI _itemNameText;  // 아이템 이름 표시용 TextMeshProUGUI
    [SerializeField] private Image _outlineImg;              // 선택된 상태를 표시할 외곽선 이미지
    [SerializeField] private Button _itemButton;             // 아이템 버튼 자체

    private ItemData _itemData;                              // 아이템 데이터
    private ItemInventoryWindow _inventoryWindow;                // InventoryWindow 참조

    // 아이템 데이터에 대한 읽기 전용 프로퍼티
    public ItemData ItemData => _itemData;

    // 아이템 버튼 초기화
    public void Init(ItemData data, ItemInventoryWindow inventoryWindow)
    {
        _itemData = data;
        _inventoryWindow = inventoryWindow;

        // 아이템 이미지 및 이름 설정
        _itemImage.sprite = _itemData.Spr;
        _itemNameText.text = _itemData.ItemName_KO;

        // 외곽선 이미지를 기본적으로 비활성화
        _outlineImg.enabled = false;

        // 버튼에 클릭 리스너 추가
        _itemButton.onClick.RemoveAllListeners(); // 중복 방지
        _itemButton.onClick.AddListener(OnItemSelected); // 클릭 시 OnItemSelected 실행
    }

    // 아이템 선택 시 동작
    private void OnItemSelected()
    {
        // InventoryWindow로 아이템 데이터 전달 및 선택 상태 처리
        _inventoryWindow.SelectItem(this);  // 현재 아이템 버튼을 선택한 것으로 처리
    }

    // 외곽선 이미지를 활성화 또는 비활성화하는 메서드
    public void SetOutline(bool isActive)
    {
        _outlineImg.enabled = isActive;
    }
}
