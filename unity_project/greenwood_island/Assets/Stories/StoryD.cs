using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StoryD : Story
{
    public override string StoryId => "StoryD";

    public StoryD()
    {
        _elements = new List<Element>
        {
            new CharacterMove(ECharacterID.Kate, 0.33f),
            new Dialogue(
                ECharacterID.Kate,
                new List<Line>
                {
                    new Line(EEmotionID.Panic, 0, "리사, 우린 이 싸움을 끝내야 해."),
                    new Line(EEmotionID.Stumped, 1, "다른 방법이 있을 거야."),
                }
            ),
            new Dialogue(
                ECharacterID.Lisa,
                new List<Line>
                {
                    new Line(EEmotionID.Sad, 0, "맞아, 우리 이렇게 싸우면 안 돼."),
                    new Line(EEmotionID.Stumped, 1, "이제 같이 협력하자."),
                }
            ),
            new CharacterMove(ECharacterID.Lisa, 0.66f),
            new Dialogue(
                ECharacterID.Kate,
                new List<Line>
                {
                    new Line(EEmotionID.Happy, 0, "그래, 이제부터 함께하자."),
                }
            ),
            new PlaceMove(EPlaceID.Town2, 2f, Ease.InOutQuad, true, Color.white),
            new Dialogue(
                ECharacterID.Lisa,
                new List<Line>
                {
                    new Line(EEmotionID.Smile, 0, "그럼 우리 이제 새로운 모험을 시작해볼까?")
                }
            ),
            new StoryTransition("StoryE") // 다음 스토리로 전환
        };
    }
}
