using UnityEngine;

[System.Serializable]
public class EventCondition
{
    public enum ConditionType
    {
        Never,
        HasItem,
        VisitedMoreThanOnce,
        EventOccured,
        CurrentPlaceID,
    }

    public ConditionType Type;      // 조건의 유형
    public string Parameter;        // 조건의 파라미터 (예: 아이템 이름)

    // 조건이 충족되었는지 검사하는 메서드
    public bool IsConditionMet()
    {
        Puzzle currentPuzzle = PuzzleManager.Instance.CurrentPuzzle;
        switch (Type)
        {
            case ConditionType.Never:
                return false;
                
            case ConditionType.HasItem:
                return ItemManager.HasItem(Parameter);

            case ConditionType.VisitedMoreThanOnce:
                return PuzzleManager.Instance.CurrentPuzzle.GetPlace(Parameter).IsVisited;

            case ConditionType.EventOccured:    
                return currentPuzzle.IsEventCleared(Parameter);

            case ConditionType.CurrentPlaceID:
                return currentPuzzle.CurrentPlace != null && currentPuzzle.CurrentPlace.PlaceID == Parameter;

            default:
                return false;
        }
    }

}
