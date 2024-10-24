using System.Collections;
using UnityEngine;

public class BGMEnter : Element
{
    private string _bgmID;
    private float _volume;

    // 생성자: SFX ID, 볼륨, 반복 여부, 반복 간격을 설정
    public BGMEnter(string bgmID, float volume = 1f)
    {
        _bgmID = bgmID;
        _volume = Mathf.Clamp(volume, 0f, 1f); // 볼륨을 0에서 1 사이로 제한
    }
    public override void ExecuteInstantly()
    {
    }

    public override IEnumerator ExecuteRoutine()
    {
        SFXManager.Instance.PlayBGM(_bgmID, _volume); // 볼륨과 반복 간격을 전달
        yield return null;
    }
}
