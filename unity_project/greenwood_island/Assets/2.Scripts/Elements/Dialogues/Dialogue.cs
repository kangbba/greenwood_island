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
        dialoguePlayer.gameObject.SetActive(true);
        dialoguePlayer.ShowUp(true, 0.5f);
        yield return new WaitForSeconds(0.5f);

        // 첫 문장 감정 상태 설정
        var firstLine = _lines[0];
        EEmotionID recentEmotionID = firstLine.EmotionID;
        int recentEmotionIndex = firstLine.EmotionIndex;

        // 캐릭터 로드 시도
        Character character = CharacterManager.GetActiveCharacter(_characterID);
        if (character != null)
        {
            Debug.LogWarning($"Dialogue :: Character '{_characterID}' not found.");
            dialoguePlayer.SetCharacterText(_characterID, character.MainColor); // 캐릭터 색상 설정
            CharacterManager.SetCharacterEmotion(_characterID, firstLine.EmotionID, firstLine.EmotionIndex, 1f);
        }
        else{
            dialoguePlayer.SetCharacterText(_characterID, Color.white); // 캐릭터 색상 설정
        }
        // 대화 진행
        for (int i = 0; i < _lines.Count; i++)
        {
            Line line = _lines[i];
            dialoguePlayer.InitDialogueText(line);
            dialoguePlayer.FadeInCharacterText(.3f);
            dialoguePlayer.FadeInDialogueText(.3f);
            yield return new WaitForSeconds(.3f);

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
            yield return dialoguePlayer.ShowLineRoutine(line.PlaySpeed);
            

            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            dialoguePlayer.FadeOutCharacterText(.3f);
            dialoguePlayer.FadeOutDialogueText(.3f);
            yield return new WaitForSeconds(.3f);
        }
        dialoguePlayer.FadeOutCharacterText(1f); // 캐릭터 색상 설정
        dialoguePlayer.FadeOutDialogueText(1f);
        yield return new WaitForSeconds(1f);
        dialoguePlayer.gameObject.SetActive(false);
    }
}
