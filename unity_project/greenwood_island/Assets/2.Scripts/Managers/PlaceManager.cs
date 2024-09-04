using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public enum EPlaceID
{
    Town1,
    Town2,
    Town3,
    Mountain,
    GreenwoodIsland,
    RyanRoom
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

    private Dictionary<EPlaceID, Place> _activePlaces = new Dictionary<EPlaceID, Place>(); // 활성화된 장소를 관리할 딕셔너리

    private Place _currentPlace; // 현재 활성화된 장소를 나타내는 필드

    public Place CurrentPlace => _currentPlace; // 현재 장소를 반환하는 게터

    public Place InstantiatePlace(EPlaceID placeID)
    {
        Place placePrefab = _placePrefabs.Find(p => p.PlaceID == placeID);

        if (placePrefab == null)
        {
            Debug.LogError($"Place prefab with ID {placeID} not found.");
            return null;
        }

        // 현재 장소가 존재하면 파괴 후 새로운 장소로 설정
        if (_currentPlace != null)
        {
            DestroyPlace(_currentPlace.PlaceID);
        }

        // 이미 활성화된 장소가 있는 경우 해당 장소 반환
        if (_activePlaces.ContainsKey(placeID))
        {
            Debug.LogWarning($"Place with ID {placeID} is already instantiated.");
            return _activePlaces[placeID];
        }

        // 장소를 생성하여 PlaceLayer에 추가
        Transform placeLayer = UIManager.Instance.WorldCanvas.PlaceLayer;
        Place place = Instantiate(placePrefab, placeLayer);

        _activePlaces.Add(placeID, place);
        _currentPlace = place; // 생성된 장소를 현재 장소로 설정

        // 디버깅 로그 추가
        Debug.Log($"Current place set to {_currentPlace.PlaceID}");

        return place;
    }

    // 기존의 DestroyPlace 메서드를 private으로 변경하여 외부에서 직접 호출되지 않도록 설정
    private void DestroyPlace(EPlaceID placeID)
    {
        if (_activePlaces.TryGetValue(placeID, out Place place))
        {
            Destroy(place.gameObject);
            _activePlaces.Remove(placeID);

            // 파괴된 장소가 현재 장소와 같다면 _currentPlace를 null로 설정
            if (_currentPlace == place)
            {
                _currentPlace = null;
                Debug.Log("Current place destroyed, set to null.");
            }
        }
        else
        {
            Debug.LogWarning($"No active place found with ID: {placeID} to destroy.");
        }
    }

    public Place GetActivePlace(EPlaceID placeID)
    {
        _activePlaces.TryGetValue(placeID, out Place place);
        return place;
    }
}
