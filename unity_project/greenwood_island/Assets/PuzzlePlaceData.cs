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
        public Transform btnTr;
        public PuzzlePlaceData placeDataToMove;
        public string eventID;
    }

    [Serializable]
    public class EventPlan
    {
        public Transform btnTr;
        public string eventID;
    }

    [SerializeField] private bool _isCleared = true;
    [SerializeField] private List<MovePlan> _movePlans;
    [SerializeField] private List<EventPlan> _eventPlans;

    public string PlaceID => gameObject.name;

    public List<MovePlan> MovePlans { get => _movePlans; }
    public List<EventPlan> EventPlans { get => _eventPlans; }
    public bool IsCleared { get => _isCleared; }

    private void Awake(){
        GetComponent<Image>().sprite = null;
        GetComponent<Image>().color = Color.clear;
    }

    public void SetCleared(bool b){
        Debug.Log("Set Cleared!");
        _isCleared = b;
    }
}
