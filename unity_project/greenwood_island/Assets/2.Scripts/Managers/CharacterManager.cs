using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public static class CharacterManager
{
    private static Dictionary<string, Character> _instantiatedCharacters = new Dictionary<string, Character>();
    private static Dictionary<string, CharacterData> _characterDataDictionary = new Dictionary<string, CharacterData>();

    public static Dictionary<string, Character> InstantiatedCharacters => _instantiatedCharacters;
    public static Dictionary<string, CharacterData> CharacterDataDictionary => _characterDataDictionary;

    // 클래스 초기화 시 모든 CharacterData를 미리 로드하여 Dictionary에 저장
    static CharacterManager()
    {
        LoadAllCharacterData();
    }

    // CharacterData를 미리 로드하는 메서드
    private static void LoadAllCharacterData()
    {
        // 스토리 리소스에서 모든 CharacterData 로드
        CharacterData[] storyCharacterDatas = Resources.LoadAll<CharacterData>("StoryResources/CharacterDatas");
        foreach (var data in storyCharacterDatas)
        {
            if (!_characterDataDictionary.ContainsKey(data.name))
            {
                _characterDataDictionary.Add(data.name, data);
            }
        }

        // 공유 리소스에서 모든 CharacterData 로드
        CharacterData[] sharedCharacterDatas = Resources.LoadAll<CharacterData>("SharedResources/CharacterDatas");
        foreach (var data in sharedCharacterDatas)
        {
            if (!_characterDataDictionary.ContainsKey(data.name))
            {
                _characterDataDictionary.Add(data.name, data);
            }
        }

        Debug.Log($"Loaded {_characterDataDictionary.Count} CharacterData assets.");
    }

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

    // CharacterID를 통해 CharacterData를 찾는 함수
    public static CharacterData GetCharacterData(string characterID)
    {
        if (_characterDataDictionary.TryGetValue(characterID, out CharacterData characterData))
        {
            return characterData;
        }
        else
        {
            Debug.LogWarning($"CharacterData not found for ID: {characterID}");
            return null;
        }
    }

    public static Character InstantiateCharacter(string characterID, float screenPeroneX)
    {
        if (IsExist(characterID))
        {
            Debug.LogWarning($"Character with ID {characterID} is already instantiated.");
            return _instantiatedCharacters[characterID];
        }

        // 빈 캐릭터 프리팹을 Resources/CharacterPrefab에서 로드
        string characterPrefabPath = "CharacterPrefab";
        GameObject characterPrefab = Resources.Load<GameObject>(characterPrefabPath);

        // 프리팹을 찾지 못했을 경우
        if (characterPrefab == null)
        {
            Debug.LogError($"Failed to load character prefab from path '{characterPrefabPath}'.");
            return null;
        }

        // 프리팹에 Character 스크립트가 부여되어 있는지 확인
        Character characterComponent = characterPrefab.GetComponent<Character>();
        if (characterComponent == null)
        {
            Debug.LogError($"The prefab at '{characterPrefabPath}' does not have a Character script attached.");
            return null;
        }

        // 캐릭터 데이터를 딕셔너리에서 로드
        CharacterData characterData = GetCharacterData(characterID);

        // 캐릭터 데이터를 찾지 못했을 경우
        if (characterData == null)
        {
            Debug.LogError($"Failed to find CharacterData for ID '{characterID}'.");
            return null;
        }

        // 캐릭터를 인스턴스화
        GameObject characterObject = Object.Instantiate(characterPrefab, UIManager.Instance.WorldCanvas.CharacterLayer.transform);
        Character character = characterObject.GetComponent<Character>();
        character.transform.localScale = Vector3.one * .4f;
        character.transform.localPosition = Vector3.down * 356;
        character.SetVisibility(false, 0f);

        // CharacterData를 사용하여 캐릭터 초기화
        character.Init(characterData);

        _instantiatedCharacters.Add(characterID, character);

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

    public static void SetCharacterEmotion(string characterID, string emotionID, int emotionIndex, float duration)
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
