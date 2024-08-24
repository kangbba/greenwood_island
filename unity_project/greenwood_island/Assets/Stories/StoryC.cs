using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StoryC : Story
{
    public override string StoryId => "StoryC";

    public StoryC()
    {
        _elements = new List<Element>
        {
            new CharacterMove(ECharacterID.Kate, 0.4f),
            new CharacterMove(ECharacterID.Lisa, 0.6f),
            new Dialogue(
                ECharacterID.Lisa,
                new List<Line>
                {
                    new Line(EEmotionID.Panic, 0, "너의 움직임이 더 빨라졌군... 하지만 이길 수 없어!"),
                }
            ),
            new PlaceMove(EPlaceID.Mountain, 1.5f, Ease.InOutQuad, true, Color.gray),
            new Dialogue(
                ECharacterID.Kate,
                new List<Line>
                {
                    new Line(EEmotionID.Happy, 1, "이번에는 내가 유리해."),
                }
            ),
            new StoryTransition(new StoryD()) // 결과에 따라 StoryD로 전환
        };
    }
}
