using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameSlot : MonoBehaviour
{
    [SerializeField] private Image _previewImg;   // 저장 이름을 표시할 이미지
    [SerializeField] private Sprite _storyDefaultThumbnail;  // 기본 썸네일 이미지
    [SerializeField] private Sprite _vacantStorySprite;  // 빈 슬롯 이미지
    
    [SerializeField] private TextMeshProUGUI _slotIndexText;   // 슬롯 인덱스를 표시할 텍스트    
    [SerializeField] private TextMeshProUGUI _storyNameText;    // 스토리 ID를 표시할 텍스트
    [SerializeField] private TextMeshProUGUI _saveTimeText;   // 저장 시간을 표시할 텍스트
    [SerializeField] private TextMeshProUGUI _saveMemoText;   // 저장 메모를 표시할 텍스트
    [SerializeField] private TextMeshProUGUI _filePathText;   // 파일 경로를 표시할 텍스트
    [SerializeField] private Button _slotBtn;             
    [SerializeField] private Button _deleteButton;            // 삭제 버튼

    private int _slotNumber;
    private GameSaveData _saveData;
    private Action _onClickAction;
    private Action _onDeleteAction;

    // 저장된 게임 데이터를 UI에 초기화하는 메서드
    public void Init(int slotNumber, GameSaveData saveData, Action onClickAction, Action onDeleteAction)
    {
        // 전달받은 데이터를 멤버 변수에 저장
        _slotNumber = slotNumber;
        _saveData = saveData;
        _onClickAction = onClickAction;
        _onDeleteAction = onDeleteAction;

        // 초기 UI 세팅을 위한 Refresh 호출
        Refresh();
    }

    // UI를 갱신하는 메서드
    public void Refresh()
    {
        _slotIndexText.SetText($"FILE {_slotNumber + 1}");
        if (_saveData == null)
        {
            // saveData가 null일 경우, 빈 슬롯으로 표시
            _previewImg.sprite = _vacantStorySprite;
            _storyNameText.text = "NO DATA";  // 스토리 없음
            _saveTimeText.text = "";
            _saveMemoText.text = "";
            _filePathText.text = GameDataManager.GetSaveFilePath(_slotNumber);  // 파일 경로는 표시
            _deleteButton.gameObject.SetActive(false);
        }
        else
        {
            // saveData가 있을 경우, 저장된 데이터 표시
            StoryData storyData = ResourcePathManager.GetStoryData(_saveData.storyID);
            _previewImg.sprite = storyData != null ? storyData.StoryThumbnail : _storyDefaultThumbnail;
            _storyNameText.text = _saveData.storyID;  // 나중에 ko로 변경
            _saveTimeText.text = _saveData.saveTimeString;
            _saveMemoText.text = _saveData.saveMemo;
            _filePathText.text = GameDataManager.GetSaveFilePath(_slotNumber);
            _deleteButton.gameObject.SetActive(true);
        }

        // 로드 버튼 클릭 시 실행할 액션을 설정
        _slotBtn.onClick.RemoveAllListeners();  // 기존 리스너 제거
        _slotBtn.onClick.AddListener(() => _onClickAction());

        // 삭제 버튼 클릭 시 실행할 액션을 설정
        _deleteButton.onClick.RemoveAllListeners();  // 기존 리스너 제거
        _deleteButton.onClick.AddListener(() => _onDeleteAction());
    }
}