using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OpeningStory : Story
{
    Color darkSkyColor = ColorUtils.CustomColor("86D8FF");
    // 스토리의 메인 업데이트 부분
    public override List<Element> UpdateElements => new List<Element>
    {
        new ImaginationEnter("Black", 0f),
        new Intertitle("이 이야기는 허구이며,\n실제 인물, 장소, 사건과는 무관합니다.", 1, 3, 1),
        new ImaginationClear(.5f),
        new ParallelElement(
            new PlaceTransition(
                new PlaceEnter(
                    "Storm",
                    initialColor : darkSkyColor
                ),
                Color.black,
                new List<PlaceEffect>(){
                    new PlaceEffect(
                        PlaceEffect.EffectType.ScaleZoom,
                        2f,
                        1.1f
                    ),
                }
            )
        ),


        new ParallelElement(
            new SFXEnter("Thunder1", 0.25f, false, 0f),
            new PlaceColor(Color.white, .1f),
            new PlaceShake()
        ),
        new PlaceColor(darkSkyColor, .2f),
        new Delay(3f),
        new ParallelElement(
            new SFXEnter("Thunder2", 0.15f, false, 0f),
            new PlaceColor(Color.white, .15f, Ease.OutElastic), // 장소 색상 조정
            new PlaceShake()
        ),
        new ParallelElement(
            new PlaceRestore(Vector2.one, Vector2.zero, Color.white, 0.3f, Ease.OutQuad), // 복구 작업
            new SFXEnter("Wind1", 0.15f, true, 0f),
            new SFXEnter("Thunder1", 0.15f, true, 5f)
        ),

        new PlaceTransition(
            new PlaceEnter("FerryInside", initialLocalScale : Vector2.one * 1.2f),
            Color.black,
            new List<PlaceEffect>(){
                new PlaceEffect(PlaceEffect.EffectType.ShowLeftward, 1f, 200),
                new PlaceEffect(PlaceEffect.EffectType.ScaleBounce, duration : 1f, strength : 1.5f)
            }
        ),

        // 라디오의 불길한 소리
        new Dialogue(
            "라디오",
            new List<Line>
            {
                new Line("…치지직, 북위 48도… 동경… 관찰 중… 주시…"),
            }
        ),
        new PlaceShake(), 

        // 라이언의 독백
        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("배는 거친 파도에 휘청거리고, 바깥에서는 빗방울이 떨어지며 윈드실드에 부딪힌다."),
                new Line("창문 너머로 보이는 하늘은 회색으로 뒤덮여 있고, 두려움이 나를 감싸기 시작했다."),
                new Line("애써 의연한 척 해보려 고개를 빼어 이 작은 여객선을 둘러본다."),
                new Line("어두운 분위기, 이 작은 배에는 나 외에 몇 명밖에 없다."),
                new Line("몇 명의 승객들이 고개를 떨군 채 침묵하고 있다. 내가 기댈 만한 사람이라고는 없어 보인다."),
            }
        ),
        new PlaceShake(), 

        new ImaginationEnter(
            "FerryOldMan",
            color : ColorUtils.CustomColor("516FB7")
        ),
        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("한 노인은 선실 구석에 앉아, 아무 말 없이 파도만 바라보고 있다."),
                new Line("흔들리는 배에도 두려움이 없어보인다. 그렇다고 강인한 모습은 아닌 것 같다."),
                new Line("바다만 바라보며 그저 혼잣말을 반복할 뿐이었다"),
            }
        ),

        // 신사와 부인의 대화
        new Dialogue(
            "노인",
            new Line("바다가… 다 기억해… 내가 본 것들… 물속에 잠겼어… 저 깊은 곳에…")
        ),
        new ImaginationClear(1f),
        new Dialogue(
            "라디오",
            new List<Line>
            {
                new Line("경로… 접근 중… 보고… 대기…"),
            }
        ),
        new PlaceShake(), 
        new ParallelElement(
            new SFXEnter("Thunder2", 0.4f, false, 0f),
            new PlaceShake(), 
            new ImaginationEnter("Black", localScale: Vector2.one * 1.5f) // 눈을 질끈 감을 때 어두워지는 연출
        ),
        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("갑작스러운 굉음과 함께 배가 크게 흔들린다. 중심을 잃고 몸이 기울어지는 게 느껴진다."),
                new Line("바닥에 굴러떨어지는 짐들, 깨진 유리, 비명 섞인 소리들이 혼란스럽게 섞여 들린다."),
                new Line("심장이 미친 듯이 뛰고, 숨이 막혀온다. 모든 것이 뒤집히는 듯한 공포가 엄습해 온다."),
                new Line("안전벨트 하나 없는 이곳에서, 인간이 이렇게 나약한 존재일 줄이야. 손에 땀이 배어 무언가를 붙잡으려 하지만, 아무것도 잡히지 않는다."),
                new Line("그저 바닥에 엎드려 눈을 질끈 감았다. 왜 이 섬에 오기로 한 걸까. 이 순간만큼은 그 선택을 후회했다."),
                new Line("이대로 끝나는 걸까? 모든 것이 한순간에 무너질 것만 같다. 나는 그저 이 순간이 빨리 끝나기를 바랄 뿐이다."),
            }
        ),
        new ImaginationClear(1f),
        new ScreenOverlayFilmClear(2f),

        new ImaginationEnter(
            "FerryOldMan",
            color: ColorUtils.CustomColor("516FB7")
        ),
        // 노인의 재등장과 불길한 대사
        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("고개를 들자 노인의 모습이 희미하게 보인다. 그는 여전히 자리에 앉아 창밖을 바라보고 있다."),
                new Line("흔들리는 배에도 전혀 동요하지 않는다. 마치 이런 일이 익숙하다는 듯이."),
                new Line("두려움을 느끼지 않는 노인의 모습에 오히려 더 섬뜩한 기분이 든다."),
            }
        ),
        new Dialogue(
            "노인",
            new List<Line>
            {
                new Line("파도는... 모든 걸 기억하지. 이곳에 묻힌 모든 것들을..."),
                new Line("나는 그저 바다가 되돌려줄 날만 기다리고 있을 뿐이지."),
            }
        ),
        new PlaceShake(), 
        new ImaginationClear(1f),
        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("노인의 혼잣말이 귓가에 맴돈다. 마치 이 배의 마지막을 예언하는 듯한 불길한 목소리..."),
                new Line("몸이 떨린다. 이대로 가라앉는 게 아닐까, 그런 생각이 머리를 스친다."),
            }
        ),
        new ImaginationClear(1f),

        new ImaginationEnter("ShipSide", color : Color.black.ModifiedAlpha(.8f)),

        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("서서히 소음이 멈추고, 배가 서서히 안정되기 시작한다."),
                new Line("천천히 눈을 떠보니, 배는 어느새 안정을 되찾고 있다."),
            }
        ),
        new ParallelElement(
            new ImaginationClear(1f),
            new ImaginationEnter("FarTown")
        ),

        // 그린우드 섬이 드러나는 장면
        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("안개가 서서히 걷히며, 그린우드 섬의 고요한 전경이 눈앞에 모습을 드러낸다."),
                new Line("평화로운 해안과 오래된 건물들이 마치 시간이 멈춘 듯 서 있다."),
                new Line("방금 전의 공포가 거짓말처럼 사라졌지만, 마음속에 남은 찝찝함은 가시지 않는다."),
                new Line("이곳이… 그린우드인가."),
            }
        )
    };
}
