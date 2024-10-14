using System.Collections;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// PuzzleEnter 클래스는 특정 퍼즐 장면을 화면에 보여주는 역할을 합니다.
/// </summary>
public class PuzzleEnter : Element
{
    private string _puzzleID;  // 퍼즐 ID를 저장할 변수

    // 생성자
    public PuzzleEnter(string puzzleID)
    {
        _puzzleID = puzzleID;
    }

    // 즉시 실행되는 메서드 (비워둠)
    public override void ExecuteInstantly()
    {
        // 필요한 경우 나중에 로직 추가 가능
    }

    // 퍼즐 장면을 화면에 보여주는 코루틴
    public override IEnumerator ExecuteRoutine()
    {
        // PlaceManager 초기화 호출
        PuzzleManager.Instance.Init(_puzzleID);

        Debug.Log($"PuzzleEnter 실행: {_puzzleID}");

        yield return CoroutineUtils.StartCoroutine(PuzzleManager.Instance.WaitUntilPuzzleCleared());
        Debug.Log($"Puzzle {_puzzleID} 완료!");

        // 코루틴 종료
        yield return null;
    }
}
