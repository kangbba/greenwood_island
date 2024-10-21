
using UnityEngine;
using System.Collections.Generic;

public class TownTour : Story
{

    public override List<Element> UpdateElements => new List<Element>()
    {
        new PuzzleEnter("PuzzleInTown"),
    };
}
