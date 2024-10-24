using System.Collections.Generic;
using UnityEngine;
using static ImageUtils;
public class FirstKateStory : Story
{
    public override List<Element> UpdateElements => new List<Element> {

        new Intertitle("한달 후"),
        new SFXEnter("BirdChirp1", 1f, true, 1f),
        new SFXEnter("BirdChirpLong1", 1f, true, 3f),
        new PlaceTransition(
            new PlaceEnter("BakeryFront"),
            Color.black,
            new PlaceEffect(PlaceEffect.EffectType.ScaleZoom, duration : 2f, strength : 1.1f)
        ),
        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("그린우드에 온 지 벌써 한 달이 지났다."),
                new Line("태풍 속에 도착했던 첫날이 엊그제 같은데, 지금은 날씨도 맑고, 섬은 평화롭기만 하다."),
                new Line("새소리, 따뜻한 햇살, 그리고 케이트의 빵 냄새가 섬을 가득 채운다."),
                new Line("케이트는 오늘도 빵집 앞에서 분주하게 진열대를 정리하고 있다."),
                new Line("그녀의 에너지는 정말 대단하다.")
            },
            fadeout: true
        ),

        new SFXsClear(),

        new PlaceTransitionWithSwipe(
            "BakeryInside",
            1f,
            SwipeMode.SwipeLeft
        ),

        new CharacterEnter(
            "Kate",
            KateEmotionID.ArmCrossed_Smile,
            .5f
        ),
        
        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("라이언! 또 빵 냄새에 이끌렸지? 네 코는 진짜 정확하단 말이야!"),
            }
        ),

        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("케이트, 이 마을에서 작은 베이커리를 운영하며 늘 활기차고 긍정적이다."),
                new Line("내가 처음 도착했을 때부터 먼저 다가와서 마을 생활에 적응하도록 도와주었다."),
                new Line("그녀의 빵은 작지만, 그 맛은 마을 사람들 사이에서 인기가 많다."),
            }
        ),

        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("응, 네 빵 냄새는 정말 참을 수가 없어. 오늘도 새벽부터 일했지?")
            }
        ),

        new EmotionChange("Kate", KateEmotionID.ArmCrossed_YeahRight),
        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("당연하지! 오늘은 날씨가 따뜻해서 발효도 잘 됐어. 그래서 더 맛있을걸?"),
                new Line("너라면 바로 알아볼 거지?")
            }
        ),

        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("발효... 그게 이렇게 중요한 줄은 몰랐네. 네가 신경 쓰는 만큼, 빵이 완벽할 수밖에 없겠어.")
            }
        ),

        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("날씨에 따라 빵이 달라지다니, 생각보다 훨씬 섬세한 일이었다."),
                new Line("케이트는 그 모든 걸 완벽하게 신경 쓰고 있다.")
            }
        ),

        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("오늘 빵은 또 어떤 맛일지 궁금하네.")
            }
        ),

        new EmotionChange("Kate", KateEmotionID.ArmCrossed_Energetic),
        new ParallelElement(
            new PlaceShake(.3f),
            new Dialogue(
                "Kate",
                new List<Line>
                {
                    new Line("기대해도 좋아! 오늘은 특별히 속을 더 부드럽게 만들었어. 정말 한 번 먹어보면 그 차이를 알 거야."),
                },
                fadeout: true
            )
        ),
        new ImaginationEnter(
            "Bread"
        ),
        new SFXsClear(),
        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("케이트가 내민 빵은 언제나 특별하다."),
                new Line("소박하지만 설명할 수 없는 깊이가 느껴진다."),
                new Line("한 입 베어 물 때마다 퍼지는 폭신한 식감은 마치 그녀의 정성이 그대로 녹아 있는 듯하다.")
            },
            fadeout: true
        ),
        new ImaginationClear(1f),
        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("정말 부드럽다... 네가 왜 이 빵을 자랑하는지 알겠어.")
            }
        ),
        new EmotionChange("Kate", KateEmotionID.ArmCrossed_Smile),

        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("그치? 자주 와. 네가 맛있게 먹으면 나도 더 힘이 나거든!"),
            }
        ),

        // 추가된 대화
        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("빵도 맛있지만, 네 베이커리 안은 항상 좋은 냄새로 가득 차 있네. 여긴 아침에 제일 바쁘지?")
            }
        ),
        new EmotionChange("Kate", KateEmotionID.OneHandRaised_Shy),
        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("맞아. 아침이면 사람들이 빵 사러 몰려오거든. 정신없이 바빠! 특히 애들이 좋아하는 초콜릿 크루아상은 눈 깜짝할 사이에 동나."),
            }
        ),

        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("크루아상이라... 그럼 내일은 그거 한 번 먹어봐야겠네. 그렇게 인기 많으면 내가 늦기 전에 서둘러야겠어.")
            }
        ),
        new EmotionChange("Kate", KateEmotionID.ArmCrossed_YeahRight),

        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("응, 네가 또 늦잠 자면 못 살지도 몰라!"),
            }
        ),

        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("케이트와 나누는 이런 대화는 일상적이지만, 어쩐지 편안하고 기분 좋은 여운을 남긴다."),
                new Line("마을에서 특별한 일은 많지 않지만, 이런 작은 순간들이 쌓여 지금의 나를 채워 주고 있었다.")
            }
        ),

        new EmotionChange("Kate", KateEmotionID.ArmCrossed_Energetic),
        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("참, 어제 마을 축제 얘기 들었어? 이번엔 좀 크게 준비한다던데.")
            }
        ),

        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("아, 축제 얘긴 들었는데 자세히는 몰라. 뭐하는 축제야?")
            }
        ),

        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("1년에 한 번 하는 수확제 같은 거야. 마을 사람들이 다 모여서 먹고 즐기고, 공연도 하고. 작은 마을이라도 이런 행사는 빠질 수 없지!"),
            }
        ),

        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("그렇구나. 그럼 나도 한 번 가봐야겠네. 사진 찍을 만한 장면도 많을 것 같고.")
            }
        ),

        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("그래! 좋은 사진 많이 찍어서 나중에 나도 보여줘. 다 같이 즐기면 좋잖아!"),
            }
        ),

        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("그래, 축제도 기대되지만 아직 마을을 제대로 다 둘러보진 못했어. 이 작은 골목길들도 참 매력 있더라.")
            }
        ),

        new EmotionChange("Kate", KateEmotionID.ArmCrossed_Smile),
        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("맞아, 마을 중심으로 가면 더 많은 사람들을 만날 수 있을 거야. 특히 분수대 주변이 가장 활기차지."),
            }
        ),

        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("분수대? 거긴 어떤 곳이야?")
            }
        ),

        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("마을 중심이야. 사람들이 자주 모이는 곳이라 산책하거나 이야기를 나누기 좋지. 넌 사진 찍을 만한 장면도 많을 거야."),
            }
        ),

        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("그렇구나. 그럼 오늘은 그쪽으로 가봐야겠다. 아직도 내가 못 본 곳이 많네.")
            }
        ),

        new EmotionChange("Kate", KateEmotionID.ArmCrossed_YeahRight),
        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("천천히 둘러봐. 분수대 근처에서 마을 사람들도 자주 마주칠 거야."),
            }
        ),

        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("케이트의 말을 들으니, 아직 내가 가보지 못한 곳들이 많다는 걸 다시 한 번 깨달았다."),
                new Line("마을 중심 분수대... 그곳에서 더 많은 사람들을 만나고, 이 섬의 분위기를 더 알아갈 수 있을지도 모른다."),
                new Line("오늘은 마을을 천천히 둘러봐야겠다.")
            }
        ),

        new AllCharactersClear(),
    };
}
