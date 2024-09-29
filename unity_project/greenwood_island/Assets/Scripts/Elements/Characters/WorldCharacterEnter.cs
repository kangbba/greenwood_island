using System.Collections;
using UnityEngine;
using DG.Tweening;  // DOTween을 사용하기 위해 추가

public class WorldCharacterEnter : Element
{
    private string _characterID;
    private string _initialEmotionID;
    private Vector2 _localScale;  // 캐릭터의 크기 (Local Scale)
    private Vector2 _localPosition;  // 캐릭터의 위치 (Local Position)
    private float _duration;  // 애니메이션 지속 시간

    // 생성자를 통해 캐릭터 정보 전달
    public WorldCharacterEnter(string characterID, string initialEmotionID, Vector2 localScale, Vector2 localPosition, float duration = 1f)
    {
        _characterID = characterID;
        _initialEmotionID = initialEmotionID;
        _localScale = localScale;
        _localPosition = localPosition;
        _duration = duration;
    }

    // Coroutine을 통해 캐릭터 등장 처리
    public override IEnumerator ExecuteRoutine()
    {
        // 캐릭터를 월드에 인스턴스화
        Character character = CharacterManager.CreateCharacterOnWorld(_characterID, _localScale, _localPosition);
        
        // 캐릭터 생성이 실패했을 경우 처리
        if (character == null)
        {
            Debug.LogWarning($"Failed to instantiate character with ID: {_characterID}");
            yield break;
        }

        character.ChangeEmotion(_initialEmotionID, 0);
        // 애니메이션 완료까지 대기
        yield return new WaitForSeconds(_duration);
    }
}
