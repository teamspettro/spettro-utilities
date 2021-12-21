[System.Serializable]
public class SaveObject
{
    //--SAVE INFO

    //#FILE SYSTEM#
    public int SaveID; //Identifies the save slot
    public string SaveMonoName; //Indentifies Mono's name in the game
    public string LastOpenedDate; //Last time you played
    //#GAME INFO#
    public GameMode GameMode; //The mode
    public int CheckpointID; //The ID of the last checkpoint you hit
    public int SceneID;
    public int LatestLevelID; //The ID of the level you've just beaten 
    public int BeforeLastLevelBeatenID; //The ID of the level before the current one (used as a double-check)
    public string Version;
}

public enum GameMode
{
    Story_Mode,
    Fast_Mode,
    Sandbox
}
