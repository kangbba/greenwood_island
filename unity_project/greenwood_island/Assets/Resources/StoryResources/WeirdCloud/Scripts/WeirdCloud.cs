using System.Collections.Generic;
using UnityEngine;

public class WeirdCloud : Story
{
    public override List<Element> UpdateElements => new List<Element>
    {
        new ScreenOverlayFilm(Color.black),
        new PlaceTransition(
            new PlaceEnter("SeasideWalkway"),
            Color.black,
            new PlaceEffect(PlaceEffect.EffectType.ScaleZoom, duration: 1f, strength: 1.1f)
        ),
        new SFXEnter("Waves", 1, true, 2),

        new ScreenOverlayFilmClear(1f),

        new CharacterEnter(
            "Kate",
            KateEmotionID.ArmCrossed_Smile,
            .5f
        ),

        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("여긴 처음 걸어보네. 마을에 온 지 한 달도 넘었는데, 해변가 쪽은 처음이야."),
                new Line("날씨가 좋다 싶으면 태풍이 한 번씩 불어와서... 뭐랄까, 날씨 운이 없었다고 해야 하나?")
            }
        ),

        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("그럴 수 있지. 섬의 날씨는 마음대로야. 익숙해질 거야, 조금만 있으면."),
                new Line("그래도 여긴 좋은 곳이야. 사람들한테서 떨어져서 바람도 시원하고, 머리도 맑아지니까.")
            }
        ),

        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("바닷바람이 케이트 말대로 머릿속을 맑게 만든다. 파도 소리가 멀리서 들리고, 고요한 길이 부드럽게 이어진다."),
                new Line("왜 이제야 이곳에 와봤을까 싶다. 한 달 전엔 상상도 못 했던 평온함이 이제는 익숙해졌다.")
            }
        ),


        new EmotionChange(
            "Kate",
            KateEmotionID.ArmCrossed_YeahRight,
            .5f
        ),
        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("어릴 땐 이 섬이 답답했어. 아무 변화도 없고, 매일 똑같은 얼굴들... 여길 벗어나고 싶었던 적이 많았지."),
                new Line("한때는 정말 떠나려고 했어. 새로운 곳, 다른 사람들이 있는 곳으로 가고 싶었거든.")
            }
        ),

        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("떠났다가 결국 돌아온 거야?")
            }
        ),

        new EmotionChange(
            "Kate",
            KateEmotionID.ArmCrossed_Smile,
            .5f
        ),

        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("떠나보려고 했어. 하지만... 쉽지 않더라고."),
                new Line("사춘기 땐 모든 게 나를 억누르는 것처럼 느껴졌지. 하지만... 글쎄, 시간이 지나면서 마음이 바뀌더라."),
                new Line("결국 돌아왔지. 여기가 내 집이니까. 떠나려 했던 것도 다 지나간 일이야.")
            }
        ),

        // 가족 이야기를 꺼내는 라이언
        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("참, 나도 한 달 후면 돌아갈 준비해야겠더라. 가족들이 기다리고 있으니까."),
                new Line("난 잠깐 이곳에서 기록만 남기고 다시 돌아갈 예정이야.")
            }
        ),

        new EmotionChange(
            "Kate",
            KateEmotionID.ArmCrossed_Sad,
            .5f
        ),

        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("가족들이 기다리고 있구나... 그럼, 돌아가는 게 맞겠네."),
                new Line("멀리서 너를 기다리는 사람이 있다는 건 좋은 일이야. 기회가 왔을 때 가야지.")
            }
        ),

        // 케이트가 애매하게 대답하는 장면
        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("케이트가 말을 돌리는 듯한 느낌이 들었다. 뭔가 망설이는 듯한...")
            }
        ),

        // 라이언이 케이트의 가족 이야기를 꺼냄
        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("근데, 케이트. 너는 가족들이 이 마을 사람이야?")
            }
        ),

        new EmotionChange(
            "Kate",
            KateEmotionID.ArmCrossed_Sad,
            .5f
        ),
        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("음... 뭐, 그런 셈이지."),
            }
        ),
        new EmotionChange(
            "Kate",
            KateEmotionID.ArmCrossed_Smile,
            .5f
        ),
        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("하지만, 그 얘긴 나중에 하자. 지금은 바람이 정말 좋잖아?")
            }
        ),
        // 케이트가 미묘하게 반응을 피하는 장면
        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("케이트가 살짝 피하는 듯한 말투였다. 정확히는 모르겠지만, 가족 이야기가 불편한 건가?"),
                new Line("아니면 그저 오래된 이야기일 뿐인가... 잘 모르겠다. 그냥 넘어가는 게 나을지도.")
            }
        ),

        // 라이언이 장단 맞추며 대화 전환
        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("그래, 맞아. 바람이 정말 시원하네."),
                new Line("뭐, 나도 가족 생각은 나중에 하자. 여기가 너무 좋아서 잠시 잊고 싶어지네.")
            }
        ),

        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("대화를 돌리는 케이트의 태도가 조금 이상하긴 하지만, 깊게 생각할 필요는 없겠지."),
                new Line("그냥 지나가는 이야기였겠지. 어차피 난 곧 돌아갈 테니까.")
            }
        ),

        // 구름에 대한 대화가 시작되는 부분
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
                        new Line("섬에 오래 있으면 저런 풍경도 자연스러워져."),
                        new Line("이 섬은 그런 신비한 순간들이 많아. 보면 볼수록 평온해지는 것 같아.")
                    }
                ),

                new Dialogue(
                    "Mono",
                    new List<Line>
                    {
                        new Line("구름에서 눈을 뗄 수 없었다. 바람이 불어도 움직이지 않는 구름... 그 고요함이 기이하게 느껴졌다."),
                        new Line("케이트의 말처럼, 이 섬엔 뭔가 말로 설명할 수 없는 게 있는 것 같다.")
                    },
                    fadeout : true
                )
            )
        ),

        new ImaginationClear(1f),

        // 이후의 카페 도착 부분
        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("저기야! 저기가 카페 씨브리즈야!"),
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
