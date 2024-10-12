using System.Collections.Generic;
using UnityEngine;

public class WeirdCloud : Story
{

    public override List<Element> UpdateElements => new List<Element> {
        new ScreenOverlayFilm(Color.black),
        new PlaceTransitionWithOverlayColor(
            "SeasideWalkway",
            1f,
            Color.black
        ),
        new PlaceScale(Vector2.one * 1.1f, 1f),
        new SFXEnter("Waves", 1, true, 2),

        new ScreenOverlayFilmClear(1f),

        new CharacterEnter(
            "Kate",
            EmotionType.Happy,
            .5f,
            0
        ),

        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("여긴 처음 걸어보네. 마을에 온 지 한 달도 넘었는데, 해변가 쪽은 처음이야."),
                new Line("날씨가 좋다 싶으면 태풍이 한 번씩 불어와서… 뭐랄까, 날씨 운이 없었다고 해야 하나?")
            }
        ),

        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("그래, 이쪽은 좀 한적하긴 하지. 대부분 사람들이 마을 중심가 쪽에 모이니까."),
                new Line("하지만 난 가끔 여기 걸어. 바닷바람 맞으면서 생각도 정리되고, 조용해서 좋거든.")
            }
        ),

        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("케이트 말처럼, 바닷바람이 가슴 깊이 스며든다. 뭔가 머리가 맑아지는 느낌이랄까."),
                new Line("파도 소리가 멀리서 잔잔하게 들려오고, 발밑으로는 고요한 길이 부드럽게 이어진다."),
                new Line("처음 걷는 길인데, 왜 이제야 와봤을까 싶다."),
                new Line("한 달 전에는 이런 평온함을 상상도 못 했지. 그땐 이 섬에 오기 전이었으니까."),
                new Line("시간이 흐르면서, 점점 이곳이 익숙해지는 것 같아. 이제는 어색하지 않다.")
            }
        ),
        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("사실 어릴 때는 이 섬이 너무 답답했어."),
                new Line("작고 조용한 건 좋지만, 그게 전부니까… 마을 사람들도 항상 같고, 볼거리도 똑같고. 매일매일이 반복된다는 느낌이었거든.")
            }
        ),

        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("그럴 수도 있겠네. 섬이 크지도 않고, 변화도 적을 테니까. 그래서 떠나고 싶었어?")
            }
        ),

        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("맞아. 한창 사춘기 때는 여기서 벗어나서, 도시로 가고 싶다는 생각만 했지."),
                new Line("새로운 사람들, 새로운 경험… 그게 그리웠어."),
                new Line("이 마을 사람들도 너무 친절하긴 한데... 그치만 뭔가...")
            }
        ),

        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("응? 그치만 뭐?")
            }
        ),

        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("아, 아니야. 아무튼... 조금 답답해서 도시로도 나가보고 싶었어."),
                new Line("결국엔 못 나갔지만 말이야.")
            }
        ),

        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("케이트도 이런 생각을 했구나. 이 작은 섬 안에 갇힌 듯한 기분… 그건 도시에서 느껴보지 못한 감정이겠지.")
            }
        ),

        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("그럼 지금은 여기에 남고 싶어? 아니면 아직도 가끔은 떠나고 싶은 생각이 들어?")
            }
        ),

        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("솔직히? 가끔은 떠나고 싶을 때도 있어."),
                new Line("근데 뭐! 결국 돌아올 곳은 여긴 걸. 어릴 때부터 보고 자란 곳이니까, 괜찮을 거야! 어때, 그렇게 나쁘진 않지?")
            }
        ),

        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("케이트의 말이 뭔가 묵직하게 다가온다."),
                new Line("나 역시 도시에서 이 섬으로 왔지만, 여기서 느낀 고요함은 내가 예상했던 것 이상이었다."),
                new Line("어쩌면 나도 언젠가 이 섬에 뿌리를 내리게 될지도 모르겠다는 생각이 들었다.")
            }
        ),

        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("네가 여기에 남고 싶어 한다는 게 이해가 되는 것 같아."),
                new Line("이곳은 변하지 않으니까, 그게 안정감을 주는 것 같기도 하고.")
            }
        ),

        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("응, 그런 느낌이야. 변하지 않는 게 좋을 때가 있어."),
                new Line("물론 여전히 때때로 새로운 걸 경험하고 싶지만, 그때는 잠시 떠나면 되는 거니까.")
            }
        ),

        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("케이트는 이 섬에서의 삶에 대한 생각이 확고하다."),
                new Line("나도 아직 이 섬에서 무엇을 찾고 있는지 확실히 모르겠지만, 이곳에서 느끼는 평온함은 부정할 수 없다.")
            }
        ),

        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("케이트와 이런저런 이야기를 나누며 걷고 있을 때, 문득 하늘이 눈에 들어왔다."),
                new Line("구름 한 덩이가 고요한 바다 위로 낮게 떠 있었고, 그 구름은 마치 은빛으로 빛나며 햇살을 받아 반짝이고 있었다.")
            }
        ),
        new ImaginationEnter(
            "WeirdCloud"
        ),

        new ParallelElement(
            new ImaginationMove(
                Vector2.down * 100,
                10f
            ),
            new ImaginationScale(
                Vector2.one * 1.3f,
                2f
            ),
            new Dialogue(
                "Ryan",
                new List<Line>
                {
                    new Line("와... 저 구름 좀 봐."),
                    new Line("이렇게 맑은 날씨에 저렇게 멋진 구름이 있다니. 마치 그림처럼 떠 있는 것 같아."),
                    new Line("뭔가... 그냥 바라보고 있으면 신비로운 기분이 들어.")
                }
            )
        ),
        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("아, 저런 구름은 이 섬에선 자주 볼 수 있어."),
                new Line("섬에 오래 있으면 익숙해질 거야. 처음엔 나도 저 구름을 보고 많이 놀랐거든."),
                new Line("그냥 경이롭지? 이 섬의 매력이 그런 거야. 자연스럽고 신비로운 풍경이 가득해.")
            }
        ),


        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("나는 구름에서 눈을 뗄 수 없었다. 마치 세상에서 딱 하나 남은 구름처럼, 그 구름은 하늘에 고요하게 떠 있었다."),
                new Line("바람이 불어오는데도 구름은 미동도 없었다. 그저 그 자리에 멈춘 듯한 모습이 경이로웠다.")
            }
        ),

        new ImaginationClear(
            1f
        ),

        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("아, 저기야! 저기가 카페 씨브리즈야!")
            }
        ),

        new PlaceTransitionWithSwipe(
            "CafeSeabreezeFront",
            1f,
            SwipeMode.SwipeLeft
        ),
        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("케이트가 손을 가리키며 들뜬 목소리로 외쳤다."),
                new Line("멀리서 작은 카페 건물이 보이기 시작했다. 바닷가 바로 앞에 자리한, 오래된 건물이지만 따뜻해 보이는 곳."),
                new Line("케이트는 정말 들뜬 얼굴로 나를 쳐다보며 발걸음을 재촉했다.")
            }
        ),

        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("조셉 할아버지를 오랜만에 뵐 수 있다니, 나 너무 신나!")
            }
        ),

        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("그녀의 밝은 모습에 나도 모르게 웃음이 났다."),
                new Line("카페 씨브리즈… 마침내 그곳에 도착하게 됐다.")
            }
        ),
    };
}
