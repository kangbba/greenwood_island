using System.Collections;
using UnityEngine;

/// <summary>
/// PlaceEnter 클래스는 특정 장소로 전환할 때의 기본 구조를 제공합니다.
/// 전환 전후의 효과를 SequentialElement로 받아서 실행합니다.
/// </summary>
public class PlaceEnter : Element
{
    private EPlaceID _newPlaceID; // 이동할 새로운 장소의 ID
    private SequentialElement _enterEffect; // 장소 전환 전 실행될 효과
    private SequentialElement _exitEffect; // 장소 전환 후 실행될 효과

    // 생성자: SequentialElement를 사용하여 전환 전, 후 효과를 순차적으로 실행
    public PlaceEnter(SequentialElement enterEffect, EPlaceID newPlaceID, SequentialElement exitEffect)
    {
        _enterEffect = enterEffect ?? new SequentialElement();
        _newPlaceID = newPlaceID;
        _exitEffect = exitEffect ?? new SequentialElement();
    }

    public override IEnumerator ExecuteRoutine()
    {
        // 장소 전환 전 효과 실행
        yield return _enterEffect.ExecuteRoutine();

        // 장소 전환 실행
        Place currentPlace = PlaceManager.Instance.CurrentPlace;
        Place newPlace = PlaceManager.Instance.InstantiatePlace(_newPlaceID);

        if (newPlace != null && currentPlace != null)
        {
            PlaceManager.Instance.DestroyPlace(currentPlace.PlaceID);
        }

        // 장소 전환 후 효과 실행
        yield return _exitEffect.ExecuteRoutine();
    }
}
