using System.Collections;
using UnityEngine;
using DG.Tweening;  // DOTween을 사용하기 위해 추가

public class CharacterExit : Element
{
    private string _characterID;
    private float _duration;

    public CharacterExit(string characterID, float duration = 1f)
    {
        this._characterID = characterID;
        this._duration = duration;
    }

    public override void ExecuteInstantly()
    {
        _duration = 0;
        Execute();
    }

    public override IEnumerator ExecuteRoutine()
    {
        // 애니메이션 완료 후 캐릭터 제거
        CharacterManager.Instance.FadeoutThenDestroyCharacter(_characterID, _duration);
        yield return new WaitForSeconds(_duration); // 애니메이션이 완료될 때까지 대기
    }
}
