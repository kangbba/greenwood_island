using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPlayer : MonoBehaviour
{
    private Character _currentCharacter;
    private Dialogue _currentDialogue;
    private int _currentLineIndex;
    private EEmotionID _previousEmotionID;

    public Character CurrentCharacter { get => _currentCharacter; }

    public void InitDialogue(Dialogue dialogue)
    {
        _currentDialogue = dialogue;
        _currentLineIndex = 0;

        // 캐릭터 인스턴스화 또는 기존 캐릭터 찾아서 사용
        _currentCharacter = InstantiateCharacter(dialogue.CharacterID);
        if (_currentCharacter == null)
        {
            Debug.LogError($"Failed to initialize character for {dialogue.CharacterID}");
            return;
        }

        // 첫 번째 라인의 감정 상태로 캐릭터 초기화
        Line firstLine = dialogue.Lines[0];
        _previousEmotionID = firstLine.EmotionID;

        _currentCharacter.Init(firstLine.EmotionID, 0, 1f); // 초기화
        WhenCurrentLinePlaying(); // 첫 번째 감정 상태 업데이트
    }

    private Character InstantiateCharacter(ECharacterID characterID)
    {
        CharacterData data = CharacterDataManager.Instance.GetCharacterData(characterID);

        if (data == null || data.characterPrefab == null)
        {
            Debug.LogError($"Failed to instantiate character for {characterID}. Check if the prefab is assigned.");
            return null;
        }

        Character character = Instantiate(data.characterPrefab, UIManager.Instance.WorldCanvas.CharacterLayer.transform);
        character.transform.localPosition = Vector3.right * _currentDialogue.ScreenPeroneX * Screen.width;
        character.transform.localRotation = Quaternion.identity;
        return character;
    }

    /// <summary>
    /// 현재 라인이 재생될 때 캐릭터의 감정 상태를 업데이트합니다.
    /// </summary>
    public void WhenCurrentLinePlaying()
    {
        if (_currentLineIndex < _currentDialogue.Lines.Count)
        {
            Line currentLine = _currentDialogue.Lines[_currentLineIndex];

            // 감정 상태가 변할 경우에만 감정 변화 애니메이션 적용
            if (currentLine.EmotionID != _previousEmotionID)
            {
                _currentCharacter.ChangeEmotion(currentLine.EmotionID, currentLine.EmotionIndex, 0.5f);
                _previousEmotionID = currentLine.EmotionID; // 이전 감정 상태를 업데이트
            }

            Debug.Log($"Character '{_currentDialogue.CharacterID}' updates emotion to {_currentLineIndex + 1}/{_currentDialogue.Lines.Count}: {currentLine.Sentence}");
        }
        else
        {
            Debug.Log("모든 대화를 완료했습니다.");
        }
    }

    /// <summary>
    /// 다음 라인이 재생될 때 호출됩니다.
    /// </summary>
    public void WhenNextLinePlaying()
    {
        if (_currentLineIndex < _currentDialogue.Lines.Count - 1)
        {
            _currentLineIndex++;
            WhenCurrentLinePlaying();
        }
        else
        {
            Debug.Log("더 이상 다음 라인이 없습니다.");
        }
    }

    /// <summary>
    /// 현재 감정 상태를 즉시 완료합니다.
    /// </summary>
    public void WhenCurrentLineCompleted()
    {
        if (_currentLineIndex < _currentDialogue.Lines.Count)
        {
            Debug.Log($"Line {_currentLineIndex + 1} completed for character {_currentDialogue.CharacterID}.");
        }
    }

    /// <summary>
    /// 캐릭터를 파괴합니다.
    /// </summary>
    public void DestroyCharacter()
    {
        if (_currentCharacter != null)
        {
            _currentCharacter.ShowCharacter(false, 1f); // 캐릭터를 부드럽게 사라지게 함
            Destroy(_currentCharacter.gameObject, 1f); // 1초 뒤에 캐릭터 오브젝트를 파괴
            _currentCharacter = null;
        }
    }

    private void OnDestroy()
    {
        // CharacterPlayer가 파괴될 때 Character도 파괴
        DestroyCharacter();
    }
}
