using UnityEngine;

public class DialogueManager : SingletonManager<DialogueManager>
{
    private const string DIALOGUEPLAYER_PREFAB_PATH = "DialogueManager/DialoguePlayerPrefab";
    private const string SNAPCHATPLAYER_PREFAB_PATH = "DialogueManager/SnapchatPlayerPrefab";
    
    private DialoguePlayer _currentDialoguePlayer; // 현재 활성화된 DialoguePlayer

    public DialoguePlayer GetOrCreateDialoguePlayer(bool isForSnapchat, bool recreate)
    {
        // 이미 _currentDialoguePlayer가 존재할 경우, 바로 반환
        if (!recreate && _currentDialoguePlayer != null)
        {
            Debug.LogWarning("DialoguePlayer already exists, returning existing instance.");
            return _currentDialoguePlayer;
        }

        if(recreate){
            Destroy(_currentDialoguePlayer.gameObject);
        }

        // 프리팹 로드
        GameObject dialoguePlayerPrefab = Resources.Load<GameObject>(isForSnapchat? SNAPCHATPLAYER_PREFAB_PATH : DIALOGUEPLAYER_PREFAB_PATH);

        // 프리팹을 찾지 못했을 경우
        if (dialoguePlayerPrefab == null)
        {
            Debug.LogError($"DialoguePlayer prefab not found at path '{DIALOGUEPLAYER_PREFAB_PATH}'.");
            return null;
        }

        // 프리팹에 DialoguePlayer 스크립트가 부착되어 있는지 확인
        DialoguePlayer dialoguePlayerComponent = dialoguePlayerPrefab.GetComponent<DialoguePlayer>();
        if (dialoguePlayerComponent == null)
        {
            Debug.LogError("The prefab does not have a DialoguePlayer script attached.");
            return null;
        }

        // UIManager의 DialogueLayer에 인스턴스화
        GameObject dialoguePlayerObject = Object.Instantiate(dialoguePlayerPrefab, UIManager.SystemCanvas.DialoguePlayerLayer);
        _currentDialoguePlayer = dialoguePlayerObject.GetComponent<DialoguePlayer>();

        // 초기화
        _currentDialoguePlayer.Init();

        return _currentDialoguePlayer;
    }

    public void FadeOutDialoguePlayer(float duration){

        if (_currentDialoguePlayer == null)
        {
            Debug.LogWarning("FadeOutDialoguePlayer :: _currentDialoguePlayer is null");
            return;
        } // DialoguePlayer가 활성화되어 있으면 _duration 시간 동안 ShowUp(false) 실행
        _currentDialoguePlayer.FadeOutPanel(duration);
    }

    public void FadeOutAndDestroy(float duration){

        FadeOutDialoguePlayer(duration);
        Destroy(_currentDialoguePlayer.gameObject, duration);

    }
}
