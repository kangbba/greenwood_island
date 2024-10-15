using System.Collections;
using UnityEngine;

[System.Serializable]
public class EventResult
{
    public enum ResultType
    {
        Event,
        Move,
        ItemGain,
        ItemLose
    }

    public ResultType Type;  // 결과 유형
    public string Parameter; // 결과 파라미터 (예: 이동할 장소 ID, 아이템 ID)

    public IEnumerator ExecuteRoutine(Puzzle puzzle)
    {
        switch (Type)
        {
            case ResultType.Event:
                yield return CoroutineUtils.StartCoroutine(puzzle.GetEvent(Parameter).ExecuteRoutine());
                break;
            case ResultType.Move:
                yield return new PuzzlePlaceTransition(Parameter).ExecuteRoutine();
                break;

            case ResultType.ItemGain:
                yield return new ItemGain(Parameter).ExecuteRoutine();
                break;

            case ResultType.ItemLose:
                yield return new ItemGain(Parameter).ExecuteRoutine();
                break;

            default:
                Debug.LogWarning("알 수 없는 결과 유형입니다.");
                yield break;
        }
    }
}
