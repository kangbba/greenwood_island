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
            "BakeryFront", 
            1f,
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
                new Line("케이트는 오늘도 빵집 앞에서 바쁘게 진열대를 정리하고 있다."),
                new Line("그 열정은 언제 봐도 대단하다."),
            },
            fadeout : true
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
                new Line("라이언! 또 왔네? 빵 냄새가 그렇게 좋아?")
            }
        ),

        new Dialogue(
            "Mono",
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
                new Line("맞아, 오늘은 날씨가 좀 따뜻해서 발효 시간을 조절했어. 온도가 높으면 반죽이 더 빨리 부풀거든. 그때마다 다르게 해야 해.", 0),
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
            "Mono",
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
                new Line("당연하지. 오늘은 빵 속이 더 폭신폭신하고 부드럽게 만들어보려고 조금 더 시간을 들였어. 먹으면 네가 바로 알 거야."),
            },
            fadeout : true
        ),
        new ImaginationEnter(
            "Bread"
        ),
        new SFXsClear(),
        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("그녀가 내미는 빵은 언제나 특별하다."),
                new Line("화려하지 않고 소박하지만, 말로는 설명 할 수 없는, 뭔가 더 깊은 게 있다."),
                new Line("한 입 베어 물 때마다 입안 가득 퍼지는 폭신한 식감이 마음을 편안하게 해주는 듯 하다."),
                new Line("아마 케이트의 열정과 정성이 담겨 있어서 그런 거겠지."),
            },
            fadeout : true
        ),
        new ImaginationClear(1f),
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
            "Mono",
            new List<Line>
            {
                new Line("케이트와 나누는 이런 대화가 요즘 나에게는 가장 소중한 시간이다."),
                new Line("평범한 일상 속에서 느껴지는 작은 행복이 이곳에 나를 묶어두는 것 같다."),
            }
        ),

        // 케이트가 조셉 할아버지에 대한 이야기를 꺼내는 부분
        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("아, 맞다. 조셉 할아버지께 빵을 가져다 드리려고 해, 요즘은 직접 오시기 힘들어 보이셔서 말이야."),
                new Line("며칠 전에도 빵을 가져다드렸어. 근데... 요새 몸이 많이 안 좋아 보이시더라구."),
            }
        ),

        // 라이언의 독백으로 조셉 할아버지에 대한 기억을 떠올리는 장면
        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("조셉 할아버지... 어렴풋하게 들은 기억이 있다."),
                new Line("마을 사람들 사이에서 종종 이름이 오갔다. 해안가에 있는 카페 주인이라던가..."),
                new Line("건강이 좋지 않다는 소리도 어딘가에서 들었던 것 같다."),
                new Line("그가 이 마을에서 중요한 인물이라는 건 알겠지만, 정작 얼굴은 한 번도 본 적이 없다."),
            }
        ),

        // 케이트가 조셉을 소개하며 라이언을 설득하는 대사
        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("해안가에서 카페 씨브리즈라는 카페를 운영하시는 분이야."),
                new Line("건강이 안 좋으셔서 한 달 넘게 문을 닫으셨다가 최근에 다시 매장을 여셨어."),
                new Line("마을 사람들이 다들 걱정할 정도로 열심히 사시는 분인데, 요즘은 어딘가 조금 달라졌다고 해야 할까..."),
                new Line("내가 가는 김에 너도 같이 갈래? 너도 한 번쯤 만나뵈면 좋을 거야. 커피도 맛있고, 그분 이야기도 흥미로울 거야."),
            },
            fadeout : true
        ),
        // 선택지를 통해 라이언의 반응을 유도
        new ChoiceSet(
            question : "어떻게 한다 ...",
            choices : new List<ChoiceContent>
            {
                new ChoiceContent(
                    "귀찮은데… 다음에 가면 안 될까?",
                    new SequentialElement(
                        new Dialogue("Ryan", new List<Line>
                        {
                            new Line("귀찮은데… 다음에 가면 안 될까?"),
                        }),
                        new Dialogue("Kate", new List<Line>
                        {
                            new Line("사실 할아버지가 요즘 사람을 잘 안 만나려 하셔서, 나 혼자 가면 괜히 더 불편해하실 것 같아."),
                            new Line("네가 같이 가면 이야기도 나누시고, 기분도 조금 나아지실 거야. 건강도 안 좋으신데 요즘 너무 외로워 보이셔."),
                            new Line("라이언, 나랑 같이 가주면 안 될까? 네가 가면 할아버지도 좀 웃으실 것 같아."),
                        }),
                        new Dialogue("Ryan", new List<Line>
                        {
                            new Line("그래... 알았어. 그러면 같이 가지."),
                        })
                    )
                ),
                new ChoiceContent(
                    "좋아, 어디 한번 같이 가보자.",
                    new SequentialElement(
                        new Dialogue("Ryan", new List<Line>
                        {
                            new Line("좋아, 어디 한번 같이 가보자."),
                        }),
                        new Dialogue("Kate", new List<Line>
                        {
                            new Line("정말 고마워, 라이언! 할아버지가 요즘 예전 같지 않으셔서 걱정이 많았어."),
                            new Line("네가 같이 가면 할아버지께도 큰 힘이 될 거야. 가서 커피 한 잔 하면서 이런저런 얘기도 나눠보자."),
                        }),
                        new Dialogue("Ryan", new List<Line>
                        {
                            new Line("조셉 할아버지가 어떤 분인지 직접 만나보면 더 잘 알겠지."),
                        })
                    )
                )
            }
        ),
        new Dialogue("Kate", new List<Line>
        {
            new Line("정말? 라이언이라면 그렇게 말해 줄 줄 알았다구!"),
            new Line("그럼 이따 마을 입구에서 보자!"),
        }),
        new Dialogue("Mono", new List<Line>
        {
            new Line("케이트는 이 마을에서 처음 나를 맞아준 사람이고, 지금까지 항상 내 편이 되어줬다. 그녀의 말이라면 믿어도 되겠지."),
            new Line("그 사람은 어떤 사람일까?"),
        }),
        new DialoguePanelClear(),
        new SFXsClear(1f),
        new ScreenOverlayFilm(Color.white, 1f),
        new AllCharactersClear(0f),
        new PlaceEnter("BakeryFront"),
        new ParallelElement(
            new PlaceMove(new Vector2(-100, 0)),
            new ScreenOverlayFilmClear()
        ),
        new PlaceRestore(),

        new CharacterAwait(
             new CharacterEnter("Kate", EmotionType.Happy, 0.5f)
        ),
        new Dialogue(
            "Kate", 
            new List<Line> 
            {
                new Line("갈 준비 다 됐어?"),
                new Line("그럼, 가자!")
            },
            fadeout : true
        )
       
   };
    
//    private UserActionPhaseEnter _userActionPhase = new UserActionPhaseEnter(
//         GameDataManager.CurrentStorySavedData,
//         "useraction1",
//         new Dictionary<UserActionType, SequentialElement>
//         {
//             {
//                 UserActionType.Talking, 
//                 new SequentialElement(
//                     new Dialogue(
//                         "Kate", 
//                         new List<Line> 
//                         {
//                             new Line("갈 준비 다 됐어?"),
//                             new Line("그럼, 가자!")
//                         }
//                     )
//                 )
//             }
//         },
//         UserActionWindow.AnchorType.TopLeft,
//         new Vector2(80, -450)
//     );

}
