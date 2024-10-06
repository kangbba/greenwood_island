using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlaceManager : SingletonManager<PlaceManager>
{
    private Place _currentPlace; // 현재 활성화된 장소
    private List<Place> _activePlaces = new List<Place>(); // 활성화된 장소 리스트


    // 현재 활성화된 장소 반환
    public Place CurrentPlace => _currentPlace;

    // 새로운 장소를 생성하는 메서드
    public Place CreatePlace(string imageID)
    {
        // 현재 스토리 이름을 사용하여 이미지 로드 시도
        Sprite placeImage = LoadPlaceImage(imageID);

        if (placeImage == null)
        {
            Debug.LogError($"Failed to load place image with ID '{imageID}' from story or shared resources.");
            return null;
        }

        // PlacePrefab 로드
        Place placePrefab = UIManager.WorldCanvas.PlacePrefab;
        if (placePrefab == null)
        {
            Debug.LogError("Failed to load PlacePrefab from Resources/PlacePrefab. Ensure the prefab exists and has a Place component attached.");
            return null;
        }

        // 새로운 장소를 인스턴스화
        _currentPlace = Object.Instantiate(placePrefab, UIManager.WorldCanvas.PlaceLayer);

        if (_currentPlace == null)
        {
            Debug.LogError("Failed to create new place.");
            return null;
        }

        // 생성된 장소의 이미지 설정
        _currentPlace.SetImage(placeImage);
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
    private Sprite LoadPlaceImage(string placeID)
    {
        // 스토리 경로에서 이미지 로드 시도
        string storyPlacePath = ResourcePathManager.GetCurrentStoryResourcePath(placeID, ResourceType.Place);
        Sprite placeImage = Resources.Load<Sprite>(storyPlacePath);

        // 스토리 경로에서 로드 성공 시 로그 출력
        if (placeImage != null)
        {
            Debug.Log($"Place image '{placeID}' loaded from Story path: '{storyPlacePath}'.");
            return placeImage;
        }

        // 스토리 경로에서 로드 실패 시, 공유 경로에서 로드 시도
        string sharedPlacePath = ResourcePathManager.GetSharedResourcePath(placeID, ResourceType.Place);
        placeImage = Resources.Load<Sprite>(sharedPlacePath);

        // 공유 경로에서 로드 성공 시 로그 출력
        if (placeImage != null)
        {
            Debug.Log($"Place image '{placeID}' loaded from Shared path: '{sharedPlacePath}'.");
        }

        return placeImage;
    }

    public void DestroyPreviousPlaces()
    {
        // 활성화된 장소가 2개 미만이면 경고 출력 후 반환
        if (_activePlaces.Count < 2)
        {
            Debug.LogWarning("PlaceManager :: 파괴할 이전 장소가 없습니다.");
            return;
        }

        // 마지막 장소를 제외한 이전 장소들을 담을 리스트 생성
        List<Place> placesToDestroy = new List<Place>(_activePlaces.Take(_activePlaces.Count - 1));

        // 남겨두지 않을 장소들을 순회하여 파괴
        foreach (Place placeToDestroy in placesToDestroy)
        {
            _activePlaces.Remove(placeToDestroy);  // 리스트에서 제거
            Object.Destroy(placeToDestroy.gameObject);  // 오브젝트 파괴
            Debug.Log($"PlaceManager :: 장소 '{placeToDestroy.name}' 파괴 완료");
        }
    }


}
