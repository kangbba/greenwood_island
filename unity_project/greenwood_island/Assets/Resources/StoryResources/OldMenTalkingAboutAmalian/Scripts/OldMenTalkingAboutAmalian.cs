using UnityEngine;
using System.Collections.Generic;

public class OldMenTalkingAboutAmalian : Story
{

    public override List<Element> UpdateElements => new List<Element>()
    {
        
        new ScreenOverlayFilm(Color.black, 0f),

        new ImaginationEnter(
            "ThreeOldMen",
            0f
        ),

        new ScreenOverlayFilmClear(),
        
        new Dialogue(
            "Mono",
            new List<Line>()
            {
                new Line("마을 노인들이 모여 이야기를 나누고 있었다."),
                new Line("멀리서 들려오는 대화 소리가 점점 귀에 들어왔다."),
                new Line("이상하게 평온한 그들의 목소리와는 달리, 뭔가 꺼림칙한 기분이 들었다.")
            }
        ),

        new Dialogue(
            "OldMan1",
            new List<Line>()
            {
                new Line("이번 축제도 아무 일 없이 끝났군. 덕분에 한동안은 마을이 무사할 거야."),
                new Line("아말리안님께서 오랜만에 다녀가셨으니, 이번에도 또 한 해는 평안하겠지.")
            }
        ),

        new Dialogue(
            "Mono",
            new List<Line>()
            {
                new Line("축제가 끝났다고? 어떤 축제지? 내가 놓친 게 있나?"),
                new Line("이상하게 느껴졌지만, 그냥 지나가는 마을 행사일지도 모른다고 넘겼다.")
            }
        ),

        new Dialogue(
            "OldMan2",
            new List<Line>()
            {
                new Line("그래, 아말리안님이 계시니까 걱정 없어. 뭐, 이번엔 누가 선정됐지?")
            }
        ),


        new Dialogue(
            "OldMan3",
            new List<Line>()
            {
                new Line("응, 그 집 애 말이야. 그... 뭐지? 그 막내 딸 말이야. 키도 크지 않고 조용했던 애 있잖아."),
                new Line("아, 맞다, 그 애가 이번에 선정됐더군. 이번엔 참 쉽게 끝났어.")
            }
        ),
        new Dialogue(
            "Mono",
            new List<Line>()
            {
                new Line("막내 딸? 선정됐다고? 무슨 말이지..."),
                new Line("뭔가 중요한 이야기를 놓치고 있는 것 같았다. 그들의 말은 너무 자연스러웠다."),
                new Line("혹시... 그들은 무슨 연극 대본이라도 읽는 걸까? 아니, 그럴 리가 없다."),
                new Line("가슴이 서늘해졌다. 뭔가 잘못된 것 같다는 생각이 들었다."),
            }
        ),

        new Dialogue(
            "OldMan1",
            new List<Line>()
            {
                new Line("음... 그 집 애였구만. 뭐, 덕분에 마을이 구원받았으니, 이제 한동안은 안심이야."),
                new Line("희생이 없었으면 이번에도 큰일 날 뻔했어.")
            }
        ),

        new Dialogue(
            "OldMan2",
            new List<Line>()
            {
                new Line("그렇지, 이번에도 무사히 넘어간 거야. 태풍이 이렇게 끝난 것만 해도 다행이지."),
                new Line("사실 그때 바람 소리가 얼마나 끔찍했는지 아직도 생각난다니까.")
            }
        ),

        new Dialogue(
            "Mono",
            new List<Line>()
            {
                new Line("태풍... 아, 한 달 전에 그 태풍."),
                new Line("그때 내가 겪었던 그 끔찍한 바람과 파도, 이 마을 주민들에게도 큰 일이었겠지."),
            }
        ),

        new Dialogue(
            "OldMan3",
            new List<Line>()
            {
                new Line("그래도 아말리안님이 신전에서 의식을 치르셨잖아. 그 덕분에 이번엔 평온하게 끝난 거지."),
                new Line("신을 위한 선물이 있어야 하는 법이니까. 그게 이 마을을 지키는 방법이지 않나?")
            }
        ),

        new ImaginationInvertEffect(true),

        new ImaginationShake(.3f),

        new Dialogue(
            "Mono",
            new List<Line>()
            {
                new Line("신을 위한 선물...?"),
                new Line("그들의 말이 머릿속에서 뒤엉켰다. 무슨 이야기를 하고 있는 거지?"),
                new Line("선정된 애... 희생... 선물...? 대체 이들이 무슨 말을 하고 있는 거야?")
            }
        ),

        new Dialogue(
            "OldMan1",
            new List<Line>()
            {
                new Line("맞아, 항상 누군가는 선정돼야 하지. 이번엔 그 애였던 거고... 그 집에선 받아들였겠지."),
                new Line("하늘이 선택하신 건데, 영광으로 받아들여야 해.")
            }
        ),

        new Dialogue(
            "Mono",
            new List<Line>()
            {
                new Line("지금 내가 들은 게 맞을까? '선정'이라니, '희생'이라니... 이게 진짜로 있었던 일인가?"),
                new Line("아니, 그럴 리가 없다. 이건 무슨 끔찍한 연극의 한 장면일지도 모른다."),
                new Line("하지만 그들의 표정은 너무나도 진지했다."),
                new Line("가슴이 점점 더 답답해졌다. 정말... 무슨 일이 일어난 거지?")
            }
        )
    };
}
