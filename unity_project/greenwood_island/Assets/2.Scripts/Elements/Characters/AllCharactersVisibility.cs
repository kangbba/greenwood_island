using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  // DOTween을 사용하기 위해 추가

[System.Serializable]
public class AllCharactersVisibility : Element
{
    private float _alpha;
    private float _duration;
    private Ease _easeType;


    public AllCharactersVisibility(float alpha, float duration = 1f, Ease easeType = Ease.InQuad)
    {
        this._alpha = alpha;
        this._duration = duration;
        this._easeType = easeType;
    }

    public override IEnumerator ExecuteRoutine()
    {
        // 각 캐릭터에 대해 CharacterExit을 생성하여 실행
        var activeCharacterIDs = CharacterManager.Instance.GetAllActiveCharacterIDs();
        foreach (var characterID in activeCharacterIDs)
        {
            Character character = CharacterManager.Instance.GetActiveCharacter(characterID);
            character.SetVisibility(_alpha, _duration, _easeType);
        }
        yield return new WaitForSeconds(_duration);

    }
}