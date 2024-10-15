using System.Collections.Generic;

public abstract class EventData
{
    public abstract Dictionary<string, SequentialElement> EventDictionary {get; set;}

    public SequentialElement GetEvent(string eventID){
        return EventDictionary.GetValueOrDefault(eventID);
    }
}