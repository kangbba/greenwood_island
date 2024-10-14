using UnityEngine;
using System.Collections.Generic;

public class PrayerForRainAfter : Story
{
    public override List<Element> UpdateElements => new List<Element>()
    {
        new PuzzleEnter(
            "PuzzleInContainer"
        )
    };
}
