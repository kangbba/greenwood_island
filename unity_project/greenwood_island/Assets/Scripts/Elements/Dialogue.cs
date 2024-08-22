using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue : Element
{
    private ECharacterID _characterID;
    private List<Line> _lines;

    public Dialogue(ECharacterID characterID, List<Line> lines)
    {
        this._characterID = characterID;
        this._lines = lines;
    }

    public ECharacterID CharacterID { get => _characterID; }
    public List<Line> Lines { get => _lines; }

    public override IEnumerator Execute()
    {
        // 이미 생성된 캐릭터를 불러옴
        Character character = CharacterManager.Instance.GetActiveCharacter(_characterID);

        // 캐릭터가 없으면 로그 출력 후 종료
        if (character == null)
        {
            Debug.LogWarning($"Character with ID: {_characterID} not found.");
            yield break;
        }

        // 첫 문장 감정 상태 설정
        var firstLine = _lines[0];
        CharacterManager.Instance.SetCharacterEmotion(_characterID, firstLine.EmotionID, firstLine.EmotionIndex);

        // // 대화 시작
        // DialogueManager.Instance.StartDialogue(this);

        // // 대화 진행
        // for (int i = 0; i < _lines.Count; i++)
        // {
        //     // 현재 상태가 Typing일 때는 대기
        //     while (DialogueManager.Instance.DialoguePlayer.CurrentDialogueState == DialogueState.Typing)
        //     {
        //         yield return null;
        //     }

        //     // 사용자의 입력을 기다림
        //     yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        //     // 상태가 Waiting일 때 문장 완료
        //     if (DialogueManager.Instance.DialoguePlayer.CurrentDialogueState == DialogueState.WaitingForNextRegex)
        //     {
        //         DialogueManager.Instance.DisplayNextColumn();
        //     }
        //     // 상태가 Waiting일 때 문장 완료
        //     if (DialogueManager.Instance.DialoguePlayer.CurrentDialogueState == DialogueState.WaitingForNextLine)
        //     {
        //         DialogueManager.Instance.DisplayNextLine();
        //     }
        // }

        // // 대화 종료
        // DialogueManager.Instance.EndDialogue();
    }
}
