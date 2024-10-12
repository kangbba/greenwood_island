using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public abstract class Puzzle : MonoBehaviour
{
    [SerializeField] private string _puzzleID;
    [SerializeField] private string _itemIDForExit;
    [SerializeField] private Button _backButton;  // 미리 바인딩된 Back 버튼
    [SerializeField] private PuzzlePlace _initialPuzzlePlace;  // 초기 PuzzlePlace

    public abstract Dictionary<string, SequentialElement> BtnEvents { get; }
    public abstract Dictionary<string, SequentialElement> EnterEvents { get; }
    public abstract Dictionary<string, SequentialElement> ExitEvents { get; }

    private List<PuzzlePlace> _puzzlePlaces;  // 모든 PuzzlePlace 목록
    private PuzzlePlace _currentPlace;  // 현재 활성화된 PuzzlePlace
    private PuzzlePlace _previousPlace;  // 이전 PuzzlePlace

    private bool _isTransitioning;  // 중복 이동 방지 플래그

    private void Start()
    {
        InitializePuzzlePlaces();
        InitializeBackButton();
        MoveToPlace(_initialPuzzlePlace);  // 초기 장소로 이동
    }

    // 모든 PuzzlePlace 초기화
    private void InitializePuzzlePlaces()
    {
        _puzzlePlaces = new List<PuzzlePlace>(GetComponentsInChildren<PuzzlePlace>(true));

        foreach (var place in _puzzlePlaces)
        {
            EnsureActive(place.gameObject);  // 비활성화된 장소 강제 활성화
            place.Initialize(this);  // 부모 Puzzle 전달하여 초기화
        }
    }

    // Back 버튼 초기화
    private void InitializeBackButton()
    {
        ShowBackButton(false, 0f);
        _backButton.onClick.AddListener(() => MoveToPreviousPlace());  // 부모 장소로 이동
    }

    // GameObject가 비활성화된 경우 강제로 활성화
    private void EnsureActive(GameObject obj)
    {
        if (!obj.activeSelf)
        {
            obj.SetActive(true);
            Debug.LogWarning($"{obj.name}이(가) 비활성화되어 강제로 활성화되었습니다.");
        }
    }

    // 외부에서 호출하는 PuzzlePlace 간 이동 처리
    public void MoveToPlace(PuzzlePlace newPlace)
    {
        if (_isTransitioning)
        {
            Debug.LogWarning("이미 장소 이동 중입니다.");
            return;  // 중복 이동 방지
        }

        if (newPlace == null)
        {
            Debug.LogError("이동할 PuzzlePlace가 설정되지 않았습니다.");
            return;
        }

        _isTransitioning = true;  // 이동 시작

        _previousPlace = _currentPlace;
        _currentPlace = newPlace;
        
        StartCoroutine(MoveToPlaceRoutine(newPlace));
    }

    
    // 내부적으로만 사용하는 코루틴: 장소 이동 처리
    private IEnumerator MoveToPlaceRoutine(PuzzlePlace newPlace)
    {
        Debug.Log($"[Puzzle] '{_previousPlace?.name ?? "없음"}'에서 '{_currentPlace.name}'로 이동합니다.");

        // 이전 장소 숨기기
        ShowBackButton(false, 1f);
        if (_previousPlace != null)
        {
            if (ExitEvents.TryGetValue(_previousPlace.EventID, out var exitEvent))
            {
                yield return StartCoroutine(exitEvent.ExecuteRoutine());
            }
            else
            {
                Debug.Log($"[Puzzle] '{_previousPlace.name}'에 대한 Exit 이벤트가 없습니다.");
            }
            _previousPlace.Show(false, 1f);
        }
        _currentPlace.Show(true, 1f);
        yield return new WaitForSeconds(1f);  // 애니메이션 대기
        ShowBackButton(true, 1f);

        if (EnterEvents.TryGetValue(_currentPlace.EventID, out var enterEvent))
        {
            yield return StartCoroutine(enterEvent.ExecuteRoutine());
        }
        else
        {
            Debug.Log($"[Puzzle] '{_currentPlace.name}'에 대한 Enter 이벤트가 없습니다.");
        }

        Debug.Log($"[Puzzle] 새 장소 '{_currentPlace.name}' 활성화 완료.");
        _isTransitioning = false;  // 이동 종료
    }


    // Back 버튼을 Fade In/Out 처리
    public void ShowBackButton(bool isVisible, float duration)
    {
        if (isVisible)
        {
            _backButton.image.DOFade(1f, duration).OnComplete(() =>
            {
                SetBackButtonInteractable(true);
            });
        }
        else
        {
            SetBackButtonInteractable(false);
            _backButton.image.DOFade(0f, duration);
        }
    }

    // Back 버튼의 상호작용 설정
    private void SetBackButtonInteractable(bool interactable)
    {
        _backButton.interactable = interactable;
    }

    // 부모 PuzzlePlace로 이동 (외부에서 직접 호출 가능)
    private void MoveToPreviousPlace()
    {
        if (_currentPlace.ParentPlace != null)
        {
            MoveToPlace(_currentPlace.ParentPlace);  // 부모 장소로 이동
        }
        else
        {
            Debug.LogWarning("더 이상 상위 PuzzlePlace가 없습니다.");
        }
    }
}
