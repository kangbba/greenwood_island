using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StoryB : Story
{
    public override EStoryID StoryId => EStoryID.StoryB;


    public StoryB()
    {
        _elements = new List<Element>
        {
            new PlaceMove(EPlaceID.Mountain),
            new SFXEnter(SFXType.CreepyWhisper),
            new CameraMoveEffect(new Vector3(0, 0, -10), 1.0f, Ease.InOutQuad), // 초기 카메라 이동 효과
            new Dialogue(
                ECharacterID.Kate,
                new List<Line>
                {
                    new Line(EEmotionID.Smile, 0, "잘 싸웠어, 리사. 정말 대단했어."), // 칭찬하는 상황에 맞게 Smile로 변경
                    new Line(EEmotionID.Happy, 1, "하지만 다음엔 내가 더 잘할 거야."), // 긍정적인 다짐을 나타내기 위해 Happy 사용
                }
            ),
            new Dialogue(
                ECharacterID.Lisa,
                new List<Line>
                {
                    new Line(EEmotionID.Smile, 0, "케이트, 너도 잘했어."), // 가볍게 웃으며 격려하는 상황에 Smile
                    new Line(EEmotionID.Sad, 1, "이번엔 내가 이겼지만, 다음엔 더 준비할 거야."), // 결의에 찬 말투를 표현하기 위해 Determined 사용
                }
            ),
            new ChoiceElement(
                "케이트의 다음 발언은?",
                new List<ChoiceOption>
                {
                    new ChoiceOption(
                        "축하한다",
                        new List<Element>
                        {
                            new CameraZoomEffect(55f, 0.5f, Ease.InCubic), // 축하하며 약간의 줌인 효과
                            new CharacterMove(ECharacterID.Kate, 0.35f, 0.5f, Ease.OutQuad), // 케이트가 살짝 앞으로 이동
                            new Dialogue(
                                ECharacterID.Kate,
                                new List<Line>
                                {
                                    new Line(EEmotionID.Happy, 0, "축하해, 리사. 정말 멋졌어!"), // 축하할 때 자연스러운 Happy 감정
                                }
                            )
                        }
                    ),
                    new ChoiceOption(
                        "다음엔 내가 이길 거야",
                        new List<Element>
                        {
                            new CameraShakeEffect(0.3f, 1.5f, 10, 60f), // 도전적인 말과 함께 가벼운 흔들림 효과
                            new CharacterMove(ECharacterID.Kate, 0.3f, 0.5f, Ease.OutQuad), // 케이트가 약간 뒤로 물러남
                            new Dialogue(
                                ECharacterID.Kate,
                                new List<Line>
                                {
                                    new Line(EEmotionID.Angry, 0, "다음엔 내가 반드시 이길 거야."), // 다짐과 도전적인 상황을 나타내기 위해 Angry 사용
                                }
                            )
                        }
                    ),
                    new ChoiceOption(
                        "함께 훈련하자",
                        new List<Element>
                        {
                            new CameraZoomRestoreEffect(0.4f, Ease.OutSine), // 훈련 제안 시 줌 복원 효과
                            new CharacterMove(ECharacterID.Kate, 0.4f, 0.5f, Ease.OutSine), // 케이트가 원래 위치로 복귀
                            new Dialogue(
                                ECharacterID.Kate,
                                new List<Line>
                                {
                                    new Line(EEmotionID.Smile, 0, "우리 함께 훈련하자. 서로 더 강해질 수 있을 거야!"), // 훈련을 제안하며 Smile 감정 사용
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
        PlaceManager.Instance.InstantiatePlace(EPlaceID.Town2);
        PlaceManager.Instance.ShowPlace(EPlaceID.Town2, 1.0f, Ease.InOutQuad);
        new PlaceMove(EPlaceID.Forest).Execute();
        // 스토리 진행 단계
        foreach (var element in _elements)
        {
            yield return element.ExecuteRoutine();
        }
        Debug.Log($"Story {StoryId} OnStory completed.");
    }

}
