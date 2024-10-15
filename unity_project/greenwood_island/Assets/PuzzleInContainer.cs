using System;
using System.Collections.Generic;

public class PuzzleInContainer : Puzzle
{
    private const string ContainerFullKey = "ContainerFullKey";    // 컨테이너 문을 여는 열쇠
    // 이벤트 ID 정의
    private const string Mountain1 = "Mountain1";  
    private const string ContainerFront = "ContainerFront";  
    private const string SearchEvent_ContainerFront_UseKey = "SearchEvent_ContainerFront_UseKey";  // 컨테이너 열기 이벤트


    // **검색 이벤트 사전**
    public override Dictionary<string, SequentialElement> SearchEvents => new()
    {
        {
            SearchEvent_ContainerFront_UseKey,
            new SequentialElement(
                new MonoDialogue("문이 굳게 잠겨 있다. 열쇠가 없으면 열 수 없을 것 같아."),
                new If(() => ItemManager.HasItem(ContainerFullKey),  // 열쇠 보유 여부 확인
                    new SequentialElement(  // 열쇠가 있을 때 실행할 시나리오
                        new ItemDemand(
                            ContainerFullKey,
                            new PuzzlePlaceTransition("ContainerInside"),
                            null
                        )
                    ),
                    new SequentialElement(  // 열쇠가 없을 때 실행할 시나리오
                        new Dialogue(
                            "Mono",
                            new List<Line>
                            {
                                new Line("문이 굳게 잠겨 있다."),
                                new Line("열쇠가 없으면 열 수 없을 것 같다."),
                            },
                            afterPanelDown: true
                        )
                    )
                )
            )
        }
    };


    // **입장 이벤트 사전**
    public override Dictionary<string, SequentialElement> EnterEvents { get; } = new()
    {
        { 
            Mountain1, 
            new SequentialElement(
                new SFXEnter(
                    "BackgroundAmbient"
                ),
                new Dialogue(
                    "Mono",
                    new List<Line>
                    {
                        new Line("비가 그치고, 야산이 고요해졌다. 하지만 이곳 어딘가에 뭔가 중요한 것이 숨겨져 있을 것 같은 느낌이 든다."),
                        new Line("주변을 살펴보자. 뭔가 단서가 있을지도 몰라.")
                    },
                    afterPanelDown : true
                )
            )
        },
        { 
            ContainerFront, 
            new SequentialElement(
                new Dialogue(
                    "Mono",
                    new List<Line>
                    {
                        new Line("이런 깊은 산속에 거대한 컨테이너가 있다니..."),
                        new Line("마치 누군가 오랫동안 이곳을 은신처나 방처럼 쓴 것 같다."),
                        new Line("문 안쪽엔 어떤 이야기가 숨어 있을까...")
                    },
                    afterPanelDown: true
                )
            )
        }
    };

    // **퇴장 이벤트 사전**
    public override Dictionary<string, SequentialElement> ExitEvents { get; } = new()
    {
    };

    // **검색 이벤트 클리어 조건**
    public override Dictionary<string, Func<bool>> SearchEventClearConditions { get; } = new()
    {
        { 
            SearchEvent_ContainerFront_UseKey, 
            () => ItemManager.HasItem("ContainerFullKey", GameDataManager.CurrentStorySavedData)
        }
    };
}
