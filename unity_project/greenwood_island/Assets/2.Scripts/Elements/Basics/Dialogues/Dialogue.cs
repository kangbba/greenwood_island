using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue : Element
{
    private ECharacterID _characterID;
    private List<Line> _lines;

    public Dialogue(ECharacterID characterID, Line line)
    {
        this._characterID = characterID;
        this._lines = new List<Line> {line};
    }
    public Dialogue(ECharacterID characterID, List<Line> lines)
    {
        this._characterID = characterID;
        this._lines = lines;
    }
    public DialoguePlayer DialoguePlayer => UIManager.Instance.SystemCanvas.DialoguePlayer;

    public ECharacterID CharacterID { get => _characterID; }
    public List<Line> Lines { get => _lines; }


    public override IEnumerator ExecuteRoutine()
    {
        // 첫 문장 감정 상태 설정
        bool isRyanOrMono = _characterID == ECharacterID.Ryan || _characterID == ECharacterID.Mono;
        var firstLine = _lines[0];
        CharacterData characterData =  CharacterManager.Instance.GetCharacterData(_characterID);
        DialoguePlayer.Clear();
        DialoguePlayer.SetCharacterText(characterData.characterName_ko, characterData.mainColor);
        DialoguePlayer.ShowPanel(true, 0.2f);
        yield return new WaitForSeconds(.4f);

        EEmotionID recentEmotionID = firstLine.EmotionID;
        int recentEmotionIndex = firstLine.EmotionIndex;

        CharacterManager.Instance.SetCharacterEmotion(_characterID, firstLine.EmotionID, firstLine.EmotionIndex, 1f);
        // 대화 진행
        for (int i = 0; i < _lines.Count; i++)
        {
            Line line = _lines[i];
            DialoguePlayer.InitLine(line);

            EEmotionID emotionID = line.EmotionID;
            int emotionIndex = line.EmotionIndex;
            
            bool isIdenticalEmotionSprite = (emotionID == recentEmotionID) && (emotionIndex == recentEmotionIndex);
            if(!isIdenticalEmotionSprite && !isRyanOrMono){
                Debug.Log($"{_characterID} 감정의 변화 {recentEmotionID}{recentEmotionIndex} -> {emotionID}{emotionIndex}");
                float transitionDuration = 2f;
                CharacterManager.Instance.SetCharacterEmotion(_characterID, line.EmotionID, line.EmotionIndex, transitionDuration);
            }
            recentEmotionID = line.EmotionID;
            recentEmotionIndex = line.EmotionIndex;
            DialoguePlayer.ShowNextSentence(line.PlaySpeed);
            yield return null;

            while(DialoguePlayer.DialogueState != EDialogueState.Finished){
                if(Input.GetMouseButtonDown(0)){
                    EDialogueState dialogueState = DialoguePlayer.DialogueState;
                    if(dialogueState == EDialogueState.Typing){
                        DialoguePlayer.CompleteCurSentence();
                    }
                    else if(dialogueState == EDialogueState.Waiting){
                        DialoguePlayer.ShowNextSentence(line.PlaySpeed);
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
