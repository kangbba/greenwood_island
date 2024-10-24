using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : Element
{
    private string _characterID; // 캐릭터 이름
    private List<Line> _lines;
    private bool _fadeout;

    public Dialogue(string characterID, Line line, bool fadeout = false)
    {
        this._characterID = characterID;
        this._lines = new List<Line> { line };
        _fadeout = fadeout;
    }

    public Dialogue(string characterID, List<Line> lines, bool fadeout = false)
    {
        this._characterID = characterID;
        this._lines = lines;
        _fadeout = fadeout;
    }
    public string CharacterID => _characterID;
    public List<Line> Lines => _lines;

    public override void ExecuteInstantly()
    {
       
    }

    public override IEnumerator ExecuteRoutine()
    {
        DialoguePlayer dialoguePlayer = DialogueManager.Instance.GetOrCreateDialoguePlayer(false, false);
        if (dialoguePlayer == null)
        {
            Debug.LogWarning("Dialogue :: dialoguePlayer is null");
            yield break;
        }

        // 캐릭터 텍스트, 이미지
        var firstLine = _lines[0];

        Character activeCharacter = CharacterManager.Instance.GetActiveCharacter(_characterID);
        CharacterData characterData =  CharacterManager.Instance.GetCharacterData(_characterID);
        //캐릭터 텍스트
        if(characterData == null){
            dialoguePlayer.ClearCharacterText();
        }
        else{
            Debug.Log(characterData.CharacterID);
            string characterStr = characterData.CharacterName_Ko;
            Color characterStrColor = characterData.CharacterColor;
            dialoguePlayer.SetCharacterText(characterStr, characterStrColor);
        }

        //다이얼로그 텍스트
        dialoguePlayer.ClearDialogueText();
        dialoguePlayer.FadeInPanel(.5f);
        yield return new WaitForSeconds(.5f);
        // 대화 진행
        for (int i = 0; i < _lines.Count; i++)
        {
            Line line = _lines[i];
            
            if(activeCharacter != null)
            {
                activeCharacter.StopTalking();  // 텍스트 표시가 완료되면 말하기 중지
            }
            yield return dialoguePlayer.ShowLineRoutine(line, line.PlaySpeedMultiplier * 1200, 
                OnLineStarted : () =>{
                    if (activeCharacter != null)
                    {
                        activeCharacter.StartTalking();  // 텍스트 표시가 완료되면 말하기 중지
                    }
                },
                OnLineComplete : () =>{
                    if (activeCharacter != null)
                    {
                        activeCharacter.StopTalking();  // 텍스트 표시가 완료되면 말하기 중지
                    }
                }
            );
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0) && !UIManager.PopupCanvas.IsPoppedUp);
            yield return null;
        }
        if(_fadeout){
            Debug.Log("다이얼로그 내림");
            dialoguePlayer.FadeOutPanel(.25f);
            yield return new WaitForSeconds(.25f);
        }
        else{
            dialoguePlayer.ClearCharacterText();
        }
    }
}
