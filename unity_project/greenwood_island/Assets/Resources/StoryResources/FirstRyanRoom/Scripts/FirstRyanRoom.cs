using UnityEngine;
using System.Collections.Generic;

public class FirstRyanRoom : Story
{
    public override List<Element> UpdateElements => new List<Element>()
    {
        new PlaceTransition(
            new PlaceEnter(
                "RyanRoom",
                initialLocalScale: Vector2.one * 1.2f,
                initialColor: ColorUtils.CustomColor("58308E")
            ),
            Color.black,
            placeEffects: new List<PlaceEffect>(){
                new PlaceEffect(PlaceEffect.EffectType.ScaleRestore, duration : 1f),
            }
        ),
        // 라이언이 집에 들어와서 생각 없이 방에 들어가는 장면
        new Dialogue(
            "Mono",
            new List<Line>()
            {
                new Line("집에 들어오자 조용하고 아늑한 공기가 감돈다."),
                new Line("문을 닫고 가만히 서 있으니, 오늘 하루가 한꺼번에 밀려오는 것 같다."),
                new Line("평소엔 이 마을이 너무 조용해서 심심할 정도였는데..."),
                new Line("오늘은 그 고요함이 오히려 어딘가 낯설게 느껴진다. 묘한 기분이다.")
            }
        ),

        // 방 안에서 느끼는 평온한 순간
        new Dialogue(
            "Mono",
            new List<Line>()
            {
                new Line("방 안은 여전히 정돈되어 있다. 마을에 있었던 소란과는 대조적이다."),
                new Line("침대에 몸을 던지고 싶어진다. 오늘 꽤 바빴지."),
                new Line("그러고 보니... 케이트는 어디 간 걸까?")
            }
        ),

        // 케이트에 대한 자연스러운 생각
        new Dialogue(
            "Mono",
            new List<Line>()
            {
                new Line("뭐, 별일 없겠지. 잠깐 어디 다녀왔을 거다."),
                new Line("내일 돌아오면 물어보면 된다.")
            },
            fadeout: true
        ),

        new PlaceTransition(
            new PlaceEnter(
                "RyanRoomCeiling",
                initialColor: Color.gray,
                initialLocalScale: Vector2.one * 1.5f
            ),
            Color.black,
            placeEffects: new List<PlaceEffect>(){
                new PlaceEffect(PlaceEffect.EffectType.ScaleRestore, duration : 3f),
            }
        ),  

        new Dialogue(
            "Mono",
            new List<Line>()
            {
                new Line("침대에 누우니 온몸의 긴장이 풀린다. 이제 좀 쉬어야겠다."),
                new Line("오늘은 꽤 재미있었고, 내일은 또 어떤 일이 있을지 궁금하다."),
                new Line("케이트도 내일이면 다시 볼 수 있겠지. 케이트에게 아말리안님에 관해서도 더 물어보는 게 좋겠다."),
            }
        ),

        // 성녀 아말리안에 대한 자연스러운 생각
        new Dialogue(
            "Mono",
            new List<Line>()
            {
                new Line("그리고... 아말리안님 이라... 참 신비한 사람이었다."),
                new Line("어린 소녀 같은데도 묘하게 압도적인 느낌이 있었다."),
                new Line("아직은 느낌 뿐이지만, 마을 사람들이 왜 그렇게 존경하는지 조금은 알 것 같다."),
                new Line("완전히 이해할 순 없어도 대단한 인물임은 분명하다.")
            }
        ),

        new ParallelElement(
            new ImaginationEnter(
                "Black",
                2f,
                color: Color.black.ModifiedAlpha(.5f)
            ),
            new Dialogue(
                "Mono",
                new List<Line>()
                {
                    new Line("눈이 점점 무거워진다. 내일이면 다 알 수 있겠지."),
                    new Line("뭐, 일단 지금은 다 잊고 푹 자는게 좋겠어.")
                },
                fadeout: true
            )
        ),

        // 다음 날 아침
        new ScreenOverlayFilm(
            Color.black,
            2f
        ),
        new Delay(2f),

        new ImaginationClear(0f),
        new ScreenOverlayFilmClear(1f),

        new PlaceColor(
            targetColor: Color.white,
            1f
        ),

        new Dialogue(
            "Mono",
            new List<Line>()
            {
                new Line("으.. 으음..."),
                new Line("별일 없을 거라 생각하면서도 마음 한 구석엔 어제 케이트가 갑자기 사라진 게 걸린다."),
            }
        ),
        
        new PlaceTransition(
            new PlaceEnter("BakeryFront"),
            Color.black,
            placeEffects: new List<PlaceEffect>(){
                new PlaceEffect(PlaceEffect.EffectType.FadeIn, duration: 1.5f)
            }
        ),

        new Dialogue(
            "Mono",
            new List<Line>()
            {
                new Line("베이커리에 도착했다. 여느 때처럼 케이트가 가게 안에서 빵을 정리하고 있다."),
                new Line("다행이다... 정말 아무 일도 없었나 보다.")
            }
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
                new Line("어, 라이언! 잘 잤어? 오늘도 빵이 정말 잘 나왔어. 혹시 배고프면 말해, 내가 특별히 챙겨줄게."),
            }
        ),

