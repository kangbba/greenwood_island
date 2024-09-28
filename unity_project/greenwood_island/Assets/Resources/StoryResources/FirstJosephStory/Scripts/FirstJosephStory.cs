using System.Collections.Generic;
using UnityEngine;

public class FirstJosephStory : Story
{
    // FirstJosephStory 스토리의 스크립트 로직을 여기에 작성하세요.
    protected override SequentialElement StartElements => new ();

    protected override SequentialElement UpdateElements => new (

        // 씨브리즈 카페 앞에 도착하는 장면
        new PlaceEnter("CafeSeabreezeFront"), // 씨브리즈 카페 외부로 장소 전환
        new ScreenOverlayFilmClear(),
        new CharacterEnter(
            "Kate",
            .5f,
            "Normal",
            0
        ),

        // 카페 앞에서의 장면
        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("해안가를 따라 조용히 걷다 보니 씨브리즈 카페가 보였다."),
                new Line("바닷바람에 이따금 흔들리는 나무 간판은 오래되었지만, 그 안에서 느껴지는 따뜻함은 여전히 건재했다."),
                new Line("한동안 문이 닫혀 있던 카페는 외부가 다소 황량해 보였지만, 창 너머로 새어 나오는 불빛은 오히려 더 따뜻해 보였다."),
                new Line("케이트는 잠시 망설이다가 문을 두드렸다. 문 뒤에서 작은 떨림이 느껴지는 듯했다.")
            }
        ),

        // 케이트가 문을 두드리는 장면
        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("조셉 할아버지, 저예요! 케이트예요. 들어가도 될까요?")
            }
        ),

        // 긴장감을 유지한 채, 문이 열리는 순간
        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("바람 소리만 들리는 고요한 순간이 지나고, 안에서 느릿하게 발걸음 소리가 들렸다."),
                new Line("덜컥, 문이 열리는 소리가 해안의 조용한 바람에 퍼졌다."),
                new Line("그 안에서 조셉 할아버지가 천천히 모습을 드러냈다.")
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
                new Line("케이트야, 이 시간에 무슨 일이냐?"),
                new Line("아직 가게는 문을 열지도 않았는데...")
            }
        ),

        // 케이트가 안부를 묻는 대사
        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("할아버지, 잘 지내셨어요? 몸은 좀 괜찮아지셨어요?")
            }
        ),

        // 조셉 할아버지의 대답
        new Dialogue(
            "Joseph",
            new List<Line>
            {
                new Line("하하, 많이 나아졌네. 다만, 옛날처럼 다시 팔팔해지긴 어렵겠지."),
                new Line("그래도 이렇게 찾아와 주니 고맙구나.")
            }
        ),

        // 조셉 할아버지가 라이언을 발견하는 장면
        new Dialogue(
            "Joseph",
            new List<Line>
            {
                new Line("그런데... 저 뒤에 있는 사람은 누구냐? 낯선 얼굴이로군.")
            }
        ),

        // 라이언의 자기소개, 직업은 말하지 않고 모호하게
        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("안녕하세요. 처음 뵙겠습니다. 저는 라이언입니다. 케이트와 함께 왔습니다.")
            }
        ),

        // 조셉 할아버지가 라이언을 흥미롭게 바라보는 장면
        new Dialogue(
            "Joseph",
            new List<Line>
            {
                new Line("그래, 케이트와 함께라면 믿을 만한 사람이겠지."),
                new Line("얼굴을 보니 뭔가 사연이 많아 보이는구나. 이곳엔 무슨 일로 온 건가?")
            }
        ),

        // 라이언이 답을 회피하며
        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("그저... 여러 가지로 알아보러 온 거죠. 마을을 둘러보는 중입니다.")
            }
        ),

        // 조셉 할아버지가 차를 대접하겠다고 제안
        new Dialogue(
            "Joseph",
            new List<Line>
            {
                new Line("흠, 마을을 둘러본다라... 그렇군. 그럼, 차라도 한 잔 하면서 이야기를 나눠 보세."),
                new Line("가게는 아직 닫았지만, 안으로 들어와. 따뜻한 차 한 잔 정도는 괜찮겠지.")
            }
        ),

        // 케이트의 긍정적인 반응
        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("할아버지 차는 언제나 특별하니까요. 그럼, 우리 안으로 들어가요."),
                new Line("라이언, 할아버지 차를 마셔보면 당신도 이 마을이 좀 더 친숙하게 느껴질 거예요.")
            }
        ),

        // 카페 안으로 들어가는 장면
        new ScreenOverlayFilm(Color.black, 1f),
        new PlaceEnter("CafeSeabreezeInside"),
        new ScreenOverlayFilmClear(1f),

        // 카페 안에서의 대화 장면
        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("씨브리즈 카페 안은 오래된 나무 향과 따뜻한 차 냄새가 가득했다."),
                new Line("작은 공간이지만, 그 안에선 무언가 오래된 시간들이 숨쉬고 있는 듯했다."),
                new Line("조셉 할아버지는 조용히 주전자를 꺼내 물을 데우며 나를 한 번 흘긋 보았다."),
                new Line("그의 시선은 나를 관찰하는 듯하면서도, 무언가를 읽어내려는 것 같았다.")
            }
        ),

        // 조셉 할아버지가 차를 준비하며 대화를 이어가는 장면
        new Dialogue(
            "Joseph",
            new List<Line>
            {
                new Line("마을은 조용하지 않나? 이런 시골에서는 시끌벅적한 일도 없을 텐데..."),
                new Line("그런데 너 같은 사람이 이곳에 온 걸 보면, 뭔가 목적이 있는 게겠지.")
            }
        ),

        // 라이언이 어색하게 반응하는 장면
        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("그냥... 여유를 찾고 싶었을 뿐입니다. 마을이 꽤 평온해서요."),
                new Line("솔직히 말하면 아직 이곳이 조금 낯설기도 하고요.")
            }
        ),

        // 조셉 할아버지가 가볍게 웃으며 반응하는 장면
        new Dialogue(
            "Joseph",
            new List<Line>
            {
                new Line("하하, 낯설다라... 누구나 처음엔 다 그런 법이지."),
                new Line("하지만 곧 익숙해질 거다. 이 마을은... 사람을 금방 품어주거든.")
            }
        )
    );

    protected override SequentialElement ExitElements => new ();

    protected override string StoryDesc => "";
}
