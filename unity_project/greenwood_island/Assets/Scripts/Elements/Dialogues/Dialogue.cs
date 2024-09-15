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
        string recentEmotionID = firstLine.EmotionID;
        int recentEmotionIndex = firstLine.EmotionIndex;

        Character activeCharacter = CharacterManager.GetActiveCharacter(_characterID);
        CharacterData characterData = CharacterManager.GetCharacterData(_characterID);

        //캐릭터 텍스트
        dialoguePlayer.ClearCharacterText();
        string characterStr = characterData != null ? characterData.CharacterName_KO : _characterID;
        Color characterStrColor = characterData != null ? characterData.MainColor : Color.white;
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
           
            Debug.Log($"{_characterID} 감정의 변화 {recentEmotionID}{recentEmotionIndex} -> {emotionID}{emotionIndex}");
            float transitionDuration = 0.5f;
            if(activeCharacter != null){
                CharacterManager.SetCharacterEmotion(_characterID, line.EmotionID, line.EmotionIndex, transitionDuration);
            }

            recentEmotionID = line.EmotionID;
            recentEmotionIndex = line.EmotionIndex;

            dialoguePlayer.ClearDialogueText();
            dialoguePlayer.FadeInDialogueText(0f);
            yield return dialoguePlayer.ShowLineRoutine(line, line.PlaySpeed);
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            dialoguePlayer.FadeOutDialogueText(.15f);
            yield return new WaitForSeconds(.15f);
        }
        dialoguePlayer.SetCharacterTextClor(Color.clear, .15f);
        yield return new WaitForSeconds(.15f);
        dialoguePlayer.SetCharacterText("");
    }
}
