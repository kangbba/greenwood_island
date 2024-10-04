using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _saveNameText;   // 저장 이름을 표시할 텍스트
    [SerializeField] private TextMeshProUGUI _storyIDText;    // 스토리 ID를 표시할 텍스트
    [SerializeField] private TextMeshProUGUI _filePathText;   // 파일 경로를 표시할 텍스트
    [SerializeField] private Button _loadButton;              // 로드 버튼

    private int _slotNumber;
    private GameSaveData _saveData;
    private Action _onClickAction;

    // 저장된 게임 데이터를 UI에 초기화하는 메서드
    public void Init(int slotNumber, GameSaveData saveData, Action onClickAction)
    {
        // 전달받은 데이터를 멤버 변수에 저장
        _slotNumber = slotNumber;
        _saveData = saveData;
        _onClickAction = onClickAction;

        // 초기 UI 세팅을 위한 Refresh 호출
        Refresh();
    }

    // UI를 갱신하는 메서드
    public void Refresh()
    {
        if (_saveData == null)
        {
            // saveData가 null일 경우, 빈 슬롯으로 표시
            _saveNameText.text = "빈 슬롯";  // 빈 슬롯 텍스트
            _storyIDText.text = "No Story";  // 스토리 없음
            _filePathText.text = GameDataManager.GetSaveFilePath(_slotNumber);  // 파일 경로는 표시
            
            // 버튼의 텍스트를 "저장"으로 변경하여 세이브 모드처럼 동작하게 설정
            _loadButton.GetComponentInChildren<TextMeshProUGUI>().text = "저장";  // 저장 버튼 텍스트 변경
        }
        else
        {
            // saveData가 있을 경우, 저장된 데이터 표시
            _saveNameText.text = _saveData.saveName;
            _storyIDText.text = _saveData.storyID;
            _filePathText.text = GameDataManager.GetSaveFilePath(_slotNumber);
            
            // 버튼의 텍스트를 "로드"로 변경
            _loadButton.GetComponentInChildren<TextMeshProUGUI>().text = "로드";
        }

        // 로드 버튼 클릭 시 실행할 액션을 설정
        _loadButton.onClick.RemoveAllListeners();  // 기존 리스너 제거
        _loadButton.onClick.AddListener(() => _onClickAction());
    }
}
