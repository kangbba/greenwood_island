using System.Collections;
using UnityEngine;
using DG.Tweening;  // DOTween을 사용하기 위해 추가

[System.Serializable]
public class CharacterEnter : Element
{
    private string _characterID;
    private float _screenPeroneX;
    private float _duration;
    private Ease _easeType;
    private string _initialEmotionID; // 초기 이모션 설정
    private int _emotionIndex;

    public CharacterEnter(string characterID, float screenPeroneX, string initialEmotionID, int emotionIndex, float duration = 1f, Ease easeType = Ease.OutQuad)
    {
        this._characterID = characterID;
        this._screenPeroneX = screenPeroneX;
        this._initialEmotionID = initialEmotionID; // 초기 이모션 설정
        this._emotionIndex = emotionIndex; 
        this._duration = duration;
        this._easeType = easeType;
    }

    public override IEnumerator ExecuteRoutine()
    {
        // 캐릭터 생성 및 위치 설정
        Character character = CharacterManager.InstantiateCharacter(_characterID, _screenPeroneX);
        character.ChangeEmotion(_initialEmotionID, _emotionIndex);
        if (character == null)
        {
            Debug.LogWarning($"Failed to instantiate character with ID: {_characterID}");
            yield break;
        }
        character.SetVisibility(true, _duration, _easeType);
        // 애니메이션 완료까지 대기
        yield return new WaitForSeconds(_duration);
    }
}
