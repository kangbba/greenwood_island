using System;
using System.Collections.Generic;

public class PuzzleInContainer : PuzzleDocument
{
    public override Dictionary<string, SequentialElement> EventDictionary => 
    new Dictionary<string, SequentialElement>()
    {
        {
            "Mountain1Enter",
            new SequentialElement(
                new Dialogue(
                    "Mono",
                    new List<Line>
                    {
                        new Line("기우제가 끝났다. 사람들은 서둘러 산을 떠났다..."),
                        new Line("바람이 살갗을 스치며 지나간다. 이상한 기분이다... 그것은 무엇이었을까?"),
                        new Line("그런 생각을 하던 중, 어느 새 이곳에 나 혼자 남겨졌다는 걸 깨달았다."),
                        new Line("내가 본 것은 단순한 우연일까, 아니면 기가 막힌 우연의 일치일까..."),
                        new Line("이대로 가만히 있어도 달라질 건 없을 것이다."),
                        new Line("주변을 살펴보자. 혹시 무언가 알 수 있을지도 몰라.")
                    },
                    fadeout: true
                )
            )
        },

        {
            "ContainerFrontEnter",
            new SequentialElement(
                new Dialogue(
                    "Mono",
                    new List<Line>
                    {
                        new Line("어...? 이것은...?"),
                        new Line("낡고 오래된 컨테이너가 여기 있었다."),
                        new Line("이런 깊은 산속에 왜 이런 게 있는 거지...?"),
                        new Line("안에 뭔가 있을지도 모른다.")
                    },
                    fadeout: true
                )
            )
        },


        {
            "ContainerFrontSearch1",
            new SequentialElement(
                new MonoDialogue("나무가 있다. 별로 특별할 것은 없어 보인다."))
        },
        {
            "ContainerFrontSearch2",
            new SequentialElement(
                new MonoDialogue("돌로 된 계단위로 이끼가 끼어 있다. 미끄러지지 않게 조심해야겠다."))
        },
        {
            "ContainerFrontSearch3",
            new SequentialElement(
                new MonoDialogue("어둠이 짙게 깔려 있다. 빛이 닿지 않는 구석이 신경 쓰인다."))
        },
        {
            "ContainerFrontSearch4",
            new SequentialElement(
                new MonoDialogue("진흙 투성이인 바닥... 별로 특별할 것은 없어 보인다."))
        },



        {
            "Mountain3Search1",
                new SequentialElement(
                    new MonoDialogue("여긴 풀이 무성하다. 발걸음을 옮기기가 쉽지 않다."))
        },
        {
            "Mountain3Search2",
            new SequentialElement(
                new MonoDialogue("풀숲 사이로 뭔가 스치는 소리가 들린다. 작은 동물인가...?"))
        },
        {
            "Mountain3Search3",
            new SequentialElement(
                new MonoDialogue("풀이 너무 우거져서, 뭐가 숨어 있어도 이상하지 않겠다."))
        },
        {
            "Mountain3GetKey",
            new SequentialElement(
                new Dialogue(
                    "Mono",
                    new List<Line>
                    {
                        new Line("어...? 저기 뭔가 반짝이는 게 보인다."),
                        new Line("풀숲을 헤치자, 녹슨 열쇠가 드러난다."),
                    },
                    fadeout: true
                ),
                new ChoiceSet(
                    "주워 볼까?",
                    new List<ChoiceContent>()
                    {
                        new ChoiceContent(
                            "줍는다",
                            new SequentialElement(new ItemGain("ContainerKey"))
                        ),
                        new ChoiceContent(
                            "그냥 둔다",
                            new SequentialElement(new MonoDialogue("아무래도, 아무 물건이나 줍는 것은 위험할지도 몰라."))
                        )
                    }
                )
            )
        },

       {
            "ContainerInsideEnter",
            new SequentialElement(
                new Dialogue(
                    "Mono",
                    new List<Line>
                    {
                        new Line("문을 열고 안으로 들어섰다."),
                        new Line("사람이 살았던 흔적들이 곳곳에 남아 있다."),
                        new Line("낡은 이불과 부서진 가구, 잡동사니들이 바닥에 널브러져 있다."),
                        new Line("시간이 멈춰버린 방 같다... 누가, 왜 이런 곳에서 살았던 걸까?"),
                        new Line("뭔가 더 찾아볼 수 있을지도 모른다.")
                    },
                    fadeout: true
                )
            )
        },

        {
            "ContainerInsideFail",
            new SequentialElement(
                new MonoDialogue("문은 굳게 잠겨있다. 열 수 있는 방법이 없을까?")
            )
        }
    };

}
