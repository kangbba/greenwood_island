using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PuzzlePlaceData : MonoBehaviour
{
    [Serializable]
    public class MovePlan
    {
        [SerializeField] private bool _isLocked;
        [SerializeField] private Transform _knobTr;
        [SerializeField] private PuzzlePlaceData _placeDataToMove;

        public PuzzlePlaceData PlaceDataToMove { get => _placeDataToMove; }
        public Transform KnobTr { get => _knobTr; }

    }

    [Serializable]
    public class EventPlan
    {
        [SerializeField] private Transform _knobTr;
        [SerializeField] private string _eventID;

        public Transform KnobTr { get => _knobTr; }
        public string EventID { get => _eventID; }
    }

    [SerializeField] private List<MovePlan> _movePlans;
    [SerializeField] private List<EventPlan> _eventPlans;
    // 추가: ParentPlaceData 속성
    private PuzzlePlaceData _parentPlaceData;

    public string PlaceID => gameObject.name;

    public List<MovePlan> MovePlans { get => _movePlans; }
    public List<EventPlan> EventPlans { get => _eventPlans; }
    public PuzzlePlaceData ParentPlaceData { get => _parentPlaceData;  }

    private void Awake(){
        GetComponent<Image>().sprite = null;
        GetComponent<Image>().color = Color.clear;
        // MovePlans 순회하며 각 placeDataToMove의 부모를 현재 객체로 설정
        foreach (var movePlan in _movePlans)
        {
            if (movePlan.PlaceDataToMove != null)
            {
                movePlan.PlaceDataToMove.SetParentPlace(this);
            }
        }
    }
    // ParentPlaceData를 설정하는 메서드
    public void SetParentPlace(PuzzlePlaceData parent)
    {
        _parentPlaceData = parent;
        Debug.Log($"{PlaceID}의 부모가 {parent.PlaceID}로 설정되었습니다.");
    }
}
