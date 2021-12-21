using System;
using System.IO;
using UnityEngine;
using Spettro.SettingsSystem;
namespace Spettro
{
    public static class SpettroResources
    {
        public static int CurrentSaveSlot = 0;
        public static Settings Settings = new Settings();
        public static bool Empty = false;
        public static string UserPath
        {
            get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Application.companyName, Application.productName); }
        }
    }


}
