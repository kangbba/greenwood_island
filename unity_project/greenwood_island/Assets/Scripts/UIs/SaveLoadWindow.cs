using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

public class SaveLoadWindow : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    
    [SerializeField] private GameSlot _gameSlotPrefab;
    [SerializeField] private Transform _savedGamesListParent;  // 저장된 게임 목록이 표시될 부모 객체
    [SerializeField] private Button _closeButton;              // Close 버튼
    [SerializeField] private ScrollRect _scrollView;
    [SerializeField] private Transform _blurPanelParent;             // BlurPanel이 생성될 부모 객체 (UI Root)
    
    private BlurPanel _blurPanelInstance;                     // BlurPanel 인스턴스
    private const float verticalSpacing = 250;                // 슬롯 간의 세로 간격
    private List<GameSlot> _gameSlots = new List<GameSlot>();
    private bool _isSaveMode;                                 // 세이브 모드인지 로드 모드인지 구분
    private bool _isActivated = false;                        // 페이드 인 완료 시 true, 페이드 아웃 중이거나 비활성화 시 false
    private StorySavedData _newSaveDataForSaveMode;             // 세이브 모드일 때 저장할 데이터

    private void Awake()
    {
        _canvasGroup.alpha = 0;  // 알파값을 0으로 설정해 시작

        // Close 버튼 바인딩 및 이벤트 연결
        _closeButton.onClick.AddListener(CloseWindow);
    }

    // Init 메서드로 세이브 모드/로드 모드 구분 및 창 활성화, 세이브 모드일 경우 외부에서 저장할 데이터 전달
    public void Init(bool isSaveMode, StorySavedData newSaveDataForSaveMode)
    {
        _isSaveMode = isSaveMode;
        _newSaveDataForSaveMode = newSaveDataForSaveMode;  // 세이브 모드일 때 저장할 데이터
        gameObject.SetActive(true);  // 창을 활성화
        RecreateSlots();

        // 페이드 인 애니메이션
        _isActivated = false;  // 페이드 인이 완료되기 전까지는 조작을 막음
        Fade(true, .5f, () => _isActivated = true);
    }


    private void Fade(bool show, float duration, System.Action onComplete = null)
    {
        if (!show && _blurPanelInstance != null)
        {
            // 블러 패널 제거 애니메이션 실행
            _blurPanelInstance.FadeOutAndDestroy(duration);
        }

        float targetAlphaValue = show ? 1f : 0f;
        // CanvasGroup의 알파값을 애니메이션
        _canvasGroup.DOFade(targetAlphaValue, duration)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() =>
            {
                onComplete?.Invoke();
            });
    }

    // 저장된 게임 목록을 시각화하는 메서드 (private)
    private void RecreateSlots()
    {
        // 이전에 생성된 항목을 모두 제거
        foreach (GameSlot slot in _gameSlots)
        {
            Destroy(slot.gameObject);
        }
        _gameSlots.Clear();
        float topSpacing = 50f;
        _scrollView.content.sizeDelta = _scrollView.content.sizeDelta.ModifiedY(+topSpacing + GameDataManager.MaxSlotCount * verticalSpacing);
        
        // 슬롯 개수만큼 무조건 인스턴스화하고 Init
        for (int i = 0; i < GameDataManager.MaxSlotCount; i++)
        {
            // 프리팹을 생성하고 부모에 추가
            GameSlot gameSlot = Instantiate(_gameSlotPrefab, _savedGamesListParent);
            gameSlot.gameObject.SetActive(true);

            // 게임 슬롯의 위치를 세로로 일정 간격 두고 배치
            RectTransform slotTransform = gameSlot.GetComponent<RectTransform>();
            int slotNumber = i;
            slotTransform.anchoredPosition = new Vector2(slotTransform.anchoredPosition.x, -topSpacing + -slotNumber * verticalSpacing);

            // 슬롯 번호에 데이터가 있는지 확인하고 Init
            StorySavedData saveData = GameDataManager.GetStorySavedData(slotNumber);

            // 로드 모드와 세이브 모드에 따른 콜백 설정
            if (_isSaveMode)
            {
                gameSlot.Init(slotNumber, saveData, () => OnGameSlotSaveClicked(slotNumber, _newSaveDataForSaveMode), () => OnGameSlotDeleteBtnClicked(slotNumber));
            }
            else
            {
                gameSlot.Init(slotNumber, saveData, () => OnGameSlotLoadClicked(slotNumber), () => OnGameSlotDeleteBtnClicked(slotNumber));
            }

            _gameSlots.Add(gameSlot);
        }
        // 슬롯 상태를 한 번에 로그로 정리
        foreach (GameSlot slot in _gameSlots)
        {
            int slotNumber = _gameSlots.IndexOf(slot);
            StorySavedData saveData = GameDataManager.GetStorySavedData(slotNumber);

            if (saveData != null)
            {
                // 저장된 데이터가 있을 경우 로그 출력
                Debug.Log($"Slot {slotNumber} has data: StoryID = {saveData.storyID}, Other Data...");
            }
            else
            {
                // 저장된 데이터가 없을 경우 로그 출력
                Debug.Log($"Slot {slotNumber} is empty.");
            }
        }
    }

    // 슬롯 클릭 시 실행할 로직 (로드 모드)
    private void OnGameSlotLoadClicked(int slotNumber)
    {
        Debug.Log("여기 작동1");

        if (!_isActivated) return;

        // 저장된 데이터를 불러올 것인지 물어보는 팝업 띄우기
        StorySavedData storySavedData = GameDataManager.GetStorySavedData(slotNumber);

        if(storySavedData != null){
            UIManager.PopupCanvas.ShowYesNoPopup("저장된 데이터를 불러오시겠습니까?", "예", "아니오", () =>
            {
                Debug.Log($"슬롯 {slotNumber}의 데이터를 불러옵니다.");
                UIManager.PopupCanvas.Clear();
                GameDataManager.LoadGameDataThenPlay(slotNumber);
            });
        }
        else{
            UIManager.PopupCanvas.ShowOkPopup("해당 슬롯에 저장된 데이터가 없습니다.", "확인", () =>
            {
                Debug.Log("데이터가 없습니다.");
            });
        }
    }

    // 슬롯 클릭 시 실행할 로직 (세이브 모드)
    private void OnGameSlotSaveClicked(int slotNumber, StorySavedData newSaveData)
    {
        if (!_isActivated) return;

        StorySavedData existingSave = GameDataManager.GetStorySavedData(slotNumber);
        if (existingSave == null)
        {
            UIManager.PopupCanvas.ShowYesNoPopup("이 슬롯에 저장하시겠습니까?", "예", "아니오", () =>
            {
                GameDataManager.SaveGameData(newSaveData, slotNumber);
                RecreateSlots();

                UIManager.PopupCanvas.ShowOkPopup("저장 완료!", "확인", CloseWindow);
            });
        }
        else
        {
            UIManager.PopupCanvas.ShowYesNoPopup("이미 데이터가 있습니다. 덮어쓰시겠습니까?", "예", "아니오", () =>
            {
                GameDataManager.SaveGameData(newSaveData, slotNumber);
                RecreateSlots();
                
                UIManager.PopupCanvas.ShowOkPopup("저장 완료!", "확인", CloseWindow);
            });
        }
    }

    public void OnGameSlotDeleteBtnClicked(int slotNumber)
    {
        if (!_isActivated) return;

        UIManager.PopupCanvas.ShowYesNoPopup("정말 이 데이터를 삭제하시겠습니까?", "예", "아니오", () =>
        {
            GameDataManager.DeleteSaveDataFile(slotNumber);
            RecreateSlots();
        });
    }


    // 불러오기 창 닫기
    public void CloseWindow()
    {
        if(!_isActivated){
            return;
        }
        _isActivated = false;
        Fade(false, 0.2f, () => Destroy(gameObject));
    }
}
