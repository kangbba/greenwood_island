using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story : MonoBehaviour
{
    private Coroutine _routine;
    public bool AutoCompleteEnabled { get; set; } = false; // 자동완성 옵션을 Story에서 관리
    DialoguePlayer DialoguePlayer => UIManager.Instance.SystemCanvas.DialoguePlayer;

    // 활성화된 CharacterPlayer 인스턴스들을 관리할 리스트
    private List<CharacterPlayer> _activeCharacterPlayers = new List<CharacterPlayer>();

    private void Start()
    {
        List<Dialogue> dialogues = new List<Dialogue>();

        // Kate의 대화
        List<Line> kateLines = new List<Line>()
        {
            new Line(EEmotionID.Happy, 0, "안녕! 정말 오랜만이야."),
            new Line(EEmotionID.Crying, 0, "지난 일은 정말 슬펐어."),
            new Line(EEmotionID.Angry, 0, "그래서 정말 화가났지"),
            new Line(EEmotionID.Panic, 0, "앞으로는 어떻게 될지 모르겠어.")
        };
        Dialogue kateDialogue = new Dialogue(ECharacterID.Kate, kateLines, -.25f);
        dialogues.Add(kateDialogue);

        // Lisa의 대화
        List<Line> lisaLines = new List<Line>()
        {
            new Line(EEmotionID.Normal, 0, "안녕하세요, 케이트."),
            new Line(EEmotionID.Sad, 0, "당신의 이야기를 들으니 마음이 아파요."),
            new Line(EEmotionID.Smile, 0, "하지만 힘을 내세요. 저도 당신을 응원할게요."),
            new Line(EEmotionID.Happy, 0, "우리는 함께 이겨낼 수 있어요!")
        };
        Dialogue lisaDialogue = new Dialogue(ECharacterID.Lisa, lisaLines, .25f);
        dialogues.Add(lisaDialogue);

        PlayDialogues(dialogues);
    }

    public void PlayDialogues(List<Dialogue> dialogues)
    {
        if (_routine != null)
        {
            Debug.LogWarning("이미 대화가 진행 중입니다.");
            return;
        }
        _routine = StartCoroutine(DialoguesCoroutine(dialogues));
    }

    public void StopDialogue()
    {
        if (_routine != null)
        {
            StopCoroutine(_routine);
            _routine = null;
            Debug.Log("대화가 중단되었습니다.");
        }

        // 모든 CharacterPlayer 인스턴스를 파괴하고 리스트 초기화
        foreach (var player in _activeCharacterPlayers)
        {
            player.DestroyCharacter();
            Destroy(player.gameObject);
        }
        _activeCharacterPlayers.Clear();
    }

    private IEnumerator DialoguesCoroutine(List<Dialogue> dialogues)
    {
        foreach (Dialogue dialogue in dialogues)
        {
            yield return StartCoroutine(DialogueCoroutine(dialogue));
        }

        Debug.Log("모든 대화가 완료되었습니다.");

        // 모든 CharacterPlayer를 통해 캐릭터를 안전하게 파괴
        foreach (var player in _activeCharacterPlayers)
        {
            // 캐릭터를 페이드아웃으로 사라지게 합니다.
            player.CurrentCharacter.ShowCharacter(false, 1f);
        }

        // 1초 대기 후 캐릭터 오브젝트를 파괴
        yield return new WaitForSeconds(1f);

        foreach (var player in _activeCharacterPlayers)
        {
            player.DestroyCharacter();
            Destroy(player.gameObject);
        }

        _activeCharacterPlayers.Clear(); // 리스트를 비워줍니다.

        _routine = null;
    }

    private IEnumerator DialogueCoroutine(Dialogue dialogue)
    {
        // 새로운 CharacterPlayer 컴포넌트를 추가 및 초기화
        CharacterPlayer characterPlayer = gameObject.AddComponent<CharacterPlayer>();
        _activeCharacterPlayers.Add(characterPlayer); // 리스트에 추가
        characterPlayer.InitDialogue(dialogue);

        // DialoguePlayer를 초기화
        DialoguePlayer.ShowPanel(true, 0.5f);
        DialoguePlayer.InitDialogue(dialogue);

        while (true)
        {
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

            if (DialoguePlayer.IsTyping)
            {
                if (AutoCompleteEnabled)
                {
                    // 자동완성 기능이 활성화된 경우, 현재 문장이 타이핑 중이면 완성
                    DialoguePlayer.CompleteCurLine();
                    characterPlayer.WhenCurrentLineCompleted(); // CharacterPlayer도 현재 상태를 업데이트
                }
            }
            else
            {
                // 현재 문장이 이미 완료되었으면 다음 문장으로 넘어감
                if (DialoguePlayer.IsLastLine)
                {
                    break;
                }

                DialoguePlayer.DisplayNextLine();
                characterPlayer.WhenNextLinePlaying(); // CharacterPlayer도 다음 라인을 업데이트
            }

            // 프레임 대기: 중복 마우스 입력 방지를 위해 추가
            yield return null;
        }

        Debug.Log($"대화가 종료되었습니다: {dialogue.CharacterID}");
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        DialoguePlayer.ShowPanel(false, 0.5f);
    }
}
