using System.Collections;
using UnityEngine;
using DG.Tweening;  // DOTween을 사용하기 위해 추가

public class CharacterEnter : Element
{
    private string _characterID;
    private string _initialEmotionID; // 초기 감정 설정을 enum으로 변경
    private float _screenPeroneX;
    private float _duration;

    public CharacterEnter(string characterID, CommonEmotionID emotionID, float screenPeroneX, float duration = .5f)
    {
        this._characterID = characterID;
        this._initialEmotionID = emotionID.ToString(); 
        this._screenPeroneX = screenPeroneX;
        _duration = duration;
    }
    public CharacterEnter(string characterID, AmalianEmotionID emotionID, float screenPeroneX, float duration = .5f)
    {
        this._characterID = characterID;
        this._initialEmotionID = emotionID.ToString(); 
        this._screenPeroneX = screenPeroneX;
        _duration = duration;
    }
    public CharacterEnter(string characterID, KateEmotionID emotionID, float screenPeroneX, float duration = .5f)
    {
        this._characterID = characterID;
        this._initialEmotionID = emotionID.ToString(); 
        this._screenPeroneX = screenPeroneX;
        _duration = duration;
    }

    public string CharacterID { get => _characterID; }
    public float Duration { get => _duration; }

    public override void ExecuteInstantly()
    {
        _duration = 0;
        Execute();
    }

    public override IEnumerator ExecuteRoutine()
    {
        // 캐릭터 생성 및 위치 설정
        Character character = CharacterManager.Instance.CreateCharacter(_characterID, _initialEmotionID, _screenPeroneX);
        if (character == null)
        {
            Debug.LogWarning($"Failed to instantiate character with ID: {_characterID}");
            yield break;
        }
        // 애니메이션 완료까지 대기
        yield return new WaitForSeconds(_duration);
    }
}
