using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserActionPhaseEnter : Element
{
    // PlaceEnter 객체와 캐릭터 데이터를 저장하는 필드
    private PlaceEnter _placeEnter;                          // 장소 정보를 저장하는 PlaceEnter 객체
    private List<WorldCharacterEnter> _characterEnterList;   // 캐릭터 진입 리스트

    // 생성자를 통해 PlaceEnter 객체, 캐릭터 진입 데이터, UIEnter를 전달받음
    public UserActionPhaseEnter(PlaceEnter placeEnter, List<WorldCharacterEnter> characterEnterList)
    {
        _placeEnter = placeEnter;
        _characterEnterList = characterEnterList;
    }

    // Coroutine을 사용하여 사용자 행동 단계 실행
    public override IEnumerator ExecuteRoutine()
    {
        Debug.Log($"User action phase started at place {_placeEnter.PlaceID}");
        yield return _placeEnter.ExecuteRoutine();
        Letterbox letterbox = UIManager.Instance.SystemCanvas.LetterBox;
        letterbox.SetOn(false, 1f);

        // 1. 캐릭터 엔터리 리스트를 순회하며 캐릭터 등장 실행
        foreach (var characterEnter in _characterEnterList)
        {
            // 각 캐릭터의 등장 실행 (Coroutine 호출)
            yield return characterEnter.ExecuteRoutine();
        }


        Debug.Log($"User action phase completed at place {_placeEnter.PlaceID}");
    }
}
