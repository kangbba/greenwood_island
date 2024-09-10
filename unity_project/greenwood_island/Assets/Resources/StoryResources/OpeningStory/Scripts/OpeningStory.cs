using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OpeningStory : Story
{
    protected override string StoryDesc => "";

/*라이언(독백):
    “…치지직… 북위 47도… 동경… 관찰 중… 주시…”

    (배는 좌우로 휘청거리고, 선장은 조타를 잡고 불안하게 시야를 주시한다. 승객들은 서로 눈치를 보며 긴장한 기색이다.)

    라이언(독백):
    배 안은 어쩐지 숨 막힐 듯한 분위기다. 이 작은 배에는 나 외에 몇 명밖에 없다. 선장은 표정이 굳어 있고, 내가 기댈 만한 사람이라고는 없다.

    (노인은 선실 구석에 앉아, 아무 말 없이 파도만 바라보며 가끔씩 눈을 감는다. 고요한 바다보다 더 깊이 잠겨 있는 듯하다. 라이언은 그 노인의 행동이 어딘지 모르게 불안하게 느껴진다.)

    라이언(독백):
    저 노인… 어쩐지 이상하게 조용하군. 아까부터 한마디도 하지 않고, 그냥 바다만 바라보는 그 시선… 뭔가가 안 맞아.

    (배는 또 한 번 크게 흔들리며, 멀리서 갑자기 쿵 하는 소리가 들린다. 그때마다 양복을 입은 신사와 그의 아내는 서로를 꽉 붙잡고 있다. 부인은 속삭이며 뭔가를 걱정스레 말하고, 신사는 신경질적인 눈빛으로 선장을 바라본다.)

    양복 입은 신사(낮은 목소리, 불안함이 묻어남):
    "여보, 그냥 좀 앉아 있어. 괜찮을 거야… 금방 도착할 거야."

    라이언(독백):
    그들은 마치 긴 여행을 떠나는 부부처럼 보이지만, 어쩐지 평범해 보이지 않는다. 아내의 떨리는 손과 신사의 불안한 눈빛이 전혀 어울리지 않는다. 왜 이런 작은 배에 탔을까?

    (라디오에서 들려오는 소리는 계속 불안정하다. 잡음 속에서 단편적인 단어들이 간헐적으로 들리며, 선장은 귀를 기울이려 하다 이내 고개를 저으며 조타를 조절한다. 교신은 계속되지만 누구도 그 정체를 묻지 않는다.)

    해적 라디오 음성(어둡고 불안정한 음성):
    “…경로… 접근 중… 보고… 대기…”

    (선장은 혼란스러운 표정을 지우지 못하고, 배는 여전히 흔들리며 길을 찾지 못하는 듯한 불안한 항해를 이어간다. 교신이 배를 삼킬 듯 끊임없이 울리고, 안개는 점점 더 짙어진다.)

    선장의 목소리(작은 한숨을 내쉬며):
    "계속해서 이런 신호가 잡히면 큰일이군… 여기서 빠져나가야 하는데…"

    라이언(독백):
    나와 같은 배에 타 있는 사람들, 그리고 이 어딘가 묘하게 어긋난 분위기. 여기 있는 모두가 불안해하고 있다는 걸 느낄 수 있다. 이건 그냥 항해가 아니다. 무언가… 잘못된 게 분명하다.

    [장면 전환: 안개가 걷히며 드러나는 그린우드 섬의 압도적인 경관]

    (배는 점차 안정을 찾고, 안개가 서서히 걷히며 그린우드 섬의 고요한 전경이 모습을 드러낸다. 평화로운 해안과 오래된 건물들이 마치 시간이 멈춘 듯 라이언의 눈앞에 펼쳐진다. 방금 전의 긴장은 거짓말처럼 사라지지만, 남은 여운은 가시지 않는다.)

    라이언(독백):
    섬에 도착했지만, 어딘지 모르게 편치 않다. 이곳이 정말 평범한 곳일까? 교신 속 불길한 목소리가 귓가에 남아 맴돈다.  */

    // 시작 부분의 요소들
    protected override SequentialElement StartElements => new
    (
    );

    
    // 스토리의 메인 업데이트 부분
    protected override SequentialElement UpdateElements => new
    (
        new PlaceEnter("port_outside_2"),
        new SFXEnter("Waves", 0.25f, true, 0f),
        new Dialogue(
            "라디오",
            new List<Line>
            {
                new Line(EEmotionID.Normal, 0, "…치지직, 북위 47도. 동경..? 관찰 중… 주시…"),
                new Line(EEmotionID.Normal, 0, "…치지직… 북위 48도… 동경… 관찰 중… 주시…"),
            }
        )
        // new Dialogue(
        //     ECharacterID.Ryan,
        //     new List<Line>
        //     {
        //         new Line(EEmotionID.Normal, 0, "배는 좌우로 휘청거리고, 선장은 조타를 잡고 불안하게 시야를 주시한다."),
        //         new Line(EEmotionID.Normal, 0, "승객들은 서로 눈치를 보며 긴장한 기색이다."),
        //         new Line(EEmotionID.Normal, 0, "배 안은 어쩐지 숨 막힐 듯한 분위기다."),
        //         new Line(EEmotionID.Normal, 0, "이 작은 배에는 나 외에 몇 명밖에 없다."),
        //         new Line(EEmotionID.Normal, 0, "선장은 표정이 굳어 있고, 내가 기댈 만한 사람이라고는 없다."),
        //         new Line(EEmotionID.Normal, 0, "노인은 선실 구석에 앉아, 아무 말 없이 파도만 바라보며 가끔씩 눈을 감는다."),
        //         new Line(EEmotionID.Normal, 0, "고요한 바다보다 더 깊이 잠겨 있는 듯하다."),
        //         new Line(EEmotionID.Normal, 0, "저 노인… 어쩐지 이상하게 조용하군. "),
        //         new Line(EEmotionID.Normal, 0, "아까부터 한마디도 하지 않고, 그냥 바다만 바라보는 그 시선… 뭔가가 안 맞아."),
        //     }
        // ),
        // new Dialogue(
        //     ECharacterID.Ryan,
        //     new List<Line>
        //     {
        //         new Line(EEmotionID.Normal, 0, "배는 또 한 번 크게 흔들리며, 멀리서 갑자기 쿵 하는 소리가 들린다."),
        //         new Line(EEmotionID.Normal, 0, "그때마다 양복을 입은 신사와 그의 아내는 서로를 꽉 붙잡고 있다. 부인은 속삭이며 뭔가를 걱정스레 말하고, 신사는 신경질적인 눈빛으로 선장을 바라본다."),
        //         new Line(EEmotionID.Normal, 0, "양복 입은 신사(낮은 목소리, 불안함이 묻어남): 여보, 그냥 좀 앉아 있어. 괜찮을 거야… 금방 도착할 거야."),
        //         new Line(EEmotionID.Normal, 0, "그들은 마치 긴 여행을 떠나는 부부처럼 보이지만, 어쩐지 평범해 보이지 않는다. 아내의 떨리는 손과 신사의 불안한 눈빛이 전혀 어울리지 않는다. 왜 이런 작은 배에 탔을까?"),
        //     }
        // ),
        // new Dialogue(
        //     ECharacterID.Radio,
        //     new List<Line>
        //     {
        //         new Line(EEmotionID.Normal, 0, "경로… 접근 중… 보고… 대기…"),
        //     }
        // ),
        // new Dialogue(
        //     ECharacterID.Captain,
        //     new List<Line>
        //     {
        //         new Line(EEmotionID.Normal, 0, "계속해서 이런 신호가 잡히면 큰일이군… 여기서 빠져나가야 하는데…"),
        //     }
        // ),
        // new Dialogue(
        //     ECharacterID.Ryan,
        //     new List<Line>
        //     {
        //         new Line(EEmotionID.Normal, 0, "나와 같은 배에 타 있는 사람들, 그리고 이 어딘가 묘하게 어긋난 분위기. 여기 있는 모두가 불안해하고 있다는 걸 느낄 수 있다."),
        //         new Line(EEmotionID.Normal, 0, "이건 그냥 항해가 아니다. 무언가… 잘못된 게 분명하다."),
        //     }
        // ),
        // new SceneTransition("FadeOut", 1.0f),
        // new Dialogue(
        //     ECharacterID.Ryan,
        //     new List<Line>
        //     {
        //         new Line(EEmotionID.Normal, 0, "배는 점차 안정을 찾고, 안개가 서서히 걷히며 그린우드 섬의 고요한 전경이 모습을 드러낸다."),
        //         new Line(EEmotionID.Normal, 0, "평화로운 해안과 오래된 건물들이 마치 시간이 멈춘 듯 라이언의 눈앞에 펼쳐진다. 방금 전의 긴장은 거짓말처럼 사라지지만, 남은 여운은 가시지 않는다."),
        //         new Line(EEmotionID.Normal, 0, "섬에 도착했지만, 어딘지 모르게 편치 않다. 이곳이 정말 평범한 곳일까? 교신 속 불길한 목소리가 귓가에 남아 맴돈다."),
        //     }
        // )
    );


    // 종료 부분의 요소들
    protected override SequentialElement ExitElements => new
    (
    );
}
