using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Story1 : Story
{
    public override EStoryID StoryId => EStoryID.Story1;
    protected override string StoryDesc => "라이언은 평화로운 그린우드에 도착하지만, 업무로 바쁜 레이첼의 지시에 따라 저녁까지 홀로 마을에서 시간을 보내게 된다.";

    /*라이언(독백):
        배가 항구에 닿자, 그린우드의 작은 부두가 모습을 드러낸다. 햇살이 잔잔히 내려앉은 물결. 한적하고 고요한 풍경이 펼쳐진다. 도시의 소란과는 전혀 다른, 시간이 멈춘 것 같은 이곳. 레이첼의 초대 덕에 오게 됐지만, 이렇게 조용한 곳이라니… 이상하리만치 평화롭다.
    */

    // 시작 부분의 요소들
    protected override SequentialElement StartElements => new
    (
    );

    // 스토리의 메인 업데이트 부분
    protected override SequentialElement UpdateElements => new
    (
        new Dialogue(
            ECharacterID.Ryan,
            new List<Line>
            {
                new Line(EEmotionID.Normal, 0, "배가 항구에 닿자, 그린우드의 작은 부두가 모습을 드러낸다."),
                new Line(EEmotionID.Normal, 0, "햇살이 잔잔히 내려앉은 물결. 한적하고 고요한 풍경이 펼쳐진다."),
                new Line(EEmotionID.Normal, 0, "도시의 소란과는 전혀 다른, 시간이 멈춘 것 같은 이곳."),
                new Line(EEmotionID.Normal, 0, "레이첼의 초대 덕에 오게 됐지만, 이렇게 조용한 곳이라니… 이상하리만치 평화롭다."),
            }
        ),
        new Dialogue(
            ECharacterID.Ryan,
            new List<Line>
            {
                new Line(EEmotionID.Normal, 0, "항구에서 천천히 내려 부두를 걸으며 주위를 둘러본다."),
                new Line(EEmotionID.Normal, 0, "부두에는 낡은 배들이 몇 척 정박해 있고, 오래된 기운이 감돈다."),
                new Line(EEmotionID.Normal, 0, "복잡한 머릿속이 잠시나마 비워지는 듯한 기분. 도시에서 느끼지 못했던 이 공기, 이 적막함."),
                new Line(EEmotionID.Normal, 0, "조금은 위안이 되기도 한다."),
            }
        ),
        new Dialogue(
            ECharacterID.Ryan,
            new List<Line>
            {
                new Line(EEmotionID.Normal, 0, "폰을 꺼내 레이첼에게 전화를 건다. 부서 이동 후에도 여전히 바쁘게 일하는 그녀."),
                new Line(EEmotionID.Normal, 0, "나보다 항상 한 발짝 앞서 있었던 사람. 벨이 몇 번 울리자마자 익숙한 목소리가 들려온다."),
            }
        ),
        new Dialogue(
            ECharacterID.Rachel,
            new List<Line>
            {
                new Line(EEmotionID.Normal, 0, "라이언, 도착했네요? 배는 편하게 타고 왔어요?"),
            }
        ),
        new Dialogue(
            ECharacterID.Ryan,
            new List<Line>
            {
                new Line(EEmotionID.Normal, 0, "여전한 목소리다. 차분하고, 자신감 넘치고, 늘 상황을 장악하고 있다는 느낌."),
                new Line(EEmotionID.Normal, 0, "일에 대한 이야기를 꺼내지 않아도, 그녀가 늘 중심에 서 있다는 걸 느끼게 한다."),
            }
        ),
        new Dialogue(
            ECharacterID.Ryan,
            new List<Line>
            {
                new Line(EEmotionID.Normal, 0, "네, 잘 도착했습니다. 여긴 참 조용하네요. 이렇게 평화로운 곳이라니…"),
            }
        ),
        new Dialogue(
            ECharacterID.Rachel,
            new List<Line>
            {
                new Line(EEmotionID.Normal, 0, "그렇죠? 그린우드는 늘 이래요. 복잡한 도시에서 벗어나 쉬기에 딱 좋죠."),
                new Line(EEmotionID.Normal, 0, "그런데 제가 오늘 오전부터 일정이 조금 길어질 것 같아요. 회사 일도 있고… 저녁에나 볼 수 있을 것 같네요."),
                new Line(EEmotionID.Normal, 0, "그때까지 좀 기다려줘요."),
            }
        ),
        new Dialogue(
            ECharacterID.Ryan,
            new List<Line>
            {
                new Line(EEmotionID.Normal, 0, "저녁까지? 여기서? 내가 이곳에 오게 된 이유를 생각하면, 좀 갑작스럽긴 하지만…"),
                new Line(EEmotionID.Normal, 0, "레이첼이 일이 바쁘다면 어쩔 수 없지. 항상 그랬으니까. 일 중심적인 그녀는 쉽게 흐트러지지 않는다."),
            }
        ),
        new Dialogue(
            ECharacterID.Ryan,
            new List<Line>
            {
                new Line(EEmotionID.Normal, 0, "알겠습니다. 그러면 저녁에 뵐게요. 그때까지 뭐라도 하면서 시간을 보내겠습니다."),
            }
        ),
        new Dialogue(
            ECharacterID.Rachel,
            new List<Line>
            {
                new Line(EEmotionID.Normal, 0, "좋아요. 이곳저곳 둘러보면서 이 마을의 분위기를 느껴보는 것도 좋을 거예요."),
                new Line(EEmotionID.Normal, 0, "그럼 이따 봐요. 늦어도 저녁 7시 전엔 끝낼게요."),
            }
        ),
        new Dialogue(
            ECharacterID.Ryan,
            new List<Line>
            {
                new Line(EEmotionID.Normal, 0, "항상 이렇다. 계획적이고, 확신에 찬 태도. 그녀가 주도하고, 난 따라가는 방식."),
                new Line(EEmotionID.Normal, 0, "이상한 건 아닌데, 어쩐지 묘하게 느껴진다. 여기에 와 있으면서도 나는 아직 어디에도 속하지 않은 느낌."),
                new Line(EEmotionID.Normal, 0, "한가로운 이곳에서 나는 무엇을 해야 할까… 어쩌면 이게 레이첼이 바라는 게 아닐까?"),
            }
        )
    );

    // 종료 부분의 요소들
    protected override SequentialElement ExitElements => new
    (
    );
}
