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
    public DialoguePlayer DialoguePlayer => UIManager.Instance.SystemCanvas.DialoguePlayer;

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
        CharacterData characterData =  CharacterManager.Instance.GetCharacterData(_characterID);
        DialoguePlayer.Clear();
        DialoguePlayer.SetCharacterText(characterData.characterName_ko, characterData.mainColor);
        // 대화 시작
        DialoguePlayer.ShowPanel(true, 0.2f);
        yield return new WaitForSeconds(.4f);
        // 대화 진행
        for (int i = 0; i < _lines.Count; i++)
        {
            Debug.Log($"{i}번째 대사 시작");
            Line line = _lines[i];
            DialoguePlayer.InitLine(line);
            DialoguePlayer.ShowNextSentence();
            //연속 마우스 호출을 막기위한 한프레임 대기
            yield return null;
            CharacterManager.Instance.SetCharacterEmotion(_characterID, line.EmotionID, line.EmotionIndex);

            while(DialoguePlayer.DialogueState != EDialogueState.Finished){
                if(Input.GetMouseButtonDown(0)){
                    EDialogueState dialogueState = DialoguePlayer.DialogueState;
                    if(dialogueState == EDialogueState.Typing){
                        DialoguePlayer.CompleteCurSentence();
                    }
                    else if(dialogueState == EDialogueState.Waiting){
                        DialoguePlayer.ShowNextSentence();
                    }
                    else if(dialogueState == EDialogueState.Finished){

                    }
                }
                yield return null;
            }
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }
        DialoguePlayer.ShowPanel(false, 0.2f);
        yield return new WaitForSeconds(.2f);
        DialoguePlayer.Clear();
    }

}
