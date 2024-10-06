using System.Collections;
using UnityEngine;
using DG.Tweening;

[System.Serializable]
public class CharacterMove : Element
{
    private string _characterID; // 캐릭터 ID를 문자열로 받음
    private float _targetScreenPeroneX; // 목표 위치 비율 (0.0f: 왼쪽, 1.0f: 오른쪽)
    private float _duration;
    private Ease _easeType;

    public CharacterMove(string characterID, float targetScreenPeroneX, float duration = 1f, Ease easeType = Ease.OutQuad)
    {
        this._characterID = characterID;
        this._targetScreenPeroneX = targetScreenPeroneX;
        this._duration = duration;
        this._easeType = easeType;
    }

    public override IEnumerator ExecuteRoutine()
    {
        // 기존 캐릭터 가져오기
        Character character = CharacterManager.Instance.GetActiveCharacter(_characterID);

        if (character == null)
        {
            Debug.LogWarning($"No active character found with ID: {_characterID} to move.");
            yield break;
        }

        // 캐릭터 이동
        CharacterManager.Instance.MoveCharacter(_characterID, _targetScreenPeroneX, _duration, _easeType);
        yield return new WaitForSeconds(_duration);
    }
}
