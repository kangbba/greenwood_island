using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public DialoguePlayer DialoguePlayer => UIManager.Instance.SystemCanvas.DialoguePlayer;

    private static DialogueManager _instance;
    public static DialogueManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DialogueManager>();
            }
            return _instance;
        }
    }

    private Dialogue _currentDialogue;

    // 대화 시작
    public void StartDialogue(Dialogue dialogue)
    {
        _currentDialogue = dialogue;

        // 대화 패널을 보여주고 대화를 초기화
        DialoguePlayer.ShowPanel(true, 0.5f);
        DialoguePlayer.InitDialogue(dialogue);
    }

    // 다음 문장 표시
    public void DisplayNextSentence()
    {
        if (DialoguePlayer.CurrentState == DialogueState.Waiting)
        {
            DialoguePlayer.DisplayNextLine();
        }
    }

    // 현재 문장을 즉시 완료
    public void CompleteCurrentSentence()
    {
        if (DialoguePlayer.CurrentState == DialogueState.Typing)
        {
            DialoguePlayer.CompleteCurLine();
        }
    }

    // 대화 종료
    public void EndDialogue()
    {
        DialoguePlayer.ShowPanel(false, 0.5f);
    }

    // 대화 상태 확인
    public DialogueState GetDialogueState()
    {
        return DialoguePlayer.CurrentState;
    }
}
