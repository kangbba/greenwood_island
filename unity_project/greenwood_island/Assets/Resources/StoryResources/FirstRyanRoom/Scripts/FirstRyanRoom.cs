using UnityEngine;
using System.Collections.Generic;

public class FirstRyanRoom : Story
{
    public override List<Element> UpdateElements => new List<Element>()
    {
        new PlaceTransition(
            new PlaceEnter(
                "RyanRoom",
                initialLocalScale:
                    Vector2.one * 1.2f,
                initialColor:
                    ColorUtils.CustomColor("58308E")
            ),
            Color.black,
            placeEffects:
            new List<PlaceEffect>(){
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
            }
        ),

        new PlaceTransition(
            new PlaceEnter(
                "RyanRoomCeiling",
                initialColor:
                Color.gray,
                initialLocalScale:
                Vector2.one * 1.5f
            ),
            Color.black,
            placeEffects:
            new List<PlaceEffect>(){
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
        ), // 라이언이 침대에 누워 쉬는 장면

        new ParallelElement(
            // 잠에 드는 장면
            new ImaginationEnter(
                "Black",
                2f,
                color:
                Color.black.ModifiedAlpha(.5f)
            ),
            new Dialogue(
                "Mono",
                new List<Line>()
                {
                    new Line("눈이 점점 무거워진다. 내일이면 다 알 수 있겠지."),
                    new Line("뭐, 일단 지금은 다 잊고 푹 자는게 좋겠어.")
                }
            )
        ),
        // 잠에 드는 장면
        new ScreenOverlayFilm(
            Color.black,
            2f
        )
    };
}
