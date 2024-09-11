using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public static class CharacterManager
{
    private static Dictionary<string, Character> _instantiatedCharacters = new Dictionary<string, Character>();

    public static Dictionary<string, Character> InstantiatedCharacters => _instantiatedCharacters;

    public static bool IsExist(string characterID)
    {
        return _instantiatedCharacters.ContainsKey(characterID);
    }

    public static int GetActiveCharacterCount()
    {
        return _instantiatedCharacters.Count;
    }

    public static List<string> GetAllActiveCharacterIDs()
    {
        return _instantiatedCharacters.Keys.ToList();
    }

    public static Character InstantiateCharacter(string characterID, float screenPeroneX, EEmotionID initialEmotionID, int emotionIndex)
    {
        if (IsExist(characterID))
        {
            Debug.LogWarning($"Character with ID {characterID} is already instantiated.");
            return _instantiatedCharacters[characterID];
        }

        // 먼저 스토리 리소스에서 캐릭터 프리팹을 찾음
        string characterPath = ResourcePathManager.GetResourcePath(characterID, string.Empty, ResourceType.Character, false);
        Character characterPrefab = Resources.Load<Character>(characterPath);

        // 스토리 리소스에 프리팹이 없으면 공유 리소스에서 찾음
        if (characterPrefab == null)
        {
            characterPath = ResourcePathManager.GetResourcePath(characterID, string.Empty, ResourceType.Character, true);
            characterPrefab = Resources.Load<Character>(characterPath);
        }

        // 프리팹을 찾지 못했을 경우
        if (characterPrefab == null)
        {
            Debug.LogError($"Failed to load character prefab from path '{characterPath}'.");
            return null;
        }

        // 캐릭터를 인스턴스화
        Character character = Object.Instantiate(characterPrefab, UIManager.Instance.WorldCanvas.CharacterLayer.transform);
        character.SetVisibility(false, 0f);

        _instantiatedCharacters.Add(characterID, character);

        // 초기 이모션 설정
        character.ChangeEmotion(initialEmotionID, emotionIndex, 0f);
        MoveCharacter(characterID, screenPeroneX, 0f, Ease.Linear);

        return character;
    }

    public static void MoveCharacter(string characterID, float targetScreenPercentageX, float duration, Ease easeType)
    {
        Character character = GetActiveCharacter(characterID);
        if (character == null)
        {
            Debug.LogError("Character not found.");
            return;
        }

        RectTransform rectTransform = character.GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Debug.LogError("Character does not have a RectTransform.");
            return;
        }

        float targetX = Screen.width * targetScreenPercentageX;
        Vector3 targetPosition = rectTransform.anchoredPosition;
        targetPosition.x = targetX - (Screen.width / 2);

        rectTransform.DOAnchorPos(new Vector2(targetPosition.x, rectTransform.anchoredPosition.y), duration).SetEase(easeType);
    }

    public static void DestroyCharacter(string characterID)
    {
        if (_instantiatedCharacters.TryGetValue(characterID, out Character character))
        {
            Object.Destroy(character.gameObject);
            _instantiatedCharacters.Remove(characterID);
        }
        else
        {
            Debug.LogWarning($"No instantiated character found with ID {characterID} to destroy.");
        }
    }

    public static Character GetActiveCharacter(string characterID)
    {
        if (IsExist(characterID))
        {
            return _instantiatedCharacters[characterID];
        }
        else
        {
            Debug.LogWarning($"No active character found with ID {characterID}.");
            return null;
        }
    }

    public static void SetCharacterEmotion(string characterID, EEmotionID emotionID, int emotionIndex, float duration)
    {
        Character character = GetActiveCharacter(characterID);
        if (character != null)
        {
            character.ChangeEmotion(emotionID, emotionIndex, duration);
        }
    }

    public static void DestroyAllCharacters()
    {
        foreach (var character in _instantiatedCharacters.Values)
        {
            Object.Destroy(character.gameObject);
        }
        _instantiatedCharacters.Clear();
    }
}
