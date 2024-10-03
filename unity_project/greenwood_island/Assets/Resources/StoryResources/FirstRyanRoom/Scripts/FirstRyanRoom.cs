
using UnityEngine;
using System.Collections.Generic;

public class FirstRyanRoom : Story
{
    // FirstRyanRoom 스토리의 스크립트 로직을 여기에 작성하세요.
    protected override SequentialElement StartElements => new (

        new PlaceTransition(
            Color.black, 
            1f,
            "BakeryFront",
            new ParallelElement(
                new CameraMove2D(new Vector2(66, 48), 0f),
                new CameraZoomByFactor(.2f, 0f)
            ),
            1f,
            true
        )
    );

    protected override SequentialElement UpdateElements => new (
        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("사실, 조셉 할아버지가 갑자기 이 그린우드 섬으로 온 이유가 궁금하기도 했다."),
                new Line("왜 갑자기 육지를 떠나, 아무도 모르는 이 작은 섬으로 왔을까."),
                new Line("하지만, 생각해보면 나도 크게 다르지 않다."),
                new Line("나 역시 어느 날 도시를 떠나 이곳으로 왔으니까."),
                new Line("그 누구에게도 그 이유를 제대로 말한 적이 없다. 사실, 나 자신에게조차도 명확히 설명하기 힘든 이유였다.")
            }
        ),
        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("도시에서의 삶은 점점 더 지루해졌다."),
                new Line("카메라를 들고 분주한 거리, 높은 빌딩, 사람들의 일상을 찍던 일도 이제는 지겨웠다."),
                new Line("예전엔 그곳의 빠른 리듬과 사람들의 복잡한 움직임이 매력적이었는데, 어느 순간부터 더 이상 그 속에서 새로움을 찾을 수 없게 되었다."),
                new Line("찍는 사진도 매일 똑같았다."),
                new Line("차갑고 거대한 도시에선 모든 것이 흘러가듯 반복될 뿐, 변하는 게 없었다.")
            }
        ),

        new PlaceTransition(
            Color.black, 
            1f,
            "Town1",
            new ParallelElement(
                new CameraMove2D(new Vector2(66, 48), 0f),
                new CameraZoomByFactor(.2f, 0f)
            ),
            1f,
            true
        ),
        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("그런 나에게 그린우드 섬 이야기가 들려왔다."),
                new Line("고요한 자연과 시간이 멈춘 듯한 작은 마을, 그리고 섬 고유의 전통이 살아있는 곳이라고 했다."),
                new Line("처음엔 단순히 신기한 이야기라고 생각했다."),
                new Line("하지만 내 머릿속에서 그린우드라는 이름은 사라지지 않았다."),
                new Line("결국, 나도 모르게 그린우드 섬에 가야겠다는 생각이 점점 더 강해졌다."),
                new Line("마치 그 섬이 나를 부르고 있는 듯한 기분이 들었다.")
            }
        ),

        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("그린우드 섬에 도착한 날, 태풍 속에서 배를 타고 섬에 내렸던 기억이 아직도 생생하다."),
                new Line("처음 발을 디뎠을 때, 비바람이 몰아쳤지만 이상하게 마음은 편안했다."),
                new Line("내리자마자 섬 곳곳에서 느껴지는 자연의 향기, 그리고 그 고요함."),
                new Line("도시에서 잃어버렸던 모든 것이 이곳에 있었다."),
                new Line("새소리, 바람, 그리고 작은 집들. 한없이 느리게 흘러가는 듯한 시간이 나를 감싸는 기분이었다.")
            }
        ),
        new PlaceTransition(
            Color.black, 
            1f,
            "Town2",
            new ParallelElement(
                new CameraMove2D(new Vector2(66, 48), 0f),
                new CameraZoomByFactor(.2f, 0f)
            ),
            1f,
            true
        ),

        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("이 섬에서 처음으로 카메라를 들었을 때, 나는 새로워진 기분을 느꼈다."),
                new Line("찍는 것마다 다르게 보였다."),
                new Line("나무 한 그루, 섬을 감싸는 해안선, 저 멀리 바다와 이어진 하늘까지."),
                new Line("이곳에서만큼은 내가 포착하는 모든 것들이 의미가 있었다."),
                new Line("섬의 조용한 풍경 속에는 말로 표현할 수 없는 따뜻함이 배어 있었다.")
            }
        ),

        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("이곳의 사람들도 따뜻했다."),
                new Line("마을 사람들은 서로를 잘 알고 있었고, 나 같은 외지인에게도 아무런 거리낌 없이 말을 걸어왔다."),
                new Line("특히 케이트. 그녀는 섬에 도착하자마자 나를 맞이해 주었다."),
                new Line("그녀가 만든 빵 냄새는 언제나 섬 전체를 감싸는 듯했고, 그 따뜻한 향기가 섬 생활의 시작을 알리는 듯했다."),
                new Line("작은 가게지만, 이곳에서 나는 단순히 빵을 먹는 게 아니라, 섬의 일상이 나에게 조금씩 스며드는 것을 느낄 수 있었다.")
            }
        ),

        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("그린우드 섬에서의 생활은 내가 원하던 그대로였다."),
                new Line("자연과 함께 살아가는 사람들, 도시에서 잊고 있던 여유로움."),
                new Line("아침에 눈을 뜨면 들리는 새소리, 창밖으로 스치는 부드러운 바람, 그리고 저 멀리서 들려오는 파도 소리까지."),
                new Line("이런 평온함은 도시에서 한 번도 경험해보지 못한 것이었다."),
                new Line("도시에서의 쫓기는 듯한 삶과는 달리, 이곳에서는 시간이 천천히 흐르는 듯했다.")
            }
        ),
        new PlaceTransition(
            Color.black, 
            1f,
            "RyanRoom",
            new ParallelElement(
                new CameraMove2D(new Vector2(66, 48), 0f),
                new CameraZoomByFactor(.2f, 0f)
            ),
            1f,
            true
        ),

        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("어쩌면 나도 조셉 할아버지처럼, 그린우드 섬에 끌려온 것일지도 모른다."),
                new Line("도시에서는 느낄 수 없었던 뭔가를 찾기 위해."),
                new Line("지금 생각해보면, 내가 이 섬에 온 이유는 단순히 도망치기 위해서만은 아니었던 것 같다."),
                new Line("이곳에서 나는 새로운 시작을 찾고 있었다."),
                new Line("잃어버렸던 감각, 사진작가로서 내가 놓치고 있던 진정한 순간을 다시 찾기 위한 여정이었을지도.")
            }
        ),

        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("그린우드 섬은 그런 점에서 나에게 완벽했다."),
                new Line("이 섬에서 나는 다시 카메라를 들 수 있었고, 새로운 풍경들을 마주할 수 있었다."),
                new Line("그리고 그 풍경들은 다시 내 마음을 움직이기 시작했다."),
                new Line("내가 무엇을 찍고 싶었는지, 왜 찍었는지에 대해 고민하게 만들었다.")
            }
        ),

        new UserActionPhaseEnter(
            UserActionWindow.AnchorType.MiddleRight,
            Vector2.zero,
            new Dictionary<UserActionType, SequentialElement>{
                {
                    UserActionType.Sleeping,
                    new SequentialElement(
                        new ScreenOverlayFilm(Color.black, 1f)
                    )
                },
            }
        )

    );

    protected override SequentialElement ExitElements => new ();

    protected override string StoryDesc => "";
}
