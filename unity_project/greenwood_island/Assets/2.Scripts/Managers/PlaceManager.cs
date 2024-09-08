using System.Collections.Generic;
using UnityEngine;

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

    public Place CurrentPlace => _currentPlace;

    [SerializeField]
    private Place _placePrefab; // 빈 프리팹을 참조할 변수

    private Place _currentPlace; // 현재 활성화된 장소
    private Place _previousPlace; // 이전에 활성화된 장소
    private List<Place> _activePlaces = new List<Place>(); // 활성화된 장소 리스트

    // 이미지 ID와 스토리 이름을 받아 새로운 장소를 생성하는 메서드
    public Place CreatePlace(string imageID, string storyName)
    {
        // 이미지 로드 시도
        Sprite placeImage = LoadPlaceImage(imageID, storyName);

        if (placeImage == null)
        {
            Debug.LogError($"Failed to load place image with ID '{imageID}' from story '{storyName}' or shared resources.");
            return null;
        }

        // 새로운 장소를 인스턴스화
        _currentPlace = Instantiate(_placePrefab, UIManager.Instance.WorldCanvas.PlaceLayer);

        if (_currentPlace == null)
        {
            Debug.LogError("Failed to create new place.");
            return null;
        }

        // 생성된 장소의 이미지 설정
        _currentPlace.SetImage(placeImage);

        _previousPlace = _currentPlace; // 생성된 장소를 이전 장소로 설정
        _activePlaces.Add(_currentPlace); // 활성화된 장소 리스트에 추가

        return _currentPlace;
    }

    // 특정 placeID에 해당하는 활성화된 Place 반환
    public Place GetActivePlace(string placeID)
    {
        foreach (var place in _activePlaces)
        {
            if (place != null && place.name == placeID)
            {
                return place;
            }
        }

        Debug.LogWarning($"Place with ID '{placeID}' is not currently active.");
        return null;
    }

    // 장소 이미지를 로드하는 메서드 (스토리 자원 우선, 실패 시 공유 자원 로드)
    private Sprite LoadPlaceImage(string imageID, string storyName)
    {
        // 스토리 경로에서 이미지 로드 시도
        string storyPlacePath = ResourcePathManager.GetResourcePath(imageID, storyName, ResourceType.Place, false);
        Sprite placeImage = Resources.Load<Sprite>(storyPlacePath);

        // 스토리 경로에서 로드 성공 시 로그 출력
        if (placeImage != null)
        {
            Debug.Log($"Place image '{imageID}' loaded from Story path: '{storyPlacePath}'.");
            return placeImage;
        }

        // 스토리 경로에서 로드 실패 시, 공유 경로에서 로드 시도
        string sharedPlacePath = ResourcePathManager.GetResourcePath(imageID, storyName, ResourceType.Place, true);
        placeImage = Resources.Load<Sprite>(sharedPlacePath);

        // 공유 경로에서 로드 성공 시 로그 출력
        if (placeImage != null)
        {
            Debug.Log($"Place image '{imageID}' loaded from Shared path: '{sharedPlacePath}'.");
        }

        return placeImage;
    }

    // 이전 장소 파괴는 이제 수동으로 처리할 예정이므로 이 메서드는 호출하지 않음
    private void DestroyPreviousPlace()
    {
        if (_previousPlace == null)
        {
            Debug.LogWarning("PlaceManager :: Previous Place is null");
            return;
        }
        _activePlaces.Remove(_previousPlace); // 리스트에서 제거
        Destroy(_previousPlace.gameObject);
    }
}
