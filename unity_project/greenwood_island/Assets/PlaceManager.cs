using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public enum EPlaceID
{
    Town1,
    Town2,
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

    [SerializeField] private Image _screenOverlayFilm;
    public Image ScreenOverlayFilm { get => _screenOverlayFilm; }

    public Place InstantiatePlace(EPlaceID placeID)
    {
        Place placePrefab = _placePrefabs.Find(p => p.PlaceID == placeID);

        if (placePrefab == null)
        {
            Debug.LogError($"Place prefab with ID {placeID} not found.");
            return null;
        }

        if (_activePlaces.ContainsKey(placeID))
        {
            Debug.LogWarning($"Place with ID {placeID} is already instantiated.");
            return _activePlaces[placeID];
        }

        // 장소를 생성하여 PlaceLayer에 추가
        Transform placeLayer = UIManager.Instance.WorldCanvas.PlaceLayer;
        Place place = Instantiate(placePrefab, placeLayer);

        _activePlaces.Add(placeID, place);

        return place;
    }

    public void ShowPlace(EPlaceID placeID, float duration, Ease easeType)
    {
        Place place = GetActivePlace(placeID);

        if (place == null)
        {
            Debug.LogWarning($"Place with ID {placeID} is not active and cannot be shown.");
            return;
        }

        place.SetVisibility(true, duration);
        // Optionally, you can apply additional effects or animation curves using the Ease parameter.
        CanvasGroup canvasGroup = place.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.DOFade(1, duration).SetEase(easeType);
        }
    }

    public void HidePlace(EPlaceID placeID, float duration, Ease easeType)
    {
        Place place = GetActivePlace(placeID);

        if (place == null)
        {
            Debug.LogWarning($"Place with ID {placeID} is not active and cannot be hidden.");
            return;
        }

        place.SetVisibility(false, duration);
        // Optionally, you can apply additional effects or animation curves using the Ease parameter.
        CanvasGroup canvasGroup = place.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.DOFade(0, duration).SetEase(easeType);
        }
    }

    public void DestroyPlace(EPlaceID placeID)
    {
        if (_activePlaces.TryGetValue(placeID, out Place place))
        {
            Destroy(place.gameObject);
            _activePlaces.Remove(placeID);
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

    public void DestroyAllPlaces()
    {
        foreach (var place in _activePlaces.Values)
        {
            Destroy(place.gameObject);
        }
        _activePlaces.Clear();
    }
}
