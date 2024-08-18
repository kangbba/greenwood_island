using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  // DOTween을 사용하기 위해 추가

[System.Serializable]
public class CharactersExit : Element
{
    private List<ECharacterID> _characterIDs;
    private float _duration;
    private Ease _easeType;

    public CharactersExit(List<ECharacterID> characterIDs, float duration = 1f, Ease easeType = Ease.InQuad)
    {
        this._characterIDs = characterIDs;
        this._duration = duration;
        this._easeType = easeType;
    }

    public override IEnumerator Execute()
    {
        // 캐릭터들을 순차적으로 페이드 아웃 처리
        foreach (var characterID in _characterIDs)
        {
            // 캐릭터 가져오기
            Character character = CharacterManager.Instance.GetActiveCharacter(characterID);

            if (character == null)
            {
                Debug.LogWarning($"No active character found with ID: {characterID} to exit.");
                continue;
            }

            // 퇴장 애니메이션 (예: 투명도 1 -> 0)
            CanvasGroup canvasGroup = character.GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                canvasGroup.DOFade(0, _duration).SetEase(_easeType);
            }
        }

        // 애니메이션 완료까지 대기
        yield return new WaitForSeconds(_duration);

        // 애니메이션 완료 후 캐릭터 제거
        foreach (var characterID in _characterIDs)
        {
            CharacterManager.Instance.DestroyCharacter(characterID);
        }
    }
}
