using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StoryA : Story
{
    public override string StoryId => "StoryA";

    protected override IEnumerator OnEnterPlace()
    {
        // 스토리 시작 시 장소 초기화
        PlaceManager.Instance.InstantiatePlace(EPlaceID.Forest);
        PlaceManager.Instance.ShowPlace(EPlaceID.Forest, 1.0f, Ease.InOutQuad);
        Debug.Log($"Story {StoryId} OnEnterPlace: Place Town1 initialized.");
        yield return null;
    }

    public StoryA()
{
    _elements = new List<Element>
    {
        new SFXEnter(SFXType.CreepyWhisper, true, 0.5f),
        new CameraMoveEffect(new Vector3(7, 0, 10), 0f, Ease.InOutQuad), // 초기 카메라 이동 효과
        new CameraMoveEffect(new Vector3(0, 0, -10), 5f, Ease.InOutQuad), // 초기 카메라 이동 효과
        new CharactersEnter(new List<ECharacterID> { ECharacterID.Kate, ECharacterID.Lisa }, new List<float> { 0.33f, 0.66f }, new List<EEmotionID> { EEmotionID.Angry, EEmotionID.Stumped },
        new List<int> { 0, 0 }, 2f, Ease.OutQuad),
        new Dialogue(
            ECharacterID.Kate,
            new List<Line>
            {
                new Line(EEmotionID.Angry, 0, "리사, 모든 걸 뺏어간 네가... 널 이대로 두고 볼 순 없어."),
                new Line(EEmotionID.Angry, 1, "너의 잘난 척, 네가 나보다 더 나은 삶을 산다는 게... 용납할 수 없어."),
            }
        ),
        new Dialogue(
            ECharacterID.Lisa,
            new List<Line>
            {
                new Line(EEmotionID.Smile, 0, "또 그 얘기야? 케이트, 나한테 화내봤자 달라질 건 없어."),
                new Line(EEmotionID.Panic, 1, "넌 늘 내 그림자였어. 그걸 인정하는 게 왜 그렇게 힘들어?"),
            }
        ),
        new ChoiceElement(
            "케이트의 다음 행동은?",
            new List<ChoiceOption>
            {
                new ChoiceOption(
                    "리사에게 과거의 사건을 들먹인다.",
                    new List<Element>
                    {
                        new Dialogue(
                            ECharacterID.Kate,
                            new List<Line>
                            {
                                new Line(EEmotionID.Angry, 2, "머리를 노리면, 네 잘난 척하는 얼굴... 더는 볼 필요 없을 거야."),
                            }
                        ),
                        new CharacterMove(ECharacterID.Kate, 0.38f, 0.5f, Ease.OutQuad), // 케이트가 왼쪽으로 이동
                        new CameraShakeEffect(),
                        new PlaceOverlayFilmEffect(Color.black.ModifiedAlpha(.5f), 1f),
                        new FXEnter(FXType.BloodDrip, 3f),
                        new Dialogue(
                            ECharacterID.Lisa,
                            new List<Line>
                            {
                                new Line(EEmotionID.Sad, 0, "너... 정말 나를 이렇게까지 증오하는 거야?"),
                            }
                        ),
                        new FXExit(FXType.BloodDrip)
                    }
                ),
                new ChoiceOption(
                    "리사의 몸을 노린다.",
                    new List<Element>
                    {
                        new CameraZoomEffect(50f, 0.5f, Ease.OutBounce), // 몸을 노리는 선택 시 카메라 줌 효과
                        new CharacterMove(ECharacterID.Kate, 0.3f, 0.5f, Ease.InOutSine), // 케이트가 약간 오른쪽으로 이동
                        new Dialogue(
                            ECharacterID.Kate,
                            new List<Line>
                            {
                                new Line(EEmotionID.Angry, 1, "너를 다치게 하면... 아마 내가 좀 더 나아질지도 몰라."),
                                new Line(EEmotionID.Angry, 1, "네 잘난 척하는 얼굴... 더는 볼 필요 없을 거야."),
                            }
                        ),
                        new PlaceOverlayFilmEffect(Color.black.ModifiedAlpha(.5f), .5f),
                        new CharacterMove(ECharacterID.Kate, 0.38f, 0.5f, Ease.OutQuad), // 케이트가 왼쪽으로 이동
                        new CameraShakeEffect(),
                        new PlaceEffect(Color.red.ModifiedAlpha(.5f), .25f),
                        new PlaceOverlayFilmEffect(Color.black.ModifiedAlpha(.7f), .25f),
                        new FXEnter(FXType.BloodDrip, 3f),
                        new Dialogue(
                            ECharacterID.Lisa,
                            new List<Line>
                            {
                                new Line(EEmotionID.Sad, 0, "너... 정말... 나를 이렇게까지..."),
                            }
                        ),
                        new PlaceOverlayFilmEffect(Color.black.ModifiedAlpha(.9f), .5f),
                        new ScreenOverlayFilmEffect(Color.black, 3f)
                    }
                ),
                new ChoiceOption(
                    "라이언을 부른다.",
                    new List<Element>
                    {
                        new CameraZoomRestoreEffect(0.5f, Ease.InOutSine), // 방어 선택 시 카메라 줌 복원 효과
                        new CharacterMove(ECharacterID.Kate, 0.33f, 0.5f, Ease.OutBack), // 케이트가 원래 위치로 복귀
                        new Dialogue(
                            ECharacterID.Kate,
                            new List<Line>
                            {
                                new Line(EEmotionID.Smile, 0, "이러다 정말 끝날지도 모르겠네... 그런데 이게..."),
                                new Line(EEmotionID.Smile, 1, "진짜 네가 원하는 거야, 리사?"),
                            }
                        )
                    }
                )
            }
        ),
        new SFXExit(SFXType.CreepyWhisper),
    };
}


    protected override IEnumerator OnStory()
    {
        // 스토리 진행 단계
        foreach (var element in _elements)
        {
            yield return element.Execute();
        }
        Debug.Log($"Story {StoryId} OnStory completed.");
    }

    protected override IEnumerator OnExitStory()
    {
        // 스토리 종료 작업
        Debug.Log($"Story {StoryId} finished.");
        yield return null;
    }
}