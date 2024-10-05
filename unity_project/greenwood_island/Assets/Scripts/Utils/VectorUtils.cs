using UnityEngine;

public static class VectorUtils
{
    // Vector2에서 랜덤 값을 반환하는 확장 메서드
    public static float RandomValue(this Vector2 range)
    {
        return Random.Range(range.x, range.y);
    }
    public static Vector3 ModifiedX(this Vector3 vector, float x)
    {
        return new Vector3(x, vector.y, vector.z);
    }

    public static Vector3 ModifiedY(this Vector3 vector, float y)
    {
        return new Vector3(vector.x, y, vector.z);
    }

    public static Vector3 ModifiedZ(this Vector3 vector, float z)
    {
        return new Vector3(vector.x, vector.y, z);
    }
    public static Vector2 ModifiedX(this Vector2 vector, float x)
    {
        return new Vector2(x, vector.y);
    }

    public static Vector2 ModifiedY(this Vector2 vector, float y)
    {
        return new Vector2(vector.x, y);
    }
}
