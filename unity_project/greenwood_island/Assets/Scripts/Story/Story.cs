using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Story : MonoBehaviour
{
    private List<Element> _elements = new List<Element>()
    {
        // Scene 1: Town1 at dawn, peaceful atmosphere
        new PlaceMove(
            EPlaceID.Town1, 
            2f, Ease.InOutQuad, true, Color.black
        ),
        // Kate enters, appearing worried and lost
        new CharacterEnter(
            ECharacterID.Kate,
            0.5f ,
            1.5f, Ease.OutQuad
        ),
        new Dialogue(
            ECharacterID.Kate,
            new List<Line>
            {
                new Line(EEmotionID.Panic, 0, "여기는 어디지? 모두가 사라진 것 같아... 너무 조용해."),
                new Line(EEmotionID.Stumped, 1, "리사... 네가 여기 있었으면 좋았을 텐데. 이 이상한 기분을 떨칠 수가 없어."),
                new Line(EEmotionID.Angry, 2, "이건 단순한 장난이 아니야. 뭔가 잘못됐어. 나는 이곳을 알아야 해!"),
                new Line(EEmotionID.Angry, 2, "이건 단순한 장난이 아니야. 뭔가 잘못됐어. 나는 이곳을 알아야 해!2나는 이곳을 알아야 해!2나는 이곳을 알아야 해!2나는 이곳을 알아야 해!2")
            }
        ),
        
        // The atmosphere shifts as a shadow looms over the town
        new PlaceOverlayFilmEffect(
            Color.black.ModifiedAlpha(0.5f), 1.5f, Ease.InOutQuad
        ),

        new CharacterMove(
            ECharacterID.Kate,
            0.33f ,
            1f, Ease.OutQuad
        ),
        // Lisa suddenly appears to calm Kate down
        new CharactersEnter(
            new List<ECharacterID> { ECharacterID.Lisa },
            new List<float> { 0.7f },
            1f, Ease.OutQuad
        ),
        new Dialogue(
            ECharacterID.Lisa,
            new List<Line>
            {
                new Line(EEmotionID.Normal, 0, "케이트, 너 혼자 걱정하지 마. 우리가 함께라면 이곳을 알아낼 수 있을 거야."),
                new Line(EEmotionID.Smile, 1, "어떤 일이 있어도 우리는 함께 할 거야. 네가 여기에 있어서 다행이야.")
            }
        ),
        new PlaceOverlayFilmEffect(
            Color.red.ModifiedAlpha(.8f), 2f, Ease.InOutQuad
        ),
        // Kate and Lisa decide to explore the town together
        new Dialogue(
            ECharacterID.Kate,
            new List<Line>
            {
                new Line(EEmotionID.Happy, 0, "맞아, 리사. 우리 함께 이 마을의 비밀을 밝혀내자."),
                new Line(EEmotionID.Panic, 1, "하지만... 무언가 우리를 지켜보고 있는 것 같아. 이곳은 안전하지 않다."),
                new Line(EEmotionID.Stumped, 2, "우리가 계속 이곳에 있어야 하는 이유가 있을 거야.")
            }
        ),
        new PlaceOverlayFilmEffect(
            Color.clear, 2f, Ease.InOutQuad
        ),
        
        // The town suddenly darkens as they explore further
        new PlaceMove(
            EPlaceID.Town2, 
            2f, Ease.InOutQuad, true, Color.white
        ),
        new PlaceEffect(
            Color.magenta, 2.5f, Ease.OutQuad
        ),

        // Lisa and Kate, feeling the increasing danger, resolve to stay together
        new Dialogue(
            ECharacterID.Lisa,
            new List<Line>
            {
                new Line(EEmotionID.Panic, 0, "이 마을은... 너무 이상해. 무언가가 우리를 보고 있는 것 같아."),
                new Line(EEmotionID.Angry, 1, "포기하지 말자, 케이트. 우리가 함께라면 이겨낼 수 있어."),
                new Line(EEmotionID.Sad, 2, "하지만... 이곳에 정말 안전한 곳이 있을까?")
            }
        ),
        new CharacterMove(
            ECharacterID.Lisa,
            0.5f,
            1f, Ease.OutQuad
        ),
        new PlaceEffect(
            Color.white, 2.5f, Ease.OutQuad
        ),
        new Dialogue(
            ECharacterID.Kate,
            new List<Line>
            {
                new Line(EEmotionID.Happy, 0, "우리는 끝까지 포기하지 않을 거야. 이 마을의 비밀을 풀어야 해."),
                new Line(EEmotionID.Crying, 1, "리사, 네가 있어 다행이야. 정말로 이곳이 무서워...")
            }
        ),

        // Scene 3: Transition to the mysterious Town3, with an eerie atmosphere
        new PlaceMove(
            EPlaceID.Town1, 
            3f, Ease.InOutQuad, true, Color.black
        ),
        new PlaceOverlayFilmEffect(
            Color.blue, 2f, Ease.InOutQuad
        ),
        new PlaceEffect(
            Color.black, 3f, Ease.OutQuad
        ),

        // The two characters feel a sense of dread as they continue to explore
        new Dialogue(
            ECharacterID.Lisa,
            new List<Line>
            {
                new Line(EEmotionID.Stumped, 0, "이곳은 우리가 처음 온 곳과 완전히 달라... 무언가가 이 마을에 숨겨져 있어."),
                new Line(EEmotionID.Angry, 1, "이제 정말 중요한 순간이야, 케이트. 절대 포기하지 말자.")
            }
        ),
        new CharacterMove(
            ECharacterID.Kate,
            0.66f,
            1f, Ease.OutQuad
        ),
        new Dialogue(
            ECharacterID.Kate,
            new List<Line>
            {
                new Line(EEmotionID.Smile, 0, "우리가 해냈어! 이 마을의 비밀을 드디어 풀었어."),
                new Line(EEmotionID.Happy, 1, "이제 집으로 돌아갈 수 있어. 리사, 정말 고마워. 네가 없었다면 해내지 못했을 거야.")
            }
        ),

        // Final Scene: The characters prepare to leave the town, feeling relieved but wary
        new CharactersExit(
            new List<ECharacterID> { ECharacterID.Lisa, ECharacterID.Kate },
            1f, Ease.InQuad
        ),
        new PlaceMove(
            EPlaceID.Town1, 
            2f, Ease.InOutQuad, true, Color.white
        )
    };

    public List<Element> GetElements()
    {
        return _elements;
    }
}
