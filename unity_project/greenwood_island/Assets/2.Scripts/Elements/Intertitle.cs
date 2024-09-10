using System.Collections;
using UnityEngine;

public class Intertitle : Element
{
    private string _text;
    private float _duration;

    public Intertitle(string text, float duration)
    {
        _text = text;
        _duration = duration;
    }

    public override IEnumerator ExecuteRoutine()
    {
        IntertitlePlayer _intertitlePlayer = UIManager.Instance.SystemCanvas.IntertitlePlayer;
        // duration이 0일 경우 경고 로그를 발생시키고 종료
        if (_duration <= 0)
        {
            Debug.LogWarning("Intertitle: Duration is set to 0 or less, which makes the intertitle display meaningless.");
            yield break;
        }

        // 텍스트를 지정된 duration 동안 노출
        _intertitlePlayer.ShowIntertitle(_text, _duration);

        // 텍스트 노출이 완료될 때까지 대기
        yield return new WaitForSeconds(_duration);
    }
}
