using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  // DOTween을 사용하기 위해 추가

[System.Serializable]
public class AllCharactersExit : Element
{
    private float _duration;
    private Ease _easeType;


    public AllCharactersExit(float duration = 1f, Ease easeType = Ease.InQuad)
    {
        this._duration = duration;
        this._easeType = easeType;
    }

    public override IEnumerator ExecuteRoutine()
    {
        List<Coroutine> exitCoroutines = new List<Coroutine>();

        // 각 캐릭터에 대해 CharacterExit을 생성하여 실행
        var activeCharacterIDs = CharacterManager.Instance.GetAllActiveCharacterIDs();
        foreach (var characterID in activeCharacterIDs)
        {
            CharacterExit characterExit = new CharacterExit(characterID, _duration, _easeType);
            Coroutine exitCoroutine = CoroutineRunner.Instance.StartCoroutine(characterExit.ExecuteRoutine());
            exitCoroutines.Add(exitCoroutine);
        }

        // 모든 퇴장 코루틴이 완료될 때까지 대기
        foreach (var coroutine in exitCoroutines)
        {
            yield return coroutine;
        }
    }
}