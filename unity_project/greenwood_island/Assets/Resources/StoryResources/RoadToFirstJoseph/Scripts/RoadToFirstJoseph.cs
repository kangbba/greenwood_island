using System.Collections.Generic;
using UnityEngine;

public class RoadToFirstJoseph : Story
{
    // FirstKateStory 스토리의 스크립트 로직을 여기에 작성하세요.
    protected override SequentialElement StartElements => new (
        new ScreenOverlayFilm(Color.black),
        new PlaceEnter("SeasideWalkway"),
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
            "Ryan",
            new List<Line>
            {
                new Line("이 길로는 처음 와보네. 마을에 온 지 한 달이 넘었지만, 해변가 쪽은 잘 안 오게 되더라."),
                new Line("날씨도 그렇고, 뭔가 익숙해질 만하면 태풍이 불어오니까…")
            }
        ),

        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("그래, 이쪽은 좀 한적하긴 하지. 대부분 사람들이 마을 중심가 쪽에 모이니까."),
                new Line("하지만 난 가끔 여기 걸어. 바닷바람 맞으면서 생각도 정리되고, 조용해서 좋거든.", "Smile")
            }
        ),

        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("바닷바람이 불어오자 케이트의 말처럼 기분이 차분해지는 느낌이다."),
                new Line("파도 소리가 멀리서 잔잔하게 들려오고, 발밑으로는 고요한 길이 부드럽게 이어진다."),
                new Line("이 길로는 처음 와봤지만, 그동안 왜 안 왔을까 싶을 정도로 평화롭고 고요하다."),
                new Line("한 달 전에는 이런 평온함을 상상도 못 했지. 그땐 이 섬에 오기 전이었으니까."),
                new Line("많은 일이 있었지만, 이제는 이곳이 조금씩 익숙해져 가는 건가...")
            }
        ),

        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("조셉 할아버지, 참 대단한 분이야. 나 어렸을 때부터 저 카페를 운영하셨거든."),
                new Line("지금은 몸이 안 좋으셔서 많이 힘들어 보이시지만, 그래도 여전히 카페에 나와 계셔.", "Concerned"),
                new Line("그분이 해주는 커피는 정말 특별해. 할아버지 손길이 닿으면 모든 게 다르게 느껴져.", "Smile")
            }
        ),

        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("그분이 그렇게 대단하신 분이야? 난 아직 한 번도 본 적이 없는데…"),
                new Line("사람들이 그분 얘기를 많이 하더라. 그 카페도 마을 사람들한테는 굉장히 소중한 장소라고 하던데.")
            }
        ),

        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("응, 한 번쯤 만나보면 좋을 거야. 조셉 할아버지가 하는 얘기는 다 귀담아 들을 만해.", "Smile"),
                new Line("그리고... 네가 만나면 할아버지도 분명 좋아하실 거야. 요즘 외로워 보이시더라고.", "Concerned")
            }
        ),

        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("그래, 한 번 만나봐야겠네. 커피도 궁금하고... 할아버지가 어떤 분인지 직접 알아보면 좋을 것 같아.")
            }
        ),

        new ImaginationEnter(
            "SeaView",
            1.5f,
            Color.white
        ),

        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("멀리서 파도가 부서지는 소리가 들려온다."),
                new Line("짙은 푸른빛의 바다와 그 위로 반짝이는 햇살이 한데 어우러져 있다."),
                new Line("케이트와 나는 해변을 따라 조용히 걸어가고, 머릿속엔 조셉 할아버지에 대한 생각이 가득했다.")
            }
        ),

        new AllImaginationsClear(),

        new AllCharactersClear(1f)
    );


    protected override SequentialElement ExitElements => new (

        new ScreenOverlayFilm(Color.black),
        new StoryTransition(new FirstJosephStory())
    );

    protected override string StoryDesc => "";



}
