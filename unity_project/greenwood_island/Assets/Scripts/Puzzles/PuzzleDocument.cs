using System.Collections.Generic;
using UnityEngine;

public abstract class PuzzleDocument : MonoBehaviour{
    
    public abstract Dictionary<string, SequentialElement> EventDictionary {get; } 

}