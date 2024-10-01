using System.Collections;
using UnityEngine;

public class PlaceTransition : Element
{
    private Color _overlayColor;             // 필름 오버레이 컬러
    private float _overlayDuration;          // 필름 덮는 시간
    private string _newPlaceID;              // 새로운 장소 ID
    private ParallelElement _initialCameraElements;  // 장소 전환 전 카메라 연출
    private float _clearDuration;            // 필름 제거 시간
    private bool _useCameraRestore;          // 장소 전환 후 카메라 연출 (카메라 복원 여부)

    // 생성자에서 장소 ID, 필름 오버레이 색상, 전환 시간, 카메라 연출 요소를 받아 초기화
    public PlaceTransition(
        Color overlayColor,               // 필름 오버레이 컬러
        float overlayDuration,            // 필름 덮는 시간
        string newPlaceID,                // 새로운 장소 ID
        ParallelElement initialCameraElements, // 장소 전환 전 카메라 연출
        float clearDuration,              // 필름 제거 시간
        bool useCameraRestore = true)     // 전환 후 카메라 복원 여부
    {
        _overlayColor = overlayColor;
        _overlayDuration = overlayDuration;
        _newPlaceID = newPlaceID;
        _initialCameraElements = initialCameraElements;
        _clearDuration = clearDuration;
        _useCameraRestore = useCameraRestore;
    }


    public override IEnumerator ExecuteRoutine()
    {
        // 2. 필름을 덮음 (사용자 지정 컬러로 ScreenOverlayFilm 효과)
        yield return new ScreenOverlayFilm(_overlayColor, _overlayDuration).ExecuteRoutine();

        // 3. 새로운 장소 생성
        Place newPlace = PlaceManager.CreatePlace(_newPlaceID);
        if (newPlace == null)
        {
            Debug.LogError("PlaceTransition :: 새로운 장소를 생성하지 못했습니다.");
            yield break;
        }
        // 4. 이전 장소 파괴
        PlaceManager.DestroyPreviousPlaces();
        Debug.Log("PlaceTransition :: 이전 장소 파괴 완료");
        // 1. 장소 전환 전 카메라 연출 실행
        if (_initialCameraElements != null)
        {
            Debug.Log("PlaceTransition :: 장소 전환 전 카메라 연출 시작");
            yield return _initialCameraElements.ExecuteRoutine();
        }


        // 5. 필름 제거 (ScreenOverlayFilmClear 효과)
        yield return new ScreenOverlayFilmClear(_clearDuration).ExecuteRoutine();
        // 6. 장소 전환 후 카메라 연출 실행
        if (_useCameraRestore)
        {
            new CameraZoomClear(_clearDuration).Execute();
            new CameraMove2DClear(_clearDuration).Execute();
        }


        Debug.Log("PlaceTransition :: 장소 전환 완료");
    }
}
