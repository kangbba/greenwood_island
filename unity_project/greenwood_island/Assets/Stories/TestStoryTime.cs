using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TestStoryTime : Story
{
    public override EStoryID StoryId => EStoryID.TestStoryTime;
    protected override string StoryDesc => "";

    // Start 단계의 Elements를 정의
    protected override SequentialElement StartElements => new
    (
        new PlaceEnter( 
            new SequentialElement(
                new ScreenOverlayFilm(Color.black, 1f)
            ),
            EPlaceID.RyanRoom, 
            new SequentialElement(
                new PlaceFilm(ColorUtils.CustomColor("3244BF"), 0f)
            )
        ), 
        new ParallelElement(
            new ScreenOverlayFilmClear(3f)
        )
    );

    // Update 단계의 Elements를 정의
    protected override SequentialElement UpdateElements => new
    (
        new Dialogue(
            ECharacterID.Mono, // 독백 캐릭터 이름을 Mono로 설정
            new List<Line>
            {
                new Line(EEmotionID.Normal, 0, "지금은 밤 12시.")
            }
        ),
        new PlaceFilm(ColorUtils.CustomColor("3244BF"), 2f),
        new Dialogue(
            ECharacterID.Mono, // 독백 캐릭터 이름을 Mono로 설정
            new List<Line>
            {
                new Line(EEmotionID.Normal, 0, "새벽이 되었다.", 10)
            }
        ),
        new PlaceFilm(ColorUtils.CustomColor("628AFF"), 2f),
        new Dialogue(
            ECharacterID.Mono, // 독백 캐릭터 이름을 Mono로 설정
            new List<Line>
            {
                new Line(EEmotionID.Normal, 0, "동이 틀랑말랑 하고있다.")
            }
        ),
        new PlaceFilm(ColorUtils.CustomColor("90B1FF"), 2f),
        new Dialogue(
            ECharacterID.Mono, // 독백 캐릭터 이름을 Mono로 설정
            new List<Line>
            {
                new Line(EEmotionID.Normal, 0, "밤샜다.")
            }
        ),
        new SFXEnter(SFXType.BirdsChirping, true, 2f), 
        new PlaceFilm(ColorUtils.CustomColor("FFFFFF"), 2f),
        new Dialogue(
            ECharacterID.Mono, // 독백 캐릭터 이름을 Mono로 설정
            new List<Line>
            {
                new Line(EEmotionID.Normal, 0, "아침이 되었다.")
            }
        ),
        new SFXExit(SFXType.BirdsChirping), 
        new Dialogue(
            ECharacterID.Mono, // 독백 캐릭터 이름을 Mono로 설정
            new List<Line>
            {
                new Line(EEmotionID.Normal, 0, "근데...")
            }
        ),
        new ParallelElement(
            new SFXEnter(SFXType.CreepyWhisper, false), 
            new SFXEnter(SFXType.CreepyTicking, true, 0f), 
            new CameraMove2D(new Vector2(0, -293f), .01f, Ease.OutQuad),
            new CameraShake(.3f, 3f),
            new PlaceFilm(ColorUtils.CustomColor("E5341B"), .01f, Ease.OutQuad),
            new CameraZoom(0.6f, .1f, Ease.OutQuad),
            new Dialogue(
                ECharacterID.Mono, // 독백 캐릭터 이름을 Mono로 설정
                new List<Line>
                {
                    new Line(EEmotionID.Normal, 0, "침대 밑에 숨어있는 사람 누구야..?", 60)
                }
            )
        ),
        new PlaceFilm(ColorUtils.CustomColor("A60021"), .15f),
        new FXEnter(FXType.BloodDrip, 2f)
    );

    protected override SequentialElement ExitElements => new (
        new ScreenOverlayFilm(Color.black)
    );
}
