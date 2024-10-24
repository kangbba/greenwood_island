using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SnapchatEnter : Element
{
    private List<CharacterEnter> _characterEnters; // 캐릭터 등장을 관리하는 리스트
    private List<Dialogue> _dialogues; // 캡슐화된 대화 목록

    // 생성자 - List<CharacterEnter>와 List<Dialogue>를 받음
    public SnapchatEnter(List<CharacterEnter> characterEnters, List<Dialogue> dialogues)
    {
        _characterEnters = characterEnters;
        _dialogues = dialogues;
    }

    // 특정 캐릭터만 하이라이트하는 함수
    public void HighlightOneCharacterOnly(string characterID, float duration)
    {
        // _characterEnters 리스트 순회
        foreach (CharacterEnter characterEnter in _characterEnters)
        {
            Character activeCharacter = CharacterManager.Instance.GetActiveCharacter(characterEnter.CharacterID);

            // 입력받은 characterID와 일치하는 경우만 하이라이트, 나머지는 해제
            if (activeCharacter != null)
            {
                bool highlight = characterEnter.CharacterID == characterID;
                activeCharacter.Highlight(highlight, duration); // Character에 Highlight(bool) 함수가 있다고 가정
            }
        }
    }

    public override void ExecuteInstantly()
    {
    }

    public override IEnumerator ExecuteRoutine()
    {
        DialoguePlayer dialoguePlayer = DialogueManager.Instance.GetOrCreateDialoguePlayer(true, true);

        if (dialoguePlayer == null)
        {
            Debug.LogWarning("DialoguePlayer is null");
            yield break;
        }
        yield return new PlaceOverlayFilm(Color.black.ModifiedAlpha(.6f), 1f).ExecuteRoutine();

        List<Element> elements = new List<Element>();
        // 캐릭터들을 등장시킴
        foreach (CharacterEnter characterEnter in _characterEnters)
        {
            elements.Add(characterEnter);
        }
        yield return new ParallelElement(elements.ToArray()).ExecuteRoutine();

        // 캐릭터 텍스트, 이미지
        foreach (Dialogue dialogue in _dialogues)
        {
            var characterID = dialogue.CharacterID;
            var lines = dialogue.Lines;

            // 해당 캐릭터만 하이라이트
            HighlightOneCharacterOnly(characterID, .3f);
            yield return new WaitForSeconds(.3f);

            Character activeCharacter = CharacterManager.Instance.GetActiveCharacter(characterID);
            CharacterData characterData = CharacterManager.Instance.GetCharacterData(characterID);

            // 캐릭터 텍스트
            if (characterData == null)
            {
                dialoguePlayer.ClearCharacterText();
            }
            else
            {
                string characterStr = characterData.CharacterName_Ko;
                Color characterStrColor = characterData.CharacterColor;
                dialoguePlayer.SetCharacterText(characterStr, characterStrColor);
            }

            // 다이얼로그 텍스트
            dialoguePlayer.ClearDialogueText();
            dialoguePlayer.FadeInPanel(.5f);
            yield return new WaitForSeconds(.5f);

            // 대화 진행
            for (int i = 0; i < lines.Count; i++)
            {
                Line line = lines[i];

                yield return dialoguePlayer.ShowLineRoutine(line, line.PlaySpeedMultiplier * 1200,
                    OnLineStarted: () => { },
                    OnLineComplete: () => { }
                );
            }

            dialoguePlayer.FadeOutPanel(.25f);
            yield return new WaitForSeconds(.25f);
        }


        DialogueManager.Instance.FadeOutAndDestroy(1f);
        yield return new WaitForSeconds(1f);
        yield return new PlaceOverlayFilmClear(1f).ExecuteRoutine();
    }
}
