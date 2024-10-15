using System.Collections;
using UnityEngine;

public class PuzzlePlaceTransition : Element
{
    private string _targetPlaceID;  // 이동할 장소의 ID

    // 생성자: 이동할 장소 ID를 받음
    public PuzzlePlaceTransition(string targetPlaceID)
    {
        _targetPlaceID = targetPlaceID;
    }

    // 즉시 실행: 비워둘 수 있음
    public override void ExecuteInstantly() { }

    // ExecuteRoutine: Puzzle에서 특정 장소로 이동하는 코루틴 실행
    public override IEnumerator ExecuteRoutine()
    {
        var puzzle = PuzzleManager.Instance.CurrentPuzzle;

        if (puzzle == null)
        {
            Debug.LogError("CurrentPuzzle이 설정되지 않았습니다.");
            yield break;
        }

        Debug.Log($"PuzzlePlaceTransition 실행: {_targetPlaceID}로 이동합니다.");
        yield return puzzle.MovePuzzlePlaceRoutine(_targetPlaceID);  // Puzzle의 코루틴 호출
    }
}
