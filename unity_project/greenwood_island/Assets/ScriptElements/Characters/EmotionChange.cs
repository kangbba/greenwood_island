using System.Collections;
using UnityEngine;

public class EmotionChange : Element
{
    private string _characterID;
    private EmotionType _emotionType;
    private float _duration;

    // 생성자: 캐릭터 ID와 감정 타입, 감정 변화 시간(지속시간)을 받습니다.
    public EmotionChange(string characterID, EmotionType emotionType, float duration = .5f)
    {
        _characterID = characterID;
        _emotionType = emotionType;
        _duration = duration;
    }

    public override void ExecuteInstantly()
    {
        _duration = 0;
        Execute();
    }

    // 감정 변경 로직을 실행하는 코루틴
    public override IEnumerator ExecuteRoutine()
    {
        // 캐릭터 매니저에서 활성화된 캐릭터를 가져옴
        Character activeCharacter = CharacterManager.Instance.GetActiveCharacter(_characterID);
        
        if (activeCharacter != null)
        {
            // 캐릭터의 감정을 변경
            activeCharacter.ChangeEmotion(_emotionType, _duration);

            // 감정 변화가 완료될 때까지 대기 (지속시간)
            yield return new WaitForSeconds(_duration);
        }
        else
        {
            Debug.LogWarning($"No active character found with ID {_characterID}.");
        }

        // 감정 변화가 완료되었음을 알림
        yield break;
    }
}
