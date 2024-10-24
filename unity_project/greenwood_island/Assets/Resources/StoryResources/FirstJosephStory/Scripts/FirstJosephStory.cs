// using System.Collections.Generic;
// using UnityEngine;

// public class FirstJosephStory : Story
// {
//     public override List<Element> UpdateElements => new List<Element> {

//         new PlaceTransition(Color.white, 1f, "CafeSeabreezeFront",  null, 1f,  true),
//         new CharacterEnter(
//             "Kate",
//             .5f,
//             0
//         ),

//         // 카페 앞에서 문을 두드리는 장면
//         new Dialogue(
//             "Mono",
//             new List<Line>
//             {
//                 new Line("케이트와 함께 씨브리즈 카페 앞에 섰다."),
//                 new Line("처음 보는 작은 카페. 오래된 나무 간판이 바닷바람에 흔들리고, 벽은 세월의 흔적으로 낡았지만 묘하게 따뜻한 느낌이었다."),
//                 new Line("케이트는 문 앞에서 잠깐 멈췄다."),
//             }
//         ),

//         // 문 두드리는 소리와 라이언의 반응
//         new CameraShake(),
//         new Dialogue(
//             "Mono",
//             new List<Line>
//             {
//                 new Line("쾅쾅, 조용했던 공간에 나무 문을 두드리는 소리가 퍼졌다."),
//             }
//         ),

//         new Dialogue(
//             "Ryan",
//             new List<Line>
//             {
//                 new Line("저기… 문 부서지겠어. 살살 치는 게 어때?")
//             }
//         ),

//         new Dialogue(
//             "Kate",
//             new List<Line>
//             {
//                 new Line("아니거든? 할아버지는 이렇게 두드려야 잘 들으신다구.")
//             }
//         ),

//         // 조셉 할아버지 등장
//         new Dialogue(
//             "Joseph",
//             new List<Line>
//             {
//                 new Line("케이트구나! 오랜만이구나. 자네는… 낯선 얼굴이군. 친구인가?")
//             }
//         ),

//         new Dialogue(
//             "Kate",
//             new List<Line>
//             {
//                 new Line("이쪽은 제 친구, 라이언이에요! 도시에서 왔구요, 사진 찍는 거에 엄청 열정적인 친구에요.")
//             }
//         ),

//         new Dialogue(
//             "Ryan",
//             new List<Line>
//             {
//                 new Line("아… 안녕하세요! 라이언이라고합니다. 만나뵙게 되어 반갑습니다!")
//             }
//         ),

//         // 카페 안으로 들어가는 장면
//         new Dialogue(
//             "Joseph",
//             new List<Line>
//             {
//                 new Line("반갑네. 밖에 서 있지 말고, 어서 들어오게. 오늘은 햇볕이 뜨겁더군."),
//             }
//         ),
        
//         new PlaceTransition(Color.black, 1f, "CafeSeabreezeInside", null, 1f, true),

//         // 카페 내부 묘사
//         new Dialogue(
//             "Mono",
//             new List<Line>
//             {
//                 new Line("카페 내부는 아늑하고 고요했다. 오래된 가구들에선 손때가 묻은 세월의 흔적이 느껴졌고, 벽에는 오래된 사진들이 걸려 있었다."),
//                 new Line("공간 전체가 뭔가 아련한 과거 속에 머물러 있는 느낌. 바닷가에서 들려오는 잔잔한 파도 소리가 이곳의 평온함을 더해준다.")
//             }
//         ),

//         // 케이트가 조셉 할아버지에게 빵을 드리는 장면
//         new Dialogue(
//             "Kate",
//             new List<Line>
//             {
//                 new Line("빵을 좀 챙겨왔어요 할아버지. 건강은 좀 어떠세요?")
//             }
//         ),

//         new Dialogue(
//             "Joseph",
//             new List<Line>
//             {
//                 new Line("고맙구나, 케이트야. 요즘 입맛이 영 없네. 이젠 나도 예전 같지 않구나.")
//             }
//         ),

//         new Dialogue(
//             "Kate",
//             new List<Line>
//             {
//                 new Line("속상하네요, 할아버지. 어서 이 빵 드시고 힘내세요.")
//             }
//         ),

//         // 조셉 할아버지가 빵을 받아들이는 장면
//         new Dialogue(
//             "Joseph",
//             new List<Line>
//             {
//                 new Line("마음은 고맙지만, 이번엔 그냥 마음만 받겠다. 나이가 들다 보니 속이 불편해서 말이지."),
//                 new Line("이 빵은 내가 잘 가지고 있다가, 주변 사람들에게 나눠 주마."),
//                 new Line("…그런데 라이언이라 했나? 자네도 이 빵을 먹어봤나?")
//             }
//         ),

