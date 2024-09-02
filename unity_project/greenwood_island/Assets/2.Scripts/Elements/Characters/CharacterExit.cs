using System.Collections;
using UnityEngine;
using DG.Tweening;  // DOTween을 사용하기 위해 추가

[System.Serializable]
public class CharacterExit : Element
{
    private ECharacterID _characterID;
    private float _duration;
    private Ease _easeType;

    public CharacterExit(ECharacterID characterID, float duration = 1f, Ease easeType = Ease.InQuad)
    {
        this._characterID = characterID;
        this._duration = duration;
        this._easeType = easeType;
    }

    public override IEnumerator ExecuteRoutine()
    {
        // 캐릭터 가져오기
        Character character = CharacterManager.Instance.GetActiveCharacter(_characterID);

        if (character == null)
        {
            Debug.LogWarning($"No active character found with ID: {_characterID} to exit.");
            yield break;
        }

        // 퇴장 애니메이션 (예: 투명도 1 -> 0)
        CanvasGroup canvasGroup = character.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.DOFade(0, _duration).SetEase(_easeType);
            yield return new WaitForSeconds(_duration); // 애니메이션이 완료될 때까지 대기
        }

        // 애니메이션 완료 후 캐릭터 제거
        CharacterManager.Instance.DestroyCharacter(_characterID);
    }
}
