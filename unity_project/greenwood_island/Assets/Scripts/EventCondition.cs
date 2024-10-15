using UnityEngine;

[System.Serializable]
public class EventCondition
{
    public enum ConditionType
    {
        HasItem,
        ItemDemand,
        Choice,
    }

    public ConditionType Type;      // 조건의 유형
    public string Parameter;        // 조건의 파라미터 (예: 아이템 이름)

    // 조건이 충족되었는지 검사하는 메서드
    public bool IsConditionMet()
    {
        switch (Type)
        {
            case ConditionType.HasItem:
                return ItemManager.HasItem(Parameter);
            default:
                return false;
        }
    }
}
