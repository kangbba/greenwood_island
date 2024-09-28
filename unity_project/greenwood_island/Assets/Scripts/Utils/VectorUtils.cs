using UnityEngine;

public static class VectorUtils
{
    // Vector2에서 랜덤 값을 반환하는 확장 메서드
    public static float RandomValue(this Vector2 range)
    {
        return Random.Range(range.x, range.y);
    }
}
