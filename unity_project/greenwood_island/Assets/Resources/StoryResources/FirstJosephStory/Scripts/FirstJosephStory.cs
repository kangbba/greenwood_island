using System.Collections.Generic;
using UnityEngine;

public class FirstJosephStory : Story
{
    // FirstJosephStory 스토리의 스크립트 로직을 여기에 작성하세요.
    protected override SequentialElement StartElements => new ();

    protected override SequentialElement UpdateElements => new (

        new PlaceTransition(Color.white, 1f, "CafeSeabreezeFront",  null, 1f,  true),
        new CharacterEnter(
            "Kate",
            "Smile",
            .5f,
            0
        ),

        // 카페 앞에서 문을 두드리는 장면
        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("케이트와 함께 씨브리즈 카페 앞에 멈춰섰다."),
                new Line("처음 보는 작은 카페였다. 오래된 나무 간판이 바닷바람에 흔들리고, 벽은 세월의 흔적으로 낡았지만 묘하게 따뜻해 보였다."),
                new Line("케이트는 잠시 문 앞에서 숨을 고르고는, 문을 두드렸다."),
            }
        ),
        new CameraShake(),
        // 카페 앞에서 문을 두드리는 장면
        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("쾅쾅, 조용했던 공간에 나무 문을 두드리는 소리가 퍼졌다.")
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

        new CharacterMove(
            "Kate",
            .33f,
            .5f
        ),
        new CharacterEnter(
            "Joseph",
            "Smile",
            .66f,
            0
        ),

        // 조셉 할아버지가 반갑게 맞이하며 안으로 들어가는 장면
        new Dialogue(
            "Joseph",
            new List<Line>
            {
                new Line("케이트구나! 어서 들어오렴. 바깥은 오늘 햇볕이 따갑더구나. 어서 들어와라."),
                new Line("자네도 함께 들어오게, 낯선 얼굴인데 친구인가?")
            }
        ),

        // 케이트가 문을 두드리는 장면
        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("네! 이 쪽은 제 친구 라이언이예요.")
            }
        ),

        // 케이트가 문을 두드리는 장면
        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("아... 안녕하세요 조셉 할아버지!")
            }
        ),
        

        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("조셉 할아버지가 반갑게 웃으며 문을 열어주었다. 그의 얼굴에는 세월의 흔적이 짙게 배어 있었지만, 그 미소는 여전히 따뜻했다.")
            }
        ),
        new DialoguePanelClear(),
        
        new PlaceTransition(Color.black, 1f, "CafeSeabreezeInside", null, 1f, true),

        // 조셉이 빵을 받아들이며 입맛이 없다고 거절하는 장면
        new Dialogue(
            "Joseph",
            new List<Line>
            {
                new Line("고맙구나, 케이트야. 하지만 요즘 들어 입맛이 별로 없네."),
            }
        ),

        // 케이트가 속상해하는 장면
        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("정말요? 할아버지, 이번엔 진짜 신경 많이 썼는데... 속상하네요."),
            }
        ),

        // 조셉이 빵에 대해 반응하고 라이언에게 복선을 던지는 장면
        new Dialogue(
            "Joseph",
            new List<Line>
            {
                new Line("마음은 고맙지만, 이번에는 그냥 마음만 받겠다. 나도 천천히 음미해야 하는데, 나이를 먹으니 요즘은 속이 영 불편해서 말이야."),
                new Line("이 빵들은 내가 가지고 있다가, 주변 사람들에게 나눠 주도록 하지."),
                new Line("그나저나, 라이언...이라 했지? 자네도 이 빵을 먹어봤나?"),
            }
        ),

        // 라이언이 빵을 먹어봤다고 대답하는 장면 - 농담 추가
        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("네! 물론이죠. 거의 매일 먹고 있는걸요?"),
                new Line("케이트가 보기엔 좀 덜렁거려도, 빵 하나 만큼은 잘 만들더라구요.")
            }
        ),

        // 케이트가 웃으며 대답하고 자연스럽게 비결을 설명하는 장면
        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("덜렁거린다니? 이런 제빵은 정말 치밀한 성격 아니면 못한다구."),
                new Line("내가 진짜 신경 많이 쓰는 거야! 반죽부터 빵이 만들어지는 순간까지 다 중요하거든."),
                new Line("이번에는 반죽할 때 발효 시간을 정말 정확하게 맞췄어! 온도도 딱 맞춰서 반죽이 탱글탱글하게 나왔다구."),
            }
        ),
        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("케이트는 빵 이야기만 나오면 다른 사람이 된 것 처럼 전문가가 된다."),
                new Line("나는 이럴 때 반 정도 귀를 닫곤 한다."),
                new Line("처음엔 이런 성격이 조금 이상하게 보였지만, 한 달 간 매일 케이트의 설교를 듣다보니 이러지 않으면 허전할 정도이다."),
            }
        ),
        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("그리고 굽는 시간도 아주 조금만 달라도 식감이 달라지니까, 그 시간을 조절하는 게 진짜 힘들거든. 속은 촉촉하고, 겉은 바삭하게."),
                new Line("사실, 이 맛의 비결은 재료에도 있어. 내가 쓰는 밀은 이 마을에서만 구할 수 있는 특별한 밀인데, 다른 곳에서는 절대 이런 맛 못 내.")
            }
        ),

        // 라이언이 아말리안 밀에 대해 궁금해하는 장면
        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("아말리안 밀...? 그게 뭐야? 처음 들어보는데?")
            }
        ),

        // 케이트가 라이언에게 아말리안 밀에 대해 설명하는 장면
        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("아, 아말리안 밀! 너는 이 마을에 온지 한 달 밖에 안 돼서 모를 수 있겠다."),
                new Line("이 마을에서만 재배되는 특별한 밀인데, 3년 전부터 키우기 시작했어."),
                new Line("원래는 이 마을에서 밀을 재배하기가 거의 불가능했는데, 장로님 덕분에 가능해졌어."),
                new Line("장로님 성함도 아말리안. 마을 사람들은 그 이후 이 밀을 아말리안 밀로 부르고 있어."),
                new Line("덕분에 지금은 마을의 자랑이 됐지. 이 밀로 만든 빵은 진짜 특별해. 고소하고, 향도 깊고, 반죽할 때 그 촉감이 달라.")
            }
        ),

        // 라이언이 감탄하는 장면
        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("아말리안 장로님이라..."),
                new Line("이 작은 섬 마을의 장로님이 그런 걸 가능하게 했다는 게 놀라웠다."),
                new Line("조셉 할아버지는 미소를 지으며 케이트의 자랑을 듣고 있었다. 그의 표정에는 깊은 생각이 묻어났지만, 말을 아끼는 듯했다."),
            }
        ),

        // 조셉이 라이언에게 화제를 돌리는 장면
        new Dialogue(
            "Joseph",
            new List<Line>
            {
                new Line("그래, 케이트의 빵은 언제나 자랑스러워. 그런데 자네는 어쩌다 이 마을에 오게 된 건가? 여기는 잘 찾아오지 않는 곳인데.")
            }
        ),

        // 라이언이 대답을 회피하며 자연스럽게 대화가 이어지는 장면
        new Dialogue(
            "Ryan",
            new List<Line>
            {
                new Line("그냥... 여유를 좀 찾고 싶어서 도시에서 왔어요. 이 마을의 풍경은 사진을 찍기 딱 좋거든요. "),
            }
        ),
        // 케이트가 라이언에게 아말리안 밀에 대해 설명하는 장면
        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("할아버지, 라이언은 도시에서 꽤 유명한 사진작가예요."),
                new Line("성격은 조금 고지식하긴 한데, 사진 하나는 끝내주거든요. 직접 보면 놀라실 거예요."),
            }
        ),

        new Dialogue(
            "Joseph",
            new List<Line>
            {
                new Line("허허, 낯설겠지. 하지만 이곳은 금방 자네를 품어줄 걸세."),
                new Line("이 마을은 오래된 이야기들과 함께 살아가는 곳이지."),
                new Line("언제든지 이 카페에 들러도 괜찮네. 따뜻한 차 한 잔 정도는 항상 준비돼 있으니 말일세."),
                new Line("섬이 처음이라면 이곳저곳 천천히 둘러보는 것도 좋겠지. 여유롭게 말일세.")
            }
        ),

        // 케이트가 조셉에게 인사하는 장면
        new Dialogue(
            "Kate",
            new List<Line>
            {
                new Line("그럼 할아버지, 저희 이만 가볼게요. 다음에 또 빵 구워서 가져올게요!"),
                new Line("몸 조심하시고, 너무 무리하지 마세요.")
            }
        )
    );

    protected override SequentialElement ExitElements => new (
        new StoryTransition(new AfterFirstJoseph()));

    protected override string StoryDesc => "";


//    private UserActionPhaseEnter _userActionPhase = new UserActionPhaseEnter(
//         new Vector2(80, -450),
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
//         }
//     );
}
