using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Story1 : Story
{
    protected override string StoryDesc => "";

    /*라이언(독백):
        파도 소리가 귓가에 부딪친다. 배는 느리게 바다 위를 가로지른다. 
        찬 바람이 옷깃을 스치고, 바닷물 냄새가 코끝을 간질인다. 
        그린우드. 이렇게 고요한 섬마을에 내가 오게 될 줄이야. 
        도시의 소음에서 벗어나니 모든 게 낯설다. 
        레이첼의 초대가 아니었다면 이곳에 올 일은 없었을 거다. 그런데… 지금 이곳에 있는 게 맞는 걸까?

        머릿속이 복잡하다. 떠나온 도시와, 그곳에서 벌어진 일들. 그때 무언가 잘못됐다는 걸 알았지만 이미 늦었었다. 
        어쩌면 내가 너무 멀리 갔던 걸까? 진실이라고 믿었던 것들이 날 어디로 데려온 걸까. 
        가슴 한편엔 아직도 떨쳐내지 못한 무언가가 남아 있다. 멈춰 서고 싶어도 멈출 수 없던 날들이 계속 떠오른다.

        저 멀리 그린우드의 모습이 보인다. 언덕 위로 작은 집들이 다닥다닥 붙어 있다. 
        낡은 벽돌과 흐릿한 창문들. 시간이 멈춘 듯, 평화롭지만 어딘가 아슬아슬하다. 
        배가 부두에 가까워질수록 풍경은 더 뚜렷해진다. 그저 조용하고 오래된 마을일 뿐이겠지, 아마도.

        배가 멈추고, 천천히 발을 내딛는다. 나무로 된 부두가 삐걱거린다. 짭조름한 바다 냄새, 오래된 나무 냄새. 
        고요함이 온몸을 감싼다. 마치 무언가가 숨을 죽이고 기다리는 듯한 느낌. 
        이곳이 나를 받아줄까? 아니면 나에게서 무언가를 빼앗으려는 걸까? 
        여기에 와 있는 게 옳은 선택인지 모르겠다.

        마을은 고요하다. 길 위엔 사람의 그림자도 보이지 않는다. 
        멀리서 바람에 흔들리는 나무들, 오래된 골목길 사이로 비치는 희미한 햇살. 
        너무도 평화로워 보이지만, 그 안에 감춰진 무언가가 있을지도 모른다는 생각이 든다. 
        여기에 숨겨진 건 뭘까. 그리고 나는 이곳에서 무엇을 찾아야 하는 걸까.

        섬마을 그린우드의 고요한 풍경은 대답하지 않는다. 
        하지만, 어딘가에서 무언가가 나를 기다리고 있을 것 같은 기분을 지울 수가 없다.   */

    // 시작 부분의 요소들
    protected override SequentialElement StartElements => new
    (
    );

    // 스토리의 메인 업데이트 부분
    protected override SequentialElement UpdateElements => new
    (
        new SFXEnter("SurgeryBeep", false, 0f),
        new Dialogue(
            ECharacterID.Ryan,
            new List<Line>
            {
                new Line(EEmotionID.Normal, 0, "의료실의 희미한 형광등이 깜빡인다. 차가운 침대에 내 몸이 묶여 있다."),
                new Line(EEmotionID.Normal, 0, "심장 박동 소리가 점점 느려진다. 숨이 막혀온다."),
                new Line(EEmotionID.Normal, 0, "몸이 무겁고, 가슴이 찢어질 듯 아프다. 마치 물속에 가라앉는 기분이다."),
            }
        ),

        new SFXEnter("SurgeryBeep", false, 0f),
        new Dialogue(
            ECharacterID.Doctor,
            new List<Line>
            {
                new Line(EEmotionID.Normal, 0, "심박수 45. 전압 올려, 제세동 준비해!"),
                new Line(EEmotionID.Normal, 0, "아직 반응이 없다. 에피네프린 투여 준비해."),
            }
        ),

        new SFXEnter("SurgeryBeep", false, 0f),
        new Dialogue(
            ECharacterID.Nurse,
            new List<Line>
            {
                new Line(EEmotionID.Normal, 0, "심박수 계속 떨어집니다. 혈압 60에 40, 반응이 없습니다!"),
                new Line(EEmotionID.Normal, 0, "에피네프린 1밀리그램 투여 완료. 아직도 반응이 미약합니다."),
            }
        ),
        new SFXEnter("SurgeryBeep", false, 0f),
        new Dialogue(
            ECharacterID.Ryan,
            new List<Line>
            {
                new Line(EEmotionID.Normal, 0, "의사의 목소리는 차갑고 단호하다. 명령이 떨어지고, 나는 마치 실험대 위에 있는 기분이다."),
                new Line(EEmotionID.Normal, 0, "간호사의 목소리가 떨린다. 나도 몸이 떨리고, 심장은 점점 더 느리게 뛴다."),
                new Line(EEmotionID.Normal, 0, "전기 충격이 또 한 번 내 가슴을 강타한다. 몸이 격렬하게 떨리며 심장이 미친 듯이 뛴다."),
                new Line(EEmotionID.Normal, 0, "숨이 막히고, 공기가 목을 조여온다. 나는 이대로 끝나는 건가?"),
                new Line(EEmotionID.Normal, 0, "눈앞이 흐려지고, 심장 소리는 점점 더 멀어져 간다. 이대로 멈출 것만 같다."),
            }
        )

    );

    // 종료 부분의 요소들
    protected override SequentialElement ExitElements => new
    (
    );
}
