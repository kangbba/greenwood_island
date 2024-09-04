using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StoryD : Story
{
    public override EStoryID StoryId => EStoryID.StoryD;

    // Start 단계의 Elements를 정의
    protected override List<Element> StartElements => new List<Element>
    {
        new PlaceEnter( 
            new SequentialElement(
                new ScreenOverlayFilm(Color.black, 1f)
            ),
            EPlaceID.RyanRoom, 
            new SequentialElement(
                new PlaceFilm(ColorUtils.CustomColor("8696FF"), 0f),
                new ScreenOverlayFilmClear()
            )
        ), 
        new SFXEnter(SFXType.BirdsChirping, true, 2f), // 창밖의 새소리가 바람을 타고 들어옴
        new ParallelElement(
            new CameraMove2DByAngle(45, 3f), // 카메라가 서서히 창문을 향해 움직임
            new ScreenOverlayFilmClear(3f)
        ),
    };

    // Update 단계의 Elements를 정의
    protected override List<Element> UpdateElements => new List<Element>
    {
        new Dialogue(
            ECharacterID.Mono, // 독백 캐릭터 이름을 Mono로 설정
            new List<Line>
            {
                new Line(EEmotionID.Normal, 0, "새벽. 새소리가 이렇게 선명하게 들릴 줄은 몰랐다. 불과 몇 주 전까지만 해도 기자로 뛰던 나. 그 시절, 이런 아침을 언제 느껴봤더라."),
                new Line(EEmotionID.Normal, 1, "늘 야근에 취재에, 기계음에 찌든 공기였지. 이런 섬마을에 와서야 이게 진짜 새벽이구나 싶다."),
                new Line(EEmotionID.Normal, 2, "평화롭다… 너무, 평화로워서 문제지."),
            }
        ),
        new PlaceEnter(
            new SequentialElement(
                new PlaceFilm(Color.clear, 1f)
            ),
            EPlaceID.RyanRoom, // 창문 앞의 새로운 시점
            new SequentialElement(
                new PlaceFilm(Color.clear, 0f),
                new PlaceFilmClear(2f)
            )
        ),
        new Dialogue(
            ECharacterID.Mono, // 독백 캐릭터 이름을 Mono로 설정
            new List<Line>
            {
                new Line(EEmotionID.Normal, 0, "레이첼이 왜 나를 여기로 부른 걸까? 물론, 그녀는 친구였고, 내가 바닥으로 떨어졌을 때 손을 내밀어 준 사람이다."),
                new Line(EEmotionID.Normal, 1, "하지만 그냥 쉬라고? 이 평화로운 섬에서? 나한테 숨을 틈을 주겠다고? 아무리 그래도, 그게 전부는 아니겠지."),
                new Line(EEmotionID.Normal, 2, "아니, 그래선 안 된다. 사람은 다들 숨기고 있는 게 있는 법이니까."),
            }
        )
    };
}
