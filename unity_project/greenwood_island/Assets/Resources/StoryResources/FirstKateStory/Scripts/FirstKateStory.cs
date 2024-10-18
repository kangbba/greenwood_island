using System.Collections.Generic;
using UnityEngine;
using static ImageUtils;

public class FirstKateStory : Story
{
    public override List<Element> UpdateElements => new List<Element> {

        new Intertitle("한달 후"),
        new SFXEnter("BirdChirp1", 1f, true, 1f),
        new SFXEnter("BirdChirpLong1", 1f, true, 3f),
        new PlaceTransition(
            new PlaceEnter("BakeryFront"),
            Color.black,
            new PlaceEffect(PlaceEffect.EffectType.ZoomIn, 2f, 1.1f)
        ),
        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("그린우드에 온 지 벌써 한 달이 지났다."),
                new Line("태풍 속에 도착했던 첫날이 엊그제 같은데, 지금은 날씨도 맑고, 섬은 평화롭기만 하다."),
                new Line("새소리, 따뜻한 햇살, 그리고 케이트의 빵 냄새가 섬을 가득 채운다."),
                new Line("케이트는 오늘도 빵집 앞에서 분주하게 진열대를 정리하고 있다."),
                new Line("그녀의 에너지는 정말 대단하다.")
            },
            fadeout: true
        ),

        new SFXsClear(),

        new PlaceTransitionWithSwipe(
            "BakeryInside",
            1f,
            SwipeMode.SwipeLeft
        ),

        new CharacterEnter(
            "Kate",
            EmotionType.Happy,
            .5f,
            0
        ),
        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("라이언! 또 왔네? 오늘도 빵 냄새 맡고 왔구나?"),
            }
        ),

        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("케이트, 이 마을에서 작은 베이커리를 운영하며 늘 활기차고 긍정적이다."),
                new Line("내가 처음 도착했을 때부터 먼저 다가와서 마을 생활에 적응하도록 도와주었다."),
                new Line("그녀의 빵은 작지만, 그 맛은 마을 사람들 사이에서 인기가 많다."),
            }
        ),

        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("응, 네 빵 냄새는 도저히 못 참겠더라. 오늘도 새벽부터 일했지?")
            }
        ),

        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("당연하지! 오늘은 날씨가 따뜻해서 발효 시간이 조금 짧았어. 빵을 만들 때 날씨에 따라 발효도 달라지니까 매번 다르게 신경 써야 해.", 0),
            }
        ),
        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("발효... 빵 굽는 게 단순한 게 아니구나.")
            }
        ),
        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("날씨에 따라 모든 게 달라지다니, 생각보다 훨씬 세심한 일이었다."),
                new Line("케이트는 이 모든 걸 꼼꼼하게 챙기고 있다.")
            }
        ),

        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("오늘 빵은 또 어떤 맛일지 기대되네.")
            }
        ),

        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("기대해도 좋아! 오늘은 특별히 속을 더 부드럽게 만들었어. 먹어보면 바로 알 거야.")
            },
            fadeout: true
        ),
        new ImaginationEnter(
            "Bread"
        ),
        new SFXsClear(),
        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("그녀가 내미는 빵은 늘 특별하다."),
                new Line("소박하지만 설명할 수 없는 깊이가 있다."),
                new Line("한 입 베어 물 때마다 퍼지는 폭신한 식감은 마치 케이트의 정성스런 마음이 담겨 있는 듯하다.")
            },
            fadeout: true
        ),
        new ImaginationClear(1f),
        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("정말 부드럽다... 네가 왜 이 빵을 자랑하는지 알겠어.")
            }
        ),

        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("그치? 자주 와! 네가 맛있게 먹으면 나도 더 기분 좋거든.")
            }
        ),

        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("케이트와 나누는 이런 대화가 요즘 나에게 가장 소중한 시간이다."),
                new Line("평범한 일상 속에서 느껴지는 작은 행복이 이곳에 나를 묶어두는 것 같다.")
            }
        ),
        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("빵을 굽는 것도, 매일 반복되는 평화로운 일상도 어딘가 섬세한 조율이 필요했다."),
                new Line("케이트의 정성스러운 손길이 느껴지는 이 빵은, 그린우드 섬의 작은 행복을 상징하는 것 같았다.")
            }
        ),
        
        new AllCharactersClear(),

        // 빵집에서 나와 섬 마을을 걷는 장면
        new PlaceTransition(
            new PlaceEnter("TownFruitStore"),
            Color.black,
            new PlaceEffect(PlaceEffect.EffectType.FadeIn, 2f, 1.1f)
        ),
        
        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("섬 마을은 여전히 고요하다."),
                new Line("햇살이 부드럽게 비치는 골목길을 걸으며, 나는 섬의 고즈넉한 풍경에 점점 익숙해져 가는 나를 느낀다."),
                new Line("삶의 속도는 천천히 흐르고, 마을 사람들은 조용히 제 할 일을 한다."),
                new Line("처음에는 이 느림이 답답했지만, 이제는 이곳이 나름의 리듬을 가지고 있다는 걸 알게 됐다."),
            }
        ),

        // 주민들과 가벼운 인사를 나누는 장면
        new CharacterEnter(
            "OldMan",
            EmotionType.Neutral,
            .5f,
            0
        ),
        new Dialogue(
            "OldMan",
            new List<Line>
            {
                new Line("안녕하세요, 라이언 씨. 오늘도 사진 찍으러 가는 길인가요?")
            }
        ),

        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("네, 어르신. 날씨가 좋아서 좋은 사진이 나올 것 같아요."),
            }
        ),

        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("이곳 사람들은 내가 사진을 찍으러 다니는 걸 모두 알고 있다."),
                new Line("마을은 작고, 사람들끼리 금방 소문이 돈다. 이런 점이 때론 부담스럽기도 하지만..."),
                new Line("그래도 이렇게 편안하게 인사를 건네는 모습은 싫지 않다."),
            }
        ),

        // 조셉 아저씨와의 우연한 만남
        new PlaceTransition(
            new PlaceEnter("VillageBench"),
            Color.clear,
            new PlaceEffect(PlaceEffect.EffectType.ZoomIn, 2f, 1.05f)
        ),
        
        new CharacterEnter(
            "Joseph",
            EmotionType.Happy,
            1f,
            0
        ),
        
        new Dialogue(
        "Mono",
        new List<Line>
            {
                new Line("저기 벤치에 앉아 계신 분은... 조셉 할아버지다."),
                new Line("카페 씨브리즈의 주인이라는 이야기를 들은 적이 있다."),
                new Line("한동안 몸이 좋지 않으셨다고 들었는데, 이제는 이렇게 밖에 나와 계시는구나.")
            }
        ),
        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("안녕하세요, 조셉 할아버지. 여기서 쉬고 계시네요?")
            }
        ),
        new Dialogue(
            "Joseph",
            new List<Line>
            {
                new Line("어, 라이언... 가끔은 이렇게 나와야지. 집에만 있으면 사람 미쳐."),
                new Line("햇빛이 좋으니 바람도 맞고, 생각도 좀 정리할 겸 나왔지.")
            }
        ),
        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("이제는 상태가 좀 나아지신 건가요?")
            }
        ),
        new Dialogue(
            "Joseph",
            new List<Line>
            {
                new Line("음... 뭐, 그냥 그런 대로야. 나이 들면 원래 이런 거 아니겠어?"),
                new Line("요즘은 그저 하루하루를 천천히 보낸다네. 섬도 그렇고, 나도 그렇고...")
            }
        ),
        
        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("조셉 할아버지는 느릿하게 말을 잇는다."),
                new Line("그의 말 속에 섬의 시간과 같은 느긋함이 묻어난다.")
            }
        ),
        
        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("카페 다시 여셨다고 들었어요. 시간 날 때 커피 마시러 꼭 갈게요."),
            }
        ),

        new Dialogue(
            "Joseph",
            new List<Line>
            {
                new Line("그래, 언제든지. 손님이 많지는 않지만 그게 또 괜찮아."),
                new Line("그린우드는 원래 한적한 곳이거든. 그런 게 좋을 때도 있고."),
            }
        ),
        
        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("그렇죠. 저도 이 섬의 조용한 분위기가 좋아요. 도시와는 완전히 다르니까."),
            }
        ),

        new Dialogue(
            "Joseph",
            new List<Line>
            {
                new Line("맞아, 도시는 너무 빨라. 이 섬은 좀... 다르지."),
                new Line("그런데 말이야, 아무리 천천히 흘러도 결국 바람이라는 게 언제 불지 모르거든."),
                new Line("뭐, 그건 나이 든 사람의 쓸데없는 걱정일 뿐일지도 모르겠지만.")
            }
        ),

        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("할아버지의 말은 그저 평범한 일상의 대화 같았다."),
                new Line("하지만 그가 덧붙인 마지막 말에서 어쩐지 묘한 기운을 느꼈다."),
                new Line("섬의 조용함 속에 감춰진 무언가가 있을지도 모른다는 생각이, 잠시 스쳐 지나갔다."),
            }
        ),
        
        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("음, 할아버지 말씀이 맞을지도 모르죠. 아무리 평온해 보여도 언제든 변할 수 있으니까요."),
            }
        ),
        
        new Dialogue(
            "Joseph",
            new List<Line>
            {
                new Line("허허, 뭐 그렇지."),
                new Line("하여튼 천천히 쉬면서 와. 커피는 준비해두겠네.")
            }
        ),
        
        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("조셉 할아버지와의 짧은 대화는 다시 일상 속으로 흘러갔다."),
                new Line("이 섬에서의 삶은 천천히, 그리고 조용하게 계속된다. 하지만..."),
                new Line("나는 섬의 고요함 이면에 더 많은 이야기가 있을지도 모른다는 생각을 떨칠 수 없었다.")
            }
        ),

    };
}
