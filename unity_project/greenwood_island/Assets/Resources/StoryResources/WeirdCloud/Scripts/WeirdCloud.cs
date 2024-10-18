using System.Collections.Generic;
using UnityEngine;

public class WeirdCloud : Story
{
    public override List<Element> UpdateElements => new List<Element> {
        new ScreenOverlayFilm(Color.black),
        new PlaceTransition(
            new PlaceEnter("SeasideWalkway"),
            Color.black,
            new PlaceEffect(PlaceEffect.EffectType.ZoomIn, 1f, 1.1f)
        ),
        new SFXEnter("Waves", 1, true, 2),

        new ScreenOverlayFilmClear(1f),

        new CharacterEnter(
            "Kate",
            EmotionType.Happy,
            .5f,
            0
        ),

        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("여긴 처음 걸어보네. 마을에 온 지 한 달도 넘었는데, 해변가 쪽은 처음이야."),
                new Line("날씨가 좋다 싶으면 태풍이 한 번씩 불어와서… 뭐랄까, 날씨 운이 없었다고 해야 하나?")
            }
        ),

        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("그러게. 근데 말이야, 이런 한적한 곳이 난 오히려 좋더라. 마을 중심은 너무 복잡하거든."),
                new Line("여기서 바람 쐬면 머리도 맑아지고, 뭔가 힘이 나는 기분이랄까? 어때, 시원하지?")
            }
        ),

        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("케이트의 말대로 바닷바람이 가슴 속 깊이 스며든다. 머리가 맑아지는 느낌."),
                new Line("파도 소리가 멀리서 들리고, 고요한 길이 부드럽게 이어진다."),
                new Line("왜 이제야 이곳에 와봤을까 싶다."),
                new Line("한 달 전엔 상상도 못 했던 평온함이 이젠 익숙해졌다.")
            }
        ),

        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("사실, 어릴 때는 이 섬이 답답했어. 작은 마을에 사람도 똑같고, 변화가 없는 게 싫었거든."),
                new Line("근데, 한편으론… 이제는 그런 게 오히려 마음을 편하게 해. 나만의 공간 같달까?")
            }
        ),

        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("그럴 수도 있지. 섬이 크지도 않고, 변화도 적으니까. 그래서 떠나고 싶었던 거야?")
            }
        ),

        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("응, 한창 사춘기 때는 도시로 나가고 싶었어. 새로운 사람들, 새로운 경험들… 그게 그리웠거든."),
                new Line("근데 결국엔 다시 여기로 돌아왔지. 왜냐면… 글쎄, 여기가 내 집이니까.")
            }
        ),

        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("그래도 가끔은 떠나고 싶은 마음이 들지 않아?")
            }
        ),

        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("당연하지! 가끔은 새로운 게 필요하잖아. 근데, 떠나도 결국엔 여기가 나를 받아줄 거란 걸 아니까 괜찮아."),
                new Line("여기서 나만의 방식으로 살아가는 게 나쁘진 않아. 너도 느끼고 있지 않아?")
            }
        ),

        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("케이트는 이곳에 대한 확신을 가지고 있다. 그 확신이 그녀를 더욱 강하게 만드는 것 같다."),
                new Line("나 역시 이 섬에서 뭔가를 찾고 있는지도 모르겠다.")
            }
        ),

        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("네가 여기에 남는다는 게 이해돼. 이곳은 변하지 않아서 오히려 안정감을 주는 것 같아.")
            }
        ),

        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("맞아! 변하지 않는 게 좋은 때도 있어. 물론, 때때로 떠나 새로운 경험을 쌓는 것도 필요하지만."),
                new Line("근데 말야, 언제든 돌아와도 이곳은 그대로 있어줄 거란 확신이 있는 게 참 좋더라고.")
            }
        ),

        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("케이트는 확고하게 이 섬에 뿌리를 내리고 있다. 나 역시 이 섬이 주는 평온함을 부정할 수 없다.")
            }
        ),

        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("우리가 이런저런 이야기를 나누며 걷는 동안, 문득 하늘이 눈에 들어왔다."),
                new Line("구름 한 덩이가 고요한 바다 위에 떠 있었고, 마치 은빛으로 빛나는 듯했다.")
            },
            fadeout : true
        ),

        new ImaginationEnter(
            "WeirdCloud"
        ),

        new ParallelElement(
            new ImaginationMove(
                Vector2.down * 100,
                10f
            ),
            new ImaginationScale(
                Vector2.one * 1.3f,
                2f
            ),
            new SequentialElement(
                new Dialogue(
                    "Ryan",
                    new List<Line>
                    {
                        new Line("저 구름 좀 봐. 마치 그림 같아."),
                        new Line("뭔가 신비로워.")
                    }
                ),
                new Dialogue(
                    "Kate",
                    new List<Line>
                    {
                        new Line("섬에 오래 있으면 저런 구름도 익숙해질 거야."),
                        new Line("이 섬에는 자연스럽고 신비로운 게 많아. 그런 풍경들을 계속 보다 보면, 마음이 편안해지지.")
                    }
                ),

                new Dialogue(
                    "Mono",
                    new List<Line>
                    {
                        new Line("그 구름에서 눈을 뗄 수 없었다. 바람이 불어도 미동도 없는 구름… 그 고요함이 신비로웠다.")
                    },
                    fadeout : true
                )
            )
        ),


        new ImaginationClear(
            1f
        ),

        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("저기야! 저기가 카페 씨브리즈야!")
            },
            fadeout : true
        ),

        new PlaceTransition(
            new PlaceEnter("CafeSeabreezeFront"),
            Color.black,
            new PlaceEffect(PlaceEffect.EffectType.FadeIn, 1f)
        ),

        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("케이트가 손을 가리키며 들뜬 목소리로 외쳤다."),
                new Line("멀리서 작은 카페 건물이 보였다. 따뜻해 보이는 곳이었다."),
                new Line("케이트는 흥분한 얼굴로 발걸음을 재촉했다.")
            }
        ),

        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("조셉 할아버지를 드디어 너에게 소개해 줄 수 있다니! 진짜 기대돼.")
            }
        ),

        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("그녀의 밝은 모습에 나도 모르게 미소가 번졌다."),
                new Line("마침내, 카페 씨브리즈에 도착했다.")
            }
        )
    };

}
