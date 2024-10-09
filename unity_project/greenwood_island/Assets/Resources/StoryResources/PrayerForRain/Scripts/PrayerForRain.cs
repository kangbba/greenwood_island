using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PrayerForRain : Story
{
    private Color _imgColorLevel1 = ColorUtils.CustomColor("C57272");
    private Color _imgColorLevel2 = ColorUtils.CustomColor("BF7272");
    private Color _imgColorLevel3 = ColorUtils.CustomColor("B74C4C");
    

    // 시작 부분의 요소들
    // 스토리의 메인 업데이트 부분
    public override List<Element> UpdateElements => new List<Element>(){

        new ImaginationEnter("Pray", Vector2.one * 2f, Vector2.up * 425, 0f),
        new SFXEnter("BackgroundAmbient", .5f, true, 0f),

        new ParallelElement(
            new ImaginationColor(_imgColorLevel1, 0f),
            new ImaginationMove(Vector2.up * 71f, 3f),
            new ImaginationScale(Vector2.one * 1.35f, 6f),
            new ScreenOverlayFilmClear(1f)
        ),
        new ParallelElement(
            new ImaginationColor(_imgColorLevel1, 5f),
            new Dialogue(
                "Mono",
                new List<Line>
                {
                    new Line("사람들이 하나둘 촛불을 들고 모여든다... 모두가 무표정하다. 뭔가 이상한 긴장감이 흐르고 있다. 이건... 단순한 기우제가 아니다.")
                }
            )
        ),
        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("광장 중심에 아말리안, 칼데르, 엘드라 세 명의 장로가 제단에 서 있다. 아말리안이 먼저 앞으로 나와, 팔을 천천히 하늘로 들어 올리며 의식을 시작한다.")
            }
        ),

        new SFXEnter("Drum1", .5f, true, 0f),
        new ParallelElement(
            new ImaginationColor(_imgColorLevel2, 5f),
            new ImaginationScale(Vector2.one * 1.25f, 5f),
            new Dialogue(
                "Amalian",
                new List<Line>
                {
                    new Line("섬을 지키는 신이시여, 우리의 기도를 들으소서. 이 가뭄을 거두어주시고, 우리의 땅에 다시금 축복의 비를 내려주소서.")
                }
            )
        ),
        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("주민들이 일제히 고개를 숙이고, 낮은 목소리로 구호를 외치기 시작한다. 그들의 움직임은 너무나도 일사불란하다.")
            }
        ),
        new Dialogue(
            "Villagers",
            new List<Line>
            {
                new Line("비를 내려주소서. 가뭄을 거두어주소서.")
            }
        ),
        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("카메라를 들고 있지만, 그들의 행동을 이해하지 못한 채 그들을 지켜본다. 북소리가 점점 더 커지고, 두 번째 장로 칼데르가 차분하게 말을 이어간다. 그는 하늘을 가리키며 말한다.")
            }
        ),
        new ParallelElement(
            new ImaginationColor(_imgColorLevel3, 5f),
            new ImaginationScale(Vector2.one * 1.1f, 5f),
            new Dialogue(
                "Calder",
                new List<Line>
                {
                    new Line("섬의 하늘이 우리의 기도를 들으리라. 바람이 불어와, 구름이 움직일 것이다. 우리의 바람이 하늘을 깨우고, 비가 내릴 것이다.")
                }
            )
        ),
        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("뭐지...? 저 사람들... 마치 조종당하는 것 같다. 이게 무슨... 모두 똑같이 움직이고, 아무도 의심하지 않는다. 장로들이 말하는 대로 하늘이 반응한다고?")
            }
        ),
        new Dialogue(
            "Eldra",
            new List<Line>
            {
                new Line("신이 우리의 기도를 들으셨다. 비가 내리리라. 우리의 헌신과 기도로 섬이 다시금 축복받을 것이다.")
            }
        ),
        
        
        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("엘드라의 말에 주민들은 땅에 엎드려, 더 강하게 구호를 외친다. 북소리가 절정에 이르고, 하늘에서 빗방울이 하나둘 떨어지기 시작한다."),
            }
        ),
        new ParallelElement(
            new SFXEnter("Drum2", .5f, true, 3f),
            new CameraShake(3f, 30f, 20, 180),
            new ImaginationInvertEffect(true),
            new ImaginationScale(Vector2.one * 1, .1f)
        ),
        new SFXEnter("Rain2", 1f, true, 0f),
        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("비... 비가 내리기 시작한다...? 진짜로... 그들의 말 한마디에 하늘이 반응한 것인가? 이건 그냥 우연일 것이다. 그렇지? 그럴 리가 없다.")
            }
        ),
        new Dialogue(
            "Amalian",
            new List<Line>
            {
                new Line("비가 내린다! 신이 우리의 기도를 들어주셨다!")
            }
        ),
        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("주민들은 빗속에서 몸을 웅크린 채, 안도감 속에서 엎드린다. 그들의 표정에는 구원이 섞여 있지만, 여전히 두려움이 남아 있다."),
                new Line("이건... 이게 우연일 리가 없다. 저들이... 정말 자연을 통제하고 있다는 것인가? 그럼 그들이 말하는 섬의 전통이... 사실인 것인가?")
            }
        )
    };

}
