using UnityEngine;
using System.Collections.Generic;

public class PrayerForRainAfter : Story
{
    public override List<Element> UpdateElements => new List<Element>()
    {
        // 첫 번째 장면 설명
        new PlaceTransition(
            Color.black,
            1f,
            "ContainerFront",  // 시작 장소
            null,
            1f,
            true
        ),
        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("비가 그치고, 야산이 고요해졌다. 하지만 이곳 어딘가에 뭔가 중요한 것이 숨겨져 있을 것 같은 느낌이 든다."),
                new Line("주변을 살펴보자. 뭔가 단서가 있을지도 몰라.")
            }
        ),

        // PlaceTransitionGroup 생성 및 실행
        new PlaceTransitionGroup(
            new List<PlaceTransitionNode>
            {
                // 첫 번째 방: ContainerFront
                new PlaceTransitionNode(
                    "ContainerFront",
                    "",  // 되돌아갈 곳 없음
                    new List<PlaceTransitionNode>
                    {
                        // 연결된 방: ContainerInside
                        new PlaceTransitionNode(
                            "ContainerInside",
                            "ContainerFront",
                            new List<PlaceTransitionNode>
                            {
                                // 연결된 방: Test1
                                new PlaceTransitionNode(
                                    "Test1",
                                    "ContainerInside",
                                    new List<PlaceTransitionNode>
                                    {
                                        // 연결된 방: Test3
                                        new PlaceTransitionNode("Test3", "Test1", new List<PlaceTransitionNode>(), new Vector2(400, 400))
                                    },
                                    new Vector2(300, 300)
                                ),
                                // 연결된 방: Test2
                                new PlaceTransitionNode(
                                    "Test2",
                                    "ContainerInside",
                                    new List<PlaceTransitionNode>
                                    {
                                        // 연결된 방: Test4
                                        new PlaceTransitionNode("Test4", "Test2", new List<PlaceTransitionNode>(), new Vector2(600, 600))
                                    },
                                    new Vector2(500, 500)
                                )
                            },
                            new Vector2(200, 200)
                        )
                    },
                    new Vector2(0, 0)
                )
            }
        )
    };
}
