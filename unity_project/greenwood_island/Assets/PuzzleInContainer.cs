using System;
using System.Collections.Generic;

public class PuzzleInContainer : Puzzle
{
    private const string ContainerFullKey = "ContainerFullKey";    // 컨테이너 문을 여는 열쇠
    // 이벤트 ID 정의
    private const string Mountain1 = "Mountain1";  
    private const string ContainerFront = "ContainerFront";  
    private const string SearchEvent_ContainerFront_UseKey = "SearchEvent_ContainerFront_UseKey";  // 컨테이너 열기 이벤트

    public override EventData EventData { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}
