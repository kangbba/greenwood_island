using System.Collections;
using UnityEngine;
using DG.Tweening;

[System.Serializable]
public class CharacterMove : Element
{
    private ECharacterID _characterID;
    private float _targetScreenPeroneX; // 목표 위치 비율 (0.0f: 왼쪽, 1.0f: 오른쪽)
    private float _duration;
    private Ease _easeType;

    public CharacterMove(ECharacterID characterID, float targetScreenPeroneX, float duration = 1f, Ease easeType = Ease.OutQuad)
    {
        this._characterID = characterID;
        this._targetScreenPeroneX = targetScreenPeroneX;
        this._duration = duration;
        this._easeType = easeType;
    }

    public override IEnumerator Execute()
    {
        // 기존 캐릭터 가져오기
        Character character = CharacterManager.Instance.GetActiveCharacter(_characterID);

        if (character == null)
        {
            Debug.LogWarning($"No active character found with ID: {_characterID} to move.");
            yield break;
        }

        // 목표 위치 계산
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found.");
            yield break;
        }

        // ViewportToWorldPoint를 사용해 목표 위치 계산 (0.0~1.0 사이의 X 값을 월드 좌표로 변환)
        Vector3 targetWorldPosition = mainCamera.ViewportToWorldPoint(new Vector3(_targetScreenPeroneX, 0.5f, character.transform.position.z - mainCamera.transform.position.z));

        // Y와 Z는 기존 값을 유지
        targetWorldPosition.y = character.transform.position.y;
        targetWorldPosition.z = character.transform.position.z;

        // 캐릭터 이동 애니메이션
        character.transform.DOMove(targetWorldPosition, _duration).SetEase(_easeType);

        // 애니메이션 완료까지 대기
        yield return new WaitForSeconds(_duration);
    }
}
