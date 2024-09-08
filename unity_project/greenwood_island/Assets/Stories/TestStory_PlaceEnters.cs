// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using DG.Tweening;

// public class TestStory_PlaceEnters : Story
// {
//     public override EStoryID StoryId => EStoryID.TestStory_PlaceEnters;
//     protected override string StoryDesc => "";

//     // 각 단계의 Elements를 상속받은 클래스에서 정의
//     protected override SequentialElement StartElements => new
//     (
//         new SFXEnter(SFXType.BirdsChirping, true, 0f), // 새소리로 평화로운 분위기 연출
//         new PEWithOverlayColor(EPlaceID.GreenwoodIsland, Color.white) // 그린우드 섬으로 장소 전환
//     );

//     protected override SequentialElement UpdateElements => new
//     (
//         new Dialogue(
//             ECharacterID.Mono,
//             new List<Line>
//             {
//                 new Line(EEmotionID.Normal, 0, "그린우드 섬... 조용하고, 평화로운 분위기가 가득하네."),
//                 new Line(EEmotionID.Happy, 1, "바람도 살랑거리고, 햇살도 따뜻하고... 이런 곳이 있었던 건가."),
//                 new Line(EEmotionID.Happy, 2, "잠시 모든 걸 잊고 이곳을 느껴봐야겠다.")
//             }
//         ),
//         // 마을 1 탐험 연출
//         new PEWithOverlayColor(EPlaceID.GreenwoodIsland, Color.grey), // 그린우드 섬으로 장소 전환
//         new Dialogue(
//             ECharacterID.Mono,
//             new List<Line>
//             {
//                 new Line(EEmotionID.Normal, 0, "하늘이 정말 맑고 예쁘다. 마을도 참 아기자기하네."),
//                 new Line(EEmotionID.Smile, 1, "여긴 다들 여유로운 표정이야. 다음엔 저 가게도 한번 들어가 봐야지."),
//             }
//         ),
//         // 마을 2 탐험 연출
//         new PEWithOverlayColor(EPlaceID.GreenwoodIsland, Color.black), // 그린우드 섬으로 장소 전환
//         new PlaceFilm(Color.blue.ModifiedAlpha(.5f), .5f), // 마을의 차분한 색감 강조
//         new CameraMove2D(new Vector2(0, 1), 2f, Ease.InOutQuad), // 카메라가 마을 2를 가로지르며 이동
//         new Dialogue(
//             ECharacterID.Mono,
//             new List<Line>
//             {
//                 new Line(EEmotionID.Normal, 0, "밤이된것같아. 이쪽은 조금 더 고요하네. 어딘가 신비로운 느낌이야."),
//                 new Line(EEmotionID.Normal, 1, "여기도 구경해봐야겠어. 뭐가 있을지 궁금하군."),
//             }
//         ),
//         // 마을 3 탐험 후 케이트와의 조우 연출
//         new PEWithOverlayColor(EPlaceID.GreenwoodIsland, Color.white), // 그린우드 섬으로 장소 전환
//         new CameraMove2D(new Vector2(1, 0), 2f, Ease.OutQuad), // 카메라가 마을 3을 감싸듯 이동
//         new Dialogue(
//             ECharacterID.Mono,
//             new List<Line>
//             {
//                 new Line(EEmotionID.Normal, 0, "여긴 또 다른 매력이 있네. 어딜 둘러봐도 신기한 것들로 가득해."),
//                 new Line(EEmotionID.Normal, 1, "누가 살고 있을까? 사람들 표정도 궁금해진다."),
//             }
//         ),
//         // 케이트와의 만남
//         new CharactersEnter(
//             new List<ECharacterID> { ECharacterID.Kate },
//             new List<float> { 0.5f }, // 화면 중앙에 위치
//             new List<EEmotionID> { EEmotionID.Smile },
//             new List<int> { 0 },
//             1.5f,
//             Ease.OutQuad
//         ),
//         new Dialogue(
//             ECharacterID.Kate,
//             new List<Line>
//             {
//                 new Line(EEmotionID.Smile, 0, "안녕하세요. 새로운 얼굴이네요. 여기서 뭘 찾고 계신가요?"),
//                 new Line(EEmotionID.Normal, 1, "이곳은 조용한 동네예요. 천천히 둘러보세요."),
//             }
//         ),
//         new Dialogue(
//             ECharacterID.Ryan,
//             new List<Line>
//             {
//                 new Line(EEmotionID.Smile, 0, "안녕하세요. 이곳이 정말 예쁘네요."),
//                 new Line(EEmotionID.Normal, 1, "이런 곳에서 지내면 마음이 편해질 것 같아요."),
//             }
//         )
//     );


//     protected override SequentialElement ExitElements => new (

//     );
// }
