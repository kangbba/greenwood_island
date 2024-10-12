using System.Collections.Generic;

public class PuzzleContainer : Puzzle
{
    // 버튼 이벤트 사전 (버튼 **ID**로 SequentialElement 참조)
    public override Dictionary<string, SequentialElement> BtnEvents { get; } = new()
    {
        { "1", 
            new SequentialElement(
                new Dialogue(
                    "Mono",
                    new List<Line>
                    {
                        new Line("내부에 무언가 반짝이는 것이 보인다.")
                    },
                    down: true
                )
            )
        },
        { "2", 
            new SequentialElement(
                new Dialogue(
                    "Mono",
                    new List<Line>
                    {
                        new Line("깊숙한 곳에 물건이 있다."),
                        new Line("긴 막대 같은 것이 필요해 보인다.")
                    },
                    down: true
                )
            )
        }
    };
    // 버튼 이벤트 사전 (버튼 **ID**로 SequentialElement 참조)
    public override Dictionary<string, SequentialElement> EnterEvents { get; } = new()
    {
        { "ContainerInside", 
            new SequentialElement(
                new Dialogue(
                    "Mono",
                    new List<Line>
                    {
                        new Line("컨테이너 창고에 들어섰다."),
                        new Line("주변이 어둡고 먼지 냄새가 난다."),
                        new Line("뭔가 중요한 것이 숨어 있을지도 모른다.")
                    },
                    down: true
                )
            )
        }
    };

    // 버튼 이벤트 사전 (버튼 **ID**로 SequentialElement 참조)
    public override Dictionary<string, SequentialElement> ExitEvents { get; } = new()
    {
        { "ContainerInside", 
            new SequentialElement(
                new Dialogue(
                    "Mono",
                    new List<Line>
                    {
                        new Line("컨테이너 창고를 나섰다."),
                        new Line("바람이 불며 문이 쾅 닫혔다."),
                        new Line("다시 들어가기 싫은 기분이 든다.")
                    },
                    down: true
                )
            )
        }
    };
}
