using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum PuzzleMode
{
    Waiting = 0,  // 대기 중
    Move = 1,     // 이동 모드
    Search = 2    // 검색 모드
}

public class PuzzleManager : SingletonManager<PuzzleManager>
{
    private Puzzle _puzzlePrefabToSpawnTest;  // 생성할 Puzzle 프리팹
    private Puzzle _currentPuzzle;  // 현재 활성화된 Puzzle 인스턴스

    public Puzzle CurrentPuzzle { get => _currentPuzzle; }

    public void Init(string puzzleID)
    {
        string resourcePath = ResourcePathManager.GetSharedResourcePath(puzzleID, ResourceType.Puzzle);
        _puzzlePrefabToSpawnTest = Resources.Load<Puzzle>(resourcePath);
        if (_puzzlePrefabToSpawnTest == null)
        {
            Debug.LogError("Puzzle Prefab이 설정되지 않았습니다!");
            return;
        }

        // Puzzle 프리팹 인스턴스화
        _currentPuzzle = Instantiate(_puzzlePrefabToSpawnTest, UIManager.SystemCanvas.PuzzleUILayer);

        // 위치 초기화 (원하는 위치에 맞게 조정)
        _currentPuzzle.transform.localPosition = Vector3.zero;
        _currentPuzzle.transform.localScale = Vector3.one;
        _currentPuzzle.Init();

        Debug.Log("Puzzle 프리팹이 인스턴스화되었습니다.");
    }
    // 퍼즐이 완료될 때까지 대기하는 코루틴
    public IEnumerator WaitUntilPuzzleCleared()
    {
        // PlaceManager.Instance.CurrentPuzzle.IsCleared가 true가 될 때까지 대기
        while (!_currentPuzzle.IsCleared)
        {
            yield return null;  // 한 프레임 대기 후 다시 검사
        }
    }
}
