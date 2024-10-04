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

    public override IEnumerator ExecuteRoutine()
    {
        DialoguePlayer dialoguePlayer = UIManager.SystemCanvas.DialoguePlayer;

        if (dialoguePlayer == null)
        {
            Debug.LogWarning("Dialogue :: dialoguePlayer is null");
            yield break;
        }
        dialoguePlayer.gameObject.SetActive(true);

        // DialoguePlayer가 활성화되어 있으면 _duration 시간 동안 ShowUp(false) 실행
        if(dialoguePlayer.IsOn)
        {
            dialoguePlayer.ShowUp(false, _duration);
            yield return new WaitForSeconds(_duration);
        }

        // 대화 텍스트를 클리어
        dialoguePlayer.ClearDialogueText();
    }
}
