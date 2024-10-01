using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public static class CharacterManager
{
    private static Dictionary<string, Character> _instantiatedCharacters = new Dictionary<string, Character>(); // 인스턴스화된 캐릭터
    private static Dictionary<string, CharacterData> _characterDataCache = new Dictionary<string, CharacterData>(); // 캐릭터 데이터 캐시

    public static Dictionary<string, Character> InstantiatedCharacters => _instantiatedCharacters;

    private static GameObject _characterHost;
    private static MonoBehaviour _characterHandler;

    // Coroutine을 실행할 임시 오브젝트와 컴포넌트 초기화
    static CharacterManager()
    {
        _characterHost = new GameObject("CoroutineUtilsHost");
        _characterHandler = _characterHost.AddComponent<CharacterHandler>();
        Object.DontDestroyOnLoad(_characterHost);
    }

    // MonoBehaviour를 상속한 임시 핸들러 클래스
    private class CharacterHandler : MonoBehaviour { }

    // 캐릭터 프리팹을 로드하여 인스턴스화하는 함수
    public static Character CreateCharacter(string characterID, float screenPeroneX)
    {
        if (IsExist(characterID))
        {
            Debug.LogWarning($"Character with ID {characterID} is already instantiated.");
            return _instantiatedCharacters[characterID];
        }

        // 캐릭터 프리팹 경로 설정 및 로드
        string path = ResourcePathManager.GetResourcePath(characterID, StoryManager.GetCurrentStoryName(), ResourceType.Character, isShared: true); // 경로 가져오기
        GameObject characterPrefab = Resources.Load<GameObject>(path);

        // 프리팹을 찾지 못했을 경우
        if (characterPrefab == null)
        {
            Debug.LogError($"Character prefab with ID '{characterID}' not found at path '{path}'.");
            return null;
        }

        // 프리팹에 Character 스크립트가 부여되어 있는지 확인
        Character characterComponent = characterPrefab.GetComponent<Character>();
        if (characterComponent == null)
        {
            Debug.LogError($"The prefab '{characterID}' does not have a Character script attached.");
            return null;
        }

        // 캐릭터를 인스턴스화
        GameObject characterObject = Object.Instantiate(characterPrefab, UIManager.Instance.SystemCanvas.CharacterLayerUI.transform);
        Character character = characterObject.GetComponent<Character>();

        // 캐릭터 등록
        _instantiatedCharacters.Add(characterID, character);

        // 캐릭터 위치 이동
        MoveCharacter(characterID, screenPeroneX, 0f, Ease.Linear);

        return character;
    }
    // 캐릭터 프리팹을 로드하여 인스턴스화하는 함수
    // 캐릭터 데이터 로드 함수
    public static CharacterData GetCharacterData(string characterID)
    {
        if (_characterDataCache.TryGetValue(characterID, out CharacterData characterData))
        {
            return characterData;
        }

        // 캐릭터 데이터 경로 설정 및 로드
        string path = $"SharedResources/CharacterDatas/CharacterData_{characterID}";
        characterData = Resources.Load<CharacterData>(path);

        if (characterData == null)
        {
            return null;
        }

        // 캐시 저장
        _characterDataCache.Add(characterID, characterData);
        return characterData;
    }

    // 인스턴스화된 캐릭터를 가져오는 함수
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

    // 캐릭터 존재 확인 함수
    public static bool IsExist(string characterID)
    {
        return _instantiatedCharacters.ContainsKey(characterID);
    }

    // 캐릭터 위치 이동 함수
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

    // 캐릭터 파괴 함수
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

    // 모든 캐릭터 파괴 함수
    public static void DestroyAllCharacters()
    {
        foreach (var character in _instantiatedCharacters.Values)
        {
            Object.Destroy(character.gameObject);
        }
        _instantiatedCharacters.Clear();
    }

    public static List<string> GetAllActiveCharacterIDs()
    {
        return _instantiatedCharacters.Keys.ToList();
    }
}
