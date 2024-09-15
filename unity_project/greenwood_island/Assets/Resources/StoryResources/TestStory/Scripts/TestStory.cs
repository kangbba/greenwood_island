
using System.Collections.Generic;
using UnityEngine;

public class TestStory : Story
{
    // TestStory 스토리의 스크립트 로직을 여기에 작성하세요.
    protected override SequentialElement StartElements => new ();

    protected override SequentialElement UpdateElements => new (
        _testUserActionEnter
    );

    protected override SequentialElement ExitElements => new ();

    protected override string StoryDesc => "";


    UserActionEnter _testUserActionEnter = new UserActionEnter(
            // 첫 번째 상위 메뉴: 말을 건다
        new UserActionBtnContent("말을 건다", new List<UserActionBtnContent>
        {
            // 첫 번째 메뉴의 하위 메뉴들
            new UserActionBtnContent("아말리안 밀에 대해 묻는다", new SequentialElement(
                new Dialogue("캐릭터A", new List<Line>
                {
                    new Line("아말리안 밀은 오래된 곳이지만 여전히 굳건해."),
                })
            )),
            new UserActionBtnContent("오늘 일상에 대해 묻는다", new SequentialElement(
                new Dialogue("캐릭터B", new List<Line>
                {
                    new Line("오늘은 평화로웠어, 이상한 일도 없었고."),
                })
            )),
            new UserActionBtnContent(
                "새로운 방문자에 대해 묻는다", 
                new List<UserActionBtnContent>
                {
                    new UserActionBtnContent(
                        "방문자의 목적은?", 
                        new SequentialElement(
                        new Dialogue("캐릭터A", new List<Line>
                            {
                                new Line("그는 자신을 연구자라 소개했어."),
                            })
                        )),
                        new UserActionBtnContent("방문자의 외모는?", new SequentialElement(
                            new Dialogue("캐릭터B", new List<Line>
                            {
                                new Line("키가 크고, 검은 옷을 입고 있었지."),
                            })
                        )
                    )
                })
        }),

        // 두 번째 상위 메뉴: 주위를 둘러본다
        new UserActionBtnContent("주위를 둘러본다", new List<UserActionBtnContent>
        {
            // 두 번째 메뉴의 하위 메뉴들
            new UserActionBtnContent("창문 밖을 본다", new SequentialElement(
                new Dialogue("나", new List<Line>
                {
                    new Line("바깥에는 잿빛 하늘과 흔들리는 나무들만 보인다."),
                })
            )),
            new UserActionBtnContent("방 안을 살핀다", new List<UserActionBtnContent>
            {
                new UserActionBtnContent("테이블 위를 살핀다", new SequentialElement(
                    new Dialogue("나", new List<Line>
                    {
                        new Line("테이블 위에는 먼지 쌓인 책과 찻잔이 놓여 있다."),
                    })
                )),
                new UserActionBtnContent("벽에 걸린 그림을 본다", new List<UserActionBtnContent>
                {
                    new UserActionBtnContent("그림의 제목을 확인한다", new SequentialElement(
                        new Dialogue("나", new List<Line>
                        {
                            new Line("그림의 제목은 '기억의 풍경'."),
                        })
                    )),
                    new UserActionBtnContent("그림의 세부 사항을 살핀다", new List<UserActionBtnContent>
                    {
                        new UserActionBtnContent("그림 속 사람들을 본다", new SequentialElement(
                            new Dialogue("나", new List<Line>
                            {
                                new Line("그림 속 사람들은 어딘가 슬퍼 보인다."),
                            })
                        )),
                        new UserActionBtnContent("그림의 배경을 본다", new SequentialElement(
                            new Dialogue("나", new List<Line>
                            {
                                new Line("배경은 바람에 흔들리는 초원이 그려져 있다."),
                            })
                        ))
                    })
                })
            })
        }),

        // 세 번째 상위 메뉴: 물건을 조사한다
        new UserActionBtnContent("물건을 조사한다", new List<UserActionBtnContent>
        {
            // 세 번째 메뉴의 하위 메뉴들
            new UserActionBtnContent("책을 조사한다", new List<UserActionBtnContent>
            {
                new UserActionBtnContent("표지를 본다", new SequentialElement(
                    new Dialogue("나", new List<Line>
                    {
                        new Line("책의 표지는 낡았고, 제목은 희미하게 보인다."),
                    })
                )),
                new UserActionBtnContent("책의 첫 페이지를 넘긴다", new List<UserActionBtnContent>
                {
                    new UserActionBtnContent("첫 문장을 읽는다", new SequentialElement(
                        new Dialogue("나", new List<Line>
                        {
                            new Line("첫 문장은 '여정의 시작은 언제나 고요했다.'"),
                        })
                    )),
                    new UserActionBtnContent("책갈피를 발견한다", new SequentialElement(
                        new Dialogue("나", new List<Line>
                        {
                            new Line("책갈피는 오래된 사진이다."),
                        })
                    ))
                })
            }),
            new UserActionBtnContent("의자를 조사한다", new SequentialElement(
                new Dialogue("나", new List<Line>
                {
                    new Line("의자는 튼튼해 보이지만 오래된 티가 난다."),
                })
            )),
            new UserActionBtnContent("탁자를 조사한다", 
            new List<UserActionBtnContent>
            {
                new UserActionBtnContent("서랍을 열어본다", new SequentialElement(
                    new Dialogue("나", new List<Line>
                    {
                        new Line("서랍 안에는 오래된 편지가 있다."),
                    })
                )),
                new UserActionBtnContent("탁자 위의 물건을 본다", new List<UserActionBtnContent>
                {
                    new UserActionBtnContent("촛대를 살핀다", new SequentialElement(
                        new Dialogue("나", new List<Line>
                        {
                            new Line("촛대는 새것처럼 깨끗하다."),
                        })
                    )),
                    new UserActionBtnContent("낡은 시계를 본다", new SequentialElement(
                        new Dialogue("나", new List<Line>
                        {
                            new Line("시계는 멈춰 있었다."),
                        })
                    ))
                })
            })
        })
    );
}
