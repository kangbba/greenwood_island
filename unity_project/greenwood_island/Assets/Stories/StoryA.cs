using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryA : Story
{
    public override string StoryId => "StoryA";

    public StoryA()
    {
        _elements = new List<Element>
        {
            new CharacterEnter(ECharacterID.Kate, 0.33f),
            new CharacterEnter(ECharacterID.Lisa, 0.66f),
            new Dialogue(
                ECharacterID.Kate,
                new List<Line>
                {
                    new Line(EEmotionID.Angry, 0, "리사, 이번에는 내가 이길 거야!"),
                    new Line(EEmotionID.Stumped, 1, "너를 쓰러뜨리기 위해 최선을 다할 거야.")
                }
            ),
            new Dialogue(
                ECharacterID.Lisa,
                new List<Line>
                {
                    new Line(EEmotionID.Smile, 0, "한번 해보자, 케이트."),
                    new Line(EEmotionID.Panic, 1, "하지만 넌 나를 이길 수 없어.")
                }
            ),
            new ChoiceElement(
                "케이트의 다음 행동은?",
                new List<ChoiceOption>
                {
                    new ChoiceOption(
                        "리사의 머리를 노린다",
                        new List<Element>
                        {
                            new Dialogue(
                                ECharacterID.Kate,
                                new List<Line>
                                {
                                    new Line(EEmotionID.Panic, 0, "머리를 노리면, 끝낼 수 있을 거야!"),
                                }
                            ),
                            new StoryTransition("StoryB") // 다음 스토리로 전환
                        }
                    ),
                    new ChoiceOption(
                        "리사의 몸을 노린다",
                        new List<Element>
                        {
                            new Dialogue(
                                ECharacterID.Kate,
                                new List<Line>
                                {
                                    new Line(EEmotionID.Stumped, 0, "몸을 노리면, 확실히 이길 수 있을 거야!"),
                                }
                            ),
                            new StoryTransition("StoryC") // 다음 스토리로 전환
                        }
                    ),
                    new ChoiceOption(
                        "방어한다",
                        new List<Element>
                        {
                            new Dialogue(
                                ECharacterID.Kate,
                                new List<Line>
                                {
                                    new Line(EEmotionID.Smile, 0, "방어를 강화하면, 나를 지킬 수 있을 거야!"),
                                }
                            ),
                            new StoryTransition("StoryD") // 다음 스토리로 전환
                        }
                    )
                }
            )
        };
    }
}
