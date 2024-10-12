using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : Element
{
    private string _characterID; // 캐릭터 이름
    private List<Line> _lines;
    private bool _down;

    public Dialogue(string characterID, Line line, bool down = false)
    {
        this._characterID = characterID;
        this._lines = new List<Line> { line };
        _down = down;
    }

    public Dialogue(string characterID, List<Line> lines, bool down = false)
    {
        this._characterID = characterID;
        this._lines = lines;
        _down = down;
    }
    public string CharacterID => _characterID;
    public List<Line> Lines => _lines;

    public override void ExecuteInstantly()
    {
        DialoguePlayer dialoguePlayer = UIManager.SystemCanvas.DialoguePlayer;

        if (dialoguePlayer == null)
        {
            return;
        }
        dialoguePlayer.SetCharacterTextClor(Color.clear, 0f);
    }

    public override IEnumerator ExecuteRoutine()
    {
        DialoguePlayer dialoguePlayer = UIManager.SystemCanvas.DialoguePlayer;

        if (dialoguePlayer == null)
        {
            Debug.LogWarning("Dialogue :: dialoguePlayer is null");
            yield break;
        }

        // 캐릭터 텍스트, 이미지
        var firstLine = _lines[0];

        Character activeCharacter = CharacterManager.Instance.GetActiveCharacter(_characterID);
        CharacterData characterData =  CharacterManager.Instance.GetCharacterData(_characterID);
        if(characterData != null){
            Debug.Log(characterData.CharacterID);
        }

        //캐릭터 텍스트
        dialoguePlayer.ClearCharacterText();
        string characterStr = characterData != null ? characterData.CharacterName_Ko : "";
        Color characterStrColor = characterData != null ? characterData.CharacterColor : Color.clear;
        dialoguePlayer.SetCharacterText(characterStr, Color.Lerp(characterStrColor, Color.black, .85f));
        dialoguePlayer.SetCharacterTextClor(Color.clear, 0f);

        //다이얼로그 텍스트
        dialoguePlayer.ClearDialogueText();
        if(!dialoguePlayer.IsOn){
            dialoguePlayer.ShowUp(true, .5f);
            yield return new WaitForSeconds(.5f);
        }
        // 대화 진행
        for (int i = 0; i < _lines.Count; i++)
        {
            dialoguePlayer.ShowAlpha(true, .1f);
            dialoguePlayer.SetCharacterTextClor(characterStrColor, .1f);
            dialoguePlayer.ClearDialogueText();
            yield return new WaitForSeconds(.1f);
            Line line = _lines[i];
            EmotionType emotionType = line.EmotionType;
            int emotionIndex = line.EmotionIndex;
            
            if(activeCharacter != null){
                activeCharacter.ChangeEmotion(line.EmotionType, line.EmotionIndex);
            }

            // ShowLineRoutine에 콜백 추가
            yield return dialoguePlayer.ShowLineRoutine(line, line.PlaySpeed, 
            () =>{
                if (activeCharacter != null && activeCharacter.CurrentEmotion != null)
                {
                    activeCharacter.CurrentEmotion.StartTalking();  // 텍스트 표시가 완료되면 말하기 중지
                }
            },
            () =>{
                if (activeCharacter != null && activeCharacter.CurrentEmotion != null)
                {
                    activeCharacter.CurrentEmotion.StopTalking();  // 텍스트 표시가 완료되면 말하기 중지
                }
            });
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0) && !UIManager.PopupCanvas.IsPoppedUp);
            if (activeCharacter != null)
            {
                activeCharacter.CurrentEmotion.StopTalking();  // 텍스트 표시가 완료되면 말하기 중지
            }
        }
        if(_down){
            Debug.Log("다이얼로그 내림");
            new DialoguePanelClear(1f).Execute();
            yield return new WaitForSeconds(1f);
        }
        else{
            dialoguePlayer.SetCharacterTextClor(Color.clear, .2f);
            yield return new WaitForSeconds(.2f);
        }
    }
}
