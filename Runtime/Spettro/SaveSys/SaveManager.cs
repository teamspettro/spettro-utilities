using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using Newtonsoft.Json;
using System;

/// 
/// Originally made by Nextin, remade by d2dyno
/// 
namespace MonoSpace.SaveSystem
{
    public class SaveManager
    {
        public static bool ENCODE_64 = true;
        public const string SAVES_DIRECTORY = "Save";

        public static string APPLICATION_DIRECTORY = CommonResources.UserPath;
        public static string FULL_PATH = Path.Combine(APPLICATION_DIRECTORY, SAVES_DIRECTORY);

        public static void Save(SaveObject obj, int saveNumber)
        {

            string savesDir = Path.Combine(APPLICATION_DIRECTORY, SAVES_DIRECTORY);
            string fileName = $"slot{saveNumber}.ms";
            string filePath = Path.Combine(savesDir, fileName);


            if (!Directory.Exists(savesDir))
                Directory.CreateDirectory(savesDir);

            CommonResources.CurrentSaveSlot = saveNumber;
            using (FileStream fileStream = File.Create(filePath))
            {
                string jsonSerialized = JsonConvert.SerializeObject(obj, Formatting.Indented);
                if (ENCODE_64)
                    jsonSerialized = EncodingManager.EncodeB64(jsonSerialized);
                byte[] buffer = Encoding.Unicode.GetBytes(jsonSerialized);
                fileStream.Write(buffer, 0, buffer.Length);
            }
            CommonResources.Empty = false;
        }

        public static SaveObject Load(int saveNumber)
        {
            string savesDir = Path.Combine(APPLICATION_DIRECTORY, SAVES_DIRECTORY);
            string fileName = $"slot{saveNumber}.ms";
            string filePath = Path.Combine(savesDir, fileName);
            


            SaveObject obj;
            CommonResources.CurrentSaveSlot = saveNumber;
            if (File.Exists(filePath))
            {

                using (FileStream fileStream = File.OpenRead(filePath))
                {
                    byte[] buffer = new byte[new FileInfo(filePath).Length];
                    fileStream.Read(buffer, 0, buffer.Length);

                    string json = Encoding.Unicode.GetString(buffer);
                    if (ENCODE_64)
                        json = (string)EncodingManager.DecodeB64(json);
                    try
                    {

                        obj = JsonConvert.DeserializeObject<SaveObject>(json);
                        
                    }
                    catch (Exception ex)
                    {
                        fileStream.Close();
                        DLog.LogError($"[SM] Could not convert SaveFile {saveNumber} into a SaveObject! Making a new one in place. {ex}");
                        DLog.LogWarning($"[SM] Save {saveNumber} is empty. ");
                        SaveObject newso = new SaveObject();
                        newso.SaveID= saveNumber;
                        Save(newso, saveNumber);
                        return newso;
                    }

                    CommonResources.Empty = false;
                }
            }
            else
            {
                CommonResources.Empty = true;
                DLog.LogWarning("[SM] The save file " + saveNumber + "was not found.");
                return null;
            }

            return obj;
        }

        public static void Delete(int saveNumber)
        {
            string fileName = $"save{saveNumber}.ms";
            string fullPath = Path.Combine(APPLICATION_DIRECTORY, SAVES_DIRECTORY, fileName);
            File.Delete(fullPath);
            DLog.LogWarning("[SM] Deleted file at " + fullPath);
        }

        public static void DeleteAll()
        {
            List<SaveObject> objs = EnumerateSavesinSaveLoc();
            foreach(SaveObject obj in objs)
            {
                Delete(obj.SaveID);
            }
        }

        public static List<SaveObject> EnumerateSavesinSaveLoc()
        {
            List<string> paths = new List<string>();
            List<SaveObject> saves = new List<SaveObject>();
            if (Directory.Exists(FULL_PATH))
            {
                paths = Directory.EnumerateFiles(FULL_PATH).ToList();
            }
            else
            {
                Directory.CreateDirectory(FULL_PATH);
                DLog.LogWarning("[SM] There are no save files / the directory does not exist.");
                return saves;
            }
            //Search from every file found and see if it's a save or not.

            for (int i = 0; i < paths.Count; i++)
            {
                //Temporary object to return.
                SaveObject obj;
                using (FileStream fileStream = File.OpenRead(paths[i]))
                {
                    //Read file
                    byte[] buffer = new byte[new FileInfo(paths[i]).Length];
                    fileStream.Read(buffer, 0, buffer.Length);

                    //Get the JSON data
                    string json = Encoding.Unicode.GetString(buffer);
                    try
                    {
                        //Try to convert it into a SaveObject
                        obj = JsonUtility.FromJson<SaveObject>(json);
                        saves.Add(obj);
                        CommonResources.Empty = false;
                    }
                    catch 
                    {
                        //If it isn't, instead of an error, just give a message.
                        DLog.LogWarning($"[SM] Path \"{paths[i]}\" is not a valid save file.");
                    }
                    
                }
                
            }
            return saves;
        }
    }
}