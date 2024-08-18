using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Story : MonoBehaviour
{
    // 여러 개의 Element 리스트를 전역적으로 선언 및 초기화
    private List<Element> _elements = new List<Element>()
    {
        // 필름 화이트 아웃 -> 필름 화이트 인 (순차적으로 실행)
        new PlaceMove(
            new List<PlaceTransition>
            {
                new PlaceTransition(EPlaceID.Town1, PlaceAnimType.ScreenFilmWhiteOut, 1f, Ease.OutQuad),
                new PlaceTransition(EPlaceID.Town2, PlaceAnimType.ScreenFilmWhiteIn, 1f, Ease.OutQuad),
            }
        ),

        // 케이트가 중앙에 등장
        new CharactersEnter(
            new List<ECharacterID> { ECharacterID.Kate },
            new List<float> { 0.5f },
            1f, Ease.OutQuad
        ),

        // 케이트 대사
        new Dialogue(
            ECharacterID.Kate,
            new List<Line>
            {
                new Line(EEmotionID.Panic, 0, "여기는... 어디지?"),
                new Line(EEmotionID.Panic, 1, "모든 것이 낯설어... 뭔가 잘못되고 있어!")
            }
        ),

        // 필름 블랙 아웃 -> 실제 장소의 블랙 아웃 -> 블랙 인 (Simultaneous 실행)
        new PlaceMove(
            new List<PlaceTransition>
            {
                new PlaceTransition(EPlaceID.Town2, PlaceAnimType.ScreenFilmBlackOut, 1f, Ease.OutQuad),
                new PlaceTransition(EPlaceID.Town2, PlaceAnimType.Blackout, 1f, Ease.OutQuad),
                new PlaceTransition(EPlaceID.Town1, PlaceAnimType.BlackIn, 1f, Ease.OutQuad),
            }
        ),

        // 리사가 등장하고, 케이트가 왼쪽으로 이동
        new CharactersEnter(
            new List<ECharacterID> { ECharacterID.Lisa },
            new List<float> { 0.7f },
            1f, Ease.OutQuad
        ),
        new CharacterMove(
            ECharacterID.Kate,
            0.3f,
            1f, Ease.OutQuad
        ),

        // 리사 대사
        new Dialogue(
            ECharacterID.Lisa,
            new List<Line>
            {
                new Line(EEmotionID.Smile, 0, "케이트, 무슨 일이 있었던 거야?"),
                new Line(EEmotionID.Normal, 1, "여기서 나가야 해!")
            }
        ),

        // 리사와 케이트가 함께 퇴장 (ZoomOut)
        new CharactersExit(
            new List<ECharacterID> { ECharacterID.Lisa, ECharacterID.Kate },
            1f, Ease.InQuad
        ),

        // 최종 장면 전환 (Blackout -> WhiteIn)
        new PlaceMove(
            new List<PlaceTransition>
            {
                new PlaceTransition(EPlaceID.Town2, PlaceAnimType.Blackout, 1f, Ease.OutQuad),
                new PlaceTransition(EPlaceID.Town1, PlaceAnimType.ScreenFilmWhiteIn, 1f, Ease.OutQuad),
            }
        )
    };

    public List<Element> GetElements()
    {
        return _elements;
    }
}
