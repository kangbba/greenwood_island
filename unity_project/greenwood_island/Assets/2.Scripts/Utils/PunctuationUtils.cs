using System.Collections.Generic;

public static class PunctuationUtils
{
    // 구두점 우선순위 배열, 긴 구두점이 먼저
    public static readonly string[] PrioritizedPunctuations = new string[]
    {
        "...", "..?", "..!", "!!", "..", "?", "!", ".", "~", ","
    };

    // 주어진 문자열이 커스텀 구두점에 해당하는지 확인하는 함수
    public static bool IsCustomPunctuation(string input)
    {
        return CustomPunctuations.Contains(input);
    }

    // 구두점을 확인하는 함수 (기존 방식과의 호환을 위해 유지)
    private static readonly HashSet<string> CustomPunctuations = new HashSet<string>(PrioritizedPunctuations);
}
