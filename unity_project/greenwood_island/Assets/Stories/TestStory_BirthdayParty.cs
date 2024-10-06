// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using DG.Tweening;

// public class TestStory_BirthdayParty : Story
// {
//     public override EStoryID StoryId => EStoryID.TestStory_BirthdayParty;
//     protected override string StoryDesc => "";
    
//     public Sprite Kate_CryingHappy {
//         get{
//              return CharacterManager.Instance.GetCharacterData(ECharacterID.Kate).characterPrefab.EmotionPlansData.EmotionPlans.Find(plan => plan.EmotionID == EEmotionID.CryingHappy).EmotionSprites[0];
//         }
//     }
//     // 시작 부분의 요소들
//     protected override SequentialElement StartElements => new
//     (
//         new ScreenOverlayFilm(Color.black, 0f),
//         new PlaceEnter(null, EPlaceID.GreenwoodIsland, null),
//         new CameraZoomByFactor(0.1f, 0f, Ease.InOutQuad), // 초기 카메라 이동 효과
//         new CameraMove2DByAngle(30, 20f),
//         new ParallelElement(
//             new CameraZoomByFactor(1f),
//             new ScreenOverlayFilm(Color.clear, 1f),
//             new CameraMove2DClear(1f)
//         )
//     );

//     // 스토리의 메인 업데이트 부분
//     protected override SequentialElement UpdateElements => new
//     (
//         new CharactersEnter(
//             new List<ECharacterID> { ECharacterID.Kate, ECharacterID.Lisa},
//             new List<float> { 0.33f, 0.66f },
//             new List<EEmotionID> { EEmotionID.Happy, EEmotionID.Smile },
//             new List<int> { 0, 0 },
//             2f,
//             Ease.OutQuad
//         ),
//         new Dialogue(
//             ECharacterID.Ryan,
//             new List<Line>
//             {
//                 new Line(EEmotionID.Happy, 0, "케이트, 생일 축하해! 오늘이 네 날이야!"),
//                 new Line(EEmotionID.Happy, 1, "네가 좋아하는 케이크도 준비했어. 다 같이 축하하자!"),
//             }
//         ),
//         new Dialogue(
//             ECharacterID.Lisa,
//             new List<Line>
//             {
//                 new Line(EEmotionID.Smile, 0, "맞아, 케이트! 오늘은 너를 위한 날이야."),
//                 new Line(EEmotionID.Smile, 1, "우리가 준비한 선물도 마음에 들었으면 좋겠어."),
//             }
//         ),
        
//         new ChoiceSetWithCorrectAnswer(
//             "케이트가 오늘 가장 좋아할 선물은 무엇일까?",
//             1, // 정답 인덱스
//             new List<ChoiceContent>
//             {
//                 new ChoiceContent(
//                     "평범한 꽃다발.",
//                     new SequentialElement
//                     (
//                         new Dialogue(
//                             ECharacterID.Kate,
//                             new Line(EEmotionID.Stumped, 0, "음... 꽃도 좋지만, 오늘은 좀 더 특별한 게 있었으면 좋겠어.")
//                         ),
//                         new Dialogue(
//                             ECharacterID.Lisa,
//                             new Line(EEmotionID.Normal, 2, "꽃이라니, 너무 과한거 아냐?")
//                         ),
//                         new Dialogue(
//                             ECharacterID.Ryan,
//                             new Line(EEmotionID.Normal, 2, "... 이건 좀 아니였나.")
//                         )
//                     )
//                 ),
//                 new ChoiceContent(
//                     "어릴 적부터 원했던 그림책.",
//                     new SequentialElement
//                     (
//                         new ParallelElement(
//                             new AllCharactersVisibility(.5f),
//                             new Dialogue(
//                                 ECharacterID.Kate,
//                                 new Line(EEmotionID.CryingHappy, 0, "이.. 이것은..!")
//                             ),
//                             new CutInEnter(
//                                 Kate_CryingHappy,
//                                 .3f,
//                                 1.4f,
//                                 Vector2.up * -1959
//                             ),
//                             new CameraShake(.5f)
//                         ),
//                         new ParallelElement(
//                             new AllCharactersVisibility(1f),
//                             new CutInClear(.3f)
//                         ),
//                         new Dialogue(
//                             ECharacterID.Kate,
//                             new Line(EEmotionID.CryingHappy, 0, "내가 이 그림책을 얼마나 찾았는지 알아? 정말 감동이야, 고마워!")
//                         ),
//                         new Dialogue(
//                             ECharacterID.Ryan,
//                             new Line(EEmotionID.Happy, 2, "맞아, 네가 좋아할 줄 알았어! 역시 이게 최고의 선물이네!")
//                         ),
//                         new Dialogue(
//                             ECharacterID.Lisa,
//                             new Line(EEmotionID.Smile, 1, "케이트가 좋아하니 우리도 기뻐!")
//                         )
//                     )
//                 ),
//                 new ChoiceContent(
//                     "아무런 포장 없이 건넨 초콜릿.",
//                     new SequentialElement
//                     (
//                         new Dialogue(
//                             ECharacterID.Kate,
//                             new Line(EEmotionID.Stumped, 0, "음... 이 초콜릿도 맛있어 보이지만, 오늘은 좀 더 특별한 게 필요해.")
//                         ),
//                         new Dialogue(
//                             ECharacterID.Lisa,
//                             new Line(EEmotionID.Normal, 2, "케이트는 초콜릿을 좋아하지 않는다구.")
//                         ),
//                         new Dialogue(
//                             ECharacterID.Ryan,
//                             new Line(EEmotionID.Normal, 0, "흠... 다시 한 번 생각해보자.")
//                         )
//                     )
//                 )
//             }
//         ),
//         new Dialogue(
//             ECharacterID.Kate,
//             new List<Line>
//             {
//                 new Line(EEmotionID.Happy, 0, "오늘 정말 즐거웠어. 모두 고마워!"),
//             }
//         )
//     );

//     // 종료 부분의 요소들
//     protected override SequentialElement ExitElements => new
//     (
//         new CharactersExit(
//             new List<ECharacterID> { ECharacterID.Kate, ECharacterID.Lisa, ECharacterID.Ryan },
//             1f,
//             Ease.InOutQuad
//         ),
//         new ScreenOverlayFilm(Color.black, 1f)
//     );
// }
