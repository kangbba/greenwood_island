using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  // DOTween을 사용하기 위해 추가

[System.Serializable]
public class AllCharactersClear : Element
{
    private float _duration;
    private Ease _easeType;


    public AllCharactersClear(float duration = 1f, Ease easeType = Ease.InQuad)
    {
        this._duration = duration;
        this._easeType = easeType;
    }

    public override IEnumerator ExecuteRoutine()
    {
        // 각 캐릭터에 대해 CharacterExit을 생성하여 실행
        var activeCharacterIDs = CharacterManager.Instance.GetAllActiveCharacterIDs();
        foreach (var characterID in activeCharacterIDs)
        {
            new CharacterExit(characterID, _duration, _easeType).Execute();
        }
        yield return new WaitForSeconds(_duration);

    }
}