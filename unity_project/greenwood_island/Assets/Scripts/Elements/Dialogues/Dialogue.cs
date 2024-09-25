using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue : Element
{
    private string _characterID; // 캐릭터 이름
    private List<Line> _lines;

    public Dialogue(string characterID, Line line)
    {
        this._characterID = characterID;
        this._lines = new List<Line> { line };
    }

    public Dialogue(string characterID, List<Line> lines)
    {
        this._characterID = characterID;
        this._lines = lines;
    }

    public string CharacterID => _characterID;
    public List<Line> Lines => _lines;

    public override IEnumerator ExecuteRoutine()
    {
        DialoguePlayer dialoguePlayer = UIManager.Instance.SystemCanvas.DialoguePlayer;

        if (dialoguePlayer == null)
        {
            Debug.LogWarning("Dialogue :: dialoguePlayer is null");
            yield break;
        }

        //클리어
        dialoguePlayer.gameObject.SetActive(true);

        // 캐릭터 텍스트, 이미지
        var firstLine = _lines[0];

        Character activeCharacter = CharacterManager.GetActiveCharacter(_characterID);
        Character characterPrefab =  CharacterManager.GetCharacterPrefab(_characterID) != null ? CharacterManager.GetCharacterPrefab(_characterID).GetComponent<Character>() : null;

        //캐릭터 텍스트
        dialoguePlayer.ClearCharacterText();
        string characterStr = characterPrefab != null ? characterPrefab.CharacterName_KO : _characterID;
        Color characterStrColor = characterPrefab != null ? characterPrefab.MainColor : Color.white;
        dialoguePlayer.SetCharacterText(characterStr);
        dialoguePlayer.SetCharacterTextClor(Color.clear, 0f);
        dialoguePlayer.SetCharacterTextClor(characterStrColor, .3f);

        //다이얼로그 텍스트
        dialoguePlayer.ClearDialogueText();
        dialoguePlayer.ShowUp(true, .3f);
        yield return new WaitForSeconds(.3f);

        // 대화 진행
        for (int i = 0; i < _lines.Count; i++)
        {
            Line line = _lines[i];
            string emotionID = line.EmotionID;
            int emotionIndex = line.EmotionIndex;
           
            if(activeCharacter != null){
                Debug.Log("StartTalking 시작");
                activeCharacter.ChangeEmotion(line.EmotionID, line.EmotionIndex);
            }

            dialoguePlayer.ClearDialogueText();
            dialoguePlayer.FadeInDialogueText(0f);
            // ShowLineRoutine에 콜백 추가
            yield return dialoguePlayer.ShowLineRoutine(line, line.PlaySpeed, 
            () =>{
                Debug.Log("테스트");
                if (activeCharacter != null)
                {
                    activeCharacter.CurrentEmotion.StartTalking(true);  // 텍스트 표시가 완료되면 말하기 중지
                }
            },
            () =>{
                Debug.Log("테스트");
                if (activeCharacter != null)
                {
                    activeCharacter.CurrentEmotion.StartTalking(false);  // 텍스트 표시가 완료되면 말하기 중지
                }
            });
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            dialoguePlayer.FadeOutDialogueText(.15f);
            yield return new WaitForSeconds(.15f);
        }
        dialoguePlayer.SetCharacterTextClor(Color.clear, .15f);
        yield return new WaitForSeconds(.15f);
        dialoguePlayer.SetCharacterText("");
    }
}
