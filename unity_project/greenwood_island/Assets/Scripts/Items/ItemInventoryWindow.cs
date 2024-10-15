using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class ItemInventoryWindow : MonoBehaviour
{
    public enum InventoryMode
    {
        ViewMode,       // 확인 모드
        SubmitMode      // 제출 모드
    }

    private ItemData _submittedItem; // 제출된 아이템 저장

    [SerializeField] private Transform _itemContainer;        
    [SerializeField] private GameObject _itemButtonPrefab;    
    [SerializeField] private TextMeshProUGUI _itemDescription; 
    [SerializeField] private Button _exitButton;              
    [SerializeField] private Button _submitButton;             

    private InventoryMode _currentMode;                       
    private IReadOnlyList<ItemData> _inventoryItemDatas;      
    private List<ItemBtn> _itemBtns = new List<ItemBtn>();    
    private ItemBtn _selectedItemBtn;

    private bool _isExitBtnClicked = false;

    public ItemData SubmittedItem { get => _submittedItem; }
    public bool IsExitBtnClicked { get => _isExitBtnClicked; }

    // 인벤토리 초기화 함수
    public void Init(IReadOnlyList<ItemData> inventoryItemDatas, InventoryMode mode)
    {
        _inventoryItemDatas = inventoryItemDatas;
        _currentMode = mode;
        _submittedItem = null;
        _isExitBtnClicked = false;

        PopulateItems();
        SetupUIForMode();

        if (_itemBtns.Count > 0)
        {
            SelectItem(_itemBtns.Last());
        }
    }

    private void SetupUIForMode()
    {
        if (_currentMode == InventoryMode.ViewMode)
        {
            _exitButton.gameObject.SetActive(true);
            _submitButton.gameObject.SetActive(false);

            _exitButton.onClick.RemoveAllListeners();
            _exitButton.onClick.AddListener(() => _isExitBtnClicked = true);
        }
        else if (_currentMode == InventoryMode.SubmitMode)
        {
            _exitButton.gameObject.SetActive(false);
            _submitButton.gameObject.SetActive(true);

            _submitButton.onClick.RemoveAllListeners();
            _submitButton.onClick.AddListener(OnSubmitItem);
        }
    }

    private void PopulateItems()
    {
        _itemContainer.DestroyAllChildren();
        _itemBtns.Clear();

        int index = 0;
        foreach (var item in _inventoryItemDatas)
        {
            GameObject itemBtnObject = Instantiate(_itemButtonPrefab, _itemContainer);
            ItemBtn itemBtn = itemBtnObject.GetComponent<ItemBtn>();
            itemBtn.transform.localPosition = Vector2.right * 300 * index;
            itemBtn.Init(item, this);

            _itemBtns.Add(itemBtn);
            index++;
        }
    }

    public void DisplayItemDetails(ItemData selectedItem)
    {
        if (selectedItem != null)
        {
            _itemDescription.text = $"ID: {selectedItem.ItemID}\n{selectedItem.Description}";
        }
        else
        {
            _itemDescription.text = "아이템을 선택하세요.";
        }
    }

    public void SelectItem(ItemBtn newSelectedItemBtn)
    {
        foreach (var itemBtn in _itemBtns)
        {
            itemBtn.SetOutline(itemBtn == newSelectedItemBtn);
        }

        _selectedItemBtn = newSelectedItemBtn;

        if (newSelectedItemBtn != null)
        {
            DisplayItemDetails(newSelectedItemBtn.ItemData);
        }
    }

    private void OnSubmitItem()
    {
        if (_selectedItemBtn != null)
        {
            _submittedItem = _selectedItemBtn.ItemData;  // 선택된 아이템 저장
            Debug.Log($"아이템 제출됨: {_submittedItem.ItemID}");
        }
        else
        {
            Debug.LogWarning("제출할 아이템이 선택되지 않았습니다.");
        }
    }
}
