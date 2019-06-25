using System;

[Serializable]
public class GameState
{
    public SerializableVector3 PlayerPosition { get; set; }
    public string LevelToLoad { get; set; }

    public int NumberOfCollectedNotesOnCurrentLevel;
}
