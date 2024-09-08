using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue : Element
{
    private string _characterName; // 캐릭터 이름
    private List<Line> _lines;

    public Dialogue(string characterName, Line line)
    {
        this._characterName = characterName;
        this._lines = new List<Line> { line };
    }

    public Dialogue(string characterName, List<Line> lines)
    {
        this._characterName = characterName;
        this._lines = lines;
    }

    public string CharacterName => _characterName;
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

        // 캐릭터 로드 시도
        Character character = CharacterManager.Instance.GetActiveCharacter(_characterName);

        if (character == null)
        {
            Debug.LogWarning($"Dialogue :: Character '{_characterName}' not found.");
            yield break;
        }

        // 첫 문장 감정 상태 설정
        var firstLine = _lines[0];
        dialoguePlayer.Clear();
        dialoguePlayer.SetCharacterText(_characterName, character.MainColor); // 캐릭터 색상 설정
        dialoguePlayer.ShowUp(true, 0.2f);
        dialoguePlayer.FadeIn(true, 0.2f);
        yield return new WaitForSeconds(0.4f);

        EEmotionID recentEmotionID = firstLine.EmotionID;
        int recentEmotionIndex = firstLine.EmotionIndex;

        // 캐릭터의 감정 상태 설정
        CharacterManager.Instance.SetCharacterEmotion(_characterName, firstLine.EmotionID, firstLine.EmotionIndex, 1f);

        // 대화 진행
        for (int i = 0; i < _lines.Count; i++)
        {
            Line line = _lines[i];
            dialoguePlayer.InitLine(line);

            EEmotionID emotionID = line.EmotionID;
            int emotionIndex = line.EmotionIndex;

            bool isIdenticalEmotionSprite = (emotionID == recentEmotionID) && (emotionIndex == recentEmotionIndex);
            if (!isIdenticalEmotionSprite)
            {
                Debug.Log($"{_characterName} 감정의 변화 {recentEmotionID}{recentEmotionIndex} -> {emotionID}{emotionIndex}");
                float transitionDuration = 0.5f;
                CharacterManager.Instance.SetCharacterEmotion(_characterName, line.EmotionID, line.EmotionIndex, transitionDuration);
            }

            recentEmotionID = line.EmotionID;
            recentEmotionIndex = line.EmotionIndex;
            dialoguePlayer.ShowNextSentence(line.PlaySpeed);
            yield return null;

            // 대화 상태 처리
            while (dialoguePlayer.DialogueState != EDialogueState.Finished)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    EDialogueState dialogueState = dialoguePlayer.DialogueState;
                    if (dialogueState == EDialogueState.Typing)
                    {
                        dialoguePlayer.CompleteCurSentence();
                    }
                    else if (dialogueState == EDialogueState.Waiting)
                    {
                        dialoguePlayer.ShowNextSentence(line.PlaySpeed);
                    }
                }
                yield return null;
            }
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }

        dialoguePlayer.FadeIn(false, 0.2f);
        yield return new WaitForSeconds(0.2f);
        dialoguePlayer.Clear();
        dialoguePlayer.gameObject.SetActive(false);
    }
}
