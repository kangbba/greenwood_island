using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OpeningStory : Story
{
    protected override string StoryDesc => "그린우드 섬으로 향하는 항해 중, 배 안의 불안한 분위기와 신비한 인물들이 점차 밝혀지는 이야기.";

    // 시작 부분의 요소들
    protected override SequentialElement StartElements => new
    (
        new ScreenOverlayFilm(Color.black),
        new PlaceEnter("Storm"),
        new CameraZoomByFactor(zoomFactor: 0.2f, duration: 0f),
        new CameraMove2DByAngle(-290, 160f, duration: 0f),
        new PlaceFilm(ColorUtils.CustomColor("86D8FF"), .3f),
        new Intertitle("이 이야기는 허구이며,\n실제 인물, 장소, 사건과는 무관합니다.", 1, 3, 1)
    );

    // 스토리의 메인 업데이트 부분
    protected override SequentialElement UpdateElements => new
    (
        new SequentialElement(
            new SFXEnter("Thunder1", 0.25f, false, 0f)
        ),
        new ParallelElement(
            new CameraShake(),
            new ScreenOverlayFilmClear(),
            new CameraZoomClear(1f),
            new CameraMove2DClear(4f)
        ),
        new SequentialElement(
            new SFXEnter("Thunder2", 0.15f, false, 0f)
        ),
        new ParallelElement(
            new CameraShake(),
            new SequentialElement(
                new PlaceOverlayFilm(Color.white.ModifiedAlpha(0.76f), .05f, Ease.OutElastic),
                new PlaceOverlayFilmClear(.1f)
            ),
            new SequentialElement(
                new PlaceFilmClear(.15f, Ease.OutElastic),
                new PlaceFilm(ColorUtils.CustomColor("86D8FF"), .2f)
            )
        ),
        new SFXEnter("Wind1", 0.15f, true, 0f),
        new SFXEnter("Thunder1", 0.15f, true, 5f),
        new CameraZoomClear(),
        new CameraMove2DClear(),
        new ScreenOverlayFilm(Color.white, .5f),
        new PlaceEnter("FerryInside"),
        new CameraZoomByFactor(.5f, 0f),
        new ScreenOverlayFilmClear(),
        new CameraMove2D(Vector2.right * 300, 3f),

        // 라디오의 불길한 소리
        new Dialogue(
            "라디오",
            new List<Line>
            {
                new Line("…치지직, 북위 48도… 동경… 관찰 중… 주시…"),
            }
        ),
        new CameraShake(),

        // 라이언의 독백
        new Dialogue(
            "",
            new List<Line>
            {
                new Line("배는 거친 파도에 휘청거리고, 바깥에서는 빗방울이 떨어지며 윈드실드에 부딪힌다."),
                new Line("창문 너머로 보이는 하늘은 회색으로 뒤덮여 있고, 두려움이 나를 감싸기 시작했다."),
                new Line("애써 의연한 척 해보려 고개를 빼어 이 작은 여객선을 둘러본다."),
                new Line("어두운 분위기, 이 작은 배에는 나 외에 몇 명밖에 없다."),
                new Line("몇 명의 승객들이 고개를 떨군 채 침묵하고 있다. 내가 기댈 만한 사람이라고는 없어 보인다."),
            }
        ),
        new CameraShake(),

        new ImaginationEnter(
            "FerryOldMan",
            1f,
            ColorUtils.CustomColor("516FB7")
        ),
        new Dialogue(
            "",
            new List<Line>
            {
                new Line("한 노인은 선실 구석에 앉아, 아무 말 없이 파도만 바라보고 있다."),
                new Line("흔들리는 배에도 두려움이 없어보인다. 그렇다고 강인한 모습은 아닌 것 같다."),
                new Line("바다만 바라보며 그저 혼잣말을 반복할 뿐이었다"),
            }
        ),

        // 신사와 부인의 대화
        new Dialogue(
            "노인",
            new List<Line>
            {
                new Line("바다가… 다 기억해… 내가 본 것들… 물속에 잠겼어… 저 깊은 곳에…"),
            }
        ),
        new CameraShake(),
        new CameraShake(3),
        new ImaginationClear(),
        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("나는 그저 이 배를 흔드는 태풍이 조금 잠잠해지기를 바랄 뿐이다."),
            }
        ),
        // 라디오의 음성
        new Dialogue(
            "라디오",
            new List<Line>
            {
                new Line("경로… 접근 중… 보고… 대기…"),
            }
        ),
        new ParallelElement(
            new PlaceFilm(Color.clear, 1f),
            new PlaceEnter("ShipSide"),
            new PlaceFilm(Color.clear, 0f),
            new CameraMove2D(Vector2.right * -497f, 0f),
            new PlaceFilmClear(1f)
        ),

        new ParallelElement(
            new Dialogue(
                "",
                new List<Line>
                {
                    new Line("배는 점차 안정을 찾고, 안개가 서서히 걷히며 그린우드 섬의 고요한 전경이 모습을 드러낸다."),
                    new Line("평화로운 해안과 오래된 건물들이 마치 시간이 멈춘 듯 라이언의 눈앞에 펼쳐진다."),
                    new Line("방금 전의 긴장은 거짓말처럼 사라지지만, 남은 여운은 가시지 않는다."),
                    new Line("섬에 도착했지만, 어딘지 모르게 편치 않다."),
                    new Line("이곳이 정말 평범한 곳일까? 교신 속 불길한 목소리가 귓가에 남아 맴돈다."),
                }
            ),
            new CameraMove2D(Vector2.right * 187, 5f),
            new ScreenOverlayFilmClear(2f)
        )

    );

    // 종료 부분의 요소들
    protected override SequentialElement ExitElements => new
    (
        new StoryTransition("FirstKateStory")
    );
}
