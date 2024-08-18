using System.Collections;
using UnityEngine;
using DG.Tweening;  // DOTween을 사용하기 위해 추가

[System.Serializable]
public class CharacterEnter : Element
{
    private ECharacterID _characterID;
    private float _screenPeroneX;
    private float _duration;
    private Ease _easeType;

    public CharacterEnter(ECharacterID characterID, float screenPeroneX, float duration = 1f, Ease easeType = Ease.OutQuad)
    {
        this._characterID = characterID;
        this._screenPeroneX = screenPeroneX;
        this._duration = duration;
        this._easeType = easeType;
    }

    public override IEnumerator Execute()
    {
        // 캐릭터 생성 및 위치 설정
        Character character = CharacterManager.Instance.InstantiateCharacter(_characterID, _screenPeroneX);

        if (character == null)
        {
            Debug.LogWarning($"Failed to instantiate character with ID: {_characterID}");
            yield break;
        }

        // 등장 애니메이션 (예: 투명도 0 -> 1)
        CanvasGroup canvasGroup = character.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0;
            canvasGroup.DOFade(1, _duration).SetEase(_easeType);
        }

        // 애니메이션 완료까지 대기
        yield return new WaitForSeconds(_duration);
    }
}
