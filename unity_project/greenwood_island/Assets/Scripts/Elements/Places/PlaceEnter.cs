using System.Collections;
using UnityEngine;

/// <summary>
/// PlaceEnter 클래스는 특정 장소로 전환할 때의 기본 구조를 제공합니다.
/// 전환 전후의 효과를 SequentialElement로 받아서 실행합니다.
/// </summary>
public class PlaceEnter : Element
{
    private string _placeID; // 이동할 새로운 장소의 이미지 ID

    // 생성자: 이미지 ID와 현재 스토리 객체를 받아서 초기화
    public PlaceEnter(string placeID)
    {
        _placeID = placeID;
    }

    public string PlaceID { get => _placeID; }

    public override IEnumerator ExecuteRoutine()
    {
        Debug.Log("PlaceEnter :: 새로운 장소 인스턴스화 시도 했음");

        // 현재 스토리의 이름을 사용하여 새로운 장소 생성
        Place newPlace = PlaceManager.CreatePlace(_placeID);

        // 이미지가 정상적으로 로드되었는지 확인
        if (newPlace == null)
        {
            Debug.LogError("PlaceEnter :: 새로운 장소를 생성하지 못했습니다.");
            yield break;
        }

        // 장소 전환 후 효과 실행
        yield return null;
    }
}
