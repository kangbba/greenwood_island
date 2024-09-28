using System.Collections;
using UnityEngine;
using DG.Tweening;  // DOTween을 사용하기 위해 추가

[System.Serializable]
public class CharacterExit : Element
{
    private string _characterID;
    private float _duration;

    public CharacterExit(string characterID, float duration = 1f)
    {
        this._characterID = characterID;
        this._duration = duration;
    }

    public override IEnumerator ExecuteRoutine()
    {
        // 캐릭터 가져오기
        Character character = CharacterManager.GetActiveCharacter(_characterID);
        if (character == null)
        {
            Debug.LogWarning($"No active character found with ID: {_characterID} to exit.");
            yield break;
        }
        character.AllEmotionsFadeOut(_duration);
        yield return new WaitForSeconds(_duration); // 애니메이션이 완료될 때까지 대기

        // 애니메이션 완료 후 캐릭터 제거
        CharacterManager.DestroyCharacter(_characterID);
    }
}
