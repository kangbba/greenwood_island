using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StoryB : Story
{
    public override string StoryId => "StoryB";

    public StoryB()
    {
        _elements = new List<Element>
        {
            new CharacterMove(ECharacterID.Kate, 0.4f),
            new CharacterMove(ECharacterID.Lisa, 0.6f),
            new Dialogue(
                ECharacterID.Lisa,
                new List<Line>
                {
                    new Line(EEmotionID.Panic, 0, "그렇게 쉽게 나를 쓰러뜨릴 수 없을 거야!"),
                }
            ),
            new PlaceMove(EPlaceID.Forest, 1.5f, Ease.InOutQuad, true, Color.green),
            new Dialogue(
                ECharacterID.Kate,
                new List<Line>
                {
                    new Line(EEmotionID.Stumped, 1, "그래도... 이번에는 내가 유리해."),
                }
            ),
            new StoryTransition(new StoryD()) // 결과에 따라 StoryD로 전환
        };
    }
}
