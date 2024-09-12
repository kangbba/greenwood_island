
// using UnityEngine;

// public class TestStory : Story
// {
//     // TestStory 스토리의 스크립트 로직을 여기에 작성하세요.
//     protected override SequentialElement StartElements => new ();

//     protected override SequentialElement UpdateElements => new (

//         new UserAction(
//             new UserActionParameter(
//                 UserActionType.Gift, 
//                 new SequentialElement(
//                     new Dialogue(
//                         "Mono",
//                          new Line(EEmotionID.Normal, 0, "선물을 줘야겠다")
//                     )
//                 ) 
//             ),
//             new UserActionParameter(
//                 UserActionType.Talk, 
//                 new SequentialElement(
//                     new Dialogue(
//                         "Mono",
//                          new Line(EEmotionID.Normal, 0, "말을 해야겠다")
//                     )
//                 ) 
//             ),
//             new UserActionParameter(
//                 UserActionType.Search, 
//                 new SequentialElement(
//                     new Dialogue(
//                         "Mono",
//                          new Line(EEmotionID.Normal, 0, "조사를 해야겠다")
//                     )
//                 ) 
//             )
//         ) 
//     );

//     protected override SequentialElement ExitElements => new ();

//     protected override string StoryDesc => "";
// }
