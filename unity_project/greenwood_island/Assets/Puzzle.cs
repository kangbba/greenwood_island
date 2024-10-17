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

    [SerializeField] private PuzzlePlace _initialPlace;
    [SerializeField] private Button _enterSearchModeBtn;
    [SerializeField] private Button _exitSearchModeBtn;
    [SerializeField] private Button _moveParentPlaceBtn;
    [SerializeField] private Transform _btnParent;

    private PuzzleMode _currentPuzzleMode;

    public bool IsCleared { get; set; }
    private List<PuzzlePlace> _allPlaces;
    private PuzzlePlace _currentPlace;
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
        var place = GetPlace(placeID);
        if (place == null) yield break;

        _isMoving = true;
        _currentPlace = place;

        SetPuzzleMode(PuzzleMode.Waiting, 0f);
        new VignetteEnter(Color.grey.ModifiedAlpha(.5f), .5f).Execute();
        new PlaceTransitionWithSwipe(placeID, .5f, ImageUtils.SwipeMode.OnlyFade).Execute();
        yield return new WaitForSeconds(.5f);
        new PlaceEffect(PlaceEffect.EffectType.PulseInfinite, 10f, .008f).Execute();
        Debug.Log("Try to visit");
        yield return _currentPlace.TryToVisitRoutine();

        SetPuzzleMode(PuzzleMode.Move, .2f);
        yield return new WaitForSeconds(.2f);

        _isMoving = false;
    }


    public PuzzlePlace GetPlace(string placeID)
    {
        return _allPlaces.Find(place => place.PlaceID == placeID);
    }
    public SequentialElement GetEvent(string eventID){
        return EventDictionary.GetValueOrDefault(eventID);
    }
}
