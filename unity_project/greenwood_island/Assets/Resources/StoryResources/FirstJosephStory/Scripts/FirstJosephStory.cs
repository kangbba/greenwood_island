using System.Collections.Generic;
using UnityEngine;

public class FirstJosephStory : Story
{
    // FirstKateStory 스토리의 스크립트 로직을 여기에 작성하세요.
    protected override SequentialElement StartElements => new (
        new ScreenOverlayFilm(Color.black),
        new PlaceEnter("SeaSidewalk"),
        new CameraZoomByFactor(zoomFactor: 0.3f, duration: 0f),
        new CameraMove2DByAngle(-80, 160f, duration: 0f)
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
            .5f,
            "Normal",
            0
        ),
        // 라이언이 처음 걸어보는 해변가 길에 대한 감상
        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("이 길로는 처음 와보네. 마을에 온 지 한 달이 넘었지만, 해변가 쪽은 잘 안 오게 되더라."),
                new Line("날씨도 그렇고, 뭔가 익숙해질 만하면 태풍이 불어오니까…"),
            }
        ),

        // 케이트의 답변
        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("그래, 이쪽은 좀 한적하긴 하지. 대부분 사람들이 마을 중심가 쪽에 모이니까."),
                new Line("하지만 난 가끔 여기 걸어. 바닷바람 맞으면서 생각도 정리되고, 조용해서 좋거든.", "Smile"),
            }
        ),

        // 라이언의 독백
        new Dialogue(
            "",
            new List<Line>
            {
                new Line("바닷바람이 불어오자 케이트의 말처럼 기분이 차분해지는 느낌이다."),
                new Line("파도 소리가 귓가에 울리고, 발밑으로는 모래가 부드럽게 미끄러진다."),
                new Line("이 길로는 처음 와봤지만, 그동안 왜 안 왔을까 싶을 정도로 평화롭고 고요하다."),
            }
        ),

        // 케이트가 조셉 할아버지에 대한 설명을 이어감
        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("조셉 할아버지, 참 대단한 분이야. 나 어렸을 때부터 저 카페를 운영하셨거든."),
                new Line("지금은 몸이 안 좋으셔서 많이 힘들어 보이시지만, 그래도 여전히 카페에 나와 계셔.", "Concerned"),
                new Line("그분이 해주는 커피는 정말 특별해. 할아버지 손길이 닿으면 모든 게 다르게 느껴져.", "Smile"),
            }
        ),

        // 라이언이 조셉 할아버지에 대해 궁금증을 품음
        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("그분이 그렇게 대단하신 분이야? 난 아직 한 번도 본 적이 없는데…"),
                new Line("사람들이 그분 얘기를 많이 하더라. 그 카페도 마을 사람들한테는 굉장히 소중한 장소라고 하던데."),
            }
        ),

        // 케이트가 조셉 할아버지를 만나면 좋을 거라고 설득하는 대사
        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("응, 한 번쯤 만나보면 좋을 거야. 조셉 할아버지가 하는 얘기는 다 귀담아 들을 만해.", "Smile"),
                new Line("그리고... 네가 만나면 할아버지도 분명 좋아하실 거야. 요즘 외로워 보이시더라고.", "Concerned"),
            }
        ),

        // 라이언이 고개를 끄덕이며 동의함
        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("그래, 한 번 만나봐야겠네. 커피도 궁금하고... 할아버지가 어떤 분인지 직접 알아보면 좋을 것 같아."),
            }
        ),

        // 바다 풍경을 강조하는 연출
        new ImaginationEnter(
            "SeaView",
            1f,
            Color.white
        ),
        new Dialogue(
            "",
            new List<Line>
            {
                new Line("멀리서 파도가 부서지는 소리가 들려온다."),
                new Line("짙은 푸른빛의 바다와 그 위로 반짝이는 햇살이 한데 어우러져 있다."),
                new Line("케이트와 나는 해변을 따라 조용히 걸어가고, 머릿속엔 조셉 할아버지에 대한 생각이 가득했다."),
            }
        ),

        new ImaginationClear(),

        new AllCharactersClear(1f),
        
        // 씨브리즈 카페 앞에 도착하는 장면
        new PlaceEnter("SeabreezeCafeFront"), // 씨브리즈 카페 외부로 장소 전환
        new ScreenOverlayFilmClear(),
        new CharacterEnter(
            "Kate",
            .5f,
            "Normal",
            0
        ),

        // 카페 앞에서의 장면
        new Dialogue(
            "",
            new List<Line>
            {
                new Line("조셉 할아버지의 카페, 씨브리즈는 해안가에 자리 잡고 있었다."),
                new Line("오래된 나무로 지어진 이 카페는 소박하지만 따뜻한 분위기를 자아냈다."),
                new Line("한 달 넘게 문을 닫아둔 탓인지, 밖은 한적했지만 창문 너머로는 불빛이 따뜻하게 비쳤다."),
            }
        ),

        // 케이트가 문을 두드리는 장면
        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("조셉 할아버지, 저예요! 케이트."),
            }
        ),
        new Dialogue(
            "",
            new List<Line>
            {
                new Line("안에서 무거운 발걸음 소리가 들리더니, 문이 천천히 열렸다."),
            }
        ),

        new ParallelElement(
            new CharacterMove(
                "Kate",
                .33f
            ),
            // 조셉 할아버지의 첫 대사
            new CharacterEnter("Joseph", .66f, "Normal", 0)
        ),

        new Dialogue(
            "Joseph",
            new List<Line>
            {
                new Line("케이트? 이 시간에 무슨 일인가?")
            }
        ),

        // 케이트가 안부를 묻는 대사
        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("조셉 할아버지, 잘 지내셨죠? 몸은 좀 괜찮으세요?")
            }
        ),

        // 조셉 할아버지의 대답
        new Dialogue(
            "Joseph",
            new List<Line>
            {
                new Line("응, 많이 나아졌네. 그래도 아직 완전히 회복된 건 아니야."),
            }
        ),

        // 조셉 할아버지가 라이언을 발견하는 장면
        new Dialogue(
            "Joseph",
            new List<Line>
            {
                new Line("그리고... 이 사람은?")
            }
        ),

        // 라이언이 자기소개
        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("안녕하세요. 라이언입니다. 신문사에서 일하고 있어요. 처음 뵙겠습니다.")
            }
        ),

        // 조셉 할아버지가 라이언을 흥미롭게 바라보는 장면
        new Dialogue(
            "Joseph",
            new List<Line>
            {
                new Line("신문사에서 왔다고? 흥미롭군."),
                new Line("케이트가 데리고 올 만한 사람이니 믿을 만하겠지.")
            }
        ),

        // 조셉 할아버지가 카페로 초대하는 대사
        new Dialogue(
            "Joseph",
            new List<Line>
            {
                new Line("카페는 아직 문을 열진 않았지만, 안에서 차라도 한 잔 하며 이야기 나누세.")
            }
        ),

        // 케이트의 긍정적인 반응
        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("그럼, 우리 안으로 들어가요!")
            }
        ),

        // 모두가 카페 안으로 들어가는 장면
        new PlaceEnter("SeabreezeCafeInterior"),
        new ScreenOverlayFilmClear(1f),

        // 카페 안에서의 대화 장면으로 연결
        new Dialogue(
            "",
            new List<Line>
            {
                new Line("씨브리즈 카페 안은 오래된 나무 가구들로 아늑한 분위기를 자아내고 있었다."),
                new Line("조셉 할아버지는 주전자를 꺼내 차를 준비하며 나를 흥미롭게 바라보았다."),
            }
        )
    );


    protected override SequentialElement ExitElements => new (

        new ScreenOverlayFilm(Color.black)
    );

    protected override string StoryDesc => "";



}
