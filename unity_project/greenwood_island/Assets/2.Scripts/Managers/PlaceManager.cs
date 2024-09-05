using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public enum EPlaceID
{
    Town1,
    Town2,
    Town3,
    Mountain,
    GreenwoodIsland,
    RyanRoom,
    Lobby
    // 다른 장소를 여기에 추가
}

public class PlaceManager : MonoBehaviour
{
    private static PlaceManager _instance;
    public static PlaceManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlaceManager>();

                if (_instance == null)
                {
                    GameObject go = new GameObject("PlaceManager");
                    _instance = go.AddComponent<PlaceManager>();
                }
            }
            return _instance;
        }
    }


    [SerializeField]
    private List<Place> _placePrefabs; // 미리 정의된 장소 프리팹 리스트

    private Place _currentPlace; // 현재 활성화된 장소를 나타내는 필드
    private Place _previousPlace; // 현재 활성화된 장소를 나타내는 필드


    public Place InstantiatePlace(EPlaceID placeID)
    {
        Place placePrefab = _placePrefabs.Find(p => p.PlaceID == placeID);

        if (placePrefab == null)
        {
            Debug.LogError($"Place prefab with ID {placeID} not found.");
            return null;
        }

        _previousPlace = _currentPlace;

        // 장소를 생성하여 PlaceLayer에 추가

        Transform placeLayer = UIManager.Instance.WorldCanvas.PlaceLayer;
        _currentPlace = Instantiate(placePrefab, placeLayer);
        Debug.Log($"Current place set to {_currentPlace.PlaceID}");


        return _currentPlace;
    }
    public void ChangeCurrentPlaceColor(Color color, float duration, Ease easeType)
    {
        if (_currentPlace != null && _currentPlace.Image != null)
        {
            _currentPlace.SetColor(color, duration, easeType);
        }
        else
        {
            Debug.LogWarning("PlaceManager :: Current Place is null or has no Image component.");
        }
    }
    // 기존의 DestroyPlace 메서드를 private으로 변경하여 외부에서 직접 호출되지 않도록 설정
    private void DestroyPreviousPlace()
    {
        if(_previousPlace == null){
            Debug.LogWarning("PlaceManager :: Previous Place is null");
            return;
        }
        Destroy(_previousPlace.gameObject);
    }

}
