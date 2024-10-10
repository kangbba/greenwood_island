using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System;

public class ItemInventoryWindow : MonoBehaviour
{
    public enum InventoryMode
    {
        ViewMode,       // 확인 모드
        SubmitMode      // 제출 모드
    }

    [SerializeField] private Transform _itemContainer;        // 스크롤뷰 안의 컨텐츠 부모 객체
    [SerializeField] private GameObject _itemButtonPrefab;    // 아이템 버튼 프리팹
    [SerializeField] private TextMeshProUGUI _itemDescription; // 아이템 설명을 표시할 TextMeshProUGUI
    [SerializeField] private Button _exitButton;              // 확인 모드에서 사용되는 나가기 버튼
    [SerializeField] private Button _submitButton;            // 제출 모드에서 사용되는 제출 버튼

    private InventoryMode _currentMode;                       // 현재 인벤토리 모드
    private IReadOnlyList<ItemData> _inventoryItemDatas;      // 인벤토리 아이템 목록
    private List<ItemBtn> _itemBtns = new List<ItemBtn>();    // 생성된 ItemBtn을 저장하는 리스트
    private ItemBtn _selectedItemBtn;                         // 현재 선택된 아이템 버튼
    private Action<ItemData> _onSubmitCallback;               // 아이템 제출 시 호출할 콜백 함수

    public ItemBtn SelectedItemBtn { get => _selectedItemBtn; }

    // 인벤토리 초기화 함수
    public void Init(IReadOnlyList<ItemData> inventoryItemDatas, InventoryMode mode, Action<ItemData> onSubmitCallback = null)
    {
        _inventoryItemDatas = inventoryItemDatas;
        _currentMode = mode;
        _onSubmitCallback = onSubmitCallback;

        // UI 초기화
        PopulateItems();
        SetupUIForMode();

        // 리스트의 마지막 아이템을 선택 처리
        if (_itemBtns.Count > 0)
        {
            SelectItem(_itemBtns.Last());
        }
    }

    // UI 모드에 따른 버튼 설정
    private void SetupUIForMode()
    {
        if (_currentMode == InventoryMode.ViewMode)
        {
            // 확인 모드 - 나가기 버튼 활성화, 제출 버튼 비활성화
            _exitButton.gameObject.SetActive(true);
            _submitButton.gameObject.SetActive(false);

            // 나가기 버튼 클릭 시 UI 파괴
            _exitButton.onClick.RemoveAllListeners();
            _exitButton.onClick.AddListener(() => Destroy(gameObject));
        }
        else if (_currentMode == InventoryMode.SubmitMode)
        {
            // 제출 모드 - 제출 버튼 활성화, 나가기 버튼 비활성화
            _exitButton.gameObject.SetActive(false);
            _submitButton.gameObject.SetActive(true);

            // 제출 버튼 클릭 시 아이템 제출 처리
            _submitButton.onClick.RemoveAllListeners();
            _submitButton.onClick.AddListener(OnSubmitItem);
        }
    }

    // 아이템 버튼을 동적으로 생성 및 배치
    private void PopulateItems()
    {
        // 기존의 자식들을 모두 제거
        _itemContainer.DestroyAllChildren();

        // 리스트를 초기화
        _itemBtns.Clear();

        // 아이템 버튼을 동적 생성하여 배치
        int index = 0;
        foreach (var item in _inventoryItemDatas)
        {
            // 아이템 버튼 인스턴스화
            GameObject itemBtnObject = Instantiate(_itemButtonPrefab, _itemContainer);

            // 버튼 초기화 (ItemData 및 InventoryWindow 전달)
            ItemBtn itemBtn = itemBtnObject.GetComponent<ItemBtn>();
            itemBtn.transform.localPosition = Vector2.right * 300 * index;
            itemBtn.Init(item, this);

            // 리스트에 아이템 버튼 추가
            _itemBtns.Add(itemBtn);
            index++;
        }
    }

    // 선택된 아이템의 세부 정보를 표시
    public void DisplayItemDetails(ItemData selectedItem)
    {
        if (selectedItem != null)
        {
            // 아이템 설명 업데이트
            _itemDescription.text = $"ID: {selectedItem.ItemID}\n{selectedItem.Description}";
        }
        else
        {
            // 선택된 아이템이 없을 때 설명 초기화
            _itemDescription.text = "아이템을 선택하세요.";
        }
    }

    // 아이템 버튼 선택 처리
    public void SelectItem(ItemBtn newSelectedItemBtn)
    {
        // 모든 아이템 버튼 순회
        foreach (var itemBtn in _itemBtns)
        {
            // 선택된 아이템과 일치하면 외곽선을 켜고, 그렇지 않으면 끔
            itemBtn.SetOutline(itemBtn == newSelectedItemBtn);
        }

        // 현재 선택된 아이템 버튼 업데이트
        _selectedItemBtn = newSelectedItemBtn;

        // 선택된 아이템의 세부 정보를 업데이트
        if (newSelectedItemBtn != null)
        {
            DisplayItemDetails(newSelectedItemBtn.ItemData);
        }
    }

    // 제출 모드에서 아이템 제출 처리
    private void OnSubmitItem()
    {
        if (_selectedItemBtn != null)
        {
            // 선택된 아이템을 외부로 방출 (콜백 호출)
            _onSubmitCallback?.Invoke(_selectedItemBtn.ItemData);

            // 필요한 동작(예: 선택된 아이템의 사용)을 실행하고, UI 파괴
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("제출할 아이템이 선택되지 않았습니다.");
        }
    }
}
