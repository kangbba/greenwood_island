using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class PuzzleManager : SingletonManager<PuzzleManager>
{
    private Puzzle _puzzlePrefabToSpawn;  // 생성할 Puzzle 프리팹
    private Puzzle _currentPuzzle;  // 현재 활성화된 Puzzle 인스턴스
    public Puzzle CurrentPuzzle { get => _currentPuzzle; }

    // 버튼 프리팹들 (Button 타입)
    private Button _enterSearchModeBtnPrefab;
    private Button _exitSearchModeBtnPrefab;
    private Button _moveParentPlaceBtnPrefab;

    // DesignedIconBtn 타입의 버튼 프리팹
    private DesignedIconButton _moveBtn;
    private DesignedIconButton _transparentBtn;
    private DesignedIconButton _discoveredBtn;
    private DesignedIconButton _checkedBtn;
    private DesignedIconButton _moveLockedBtn;
    public DesignedIconButton MoveBtn { get => _moveBtn; }
    public DesignedIconButton TransparentBtn { get => _transparentBtn; }
    public DesignedIconButton DiscoveredBtn { get => _discoveredBtn; }
    public DesignedIconButton CheckedBtn { get => _checkedBtn; }
    public DesignedIconButton MoveLockedBtn { get => _moveLockedBtn; }

    private const string ResourcesFolderPath = "PuzzleManager"; // Resources 폴더 경로

    public void Init(string puzzleID)
    {
        string resourcePath = ResourcePathManager.GetCurrentStoryResourcePath(puzzleID, ResourceType.Puzzle);
        _puzzlePrefabToSpawn = Resources.Load<Puzzle>(resourcePath);
        if (_puzzlePrefabToSpawn == null)
        {
            Debug.LogError("Puzzle Prefab이 설정되지 않았습니다!");
            return;
        }
        LoadButtonPrefabs();  // 버튼 프리팹 로드

        // Puzzle 프리팹 인스턴스화
        _currentPuzzle = Instantiate(_puzzlePrefabToSpawn, UIManager.SystemCanvas.PuzzleUILayer);

        // 위치 초기화 (원하는 위치에 맞게 조정)
        _currentPuzzle.transform.localPosition = Vector3.zero;
        _currentPuzzle.transform.localScale = Vector3.one;
        _currentPuzzle.Init(_enterSearchModeBtnPrefab, _exitSearchModeBtnPrefab, _moveParentPlaceBtnPrefab);

        Debug.Log("Puzzle 프리팹이 인스턴스화되었습니다.");
    }   
    public void DestroyCurrentPuzzle(){
        Destroy(_currentPuzzle.gameObject);
        _currentPuzzle = null;
    }
   
    // 조건 검사
    public static bool CheckEventConditionAllMet(List<EventCondition> eventConditions)
    {
        foreach (var condition in eventConditions)
        {
            if (!condition.IsConditionMet())
            {
                Debug.LogWarning($"[EventTriggerZone] 조건 미충족: {condition}");
                return false;
            }
        }
        Debug.Log("[EventTriggerZone] 모든 조건 충족.");
        return true;
    }
    // ResourcePathManager를 이용해 버튼 프리팹을 로드하는 메서드
    private void LoadButtonPrefabs()
    {
        _enterSearchModeBtnPrefab = Resources.Load<Button>($"{ResourcesFolderPath}/BottomBtnPrefab_SearchMode");
        _exitSearchModeBtnPrefab = Resources.Load<Button>($"{ResourcesFolderPath}/BottomBtnPrefab_ExitSearchMode");
        _moveParentPlaceBtnPrefab = Resources.Load<Button>($"{ResourcesFolderPath}/BottomBtnPrefab_GoParentPlace");

        _moveBtn = LoadButtonPrefab("EventZoneBtnPrefab_Move");
        _transparentBtn = LoadButtonPrefab("EventZoneBtnPrefab_Transparent");
        _discoveredBtn = LoadButtonPrefab("EventZoneBtnPrefab_Discovered");
        _checkedBtn = LoadButtonPrefab("EventZoneBtnPrefab_Checked");
        _moveLockedBtn = LoadButtonPrefab("EventZoneBtnPrefab_MoveLocked");

        Debug.Log("모든 버튼 프리팹이 로드되었습니다.");
    }
    // 버튼 프리팹을 리소스에서 로드하는 헬퍼 메서드
    private DesignedIconButton LoadButtonPrefab(string prefabName)
    {
        var buttonPrefab = Resources.Load<DesignedIconButton>($"{ResourcesFolderPath}/{prefabName}");
        if (buttonPrefab == null)
        {
            Debug.LogError($"{prefabName} 프리팹을 찾을 수 없습니다!");
        }
        return buttonPrefab;
    }

    // 퍼즐이 완료될 때까지 대기하는 코루틴
    public IEnumerator WaitUntilPuzzleCleared()
    {
        // PlaceManager.Instance.CurrentPuzzle.IsCleared가 true가 될 때까지 대기
        while(!_currentPuzzle.GetIsPuzzleCleared()){
            yield return null;
        }
        Destroy(_currentPuzzle.gameObject);
    }
}
