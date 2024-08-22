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
    public DialoguePlayer DialoguePlayer => DialogueManager.Instance.DialoguePlayer;

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

        // 대화 시작
        DialogueManager.Instance.StartDialogue(this);

        // 대화 진행
        for (int i = 0; i < _lines.Count; i++)
        {
            Debug.Log($"{i}번째 대사 시작");
            Line line = _lines[i];
            DialogueManager.Instance.InitLine(line);
            DialogueManager.Instance.ShowNextSentence();
            //연속 마우스 호출을 막기위한 한프레임 대기
            yield return null;
            CharacterManager.Instance.SetCharacterEmotion(_characterID, line.EmotionID, line.EmotionIndex);

            while(DialoguePlayer.DialogueState != EDialogueState.Finished){
                if(Input.GetMouseButtonDown(0)){
                    EDialogueState dialogueState = DialoguePlayer.DialogueState;
                    if(dialogueState == EDialogueState.Typing){
                        DialogueManager.Instance.CompleteCurSentence();
                    }
                    if(dialogueState == EDialogueState.Waiting){
                        DialogueManager.Instance.ShowNextSentence();
                    }
                }
                yield return null;
            }
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }
        DialogueManager.Instance.EndDialogue();
    }

}
