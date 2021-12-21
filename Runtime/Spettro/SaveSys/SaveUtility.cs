using Spettro.SaveSystem;
using UnityEngine;

public static class SaveUtility
{
    public static void SaveRepair(int slot)
    {
        ///This function checks if some settings are incorrect and missing, so that it can repair them and set defaults.
        var so = SaveManager.Load(slot);
        bool hadToRepair = false;
        if (string.IsNullOrEmpty(so.SaveMonoName))
        {
            so.SaveMonoName = "Mono";
            hadToRepair = true;
        }
        if (string.IsNullOrEmpty(so.LastOpenedDate))
        {
            so.LastOpenedDate = "01/01/2021";
            hadToRepair = true;
        }
        if (hadToRepair)
        {
            Debug.LogWarning($"Had to repair save {slot}. [OLI]");
        }
    }

    public static void SetDefault(int slot)
    {
        
    }
}
