using UnityEngine;

public static class TransformUtils
{
    // Transform을 확장하는 DestroyAllChildren 메서드
    public static void DestroyAllChildren(this Transform tr)
    {
        foreach (Transform child in tr)
        {
            // 즉시 파괴하지 않고, 다음 프레임에 파괴되도록 처리
            GameObject.Destroy(child.gameObject);
        }
    }
}
