using System.Collections.Generic;
using UnityEngine;

public class AfterFirstJoseph : Story
{
    protected override SequentialElement StartElements => new (
        new PlaceTransition(Color.black, 1f, "SeasideWalkway", null, 1f, true),
        new CameraZoomByFactor(zoomFactor: 0.3f, duration: 0f),
        new CameraMove2DByAngle(-80, 160f, duration: 0f),
        new SFXEnter("Waves", 1, true, 2)
    );

    protected override SequentialElement UpdateElements => new (
        new ParallelElement(
            new ScreenOverlayFilmClear(),
            new CameraZoomClear(1f),
            new CameraMove2DClear(3f)
        ),

        new ScreenOverlayFilmClear(1f),

        new CharacterEnter(
            "Kate",
            "Normal",
            .5f,
            0
        ),


        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("그렇지? 그게 할아버지의 매력이지."),
                new Line("할아버지는 언제나 우리에게 깊은 걸 알려주시는 것 같아, 직접 말씀하시진 않아도.", "Smile")
            }
        ),

        new ImaginationEnter(
            "WeirdCloud",
            1f,
            Color.white
        ),
        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("케이트와 이런저런 이야기를 나누며 걷고 있을 때, 문득 하늘이 눈에 들어왔다."),
                new Line("구름 한 덩이가 고요한 바다 위로 낮게 떠 있었고, 그 구름은 마치 은빛으로 빛나며 햇살을 받아 반짝이고 있었다.")
            }
        ),


        new DialoguePanelClear(),

        new UserActionPhaseEnter(
            UserActionWindow.AnchorType.TopRight,
            new Vector2(-300, -300),
            new Dictionary<UserActionType, SequentialElement>{ 
                {UserActionType.TakingPicture, new SequentialElement(
                    // 라이언이 구름을 경이롭게 여기는 장면
                    new Dialogue(
                        "Ryan",
                        new List<Line>
                        {
                            new Line("사진을 찍어둬야겠어."),
                        }
                    ),
                    new TakingPhoto(
                        "WeirdCloud"
                    )
                )} ,
                {UserActionType.Talking, new SequentialElement(
                    // 라이언이 구름을 경이롭게 여기는 장면
                    new Dialogue(
                        "Ryan",
                        new List<Line>
                        {
                            new Line("와... 저 구름 좀 봐."),
                            new Line("이렇게 맑은 날씨에 저렇게 멋진 구름이 있다니. 마치 그림처럼 떠 있는 것 같아."),
                            new Line("뭔가... 그냥 바라보고 있으면 신비로운 기분이 들어."),
                        }
                    ),

                    // 케이트가 익숙하게 설명하는 장면
                    new Dialogue(
                        "Kate",
                        new List<Line>
                        {
                            new Line("아, 저런 구름은 이 섬에선 자주 볼 수 있어."),
                            new Line("섬에 오래 있으면 익숙해질 거야. 처음엔 나도 저 구름을 보고 많이 놀랐거든.", "Calm"),
                            new Line("그냥 경이롭지? 이 섬의 매력이 그런 거야. 자연스럽고 신비로운 풍경이 가득해.")
                        }
                    )
                )} 
            }
        ),
        new AllImaginationsClear(),

        // 라이언이 구름을 더 자세히 관찰하는 장면
        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("나는 구름에서 눈을 뗄 수 없었다. 마치 세상에서 딱 하나 남은 구름처럼, 그 구름은 하늘에 고요하게 떠 있었다."),
                new Line("바람이 불어오는데도 구름은 미동도 없었다. 그저 그 자리에 멈춘 듯한 모습이 경이로웠다.")
            }
        )
    );

    protected override SequentialElement ExitElements => new (
        new ScreenOverlayFilm(Color.black)
    );

    protected override string StoryDesc => "";
}
