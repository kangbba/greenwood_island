using UnityEngine;

public static class ColorUtils
{
    public static Color ModifiedAlpha(this Color color, float alpha)
    {
        return new Color(color.r, color.g, color.b, alpha);
    }
}
