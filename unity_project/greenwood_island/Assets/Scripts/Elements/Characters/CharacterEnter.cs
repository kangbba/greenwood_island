using System.Collections;
using UnityEngine;
using DG.Tweening;  // DOTween을 사용하기 위해 추가

public class CharacterEnter : Element
{
    private string _characterID;
    private string _initialEmotionID; // 초기 이모션 설정
    private float _screenPeroneX;
    private float _duration;
    private int _emotionIndex;

    public CharacterEnter(string characterID, string initialEmotionID, float screenPeroneX, int emotionIndex = 0, float duration = 1f)
    {
        this._characterID = characterID;
        this._initialEmotionID = initialEmotionID; // 초기 이모션 설정
        this._screenPeroneX = screenPeroneX;
        this._emotionIndex = emotionIndex; 
        this._duration = duration;
    }

    public override IEnumerator ExecuteRoutine()
    {
        // 캐릭터 생성 및 위치 설정
        Character character = CharacterManager.CreateCharacter(_characterID, _screenPeroneX);
        if (character == null)
        {
            Debug.LogWarning($"Failed to instantiate character with ID: {_characterID}");
            yield break;
        }
        // 애니메이션 완료까지 대기
        yield return new WaitForSeconds(_duration);
        character.ChangeEmotion(_initialEmotionID, _emotionIndex);
    }
}
