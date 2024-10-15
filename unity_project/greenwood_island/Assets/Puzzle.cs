using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public abstract class Puzzle : MonoBehaviour
{
    [SerializeField] private PuzzlePlaceData _initialPlaceData;
    private List<PuzzlePlaceData> _allPlaceDatas;

    private PuzzlePlaceData _currentPlaceData;
    private bool _isCleared;

    [SerializeField] private Button _enterSearchModeBtn;
    [SerializeField] private Button _exitSearchModeBtn;
    [SerializeField] private Button _movePreviousPlaceBtn;
    [SerializeField] private Image _clearMarkPrefab;
    [SerializeField] private Transform _btnParent;

    [SerializeField] private Button _moveButtonPrefab;
    [SerializeField] private Button _eventButtonPrefab;

    private List<Button> _moveBtns = new List<Button>();
    private List<Button> _eventBtns = new List<Button>();

    public bool IsCleared => _isCleared;
    private bool _isMoving = false;

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
        _movePreviousPlaceBtn.onClick.AddListener(MovePreviousPlace);
    }

    public void Init()
    {
        MovePlaceAndRefreshUI(_initialPlaceData.PlaceID);
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
                ShowAndActiveButton(_movePreviousPlaceBtn, _currentPlaceData?.ParentPlaceData != null, duration);
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

    public void MovePlaceAndRefreshUI(string placeID)
    {
        StartCoroutine(MovePlaceAndRefreshUIRoutine(placeID));
    }

    private void MovePreviousPlace()
    {
        if (_currentPlaceData?.ParentPlaceData == null)
        {
            Debug.LogWarning("ParentPlaceData is null");
            return;
        }
        MovePlaceAndRefreshUI(_currentPlaceData.ParentPlaceData.PlaceID);
    }
    //이동 핵심 루틴
    public IEnumerator MovePlaceAndRefreshUIRoutine(string placeID)
    {

        if (_isMoving){
            Debug.LogWarning("이미 이동중에 호출되었습니다.");
            yield break;
        }
        var placeData = GetPlaceData(placeID);
        if (placeData == null)
        {
            Debug.LogError($"PlaceData not found for placeID: {placeID}");
            yield break;
        }
        Debug.Log($"Moving to place: {placeID}");

        _isMoving = true;
        _currentPlaceData = placeData;

        FadeOutAndDestroyAllButtons();
        new PlaceTransitionWithSwipe(placeID, 1f, ImageUtils.SwipeMode.OnlyFade).Execute();
        yield return new WaitForSeconds(1f);

        if (EnterEvents.TryGetValue(placeID, out var enterEvent) &&
            !_clearedEnterEventPlaceIDs.Contains(placeID))
        {
            _clearedEnterEventPlaceIDs.Add(placeID);
            SetModeUI(PuzzleMode.Waiting, 0f);
            yield return StartCoroutine(enterEvent.ExecuteRoutine());
        }

        CreateMoveBtns(placeData.MovePlans);
        CreateEventBtns(placeData.EventPlans);
        SetModeUI(PuzzleMode.Move, 0f);
        _isMoving = false;
    }

    public PuzzlePlaceData GetPlaceData(string placeID)
    {
        return _allPlaceDatas.FirstOrDefault(data => data.PlaceID == placeID);
    }

    private void SearchPoint(string eventID)
    {
        if (_clearedSearchEventIDs.Contains(eventID)) return;
        StartCoroutine(SearchPointRoutine(eventID));
    }

    private IEnumerator SearchPointRoutine(string eventID)
    {
        SetModeUI(PuzzleMode.Waiting, 0f);
        yield return StartCoroutine(SearchEvents[eventID].ExecuteRoutine());

        if (SearchEventClearConditions[eventID]())
        {
            _clearedSearchEventIDs.Add(eventID);
        }

        RecreateAllBtns(_currentPlaceData);
        SetModeUI(PuzzleMode.Move, 0f);
    }

    public void RecreateAllBtns(PuzzlePlaceData puzzlePlaceData)
    {
        FadeOutAndDestroyAllButtons();
        CreateMoveBtns(puzzlePlaceData.MovePlans);
        CreateEventBtns(puzzlePlaceData.EventPlans);
    }

    private void ShowAndActiveButton(Button button, bool active, float duration)
    {
        button.interactable = false;
        button.image.ScaleImage(active ? Vector2.one : Vector2.zero, duration, Ease.OutQuad)
            .OnComplete(() => button.interactable = active);
    }

    private void CreateMoveBtns(List<PuzzlePlaceData.MovePlan> plans)
    {
        _moveBtns.Clear();
        foreach (var plan in plans)
        {
            var newButton = Instantiate(_moveButtonPrefab, _btnParent);
            newButton.transform.localPosition = plan.KnobTr.localPosition;
            newButton.onClick.AddListener(() => MovePlaceAndRefreshUI(plan.PlaceDataToMove.PlaceID));
            ShowAndActiveButton(newButton, true, 0f);
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

    private void FadeOutAndDestroyAllButtons()
    {
        _btnParent.DestroyAllChildren();
        _moveBtns.Clear();
        _eventBtns.Clear();
    }

    private void ShowMoveBtns(bool active, float duration)
    {
        foreach (var button in _moveBtns)
        {
            ShowAndActiveButton(button, active, duration);
        }
    }

    private void ShowEventBtns(bool active, float duration)
    {
        foreach (var button in _eventBtns)
        {
            ShowAndActiveButton(button, active, duration);
        }
    }
}
