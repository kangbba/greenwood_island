using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

public class SaveLoadWindow : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Transform _savedGamesListParent;  // 저장된 게임 목록이 표시될 부모 객체
    [SerializeField] private Button _closeButton;              // Close 버튼
    [SerializeField] private ScrollRect _scrollView;              // Close 버튼
    [SerializeField] private Material _blurMat;              // Close 버튼
    [SerializeField] private Image _background; 
    private const float verticalSpacing = 250;     // 슬롯 간의 세로 간격

    private List<GameSlot> _gameSlots = new List<GameSlot>();
    private bool _isSaveMode; // 세이브 모드인지 로드 모드인지 구분
    private bool _isActivated = false;  // 페이드 인 완료 시 true, 페이드 아웃 중이거나 비활성화 시 false

    private GameSaveData _newSaveDataForSaveMode; // 세이브 모드일 때 저장할 데이터

    // Awake에서 Resources 폴더에서 프리팹을 미리 가져오기
    private void Awake()
    {

        _canvasGroup.alpha = 0;  // 알파값을 0으로 설정해 시작
        Material inst_blurMat = Instantiate(_blurMat);
        _background.material = inst_blurMat;

        // Close 버튼 바인딩 및 이벤트 연결
        _closeButton.onClick.AddListener(CloseWindow);

    }

    // Init 메서드로 세이브 모드/로드 모드 구분 및 창 활성화, 세이브 모드일 경우 외부에서 저장할 데이터 전달
    public void Init(bool isSaveMode, GameSaveData newSaveDataForSaveMode = null)
    {
        _isSaveMode = isSaveMode;
        _newSaveDataForSaveMode = newSaveDataForSaveMode;  // 세이브 모드일 때 저장할 데이터
        gameObject.SetActive(true);  // 창을 활성화
        RecreateSlots();

        // 페이드 인 애니메이션
        _isActivated = false;  // 페이드 인이 완료되기 전까지는 조작을 막음
             // DoTween을 이용해 블러 값을 0에서 targetBlurValue까지 변화
       

       Fade(true, 1f, () => _isActivated = true);
    }
    private void Fade(bool show, float duration, System.Action onComplete = null)
    {
        // show가 true면 targetBlurValue는 1f, false면 0f
        float targetBlurValue = show ? 1f : 0f;
        float targetAlphaValue = show ? 1f : 0f;

        // 블러 값을 애니메이션
        DOTween.To(() => _background.material.GetFloat("_Size"), x => _background.material.SetFloat("_Size", x), targetBlurValue, duration)
            .SetEase(Ease.InOutQuad);

        // CanvasGroup의 알파값을 애니메이션
        _canvasGroup.DOFade(targetAlphaValue, duration)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() =>
            {
                _isActivated = show;  // 페이드 인이면 활성화, 페이드 아웃이면 비활성화

                Debug.Log("Fade 애니메이션 완료. 활성화 여부: " + _isActivated);

                // onComplete 콜백 함수가 있다면 실행
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

        _scrollView.content.sizeDelta = _scrollView.content.sizeDelta.ModifiedY(GameDataManager.MaxSlotCount * verticalSpacing);
        // 슬롯 개수만큼 무조건 인스턴스화하고 Init
        for (int i = 0; i < GameDataManager.MaxSlotCount; i++)
        {
            // 프리팹을 생성하고 부모에 추가
            GameSlot gameSlotPrefab = UIManager.GameSlotPrefab;
            GameSlot gameSlot = Instantiate(gameSlotPrefab, _savedGamesListParent);
            gameSlot.gameObject.SetActive(true);

            // 게임 슬롯의 위치를 세로로 일정 간격 두고 배치
            RectTransform slotTransform = gameSlot.GetComponent<RectTransform>();
            int slotNumber = i;
            slotTransform.anchoredPosition = new Vector2(slotTransform.anchoredPosition.x, -slotNumber * verticalSpacing);

            // 슬롯 번호에 데이터가 있는지 확인하고 Init
            GameSaveData saveData = GameDataManager.GetGameSaveDataFromSlotNumber(slotNumber);

            // 로드 모드와 세이브 모드에 따른 콜백 설정
            if (_isSaveMode)
            {
                // 세이브 모드일 때, 저장할 데이터를 사용하여 Init
                gameSlot.Init(slotNumber, saveData, () => OnGameSlotSaveClicked(slotNumber, _newSaveDataForSaveMode), () => OnGameSlotDeleteBtnClicked(slotNumber));
            }
            else
            {
                // 로드 모드일 때, 슬롯 번호로 게임을 로드
                gameSlot.Init(slotNumber, saveData, () => OnGameSlotLoadClicked(slotNumber), () => OnGameSlotDeleteBtnClicked(slotNumber));
            }

            _gameSlots.Add(gameSlot);
        }
        
    }

    // 슬롯 클릭 시 실행할 로직 (로드 모드)
    private void OnGameSlotLoadClicked(int slotNumber)
    {
        Debug.Log("여기 작동1");

        if (!_isActivated) return;

        // 저장된 데이터를 불러올 것인지 물어보는 팝업 띄우기
        ShowYesNoPopup("저장된 데이터를 불러오시겠습니까?", "예", "아니오", () =>
        {
            // 사용자가 "예"를 선택한 경우에만 데이터 불러오기
            Debug.Log($"슬롯 {slotNumber}의 데이터를 불러옵니다.");
            GameDataManager.LoadGameData(slotNumber);
        });
    }

    

    // 슬롯 클릭 시 실행할 로직 (세이브 모드)
    private void OnGameSlotSaveClicked(int slotNumber, GameSaveData newSaveData)
    {
        Debug.Log("여기 작동2");
        if (!_isActivated) return;

        GameSaveData existingSave = GameDataManager.GetGameSaveDataFromSlotNumber(slotNumber);
        if (existingSave == null)
        {
            // 저장할 데이터가 없는 경우 - 새로 저장할 것인지 묻는 팝업 띄우기
            ShowYesNoPopup("이 슬롯에 저장하시겠습니까?", "예", "아니오", () =>
            {
                Debug.Log($"슬롯 {slotNumber}에 새로 저장.");
                // GameDataManager.SaveGameData 호출
                GameDataManager.SaveGameData(newSaveData, slotNumber);
                RecreateSlots();
            });
        }
        else
        {
            // 저장할 데이터가 있는 경우 - 덮어쓸 것인지 묻는 팝업 띄우기
            ShowYesNoPopup("이미 데이터가 있습니다. 덮어쓰시겠습니까?", "예", "아니오", () =>
            {
                Debug.Log($"슬롯 {slotNumber}에 덮어씁니다.");
                // GameDataManager.SaveGameData 호출
                GameDataManager.SaveGameData(newSaveData, slotNumber);
                RecreateSlots();
            });
        }
    }
    public void OnGameSlotDeleteBtnClicked(int slotNumber)
    {
        Debug.Log("여기 작동1");

        if (!_isActivated) return;

        // 정말 삭제할 것인지 물어보는 확인 팝업 띄우기
        ShowYesNoPopup("정말 이 데이터를 삭제하시겠습니까?", "예", "아니오", () =>
        {
            // 사용자가 "예"를 선택한 경우에만 삭제 수행
            Debug.Log($"슬롯 {slotNumber}을(를) 삭제합니다.");
            GameDataManager.DeleteSaveDataFile(slotNumber);
            RecreateSlots();
        });
    }



    // 예/아니오 팝업 호출 메서드
    private void ShowYesNoPopup(string message, string yesText, string noText, System.Action onYesAction)
    {
        YesNoPopup popupInstance = Instantiate(UIManager.YesNoPopupPrefab, transform);
        popupInstance.Init(message, yesText, noText, onYesAction);
    }

    // 불러오기 창 닫기
    public void CloseWindow()
    {
       Fade(false, .5f, () => Destroy(gameObject));
    }
}
