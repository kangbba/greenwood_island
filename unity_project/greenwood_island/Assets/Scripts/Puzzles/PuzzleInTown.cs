using System;
using System.Collections.Generic;

public class PuzzleInTown : Puzzle
{
    public override bool GetIsPuzzleCleared(){
        return (_currentPlace.PlaceID == "BakeryInside") && IsEventCleared("TownFountainEnter"); 
    }
    public override Dictionary<string, SequentialElement> EventDictionary => 
    new Dictionary<string, SequentialElement>()
    {
        {
            "TownFruitStoreFrontEnter",
            new SequentialElement(
                new Dialogue(
                    "FruitSeller",
                    new List<Line>
                    {
                        new Line("쌉니다! 싸요! 여기 과일 싱싱한 거 한 번 보세요! 오늘 딴 제일 신선한 거예요!"),
                        new Line("어이, 거기 지나가는 양반! 안 사도 좋으니까 구경이라도 해보라고~!"),
                        new Line("...젊은이, 못 보던 얼굴인데? 여기 처음 와 본 건가?")
                    }
                ),

                new Dialogue(
                    "Ryan",
                    new List<Line>
                    {
                        new Line("하하하... 오늘 처음 와봤어요. 여긴는 엄청 활기차네요. 과일이 정말 신선해 보이네요."),
                    }
                ),

                new Dialogue(
                    "FruitSeller",
                    new List<Line>
                    {
                        new Line("그럼! 이 동네에서 내가 제일 신선한 과일을 판다니까!"),
                        new Line("올해도 농사가 잘됐거든. 전부 다 아말리안님 덕분이야."),
                        new Line("그분이 날씨를 항상 좋게 만들어주셔서 농사짓기가 아주 편해. 덕분에 과일 맛도 끝내주지."),
                        new Line("이 과일은 아말리안님께 바치는 과일 중에서도 제일 좋은 거라니까!"),
                        new Line("한번 먹어봐. 한입 베어 물면 다른 데 과일은 생각도 안 날 걸?")
                    }
                ),
                new Dialogue(
                    "Mono",
                    new List<Line>
                    {
                        new Line("아말리안님...?"),
                        new Line("누구일까? 마을 사람들한테 꽤 중요한 인물인 것 같은데."),
                    },
                    fadeout: true
                )
            )
        },
        {
            "Town3Enter",
            new SequentialElement(
                new ParallelElement(
                    new CharacterEnter(
                        "Kid1",
                        EmotionType.Default,
                        .33f
                    ),
                    new CharacterEnter(
                        "Kid2",
                        EmotionType.Default,
                        .66f
                    )
                ),
                new Dialogue(
                    "Kid1",
                    new List<Line>
                    {
                        new Line("잡아봐! 이번엔 내가 절대 안 잡힐 거야!"),
                        new Line("이 골목은 내가 다 알아! 너 절대 못 따라올걸?"),
                    }
                ),
                
                new Dialogue(
                    "Kid2",
                    new List<Line>
                    {
                        new Line("흐흐! 이번엔 내가 더 빨라! 너 못 잡을 거야!"),
                        new Line("준비됐지? 자, 술래잡기 시작!")
                    }
                ),

                new Dialogue(
                    "Kid1",
                    new List<Line>
                    {
                        new Line("잠깐만, 저기 누가 오고 있어!"),
                        new Line("...어? 형아, 못 보던 얼굴인데! 여긴 처음 온 거지?"),
                    }
                ),

                new Dialogue(
                    "Ryan",
                    new List<Line>
                    {
                        new Line("하하, 맞아. 오늘 처음 왔어. 너희들 술래잡기 하고 있었구나."),
                    }
                ),

                new Dialogue(
                    "Kid2",
                    new List<Line>
                    {
                        new Line("맞아! 형아도 같이 놀래? 우리가 술래잡기 엄청 잘해!"),
                        new Line("여긴 골목이 좁아서 달아나기 딱 좋거든!")
                    }
                ),

                new Dialogue(
                    "Ryan",
                    new List<Line>
                    {
                        new Line("근데 여긴 좀 위험해 보이는데, 이렇게 좁은 골목에서 뛰다가 다치지 않겠어?")
                    }
                ),

                new Dialogue(
                    "Kid1",
                    new List<Line>
                    {
                        new Line("에이~ 우리 여긴 늘 안전해! 아무도 다친 적 없거든."),
                        new Line("엄마가 여기선 걱정하지 말라고 했어. 아말리안님 덕분이래!")
                    }
                ),

                new Dialogue(
                    "Kid2",
                    new List<Line>
                    {
                        new Line("맞아! 아말리안님이 우리 마을을 지켜주셔서 여기선 걱정 없어!"),
                    }
                ),
                new ChoiceSet(
                    "아말리안이 누군지에 대해 물어볼까?",
                    new List<ChoiceContent>(){
                        new ChoiceContent(
                            "물어본다.", 
                            new SequentialElement(
                                new Dialogue(
                                    "Ryan",
                                    new List<Line>
                                    {
                                        new Line("아말리안님? 그분이 누구야?")
                                    }
                                ),

                                new Dialogue(
                                    "Kid1",
                                    new List<Line>
                                    {
                                        new Line("음, 나도 한 번도 못 봤어!"),
                                        new Line("엄마가 말했는데, 아말리안님이 계셔서 우리가 이렇게 안전하게 놀 수 있대."),
                                        new Line("형아도 아말리안님 덕분에 안전한 거야!"),
                                    }
                                ),

                                new Dialogue(
                                    "Kid2",
                                    new List<Line>
                                    {
                                        new Line("맞아! 아말리안님이 있으니까 마을이 항상 평화롭고 안전해! 술래잡기도 걱정 없어!"),
                                        new Line("엄마가 그러는데, 아말리안님은 엄청 멋진 옷을 입고 계신대! 빛나는 옷이라나?"),
                                        new Line("나도 꼭 한 번 보고 싶어!")
                                    }
                                ),
                                new Dialogue(
                                    "Mono",
                                    new List<Line>
                                    {
                                        new Line("아이들은 역시 순수하다. 부모님 말만 듣고도 그렇게 믿고 있구나."),
                                        new Line("나도 저런 시절이 있었지..."),
                                    }
                                )
                            )
                        ),
                        new ChoiceContent(
                            "물어보지 않는다.", 
                            new SequentialElement(
                                new Dialogue(
                                    "Mono",
                                    new List<Line>
                                    {
                                        new Line("아말리안이 누군지는 몰라도, 마을의 두터운 신뢰를 받고 있는 것 같다."),
                                    },
                                    fadeout: true
                                )
                            )
                        )
                    }

                )
                
            )
        },
        {
            "TownFountainEnter",
            new SequentialElement(

                // 마을 사람들이 웅성거림
                new Dialogue(
                    "OldMan1",
                    new List<Line>
                    {
                        new Line("그분이 오신다... 믿을 수 없어, 정말이야! 아말리안님께서 이곳에 오신다고!")
                    }
                ),

                new Dialogue(
                    "Villager1",
                    new List<Line>
                    {
                        new Line("이 누추한 곳에 무슨 일이시지? 아말리안님이 직접 오시다니..."),
                    }
                ),

                new Dialogue(
                    "Villager2",
                    new List<Line>
                    {
                        new Line("조용히 해! 아말리안님이 오신다. 제대로 인사드려야 해!")
                    }
                ),

                new Dialogue(
                    "OldMan2",
                    new List<Line>
                    {
                        new Line("평생 한 번 볼까 말까 한 분이라지... 이렇게 가까이서 보다니, 믿기 어렵군...")
                    }
                ),

                // 라이언의 독백
                new Dialogue(
                    "Mono",
                    new List<Line>
                    {
                        new Line("아말리안님... 마을 사람들에게 이렇게까지 경외심을 받는 분이라니."),
                        new Line("하지만 누구지? 그동안 들었던 아말리안님에 대한 이야기는 신비롭기만 했는데."),
                        new Line("직접 보면... 무슨 느낌일까?")
                    }
                ),

                // 아말리안님의 등장
                new CharacterEnter(
                    "Amalian",
                    EmotionType.Default,
                    .5f
                ),

                // 마을 사람들의 반응
                new Dialogue(
                    "Villager1",
                    new List<Line>
                    {
                        new Line("저기 오신다! 아말리안님이시다..."),
                        new Line("정말 성스러운 분이셔. 우리가 무사한 건 모두 아말리안님 덕분이지.")
                    }
                ),

                new Dialogue(
                    "Villager2",
                    new List<Line>
                    {
                        new Line("그분의 기적은 언제나 우리를 보호해주시지. 아말리안님이 계셔서 이 마을이 이렇게 평화로운 거야."),
                    }
                ),

                // 라이언의 독백: 아말리안님의 첫인상
                new Dialogue(
                    "Mono",
                    new List<Line>
                    {
                        new Line("저 분이... 아말리안님이라고?"),
                        new Line("뭔가... 예상과는 완전히 달라. 성인이 아닌 어린 소녀라니..."),
                        new Line("작고 여린 체구, 그런데도 성스러운 로브가 그녀를 감싸고 있다. 그녀가 어떻게 이런 위엄을 가질 수 있지?"),
                        new Line("분명 다른 세계에서 온 사람처럼 느껴져. 마을 사람들은 그녀를 이렇게까지 존경하는데, 도대체 무슨 일을 해왔던 걸까?")
                    }
                ),

                new Dialogue(
                    "Amalian",
                    new List<Line>
                    {
                        new Line("엘드라, 이곳의 공기는 변함없이 순수하구나. 마을은 나의 보호 속에 평온한가?")
                    }
                ),
                
                // 아말리안님의 등장
                new CharacterMove(
                    "Amalian",
                    .33f
                ),
                // 엘드라와의 대화 시작
                new CharacterEnter(
                    "Eldra",
                    EmotionType.Default,
                    .66f
                ),


                new Dialogue(
                    "Eldra",
                    new List<Line>
                    {
                        new Line("네, 아말리안님. 마을은 평화롭습니다. 다만 지난 폭풍으로 몇몇 가구가 피해를 입었습니다. 복구가 필요합니다."),
                    }
                ),

                new Dialogue(
                    "Amalian",
                    new List<Line>
                    {
                        new Line("바람은 우리를 시험하려는 신의 속삭임일 뿐... 그대는 모든 것을 평온으로 되돌리라. 아무도 불편함을 느끼지 않게 하라."),
                    }
                ),

                new Dialogue(
                    "Eldra",
                    new List<Line>
                    {
                        new Line("알겠습니다, 아말리안님. 바로 조치하겠습니다."),
                    }
                ),

                // 라이언의 독백: 아말리안의 영향력
                new Dialogue(
                    "Ryan",
                    new List<Line>
                    {
                        new Line("아말리안님..."),
                    }
                ),
                new Dialogue(
                    "Mono",
                    new List<Line>
                    {
                        new Line("이제는 저절로 그렇게 부르게 된다. 그 힘을 거스를 수 없는 느낌이다."),
                        new Line("그녀가 단순한 소녀가 아닌 건 분명해. 이 마을 사람들 모두가 그녀를 경외하는 이유가 뭘까?"),
                        new Line("도대체 어떤 존재이길래 마을 사람들이 그녀에게 이렇게 의지하는 걸까?")
                    }
                ),

                // 아말리안의 시찰 지시
                new Dialogue(
                    "Amalian",
                    new List<Line>
                    {
                        new Line("나는 마을을 직접 돌아보겠다. 이곳에 신의 축복이 여전히 깃들어 있는지 확인해야 하느니라."),
                    }
                ),

                new Dialogue(
                    "Eldra",
                    new List<Line>
                    {
                        new Line("네, 아말리안님."),
                    }
                ),

                new Dialogue(
                    "Amalian",
                    new List<Line>
                    {
                        new Line("모든 자들이 신의 가호 아래 머물 수 있도록... 나의 존재가 이 마을에 축복이 되기를."),
                        new Line("여기서는 그 누구도 두려워할 필요가 없노라.")
                    }
                ),

                // 라이언의 마지막 독백: 의문과 경외
                new Dialogue(
                    "Mono",
                    new List<Line>
                    {
                        new Line("이 어린 소녀가 이렇게나 큰 힘과 영향력을 가진 존재라니..."),
                        new Line("마을 사람들이 그녀를 이토록 따르는 이유가 분명히 있어."),
                        new Line("아말리안님... 도대체 그녀는 어떤 존재인 거지?")
                    }
                )
            )
        },

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
