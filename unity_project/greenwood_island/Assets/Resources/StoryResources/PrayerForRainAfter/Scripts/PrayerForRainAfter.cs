using UnityEngine;
using System.Collections.Generic;

public class PrayerForRainAfter : Story
{
    public override List<Element> UpdateElements => new List<Element>()
    {
        new Dialogue(
            "Mono",
            new List<Line>
            {
                new Line("비가 그치고, 야산이 고요해졌다. 하지만 이곳 어딘가에 뭔가 중요한 것이 숨겨져 있을 것 같은 느낌이 든다."),
                new Line("주변을 살펴보자. 뭔가 단서가 있을지도 몰라.")
            }
        ),
    };
}
