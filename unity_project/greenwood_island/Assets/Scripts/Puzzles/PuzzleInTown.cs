using System;
using System.Collections.Generic;

public class PuzzleInTown : Puzzle
{
    public override Dictionary<string, SequentialElement> EventDictionary => 
    new Dictionary<string, SequentialElement>()
    {
        {
            "BakeryInsideEnter",
            new SequentialElement(
                new CharacterEnter(
                    "Kate",
                    EmotionType.ArmCrossed_Smile,
                    .5f
                ),
                new Dialogue(
                    "Kate",
                    new List<Line>
                    {
                        new Line("마을엔 즐거운 곳들이 많아. 좀 더 둘러보고 와!"),
                    },
                    fadeout: true
                )
            )
        },
        {
            "BakeryInsideExit",
            new SequentialElement(
                new AllCharactersClear()
            )
        },
        {
            "NotNeeded",
            new SequentialElement(
                new Dialogue(
                    "Mono",
                    new List<Line>
                    {
                        new Line("지금은 마을 외곽으로 갈 필욘 없을 것 같아. 케이트 말 처럼 마을 내부를 둘러보자."),
                    },
                    fadeout: true
                )
            )
        },
    };

}