        // 라이언이 걱정된 마음을 숨기며 대화를 이어가는 장면
        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("응, 잘 잤어. 근데... 어제 너 어디 갔었어? 괜히 걱정했잖아."),
            }
        ),

        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("어제? 아, 잠깐 볼일 좀 봤어. 설마 내가 없어서 불안했어?"),
                new Line("내가 그렇게 중요한 사람인가 봐?")
            }
        ),

        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("하하, 아냐. 그냥 네가 갑자기 없으니까 좀 이상하더라고."),
                new Line("별거 아니긴 했는데... 솔직히 말하면, 좀 신경 쓰였어."),
            }
        ),

        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("걱정해준 건 고마운데 말이야... 내가 무슨 모험이라도 떠난 줄 알았어?"),
                new Line("난 여기 베이커리에 뿌리내리고 있는 사람이야, 떠날 일은 없을 거니까 안심해."),
            }
        ),

        new Dialogue(
            "Ryan",
            new List<Line>()
            {
                new Line("그래, 뭐 그렇겠지. 괜히 내가 오버한 것 같아."),
            }
        ),

        // 라이언이 성녀 아말리안에 대해 물어보는 장면
        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("근데, 어제 마을 분수대에서 성녀 아말리안을 봤어. 사람들이 엄청 경외하는 분위기더라."),
                new Line("어린 소녀 같은데도 묘하게 압도적인 느낌이었어. 너도 그분을 본 적 있어?")
            }
        ),

        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("아말리안님... 그분은 우리 마을의 상징이야. 모습을 자주 드러내시는 분이아닌데, 운이 좋구나?"),
                new Line("모두가 그분 덕에 이 마을이 안전하다고 믿고 있어. 뭐, 그런 점에서 존경받을 수밖에 없지.")
            }
        ),

        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("그렇구나. 마을 사람들이 왜 그렇게 존경하는지 이제 조금 알 것 같아."),
                new Line("어린 소녀 같은데도, 묘하게 그분을 보면 압도당하는 기분이 들었어.")
            }
        ),

        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("응, 많은 사람들이 그렇게 느껴. 신비로운 분이시지. 하지만..."),
                new Line("가까이 다가가는 건 쉽지 않아. 그분은 우리와 좀 다른 세계에 계신 것 같거든.")
            }
        ),

        new Dialogue(
            "Mono",
            new List<Line>()
            {
                new Line("케이트의 말 속에서 무언가 알 수 없는 감정이 느껴졌다."),
                new Line("그녀도 성녀 아말리안에 대해 단순히 존경만 하는 건 아닌 것 같다."),
                new Line("하지만 더 이상 물어보는 건 그만둬야겠다고 생각했다. 어차피 오늘은 평화로운 하루가 될 테니까.")
            }
        ),

        // 대화 종료 후 베이커리의 평온함을 묘사
        new Dialogue(
            "Mono",
            new List<Line>()
            {
                new Line("빵 냄새가 가게 안을 가득 채웠고, 케이트는 평소처럼 활기차게 일을 하고 있다."),
                new Line("오늘은 그냥 이 평온함을 즐기기로 했다.")
            }
        )
    };
}
