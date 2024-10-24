using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public enum PuzzleMode
{
    Waiting = 0,  // 대기 중
    Move = 1,     // 이동 모드
    Search = 2    // 검색 모드
}
public abstract class Puzzle : MonoBehaviour
{
    public abstract Dictionary<string, SequentialElement> EventDictionary {get; }
    private List<string> _clearedEventKeys = new List<string>();

    [SerializeField] private PuzzlePlace _initialPlace;
    [SerializeField] private Button _enterSearchModeBtn;
    [SerializeField] private Button _exitSearchModeBtn;
    [SerializeField] private Button _moveParentPlaceBtn;
    [SerializeField] private Transform _btnParent;

    private PuzzleMode _currentPuzzleMode;

    public abstract bool GetIsPuzzleCleared();
    private List<PuzzlePlace> _allPlaces;
    protected PuzzlePlace _currentPlace;
    private bool _isMoving = false;

    public Transform BtnParent { get => _btnParent;  }
    public PuzzleMode CurrentPuzzleMode { get => _currentPuzzleMode;  }

    private void Awake()
    {
        _allPlaces = GetComponentsInChildren<PuzzlePlace>().ToList();

        _exitSearchModeBtn.onClick.AddListener(() => SetPuzzleMode(PuzzleMode.Move, 0f));
        _enterSearchModeBtn.onClick.AddListener(() => SetPuzzleMode(PuzzleMode.Search, 0f));
        _moveParentPlaceBtn.onClick.AddListener(MoveParentPlace);
    }
    public void Init()
    {
        _clearedEventKeys.Clear();
        foreach (var place in _allPlaces)
        {
            place.InitPuzzlePlace(this);
        }
        MovePuzzlePlace(_initialPlace.PlaceID);
    }

    public void SetPuzzleMode(PuzzleMode puzzleMode, float duration)
    {
        _currentPuzzleMode = puzzleMode;
        switch (puzzleMode)
        {
            case PuzzleMode.Waiting:
                UIManager.CursorCanvas.UiCursor.SetCursorMode(UICursor.CursorMode.Normal);
                SetButtonVisibility(false, false, false, duration);
                HideAllPuzzlePlaceZones(0);
                break;

            case PuzzleMode.Move:
                UIManager.CursorCanvas.UiCursor.SetCursorMode(UICursor.CursorMode.Normal);
                SetButtonVisibility(true, false, _currentPlace?.ParentPlace != null, duration);
                HideAllPuzzlePlaceZones(0);
                _currentPlace.ShowMoveZonesProperly(duration);
                break;

            case PuzzleMode.Search:
                UIManager.CursorCanvas.UiCursor.SetCursorMode(UICursor.CursorMode.Magnifier);
                SetButtonVisibility(false, true, false, duration);
                HideAllPuzzlePlaceZones(0);
                _currentPlace.ShowEtcZonesProperly(duration);
                break;

            default:
                UIManager.CursorCanvas.UiCursor.SetCursorMode(UICursor.CursorMode.Normal);
                Debug.LogError("알 수 없는 PuzzleMode입니다.");
                break;
        }
    }


    private void SetButtonVisibility(bool showEnterSearchModeBtn, bool showExitSearchModeBtn, bool showMoveParentPlaceBtn, float duration)
    {
        _exitSearchModeBtn.SetActiveWithScale(showExitSearchModeBtn, duration);
        _enterSearchModeBtn.SetActiveWithScale(showEnterSearchModeBtn, duration);
        _moveParentPlaceBtn.SetActiveWithScale(showMoveParentPlaceBtn, duration);
    }

    private void HideAllPuzzlePlaceZones(float duration)
    {
        foreach (var place in _allPlaces)
        {
            place.HideAllZones(duration);
        }
    }

    public void MovePuzzlePlace(string placeID)
    {
        StartCoroutine(MovePuzzlePlaceRoutine(placeID));
    }

    private void MoveParentPlace()
    {
        if (_currentPlace?.ParentPlace == null) return;
        MovePuzzlePlace(_currentPlace.ParentPlace.PlaceID);
    }

    public IEnumerator MovePuzzlePlaceRoutine(string placeID)
    {
        if (_isMoving) {
            Debug.LogWarning("Moving중 다른 moving이 호출됨");
            yield break;
        }
        var targetPlace = GetPlace(placeID);
        if (targetPlace == null) yield break;

        _isMoving = true;

        SetPuzzleMode(PuzzleMode.Waiting, 0f);

        if(_currentPlace != null){
            yield return _currentPlace.TryToExitRoutine();
        }
        _currentPlace = targetPlace;

        new PlaceTransitionWithSwipe(placeID, .5f, ImageUtils.SwipeMode.OnlyFade).Execute();
        yield return new WaitForSeconds(.5f);


        if(GetIsPuzzleCleared()){
            Debug.Log("퍼즐 cleared상태 이므로 out");
        }
        else{
            Debug.Log("Try to visit");
            yield return _currentPlace.TryToEnterRoutine();
            SetPuzzleMode(PuzzleMode.Move, .2f);
            yield return new WaitForSeconds(.2f);
        }
        
        _isMoving = false;
    }


    public PuzzlePlace GetPlace(string placeID)
    {
        return _allPlaces.Find(place => place.PlaceID == placeID);
    }
    public SequentialElement GetEvent(string eventID){
        return EventDictionary.GetValueOrDefault(eventID);
    }

    public IEnumerator ExecuteEvent(string eventID)
    {
        if (string.IsNullOrEmpty(eventID))
        {
            Debug.LogWarning($"[PuzzlePlace] 이벤트 ID가 비어 있습니다.");
            yield break;
        }

        var eventElement = GetEvent(eventID);

        if(eventElement == null){

            Debug.LogWarning($"[EventTriggerZone] 이벤트 '{eventID}'를 찾을 수 없습니다.");
            yield break;
        }
        yield return new WaitForSeconds(.3f);

        yield return StartCoroutine(eventElement.ExecuteRoutine());

        if(!IsEventCleared(eventID)){
            Debug.Log($"{eventID} 의 이벤트는 완료된것으로 등록되었음");
            SetEventCleared(eventID, true);
        }

        new AllCharactersClear(.3f).Execute();
        new DialoguePanelClear(.3f).Execute();
        yield return new WaitForSeconds(.3f);
    }

    public bool IsEventCleared(string eventID){
        return _clearedEventKeys.Contains(eventID);
    }

    public void SetEventCleared(string eventID, bool setClear){
        if(setClear){
            if(!_clearedEventKeys.Contains(eventID)){
                _clearedEventKeys.Add(eventID);
            }
        }
        else{
            if(_clearedEventKeys.Contains(eventID)){
                _clearedEventKeys.Remove(eventID);
            }
        }
    }
}
