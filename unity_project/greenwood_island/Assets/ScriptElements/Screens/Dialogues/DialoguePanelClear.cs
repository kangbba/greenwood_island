using System.Collections;
using UnityEngine;

public class DialoguePanelClear : Element
{
    private float _duration;

    // 생성자에서 _duration을 전달받고, 기본값은 0.3f로 설정
    public DialoguePanelClear(float duration = 0.3f)
    {
        _duration = duration;
    }

    public override void ExecuteInstantly()
    {
        _duration = 0;
        Execute();
    }

    public override IEnumerator ExecuteRoutine()
    {
        DialogueManager.Instance.FadeOutDialoguePlayer(_duration);
        yield return new WaitForSeconds(_duration);
    }
}
