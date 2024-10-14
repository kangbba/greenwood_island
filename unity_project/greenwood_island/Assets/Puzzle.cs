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
    private PuzzlePlaceData _previousPlaceData;

    private bool _isCleared;

    [SerializeField] private Button _enterSearchModeBtn; // 검색 모드로 전환하는 버튼
    [SerializeField] private Button _exitSearchModeBtn;  // 이동 모드로 전환하는 버튼
    // [SerializeField] private Button _enterWaitingModeBtn;  // 대기 모드로 전환하는 버튼
    [SerializeField] private Button _movePreviousPlaceBtn;  // 대기 모드로 전환하는 버튼

    //
    [SerializeField] private Button _moveButtonPrefab;  // 동적 버튼 생성에 사용할 프리팹
    [SerializeField] private Button _eventButtonPrefab;  // 동적 버튼 생성에 사용할 프리팹

    private List<Button> _moveBtns = new List<Button>();  // 현재 활성화된 이동 버튼들
    private List<Button> _eventBtns = new List<Button>();  // 현재 활성화된 검색 버튼들

    public abstract Dictionary<string, SequentialElement> SearchEvents { get; }
    public abstract Dictionary<string, SequentialElement> EnterEvents { get; }
    public abstract Dictionary<string, SequentialElement> ExitEvents { get; }
    public bool IsCleared { get => _isCleared; }
    private bool _isMoving = false;  // 중복 실행 방지 플래그

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
                ShowAndActiveButton(_movePreviousPlaceBtn, _currentPlaceData != _initialPlaceData, duration);
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
        if(_previousPlaceData == null){
            Debug.LogWarning("previous place data is null");
            return;
        }
        MovePlaceAndRefreshUI(_previousPlaceData);
    }

    // 코루틴 함수: 대기 시간 포함
    private IEnumerator MovePlaceAndRefreshUIRoutine(PuzzlePlaceData placeData)
    {
        if (placeData == null)
        {
            Debug.LogError("place data is null");
            yield break;
        }

        _isMoving = true;  // 실행 중으로 설정

        _previousPlaceData = _currentPlaceData;
        _currentPlaceData = placeData;

        // 장소 전환 애니메이션 실행
        new PlaceTransitionWithSwipe(placeData.PlaceID, 1f, ImageUtils.SwipeMode.OnlyFade).Execute();

        FadeOutAndDestroyAllButtons();  // 버튼 제거 시작

        yield return null;
        // 새 장소에 맞는 버튼 생성
        CreateMoveBtns(placeData.MovePlans);
        CreateEventBtns(placeData.EventPlans);

        SetModeUI(PuzzleMode.Move, 0f);  // UI 갱신
        yield return new WaitForSeconds(1f);  // 1초 대기

        _isMoving = false;  // 실행 완료 후 해제
    }
    


    private void SearchPoint(string eventID){

        StartCoroutine(SearchPointRoutine(eventID));
    }

    private IEnumerator SearchPointRoutine(string eventID){

        SetModeUI(PuzzleMode.Waiting, 0f);
        yield return StartCoroutine(SearchEvents[eventID].ExecuteRoutine());
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

            var newButton = Instantiate(_moveButtonPrefab, plan.btnTr);
            newButton.transform.localPosition = Vector2.zero;
            newButton.onClick.AddListener(() => MovePlaceAndRefreshUI(plan.placeDataToMove));
            ShowAndActiveButton(newButton, false, 0f);
            _moveBtns.Add(newButton);
        }
    }

    private void CreateEventBtns(List<PuzzlePlaceData.EventPlan> plans){
        _eventBtns.Clear();
        foreach(var plan in plans){

            var newButton = Instantiate(_moveButtonPrefab, plan.btnTr);
            newButton.transform.localPosition = Vector2.zero;
            newButton.onClick.AddListener(() => SearchPoint(plan.eventID));
            ShowAndActiveButton(newButton, false, 0f);
            _eventBtns.Add(newButton);
        }
    }

    private void FadeOutAndDestroyAllButtons(){
     
        foreach (var btn in _moveBtns)
        {
            Destroy(btn.gameObject);  // 이동 버튼 파괴
        }
        _moveBtns.Clear();  // 리스트 초기화

        foreach (var btn in _eventBtns)
        {
            Destroy(btn.gameObject);  // 검색 버튼 파괴
        }
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
