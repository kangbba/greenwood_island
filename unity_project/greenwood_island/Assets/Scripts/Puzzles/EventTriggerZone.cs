using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EventZoneStatus
{
    Disabled,
    Move,
    Transparent,
    Discovered,
    Checked,
    MoveLocked,

}
public class EventTriggerZone : MonoBehaviour
{
    [SerializeField] private List<EventCondition> _conditions;  // 조건 목록
    [SerializeField] private string _failureEventID;  // 실패 시 이벤트 ID
    [SerializeField] private EventResult _reward;  // 보상 (단일 보상)

    private Puzzle _parentPuzzle;  // 부모 Puzzle 참조
    private int _clickCount = 0;  // 시도 횟수
    private int _successCount = 0;  // 조건을 뚫은 조회 횟수
    private bool _isRewardCleared = false;

    private DesignedIconButton _eventZoneBtn;  // 동적으로 할당된 버튼 인스턴스

    public EventResult Reward => _reward;

    public bool IsRewardMoveType => Reward.Type == EventResult.ResultType.Move;


    public bool IsRewardCleared { get => _isRewardCleared; }
    public bool IsConditionExist { get => _conditions != null && _conditions.Count > 0; }
    public int ClickCount { get => _clickCount; } // 조건 상관없이 클릭 수
    public int SuccessCount { get => _successCount; }

    // 부모 Puzzle에서 동적 버튼 생성 및 설정
    public void Init(Puzzle parentPuzzle)
    {
        _parentPuzzle = parentPuzzle;

        SetMode(EventZoneStatus.Disabled, 0f);
    }

    


    // 이벤트 시도 루틴
    public IEnumerator TryTriggerEventRoutine()
    {
        PuzzleMode currentPuzzleMode = _parentPuzzle.CurrentPuzzleMode;
        _clickCount++;
        Debug.Log($"[EventTriggerZone] 클릭 횟수: {_clickCount}");
        _parentPuzzle.SetPuzzleMode(PuzzleMode.Waiting, 0f);
        if (!CheckConditions())
        {
            yield return _parentPuzzle.ExecuteEvent(_failureEventID);
            _parentPuzzle.SetPuzzleMode(currentPuzzleMode, 0f);
            yield break;
        }

        yield return StartCoroutine(_reward.ExecuteRoutine(_parentPuzzle));  // 보상 실행
        _successCount++;
        _isRewardCleared = true;
        _parentPuzzle.SetPuzzleMode(currentPuzzleMode, 0f);
        Debug.Log($"[EventTriggerZone] 보상 클리어 여부 : {_isRewardCleared}");
    }

    // 조건 검사
    private bool CheckConditions()
    {
        foreach (var condition in _conditions)
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

    // 모드에 따른 버튼 아이콘 및 가시성 업데이트
    private void ShowButton(bool b, float duration)
    {
        if(_eventZoneBtn == null){
            Debug.LogWarning("ShowButton 실패, _evenzontbtn is null");
        }
        _eventZoneBtn.SetActiveWithScale(b, duration);
    }

    public void SetMode(EventZoneStatus zoneStatus, float duration){

        if(_eventZoneBtn != null){
            Destroy(_eventZoneBtn.gameObject);
        }
        CreateEventZoneBtn(zoneStatus);
        switch (zoneStatus){
            case EventZoneStatus.Move:
                ShowButton(true, duration);
                break;
            case EventZoneStatus.Transparent:
                ShowButton(true, duration);
                break;
            case EventZoneStatus.Discovered:
                ShowButton(true, duration);
                break;
            case EventZoneStatus.Checked:
                ShowButton(true, duration);
                break;
            case EventZoneStatus.MoveLocked:
                ShowButton(true, duration);
                break;
            default:
                break;
        }
    }
    private void CreateEventZoneBtn(EventZoneStatus eventZoneStatus){

        DesignedIconButton btnPrefabToSpawn;
        switch(eventZoneStatus){
            case EventZoneStatus.Move:
                btnPrefabToSpawn = PuzzleManager.Instance.MoveBtn;
                break;
            case EventZoneStatus.Transparent:
                btnPrefabToSpawn = PuzzleManager.Instance.TransparentBtn;
                break;
            case EventZoneStatus.Discovered:
                btnPrefabToSpawn = PuzzleManager.Instance.DiscoveredBtn;
                break;
            case EventZoneStatus.Checked:
                btnPrefabToSpawn = PuzzleManager.Instance.CheckedBtn;
                break;
            case EventZoneStatus.MoveLocked:
                btnPrefabToSpawn = PuzzleManager.Instance.MoveLockedBtn;
                break;
            default:
                btnPrefabToSpawn = null;
                break;
        }
        if(btnPrefabToSpawn != null){
            _eventZoneBtn = Instantiate(btnPrefabToSpawn, _parentPuzzle.BtnParent);
            // 클릭 이벤트에 TryTriggerEventRoutine 연결
            _eventZoneBtn.Button.onClick.AddListener(() => StartCoroutine(TryTriggerEventRoutine()));

            _eventZoneBtn.transform.position = transform.position;
            ShowButton(false, 0f);
        }
    }

    // OnValidate: GameObject 이름 기반으로 보상 자동 할당
    private void OnValidate()
    {
        AssignRewardFromName();
    }

    // 이름을 분석하여 보상을 할당하는 메서드
    private void AssignRewardFromName()
    {
        string name = gameObject.name.Trim();

        if (name.StartsWith("(->)"))
        {
            _reward = new EventResult { Type = EventResult.ResultType.Move, Parameter = name.Substring(4).Trim() };
        }
        else if (name.StartsWith("(E)"))
        {
            _reward = new EventResult { Type = EventResult.ResultType.Event, Parameter = name.Substring(3).Trim() };
        }
        else if (name.StartsWith("(+)"))
        {
            _reward = new EventResult { Type = EventResult.ResultType.ItemGain, Parameter = name.Substring(3).Trim() };
        }
        else if (name.StartsWith("(-)"))
        {
            _reward = new EventResult { Type = EventResult.ResultType.ItemLose, Parameter = name.Substring(3).Trim() };
        }

        Debug.Log($"[EventTriggerZone] '{gameObject.name}'에서 보상 할당 완료: {_reward.Type}, {_reward.Parameter}");
    }
}
