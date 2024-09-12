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
        dialoguePlayer.ClearAll(0f);

        // 캐릭터 텍스트, 이미지
        var firstLine = _lines[0];
        EEmotionID recentEmotionID = firstLine.EmotionID;
        int recentEmotionIndex = firstLine.EmotionIndex;
        Character character = CharacterManager.GetActiveCharacter(_characterID);
        if (character != null)
        {
            Debug.LogWarning($"Dialogue :: Character '{_characterID}' not found.");
            dialoguePlayer.SetCharacterText(_characterID, character.MainColor, .5f); // 캐릭터 색상 설정
            CharacterManager.SetCharacterEmotion(_characterID, firstLine.EmotionID, firstLine.EmotionIndex, .3f);
        }
        else{
            dialoguePlayer.SetCharacterText(_characterID, Color.white, .3f); // 캐릭터 색상 설정
        }

        //다이얼로그 텍스트
        dialoguePlayer.FadeInDialogueText(.3f);
        dialoguePlayer.ShowUp(true, .3f);
        yield return new WaitForSeconds(.3f);

        // 대화 진행
        for (int i = 0; i < _lines.Count; i++)
        {
            Line line = _lines[i];

            EEmotionID emotionID = line.EmotionID;
            int emotionIndex = line.EmotionIndex;

            bool isIdenticalEmotionSprite = (emotionID == recentEmotionID) && (emotionIndex == recentEmotionIndex);
            if (!isIdenticalEmotionSprite)
            {
                Debug.Log($"{_characterID} 감정의 변화 {recentEmotionID}{recentEmotionIndex} -> {emotionID}{emotionIndex}");
                float transitionDuration = 0.5f;
                if(character != null){
                        CharacterManager.SetCharacterEmotion(_characterID, line.EmotionID, line.EmotionIndex, transitionDuration);
                }
            }

            recentEmotionID = line.EmotionID;
            recentEmotionIndex = line.EmotionIndex;

            dialoguePlayer.ClearAll(0f);
            dialoguePlayer.FadeInDialogueText(.3f);
            yield return new WaitForSeconds(.3f);

            yield return dialoguePlayer.ShowLineRoutine(line, line.PlaySpeed);
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            
            dialoguePlayer.FadeOutDialogueText(.3f);
            yield return new WaitForSeconds(.3f);
        }
        dialoguePlayer.FadeOutDialogueText(.3f);
        yield return new WaitForSeconds(.3f);
    }
}
