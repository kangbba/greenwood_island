using System.Collections;
using UnityEngine;
using DG.Tweening;  // DOTween을 사용하기 위해 추가

public class CharacterEnter : Element
{
    private string _characterID;
    private EmotionType _initialEmotionType; // 초기 감정 설정을 enum으로 변경
    private float _screenPeroneX;
    private float _duration;
    private int _emotionIndex;
    private Character.AnchorType _anchorType;  // 앵커 타입 enum 추가

    // 생성자에서 EmotionType과 AnchorType을 받도록 수정
    public CharacterEnter(string characterID, EmotionType initialEmotionType, float screenPeroneX, Character.AnchorType anchorType = Character.AnchorType.Bottom, int emotionIndex = 0, float duration = 1f)
    {
        this._characterID = characterID;
        this._initialEmotionType = initialEmotionType; // 초기 감정을 enum으로 설정
        this._screenPeroneX = screenPeroneX;
        this._emotionIndex = emotionIndex;
        this._duration = duration;
        this._anchorType = anchorType;  // 앵커 타입 설정
    }

    public override IEnumerator ExecuteRoutine()
    {
        // 캐릭터 생성 및 위치 설정
        Character character = CharacterManager.Instance.CreateCharacter(_characterID, _screenPeroneX);
        if (character == null)
        {
            Debug.LogWarning($"Failed to instantiate character with ID: {_characterID}");
            yield break;
        }

        // 애니메이션 완료까지 대기
        yield return new WaitForSeconds(_duration);

        // 감정을 enum 기반으로 변경
        character.ChangeEmotion(_initialEmotionType, _emotionIndex);

        // RectMask 설정도 enum 기반으로 변경
        character.SetRectMask(_initialEmotionType, _anchorType, true, _duration, Ease.Linear);
    }
}
