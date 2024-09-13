using System.Collections.Generic;
using UnityEngine;

public class FirstKateStory : Story
{
    // FirstKateStory 스토리의 스크립트 로직을 여기에 작성하세요.
    protected override SequentialElement StartElements => new (
        new ScreenOverlayFilm(Color.black),
        new PlaceEnter("BakeryFront"),
        new CameraZoomByFactor(zoomFactor: 0.3f, duration: 0f),
        new CameraMove2DByAngle(-80, 160f, duration: 0f),
        new SFXEnter("BirdChirp1", 1f, true, 1f),
        new SFXEnter("BirdChirpLong1", 1f, true, 3f)
    );

    protected override SequentialElement UpdateElements => new (
        new ParallelElement(
            new ScreenOverlayFilmClear(),
            new CameraZoomClear(1f),
            new CameraMove2DClear(3f)
        ),
        new Dialogue(
            "",
            new List<Line>
            {
                new Line("그린우드에 온 지 벌써 한 달이 지났다."),
                new Line("태풍 속에 도착했던 첫날이 엊그제 같은데, 지금은 날씨도 맑고, 섬은 평화롭기만 하다."),
                new Line("새소리, 따뜻한 햇살, 그리고 케이트의 빵 냄새가 섬을 가득 채운다."),
                new Line("케이트는 오늘도 빵집 앞에서 바쁘게 진열대를 정리하고 있다."),
                new Line("그 열정은 언제 봐도 대단하다."),
            }
        ),
        new ScreenOverlayFilm(Color.white),
        new PlaceEnter("BakeryInside"),
        new ScreenOverlayFilmClear(),
        new CameraZoomClear(1f),

        new CharacterEnter(
            "Kate",
            .5f,
            "Normal",
            0
        ),
        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("라이언! 또 왔네? 빵 냄새가 그렇게 좋아?")
            }
        ),

        new Dialogue(
            "",
            new List<Line>
            {
                new Line("케이트, 그녀는 이 마을에서 작은 베이커리를 운영하고 있다."),
                new Line("내가 마을에 도착하자마자 말을 걸어주며 마을에 적응하는데 도움을 주었다."),
                new Line("작은 베이커리이지만 그 맛은 훌륭하다. 마을 사람들도 이곳을 참 좋아한다."),
            }
        ),

        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("응, 네 빵 냄새는 진짜 못 참겠어. 너 오늘도 새벽부터 일어났지?")
            }
        ),

        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("맞아, 오늘은 날씨가 좀 따뜻해서 발효 시간을 조절했어. 온도가 높으면 반죽이 더 빨리 부풀거든. 그때마다 다르게 해야 해.", "Smile", 0),
            }
        ),
        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("발효... 그냥 빵 굽는 게 전부가 아니었구나?"),
            }
        ),
        new Dialogue(
            "",
            new List<Line>
            {
                new Line("매번 다른 날씨에 따라 조절해야 한다니, 생각보다 더 섬세한 일이었다."),
                new Line("케이트는 이런 작은 디테일까지 다 신경 쓰고 있구나."),
            }
        ),

        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("그럼 오늘 빵은 또 다른 맛이겠네? 매번 먹을 때마다 기대돼."),
            }
        ),

        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("당연하지. 오늘은 빵 속이 더 폭신폭신하고 부드럽게 만들어보려고 조금 더 시간을 들였어. 먹으면 네가 바로 알 거야.", "Normal"),
            }
        ),

        new ImaginationEnter(
            "Bread",
            1f,
            Color.white
        ),
        new Dialogue(
            "",
            new List<Line>
            {
                new Line("그녀가 내미는 빵은 언제나 특별하다."),
                new Line("화려하지 않고 소박하지만, 말로는 설명 할 수 없는, 뭔가 더 깊은 게 있다."),
                new Line("아마 케이트의 열정과 정성이 담겨 있어서 그런 거겠지."),
                new Line("한 입 베어 물 때마다 입안 가득 퍼지는 폭신한 식감이, 이 섬에서의 일상이 점점 더 좋아진다."),
            }
        ),

        new ImaginationClear(),
        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("확실히... 진짜 부드럽네. 네가 왜 이 빵을 그렇게 자랑하는지 알겠어."),
            }
        ),

        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("그럼 자주 와. 너도 맛있게 먹는 거 보면 나도 기분 좋거든."),
            }
        ),

        new Dialogue(
            "",
            new List<Line>
            {
                new Line("케이트와 나누는 이런 대화가 요즘 나에게는 가장 소중한 시간이다."),
                new Line("평범한 일상 속에서 느껴지는 작은 행복이 이곳에 나를 묶어두는 것 같다."),
            }
        ),
        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("근데 있잖아, 라이언! 내가 너한테 소개해줄 사람이 있는데 말이야..."),
                new Line("케이트가 기대에 찬 표정으로 나를 바라보며 말했다."),
            }
        ),
        new ChoiceSet(
            "어떻게 반응할까?",
            new List<ChoiceContent>
            {
                new ChoiceContent(
                    "누군데? 갑자기 왜?",
                    new SequentialElement(
                        new Dialogue("Ryan", new List<Line>
                        {
                            new Line("누군데? 갑자기 왜?", "Normal"),
                        }),
                        new Dialogue("Kate", new List<Line>
                        {
                            new Line("우리 마을에서 중요한 사람이야. 직접 만나보면 너도 이해할 거야.", "Smile")
                        }),
                        new Dialogue("Ryan", new List<Line>
                        {
                            new Line("갑작스럽지만... 좋아, 갈게!", "Normal")
                        })
                    )
                ),
                new ChoiceContent(
                    "귀찮은데… 그냥 다음에 만나면 안 돼?",
                    new SequentialElement(
                        new Dialogue("Ryan", new List<Line>
                        {
                            new Line("귀찮은데… 그냥 다음에 만나면 안 돼?", "Normal")
                        }),
                        new Dialogue("Kate", new List<Line>
                        {
                            new Line("라이언, 이건 놓치면 후회할걸? 조셉 할아버지는 특별한 분이야.", "Surprised"),
                            new Line("다녀오면 분명 나중에 고마워할 거야. 나만 믿어봐!", "Smile")
                        }),
                        new Dialogue("Ryan", new List<Line>
                        {
                            new Line("알았어, 알았어. 가면 되는 거지?", "Normal")
                        })
                    )
                ),
                new ChoiceContent(
                    "좋아, 어디 한번 만나보자고.",
                    new SequentialElement(
                        new Dialogue("Ryan", new List<Line>
                        {
                            new Line("좋아, 어디 한번 만나보자고.", "Normal")
                        })
                    )
                )
            }
        ),
        new Dialogue("Kate", new List<Line>
        {
            new Line("정말? 라이언이라면 그렇게 말해 줄 줄 알았다구!", "Smile"),
            new Line("그럼 이따 가게 앞으로 와 줘.", "Smile"),
        }),
        new Dialogue("", new List<Line>
        {
            new Line("케이트는 이 마을에서 처음 나를 맞아준 사람이고, 지금까지 항상 내 편이 되어줬다. 그녀의 말이라면 믿어도 되겠지.", "Normal"),
            new Line("그 사람은 어떤 사람일까?", "Normal"),
        }),

        new ScreenOverlayFilm(Color.black, 1f),
        new SFXsClear(1f),
        new PlaceEnter("BakeryFront"),
        new ScreenOverlayFilmClear(),

        new ActionMenu("다음 행동은?", new List<ActionMenu>{
            new ActionMenu(
                "대화하기",
                new SequentialElement(
                    new Dialogue("Kate", new List<Line>
                    {
                        new Line("준비됐어? 얼른 가자!", "Normal")
                    })
                )
            )
        })



    );

    protected override SequentialElement ExitElements => new ();

    protected override string StoryDesc => "";


    // ActionMenu actionMenu = new ActionMenu(
    //     "다음 행동은?",
    //     new List<ActionMenu>
    //     {
    //         // 첫 번째 상위 메뉴: 말을 건다
    //         new ActionMenu("말을 건다", new List<ActionMenu>
    //         {
    //             // 첫 번째 메뉴의 하위 메뉴들
    //             new ActionMenu("아말리안 밀에 대해 묻는다", new SequentialElement(
    //                 new Dialogue("캐릭터A", new List<Line>
    //                 {
    //                     new Line("아말리안 밀은 오래된 곳이지만 여전히 굳건해."),
    //                 })
    //             )),
    //             new ActionMenu("오늘 일상에 대해 묻는다", new SequentialElement(
    //                 new Dialogue("캐릭터B", new List<Line>
    //                 {
    //                     new Line("오늘은 평화로웠어, 이상한 일도 없었고."),
    //                 })
    //             )),
    //             new ActionMenu(
    //                 "새로운 방문자에 대해 묻는다", 
    //                 new List<ActionMenu>
    //                 {
    //                     new ActionMenu(
    //                         "방문자의 목적은?", 
    //                         new SequentialElement(
    //                         new Dialogue("캐릭터A", new List<Line>
    //                             {
    //                                 new Line("그는 자신을 연구자라 소개했어."),
    //                             })
    //                         )),
    //                         new ActionMenu("방문자의 외모는?", new SequentialElement(
    //                             new Dialogue("캐릭터B", new List<Line>
    //                             {
    //                                 new Line("키가 크고, 검은 옷을 입고 있었지."),
    //                             })
    //                         )
    //                     )
    //                 })
    //         }),

    //         // 두 번째 상위 메뉴: 주위를 둘러본다
    //         new ActionMenu("주위를 둘러본다", new List<ActionMenu>
    //         {
    //             // 두 번째 메뉴의 하위 메뉴들
    //             new ActionMenu("창문 밖을 본다", new SequentialElement(
    //                 new Dialogue("나", new List<Line>
    //                 {
    //                     new Line("바깥에는 잿빛 하늘과 흔들리는 나무들만 보인다."),
    //                 })
    //             )),
    //             new ActionMenu("방 안을 살핀다", new List<ActionMenu>
    //             {
    //                 new ActionMenu("테이블 위를 살핀다", new SequentialElement(
    //                     new Dialogue("나", new List<Line>
    //                     {
    //                         new Line("테이블 위에는 먼지 쌓인 책과 찻잔이 놓여 있다."),
    //                     })
    //                 )),
    //                 new ActionMenu("벽에 걸린 그림을 본다", new List<ActionMenu>
    //                 {
    //                     new ActionMenu("그림의 제목을 확인한다", new SequentialElement(
    //                         new Dialogue("나", new List<Line>
    //                         {
    //                             new Line("그림의 제목은 '기억의 풍경'."),
    //                         })
    //                     )),
    //                     new ActionMenu("그림의 세부 사항을 살핀다", new List<ActionMenu>
    //                     {
    //                         new ActionMenu("그림 속 사람들을 본다", new SequentialElement(
    //                             new Dialogue("나", new List<Line>
    //                             {
    //                                 new Line("그림 속 사람들은 어딘가 슬퍼 보인다."),
    //                             })
    //                         )),
    //                         new ActionMenu("그림의 배경을 본다", new SequentialElement(
    //                             new Dialogue("나", new List<Line>
    //                             {
    //                                 new Line("배경은 바람에 흔들리는 초원이 그려져 있다."),
    //                             })
    //                         ))
    //                     })
    //                 })
    //             })
    //         }),

    //         // 세 번째 상위 메뉴: 물건을 조사한다
    //         new ActionMenu("물건을 조사한다", new List<ActionMenu>
    //         {
    //             // 세 번째 메뉴의 하위 메뉴들
    //             new ActionMenu("책을 조사한다", new List<ActionMenu>
    //             {
    //                 new ActionMenu("표지를 본다", new SequentialElement(
    //                     new Dialogue("나", new List<Line>
    //                     {
    //                         new Line("책의 표지는 낡았고, 제목은 희미하게 보인다."),
    //                     })
    //                 )),
    //                 new ActionMenu("책의 첫 페이지를 넘긴다", new List<ActionMenu>
    //                 {
    //                     new ActionMenu("첫 문장을 읽는다", new SequentialElement(
    //                         new Dialogue("나", new List<Line>
    //                         {
    //                             new Line("첫 문장은 '여정의 시작은 언제나 고요했다.'"),
    //                         })
    //                     )),
    //                     new ActionMenu("책갈피를 발견한다", new SequentialElement(
    //                         new Dialogue("나", new List<Line>
    //                         {
    //                             new Line("책갈피는 오래된 사진이다."),
    //                         })
    //                     ))
    //                 })
    //             }),
    //             new ActionMenu("의자를 조사한다", new SequentialElement(
    //                 new Dialogue("나", new List<Line>
    //                 {
    //                     new Line("의자는 튼튼해 보이지만 오래된 티가 난다."),
    //                 })
    //             )),
    //             new ActionMenu("탁자를 조사한다", new List<ActionMenu>
    //             {
    //                 new ActionMenu("서랍을 열어본다", new SequentialElement(
    //                     new Dialogue("나", new List<Line>
    //                     {
    //                         new Line("서랍 안에는 오래된 편지가 있다."),
    //                     })
    //                 )),
    //                 new ActionMenu("탁자 위의 물건을 본다", new List<ActionMenu>
    //                 {
    //                     new ActionMenu("촛대를 살핀다", new SequentialElement(
    //                         new Dialogue("나", new List<Line>
    //                         {
    //                             new Line("촛대는 새것처럼 깨끗하다."),
    //                         })
    //                     )),
    //                     new ActionMenu("낡은 시계를 본다", new SequentialElement(
    //                         new Dialogue("나", new List<Line>
    //                         {
    //                             new Line("시계는 멈춰 있었다."),
    //                         })
    //                     ))
    //                 })
    //             })
    //         })
    //     }
    // );

}
