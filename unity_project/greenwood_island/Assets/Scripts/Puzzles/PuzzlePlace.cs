using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PuzzlePlace : MonoBehaviour
{
    [SerializeField, ReadOnly] private List<EventTriggerZone> _eventTriggerZones = new List<EventTriggerZone>();
    public string PlaceID => gameObject.name;

    [SerializeField] private string _enterEventID;
    [SerializeField] private string _exitEventID;
    [SerializeField] private List<EventCondition> _unlockConditions;
    [SerializeField] private string _failureEventID;

    private Puzzle _parentPuzzle;
    private PuzzlePlace _parentPlace;
    private bool _isVisited = false;
    public bool IsVisited => _isVisited;
    public PuzzlePlace ParentPlace => _parentPlace;

    private void Awake()
    {
        // 기본 이미지 설정 초기화
        var image = GetComponent<Image>();
        if (image != null)
        {
            image.sprite = null;
            image.color = Color.clear;
            image.raycastTarget = false;
        }
    }
    private void OnValidate(){
        AssignEventTriggerZones();
    }
    // 직속 자식 오브젝트에서 EventTriggerZone 자동 할당
    private void AssignEventTriggerZones()
    {
        _eventTriggerZones.Clear();

        foreach (Transform child in transform)
        {
            var zone = child.GetComponent<EventTriggerZone>();
            if (zone != null)
            {
                _eventTriggerZones.Add(zone);
            }
        }

        _enterEventID = $"{name}Enter";
        _exitEventID = $"{name}Exit";

        Debug.Log($"[PuzzlePlace] '{PlaceID}'에 {_eventTriggerZones.Count}개의 이벤트 트리거 존이 할당되었습니다.");
    }

    public void InitPuzzlePlace(Puzzle parentPuzzle)
    {
        _parentPuzzle = parentPuzzle;
        Debug.Log($"[PuzzlePlace] '{PlaceID}'가 부모 퍼즐과 연결되었습니다.");

        foreach (var zone in _eventTriggerZones)
        {
            zone.Init(parentPuzzle);

            if (zone.IsRewardMoveType)
            {
                string placeIdToGo = zone.Reward.Parameter;
                var targetPlace = _parentPuzzle.GetPlace(placeIdToGo);
                if (targetPlace != null)
                {
                    targetPlace.SetParentPlace(this);
                }
            }
        }
    }

    public void SetParentPlace(PuzzlePlace parentPlace)
    {
        _parentPlace = parentPlace;
        Debug.Log($"{PlaceID}의 부모가 {parentPlace.PlaceID}로 설정되었습니다.");
    }

    public IEnumerator TryToEnterRoutine()
    {
        if (!CheckUnlockConditions())
        {
            yield return _parentPuzzle.ExecuteEvent(_failureEventID);
            yield break;
        }

        if(!_isVisited){
            _isVisited = true;
            yield return _parentPuzzle.ExecuteEvent(_enterEventID);
        }
    }
    public IEnumerator TryToExitRoutine()
    {
        yield return _parentPuzzle.ExecuteEvent(_exitEventID);
    }

    public bool CheckUnlockConditions()
    {
        foreach (var condition in _unlockConditions)
        {
            if (!condition.IsConditionMet())
            {
                Debug.Log($"[PuzzlePlace] 조건 미충족: {condition}");
                return false;
            }
        }
        return true;
    }
    public void HideAllZones(float duration){

        foreach (var zone in _eventTriggerZones)
        {
            zone.SetMode(EventZoneStatus.Disabled, duration);
        }
    }
    public void ShowMoveZonesProperly(float duration)
    {
        var moveZones = _eventTriggerZones.Where(zone => zone.IsRewardMoveType).ToList();

        foreach (var zone in moveZones)
        {   
            if(zone.IsConditionExist){
                zone.SetMode(EventZoneStatus.MoveLocked, duration);
            }
            else{
                zone.SetMode(EventZoneStatus.Move, duration);
            }
        }
    }
    public void ShowEtcZonesProperly(float duration)
    {
        var etcZones = _eventTriggerZones.Where(zone => !zone.IsRewardMoveType).ToList();

        foreach (var zone in etcZones)
        {
            if(zone.IsRewardCleared){
                zone.SetMode(EventZoneStatus.Checked, duration);
            }
            else{
                if(zone.SuccessCount == 0){
                    zone.SetMode(EventZoneStatus.Transparent, duration);
                }
                else{
                    zone.SetMode(EventZoneStatus.Discovered, duration);
                }
            }
        }
    }
}
