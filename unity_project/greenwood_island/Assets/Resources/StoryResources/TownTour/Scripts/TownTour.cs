using UnityEngine;
using System.Collections.Generic;

public class TownTour : Story
{
    public override List<Element> UpdateElements => new List<Element>()
    {
        new PuzzleEnter("PuzzleInTown"),
        
        new Dialogue(
            "Ryan",
            new List<Line>()
            {
                new Line("케이트? 여기 있어?"),
                new Line("케이트...? 어디 간 거야?"),
                new Line("케이트!"),
            }
        ),
        
        // 빵집 내부를 관찰하는 라이언의 독백
        new Dialogue(
            "Mono",
            new List<Line>()
            {
                new Line("조용하네... 아무도 없는 것 같아."),
                new Line("의자가 엉망으로 놓여 있고, 빵이 여기저기 흩어져 있다. 케이트가 급하게 나간 게 분명하다."),
                new Line("평소엔 항상 깔끔하게 정리해뒀던 것 같은데, 오늘은 전혀 아니잖아."),
            }
        ),

        // 라이언이 농담을 던지며 상황을 넘기려는 장면
        new Dialogue(
            "Ryan",
            new List<Line>()
            {
                new Line("하하, 설마... 그동안 깨끗한 척했던 거구만? 내가 그럴 줄 알았다구."),
            }
        ),
        
        // 다시 진지해지는 라이언의 독백
        new Dialogue(
            "Mono",
            new List<Line>()
            {
                new Line("... 그래도 뭔가 이상하다. 케이트가 이렇게 아무 말 없이 사라질 리가 없는데."),
                new Line("정말 아말리안님을 보러 갔을까? 아니면 피곤해서 집에 간 걸까?"),
                new Line("별일 아니겠지. 그렇겠지."),
            }
        ),

        // 라이언이 상황을 대수롭지 않게 넘기며 마무리하는 장면
        new Dialogue(
            "Mono",
            new List<Line>()
            {
                new Line("뭐, 됐어. 나도 집에나 가야겠다."),
                new Line("오늘 하루도 꽤 즐거웠으니까 말이야."),
            }
        )
    };
}
