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
        List<Coroutine> enterCoroutines = new List<Coroutine>();

        // 각 캐릭터에 대해 CharacterEnter을 생성하여 실행
        for (int i = 0; i < _characterIDs.Count; i++)
        {
            CharacterEnter characterEnter = new CharacterEnter(_characterIDs[i], _screenPeroneXs[i], _duration, _easeType);
            Coroutine enterCoroutine = CoroutineRunner.Instance.StartCoroutine(characterEnter.Execute());
            enterCoroutines.Add(enterCoroutine);
        }

        // 모든 캐릭터의 등장 코루틴이 완료될 때까지 대기
        foreach (var coroutine in enterCoroutines)
        {
            yield return coroutine;
        }
    }
}