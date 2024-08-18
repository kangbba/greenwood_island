using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  // DOTween을 사용하기 위해 추가

[System.Serializable]
public class CharactersEnter : Element
{
    private List<ECharacterID> _characterIDs;
    private List<float> _screenPeroneXs; // 각 캐릭터의 위치 비율을 관리
    private float _duration;
    private Ease _easeType;

    public CharactersEnter(List<ECharacterID> characterIDs, List<float> screenPeroneXs, float duration = 1f, Ease easeType = Ease.OutQuad)
    {
        // 각 캐릭터에 대응하는 위치 리스트가 동일한 길이인지 확인
        if (characterIDs.Count != screenPeroneXs.Count)
        {
            Debug.LogError("CharacterIDs and ScreenPeroneXs lists must have the same number of elements.");
            return;
        }

        this._characterIDs = characterIDs;
        this._screenPeroneXs = screenPeroneXs;
        this._duration = duration;
        this._easeType = easeType;
    }

    public override IEnumerator Execute()
    {
        for (int i = 0; i < _characterIDs.Count; i++)
        {
            EnterCharacter(_characterIDs[i], _screenPeroneXs[i]);
        }

        // 애니메이션 완료까지 대기
        yield return new WaitForSeconds(_duration);
    }

    private void EnterCharacter(ECharacterID characterID, float screenPeroneX)
    {
        // 캐릭터 생성 및 위치 설정
        Character character = CharacterManager.Instance.InstantiateCharacter(characterID, screenPeroneX);

        if (character == null)
        {
            Debug.LogWarning($"Failed to instantiate character with ID: {characterID}");
            return;
        }

        // 등장 애니메이션 (예: 투명도 0 -> 1)
        CanvasGroup canvasGroup = character.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0;
            canvasGroup.DOFade(1, _duration).SetEase(_easeType);
        }
    }
}