//         // 라이언과 케이트의 대화
//         new Dialogue(
//             "Ryan",
//             new List<Line>
//             {
//                 new Line("네! 거의 매일 아침 먹고 있어요. 지나갈 때마다 냄새가 유혹하는걸요."),
//                 new Line("케이트 처럼 덜렁거리는 성격인데, 빵은 어떻게 이렇게 잘 만드는지 모르겠어요.")
//             }
//         ),

//         new Dialogue(
//             "Kate",
//             new List<Line>
//             {
//                 new Line("덜렁거리다니? 하, 내가 얼마나 치밀한 성격인지 모르나 본데…"),
//                 new Line("반죽부터 굽기까지, 하나도 허투루 하는 게 없다고. 오늘도 발효 시간 맞추느라 새벽부터 일어났다구!")
//             }
//         ),

//         // 라이언의 독백
//         new Dialogue(
//             "Mono",
//             new List<Line>
//             {
//                 new Line("빵 얘기만 나오면 케이트는 완전히 다른 사람이 된다. 이 열정에 푹 빠져 있는 모습은 한 달 동안 지켜봤지만, 여전히 흥미롭다.")
//             }
//         ),

//         new Dialogue(
//             "Kate",
//             new List<Line>
//             {
//                 new Line("굽는 시간도 조금만 달라도 맛이 확 달라져서, 항상 조절하는 게 중요해. 속은 촉촉하게, 겉은 바삭하게."),
//                 new Line("그리고 이 맛의 비결은 재료야. 내가 쓰는 밀, 즉 아말리안 밀은 이 마을에서만 나는 특별한 밀이야. 다른 곳에서는 절대 이런 맛이 안 나거든.")
//             }
//         ),

//         new Dialogue(
//             "Ryan",
//             new List<Line>
//             {
//                 new Line("아말리안 밀...? 처음 들어보는데?")
//             }
//         ),

//         new Dialogue(
//             "Kate",
//             new List<Line>
//             {
//                 new Line("아, 너는 아직 잘 모를 수 있겠다. 이 마을에서만 재배되는 밀인데, 3년 전부터 키우기 시작했어."),
//                 new Line("장로님 덕분에 가능해진 거야. 이 밀로 만든 빵은 진짜 특별해.")
//             }
//         ),

//         // 라이언의 독백
//         new Dialogue(
//             "Mono",
//             new List<Line>
//             {
//                 new Line("아말리안 장로님... 이 작은 섬의 장로가 그렇게 중요한 역할을 했다는 게 놀랍다."),
//                 new Line("조셉 할아버지는 케이트의 말을 들으며 미소를 지었지만, 뭔가 깊은 생각에 빠져 있는 듯했다.")
//             }
//         ),

//         // 조셉 할아버지가 라이언에게 화제를 돌리는 장면
//         new Dialogue(
//             "Joseph",
//             new List<Line>
//             {
//                 new Line("그래, 케이트의 빵은 항상 자랑스럽구나. 그런데 라이언, 자네는 어떻게 이 마을에 오게 되었나?")
//             }
//         ),

//         new Dialogue(
//             "Ryan",
//             new List<Line>
//             {
//                 new Line("그냥... 도시 생활에서 벗어나고 싶었어요. 이 마을의 풍경이 너무 좋아서 사진 찍으러 왔거든요.")
//             }
//         ),

//         new Dialogue(
//             "Kate",
//             new List<Line>
//             {
//                 new Line("할아버지, 라이언은 사진작가예요. 사진 하나는 정말 끝내줘요.")
//             }
//         ),

//         // 조셉 할아버지가 라이언에게 따뜻하게 말하는 장면
//         new Dialogue(
//             "Joseph",
//             new List<Line>
//             {
//                 new Line("허허, 그렇군. 이 마을은 금방 자네를 품어줄 걸세."),
//                 new Line("섬을 천천히 둘러보게. 이 카페에 언제든 들러도 좋네.")
//             }
//         ),

//         // 케이트가 인사하며 떠나는 장면
//         new Dialogue(
//             "Kate",
//             new List<Line>
//             {
//                 new Line("그럼 할아버지, 저희 이만 가볼게요. 다음에 또 빵 구워서 가져올게요!"),
//                 new Line("몸 조심하시고, 너무 무리하지 마세요.")
//             }
//         )
//     };

// }
