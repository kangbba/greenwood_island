using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public abstract class Puzzle : MonoBehaviour
{
    [SerializeField] private PuzzlePlaceData _initialPlaceData;
    private List<PuzzlePlaceData> _allPlaceDatas;

    private PuzzlePlaceData _currentPlaceData;

    private bool _isCleared;

    [SerializeField] private Button _enterSearchModeBtn; // 검색 모드로 전환하는 버튼
    [SerializeField] private Button _exitSearchModeBtn;  // 이동 모드로 전환하는 버튼
    // [SerializeField] private Button _enterWaitingModeBtn;  // 대기 모드로 전환하는 버튼
    [SerializeField] private Button _movePreviousPlaceBtn;  // 대기 모드로 전환하는 버튼
    [SerializeField] private Image _clearMarkPrefab;
    [SerializeField] private Transform _btnParent;

    //
    [SerializeField] private Button _moveButtonPrefab;  // 동적 버튼 생성에 사용할 프리팹
    [SerializeField] private Button _eventButtonPrefab;  // 동적 버튼 생성에 사용할 프리팹

    private List<Button> _moveBtns = new List<Button>();  // 현재 활성화된 이동 버튼들
    private List<Button> _eventBtns = new List<Button>();  // 현재 활성화된 검색 버튼들

    public bool IsCleared { get => _isCleared; }
    private bool _isMoving = false;  // 중복 실행 방지 플래그
    private HashSet<string> _clearedSearchEventIDs = new HashSet<string>();
    private HashSet<string> _clearedEnterEventPlaceIDs = new HashSet<string>();

    public abstract Dictionary<string, SequentialElement> EnterEvents { get; }
    public abstract Dictionary<string, SequentialElement> ExitEvents { get; }
    public abstract Dictionary<string, SequentialElement> SearchEvents { get; }
    public abstract Dictionary<string, Func<bool>> SearchEventClearConditions { get; }



    private void Awake()
    {
        _allPlaceDatas = GetComponentsInChildren<PuzzlePlaceData>().ToList();
        _exitSearchModeBtn.onClick.AddListener(() => SetModeUI(PuzzleMode.Move, 0f)); 
        _enterSearchModeBtn.onClick.AddListener(() => SetModeUI(PuzzleMode.Search, 0f)); 
       // _enterWaitingModeBtn.onClick.AddListener(() => SetMode(PuzzleMode.Waiting));  
        _movePreviousPlaceBtn.onClick.AddListener(() => MovePreviousPlace()); 

    
    }
    public void Init(){

        MovePlaceAndRefreshUI(_initialPlaceData);
        _clearedSearchEventIDs.Clear();
        _clearedEnterEventPlaceIDs.Clear();
    }
    public void SetModeUI(PuzzleMode puzzleMode, float duration)
    {
        switch (puzzleMode)
        {
            case PuzzleMode.Waiting:
                Debug.Log("대기 모드입니다.");
                UIManager.CursorCanvas.UiCursor.SetCursorMode(UICursor.CursorMode.Normal);
                ShowAndActiveButton(_exitSearchModeBtn, false, duration);
                ShowAndActiveButton(_enterSearchModeBtn, false, duration);
                ShowAndActiveButton(_movePreviousPlaceBtn, false, duration);
                ShowMoveBtns(false, duration);
                ShowEventBtns(false, duration);
                break;
            case PuzzleMode.Move:
                Debug.Log("이동 모드입니다.");
                UIManager.CursorCanvas.UiCursor.SetCursorMode(UICursor.CursorMode.Normal);
                ShowAndActiveButton(_exitSearchModeBtn, false, duration);
                ShowAndActiveButton(_enterSearchModeBtn, true, duration);
                ShowAndActiveButton(_movePreviousPlaceBtn, _currentPlaceData.ParentPlaceData != null, duration);
                ShowMoveBtns(true, duration);
                ShowEventBtns(false, duration);
                break;

            case PuzzleMode.Search:
                Debug.Log("검색 모드입니다.");
                UIManager.CursorCanvas.UiCursor.SetCursorMode(UICursor.CursorMode.Magnifier);
                ShowAndActiveButton(_exitSearchModeBtn, true, duration);
                ShowAndActiveButton(_enterSearchModeBtn, false, duration);
                ShowAndActiveButton(_movePreviousPlaceBtn, false, duration);
                ShowMoveBtns(false, duration);
                ShowEventBtns(true, duration);
                break;

            default:
                Debug.LogError("알 수 없는 PuzzleMode입니다.");
                break;
        }
    }
    public void MovePlaceAndRefreshUI(PuzzlePlaceData placeData)
    {
        if (_isMoving) return;  // 이미 실행 중이면 무시
        StartCoroutine(MovePlaceAndRefreshUIRoutine(placeData));  // 코루틴 실행
    }
    private void MovePreviousPlace(){
        if(_currentPlaceData.ParentPlaceData == null){
            Debug.LogWarning("ParentPlaceData is null");
            return;
        }
        MovePlaceAndRefreshUI(_currentPlaceData.ParentPlaceData);
    }

    // 코루틴 함수: 대기 시간 포함
    private IEnumerator MovePlaceAndRefreshUIRoutine(PuzzlePlaceData placeData)
    {
        if (placeData == null)
        {
            Debug.LogError("MovePlaceAndRefreshUIRoutine: placeData is null");
            yield break;
        }

        Debug.Log($"Moving to place: {placeData.PlaceID}");

        _isMoving = true;  // 실행 중 설정
        _currentPlaceData = placeData;

        // 버튼 제거 및 장소 전환 애니메이션 시작
        Debug.Log("Starting fade-out animation and removing buttons.");
        FadeOutAndDestroyAllButtons();
        new PlaceTransitionWithSwipe(placeData.PlaceID, 1f, ImageUtils.SwipeMode.OnlyFade).Execute();
        yield return new WaitForSeconds(1f);

        // 진입 이벤트 처리
        string placeID = placeData.PlaceID;
        if (EnterEvents.TryGetValue(placeID, out var enterEvent))
        {
            if (!_clearedEnterEventPlaceIDs.Contains(placeID))
            {
                Debug.Log($"Executing EnterEvent for place: {placeID}");
                _clearedEnterEventPlaceIDs.Add(placeID);
                SetModeUI(PuzzleMode.Waiting, 0f);

                // Enter 이벤트 코루틴 실행
                yield return StartCoroutine(enterEvent.ExecuteRoutine());
            }
            else
            {
                Debug.Log($"EnterEvent for place {placeID} already cleared. Skipping.");
            }
        }
        else
        {
            Debug.Log($"No EnterEvent found for place: {placeID}");
        }

        // 새 장소에 맞는 버튼 생성
        Debug.Log($"Creating buttons for new place: {placeID}");
        CreateMoveBtns(placeData.MovePlans);
        CreateEventBtns(placeData.EventPlans);

        // UI 모드 갱신
        SetModeUI(PuzzleMode.Move, 0f);
        Debug.Log("UI updated to Move mode.");

        _isMoving = false;  // 이동 완료
        Debug.Log($"Finished moving to place: {placeID}. Movement unlocked.");
    }


    public void RecreateAllBtns(PuzzlePlaceData puzzlePlaceData){

        FadeOutAndDestroyAllButtons();  // 버튼 제거 시작
        CreateMoveBtns(puzzlePlaceData.MovePlans);
        CreateEventBtns(puzzlePlaceData.EventPlans);
    }
    

    private void SearchPoint(string eventID)
    {
        if (_clearedSearchEventIDs.Contains(eventID))
        {
            Debug.Log($"퀘스트 {eventID}는 이미 완료되었습니다.");
            return; // 이미 완료된 경우 실행하지 않음
        }

        StartCoroutine(SearchPointRoutine(eventID));
    }


    private IEnumerator SearchPointRoutine(string eventID){

        SetModeUI(PuzzleMode.Waiting, 0f);
        yield return StartCoroutine(SearchEvents[eventID].ExecuteRoutine());
        // 퀘스트 완료로 상태 업데이트
        if (SearchEventClearConditions[eventID]())
        {
            _clearedSearchEventIDs.Add(eventID);
        }
        RecreateAllBtns(_currentPlaceData);
        SetModeUI(PuzzleMode.Move, 0f);
    }

    private void ShowAndActiveButton(Button button, bool b, float duration) // 딜레이 옵션 추가
    {
        button.interactable = false;  // 버튼 초기 상태를 비활성화

        if (b)
        {
            button.image.ScaleImage(Vector2.one, duration, Ease.OutQuad)  // 딜레이 후 애니메이션 시작
                .OnComplete(() => { button.interactable = true; });  // 완료 후 버튼 활성화
        }
        else
        {
            button.image.ScaleImage(Vector2.zero, duration, Ease.OutQuad)  // 딜레이 후 애니메이션 시작
                .OnComplete(() => { });  // 완료 후 추가 작업 없음
        }
    }

    private void CreateMoveBtns(List<PuzzlePlaceData.MovePlan> plans){
        _moveBtns.Clear();
        foreach(var plan in plans){

            var newButton = Instantiate(_moveButtonPrefab, _btnParent);
            newButton.transform.localPosition = plan.KnobTr.localPosition;
            newButton.onClick.AddListener(() => MovePlaceAndRefreshUI(plan.PlaceDataToMove));
            ShowAndActiveButton(newButton, false, 0f);
            _moveBtns.Add(newButton);
        }
    }

    private void CreateEventBtns(List<PuzzlePlaceData.EventPlan> plans){
        _eventBtns.Clear();
        foreach(var plan in plans){
            
            // 퀘스트 완료 여부에 따라 버튼 활성화 설정
            bool isCompleted = _clearedSearchEventIDs.Contains(plan.EventID);
            if(isCompleted){
                var newButton = Instantiate(_clearMarkPrefab, _btnParent);
                newButton.transform.localPosition = plan.KnobTr.localPosition;
            }
            else{
                var newButton = Instantiate(_moveButtonPrefab, _btnParent);
                newButton.transform.localPosition = plan.KnobTr.localPosition;

                
                newButton.onClick.AddListener(() => SearchPoint(plan.EventID));
                ShowAndActiveButton(newButton, false, 0f);
                _eventBtns.Add(newButton);
            }
        }
    }

    private void FadeOutAndDestroyAllButtons(){
     
        _btnParent.DestroyAllChildren();
        _moveBtns.Clear();  // 리스트 초기화
        _eventBtns.Clear();  // 리스트 초기화
    }

   private void ShowMoveBtns(bool b, float totalDuration)
    {
        int count = _moveBtns.Count;
        for (int i = 0; i < count; i++)
        {
            ShowAndActiveButton(_moveBtns[i], b, totalDuration);
        }
    }

    private void ShowEventBtns(bool b, float totalDuration)
    {
        int count = _eventBtns.Count;
        for (int i = 0; i < count; i++)
        {
            ShowAndActiveButton(_eventBtns[i], b, totalDuration);
        }
    }


}
