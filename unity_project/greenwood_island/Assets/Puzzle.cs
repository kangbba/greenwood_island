using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private PuzzlePlace _initialPlace;
    [SerializeField] private Button _enterSearchModeBtn;
    [SerializeField] private Button _exitSearchModeBtn;
    [SerializeField] private Button _moveParentPlaceBtn;
    [SerializeField] private Transform _btnParent;

    public bool IsCleared { get; set; }
    private List<PuzzlePlace> _allPlaces;
    private PuzzlePlace _currentPlace;
    private bool _isMoving = false;

    public abstract EventData EventData { get; set; }
    public Transform BtnParent { get => _btnParent;  }

    private void Awake()
    {
        _allPlaces = GetComponentsInChildren<PuzzlePlace>().ToList();

        _exitSearchModeBtn.onClick.AddListener(() => SetModeUI(PuzzleMode.Move, 0f));
        _enterSearchModeBtn.onClick.AddListener(() => SetModeUI(PuzzleMode.Search, 0f));
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

    public void SetModeUI(PuzzleMode puzzleMode, float duration)
    {
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
        if (_isMoving) yield break;

        var place = GetPlace(placeID);
        if (place == null) yield break;

        _isMoving = true;
        _currentPlace = place;

        HideAllPuzzlePlaceZones(0f);
        SetButtonVisibility(false, false, false, 0f);
        new PlaceTransitionWithSwipe(placeID, .5f, ImageUtils.SwipeMode.OnlyFade).Execute();
        yield return new WaitForSeconds(.5f);

        SetModeUI(PuzzleMode.Move, .5f);
        yield return new WaitForSeconds(.5f);

        _isMoving = false;
    }


    public PuzzlePlace GetPlace(string placeID)
    {
        return _allPlaces.Find(place => place.PlaceID == placeID);
    }
}
