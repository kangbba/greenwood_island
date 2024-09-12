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
                new Line("케이트의 웃음과 빵 냄새가 가득한 이 마을에서, 오늘도 하루가 이렇게 흘러간다."),
            }
        )
    );

    protected override SequentialElement ExitElements => new ();

    protected override string StoryDesc => "";
}
