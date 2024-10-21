
using UnityEngine;
using System.Collections.Generic;

public class OrdinaryIsland : Story
{

    public override List<Element> UpdateElements => new List<Element>()
    {
        new PlaceTransition(
            new PlaceEnter("TownOpenField3"), Color.black, new PlaceEffect(PlaceEffect.EffectType.FadeIn, 1f)),

        new PlaceTransition(
            new PlaceEnter(
                "TownOpenField1"), 
                Color.black, 
                new List<PlaceEffect>(){
                    new PlaceEffect(PlaceEffect.EffectType.ZoomIn, 4f, 1.2f),
                    new PlaceEffect(PlaceEffect.EffectType.ShowLeftward, 3f, 150),
                }
            ),

        new Dialogue("OldMan", new List<Line>
        {
            new Line("어, 라이언! 여기서 뭐하나? 산책하는 거야?")
        }),

        new Dialogue("Ryan", new List<Line>
        {
            new Line("네, 그냥 마을 좀 둘러보려고요. 오늘따라 날씨가 참 좋아서요.")
        }),

        new Dialogue("OldMan", new List<Line>
        {
            new Line("하하, 그거면 충분하지. 여기선 딱히 뭐 할 것도 없고, 날씨 좋으면 그걸로 된 거지."),
            new Line("자네도 이제 섬 사람 다 됐네?")
        }),

        new ImaginationEnter("TownOpenField4"),
        new Dialogue("Mono", new List<Line>
        {
            new Line("햇빛이 나무들 사이로 부드럽게 쏟아져 내리며, 잔디 위에 흩어진 작은 반짝임을 만들었다."),
            new Line("푸르른 잔디는 바람에 따라 미묘하게 일렁였고, 나무 잎사귀들이 서로 부딪히며 내는 소리는 섬의 고요 속에서 더욱 선명하게 들려왔다."),
            new Line("여기서 느끼는 평온함은, 도시에서 마주했던 그 복잡하고 끝없는 소음과는 너무도 다르다."),
            new Line("도시에선 하루가 마치 쏜살같이 지나갔지. 거리는 언제나 붐볐고, 나도 그 속에서 그저 한 점에 불과했다."),
            new Line("하지만 이곳에선, 모든 게 느리게 흘러가는 것 같다. 마치 시간이 나를 따라 움직이는 것처럼."),
            new Line("나무 아래에 서 있자니, 이상하게도 마음 한구석에서 잊고 지냈던 여유가 다시 찾아오는 기분이다."),
            new Line("이 고요함이 어색하면서도... 마치, 잠시 잊고 있던 나를 다시 찾아가는 과정 같은 기분이 든다."),
            new Line("도시의 빠름에 익숙해졌던 내겐, 이 섬의 느림이 처음엔 불편했지만, 이제는 그 속에서 조금씩 숨을 쉬게 되는 듯하다.")
        }),
        new ImaginationClear(1f),

        new Dialogue("Ryan", new List<Line>
        {
            new Line("그러게요. 예전 같으면 이런 날씨에 뭐라도 해야 할 것 같았는데, 이젠 그냥 이렇게 걷기만 해도 좋네요.")
        }),

        new Dialogue("OldMan", new List<Line>
        {
            new Line("그게 다야. 복잡하게 생각할 필요 없어. 여기 사람들 다 그러잖아. 서두르지 않고, 그냥 천천히 사는 거지.")
        }),


        new Dialogue("Mono", new List<Line>
        {
            new Line("광장 한켠에선 몇몇 주민들이 가벼운 대화를 나누고 있었다. 그들의 얼굴엔 여유가 묻어 있었다."),
            new Line("섬의 느릿한 시간이 그대로 흐르고 있었다.")
        }),


        new Dialogue("Ryan", new List<Line>
        {
            new Line("맞아요. 다들 여유롭게 지내시더라고요. 처음엔 적응이 안 됐는데, 이제는 익숙해졌어요.")
        }),

        new Dialogue("OldMan", new List<Line>
        {
            new Line("자네가 그런 걸 느낀다면, 섬에서 잘 지내고 있는 거야."),
            new Line("하지만 아직 못 본 데도 많을 거야. 부두만 보지 말고, 마을도 천천히 둘러보게. 구석구석 재밌는 데가 있을 테니까.")
        }),

        new Dialogue("Ryan", new List<Line>
        {
            new Line("좋은 생각이네요. 한 번 마을을 더 돌아봐야겠어요.")
        }),
        new PlaceTransition(
            new PlaceEnter("TownOpenField2"), Color.black, new List<PlaceEffect>(){
                    new PlaceEffect(PlaceEffect.EffectType.ZoomIn, 4f, 1.2f),
                    new PlaceEffect(PlaceEffect.EffectType.ShowRightward, 3f, 150),
                }),


        new Dialogue("Mono", new List<Line>
        {
            new Line("노인은 미소를 지으며 손짓했다. 부두를 떠나 마을을 둘러보라는 그의 제안에, 나는 고개를 끄덕이며 주변을 다시 한 번 돌아본다."),
            new Line("이곳의 고요함이 주는 편안함 속에서, 나는 자연스럽게 섬을 더 탐험할 생각을 하게 된다.")
        }),
    };
}
