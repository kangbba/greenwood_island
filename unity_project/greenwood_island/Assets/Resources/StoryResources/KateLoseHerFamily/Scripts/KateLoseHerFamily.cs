using UnityEngine;
using System.Collections.Generic;

public class KateLoseHerFamily : Story
{
    public override List<Element> UpdateElements => new List<Element>()
    {
        // 플래시백 시작
        new ScreenOverlayFilm(Color.black),
        new ScreenOverlayFilmClear(1f),

        new VignetteEnter(Color.white.ModifiedAlpha(.5f), 1f),

        // 신전 내부로 들어가는 장면
        new PlaceTransition(
            new PlaceEnter("RuinEnterance"),
            Color.black,
            new PlaceEffect(PlaceEffect.EffectType.ScaleZoom, duration: 1.5f, strength: 1.1f)
        ),

        new Dialogue(
            "KateYoung",
            new List<Line>
            {
                new Line("엄마, 아빠! 저기 봐요! 여기 무슨 비밀이 있는 것 같아요!"),
            }
        ),

        new Dialogue(
            "KateMother",
            new List<Line>
            {
                new Line("케이트! 너무 빨리 달리면 위험해. 조심해, 이곳은 우리가 모르는 게 많단다."),
            }
        ),

        new Dialogue(
            "KateFather",
            new List<Line>
            {
                new Line("맞아, 천천히 가자. 다칠 수 있어."),
            }
        ),

        new Dialogue(
            "KateYoung",
            new List<Line>
            {
                new Line("괜찮아요! 나 다칠 리 없어요! 여기가 엄청 멋져요!"),
            }
        ),

        new PlaceTransition(
            new PlaceEnter("RuinField"),
            Color.black,
            new PlaceEffect(PlaceEffect.EffectType.ScaleZoom, duration: 1.5f, strength: 1.1f)
        ),

        // 케이트가 신전 안쪽으로 달려가는 장면
        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("케이트는 부모님을 앞서 신전 깊은 곳으로 달려갔다. 무언가 특별한 것을 찾으려는 듯, 호기심에 가득 차 있었다."),
            }
        ),

        // 신전의 공기가 무거워지는 장면
        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("그러나 신전은 점점 더 어두워졌고, 공기는 무겁게 느껴졌다."),
                new Line("케이트는 갑자기 무언가 이상한 기운을 느끼고 걸음을 멈췄다."),
            }
        ),

        // 케이트가 뒤돌아보며 부모님이 아말리안과 마주친 장면 목격
        new Dialogue(
            "KateYoung",
            new List<Line>
            {
                new Line("엄마, 아빠...?")
            }
        ),

        new PlaceTransition(
            new PlaceEnter("Ruin"),
            Color.black,
            new PlaceEffect(PlaceEffect.EffectType.ScaleZoom, duration: 1.5f, strength: 1.1f)
        ),


        new CharacterEnter(
            "Amalian",
            AmalianEmotionID.HandsTogether_Smile,
            .33f
        ),
        new CharacterEnter(
            "Eldra",
            CommonEmotionID.Default,
            .66f
        ),

        // 성녀 아말리안과 엘드라가 부모님과 마주하는 장면
        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("케이트는 부모님이 신전 깊숙한 곳에서 두 낯선 이와 마주하고 있는 장면을 멀리서 지켜보았다."),
                new Line("그들은 성녀 아말리안과 그녀의 수행자 엘드라였다."),
            }
        ),

        new Dialogue(
            "Amalian",
            new List<Line>
            {
                new Line("여긴 두 분이 있을 곳이 아닙니다."),
            }
        ),

        new Dialogue(
            "KateMother",
            new List<Line>
            {
                new Line("우린 그저 이곳의 구조를 살펴보러 왔을 뿐이에요. 아무도 방해할 생각은 없어요."),
            }
        ),

        new Dialogue(
            "KateFather",
            new List<Line>
            {
                new Line("맞습니다. 저희는 둘이서만 왔습니다. 아이는 없어요. 그냥 길을 잃었을 뿐입니다."),
            }
        ),

        // 성녀 아말리안이 부모님의 거짓말을 지적하지 않으며 냉정하게 대처
        new Dialogue(
            "Amalian",
            new List<Line>
            {
                new Line("두 분이 숨기려는 건 아무 의미 없습니다. 이 섬에서 일어나는 모든 일은 제 시야를 벗어나지 않습니다."),
            }
        ),

        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("부모님은 케이트를 지키기 위해 끝까지 그녀를 숨기려 했지만, 아말리안은 이미 모든 것을 알고 있었다."),
            }
        ),

        // 부모님이 케이트를 보호하려 하지만 더 이상 방법이 없음
        new Dialogue(
            "KateFather",
            new List<Line>
            {
                new Line("우리가 잘못했습니다... 그냥 돌아가게 해주세요."),
            }
        ),

        new Dialogue(
            "Amalian",
            new List<Line>
            {
                new Line("이미 선택은 끝났습니다. 돌아갈 길은 없습니다."),
            }
        ),


        // 엘드라가 부모님에게 다가가며 결말이 다가옴
        new Dialogue(
            "Eldra",
            new List<Line>
            {
                new Line("그들은 이미 선택의 대가를 치를 준비가 된 것 같습니다."),
            }
        ),

        new FXEnter("BloodDrip"),
        new Dialogue(
            "KateYoung",
            new List<Line>
            {
                new Line("엄마... 아빠...")
            }
        ),

        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("엘드라가 손을 뻗자 부모님은 힘없이 쓰러졌다. 케이트는 그 광경을 멀리서 지켜보며 얼어붙었다."),
            }
        ),

        // 성녀 아말리안이 마지막으로 냉정한 말을 남기며 퇴장
        new Dialogue(
            "Amalian",
            new List<Line>
            {
                new Line("섬의 법을 어긴 자에게는 언제나 똑같은 결말이 따릅니다."),
            }
        ),

        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("케이트는 부모님의 시신을 바라보며 눈물을 흘렸다. 그러나 성녀 아말리안과 엘드라는 아무런 말 없이 그 자리를 떠났다."),
            }
        ),

        // 플래시백 종료
        new ScreenOverlayFilm(Color.black, 1.5f),
    };
}
