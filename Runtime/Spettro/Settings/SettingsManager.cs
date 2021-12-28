using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Spettro.SettingsSystem
{
    /// <summary>
    /// Script that manages settings with d2dyno's Setting System.
    /// </summary>
    public static class SettingsManager
    {
        #region Get
        public static int GetInt(string name, int defaultValue = 0)
        {
            if (SpettroResources.Settings.Int == null)
            {
                SpettroResources.Settings.Int = new Dictionary<string, int>();
            }
            var settingsInt = SpettroResources.Settings.Int;
            if (settingsInt.ContainsKey(name))
            {
                int keyVal;
                settingsInt.TryGetValue(name, out keyVal);
                return keyVal;
            }
            else
            {
                settingsInt.Add(name, defaultValue);
                SpettroResources.Settings.Int = settingsInt;
                return defaultValue;
            }
        }
        public static float GetFloat(string name, float defaultValue = 0)
        {
            if (SpettroResources.Settings.Float == null)
            {
                SpettroResources.Settings.Float = new Dictionary<string, float>();
            }
            var settingsFloat = SpettroResources.Settings.Float;
            if (settingsFloat.ContainsKey(name))
            {
                float keyVal;
                settingsFloat.TryGetValue(name, out keyVal);
                return keyVal;
            }
            else
            {
                settingsFloat.Add(name, defaultValue);
                SpettroResources.Settings.Float = settingsFloat;
                return defaultValue;
            }
        }

        public static string GetString(string name, string defaultValue = "")
        {
            if (SpettroResources.Settings.String == null)
            {
                SpettroResources.Settings.String = new Dictionary<string, string>();
            }          
            var settingsString = SpettroResources.Settings.String;
            if (settingsString.ContainsKey(name))
            {
                string keyVal;
                settingsString.TryGetValue(name, out keyVal);
                return keyVal;
            }
            else
            {
                settingsString.Add(name, defaultValue);
                SpettroResources.Settings.String = settingsString;
                return defaultValue;
            }
        }
        public static bool GetBool(string name, bool defaultValue = false)
        {
            if (SpettroResources.Settings.Bool == null)
            {
                SpettroResources.Settings.Bool = new Dictionary<string, bool>();
            }
            var settingsBool = SpettroResources.Settings.Bool;
            if (settingsBool.ContainsKey(name))
            {
                bool keyVal;
                settingsBool.TryGetValue(name, out keyVal);
                return keyVal;
            }
            else
            {
                settingsBool.Add(name, defaultValue);
                SpettroResources.Settings.Bool = settingsBool;
                return defaultValue;
            }
        }
        public static object EnumerateSettings()
        {
            return SpettroResources.Settings.ExportSettings();
        }
        #endregion
        #region Set
        public static void SetInt(string name, int value)
        {
            if (SpettroResources.Settings.Int == null)
            {
                SpettroResources.Settings.Int = new Dictionary<string, int>();
            }
           
            var settingsInt = SpettroResources.Settings.Int;
            settingsInt[name] = value;
            SpettroResources.Settings.Int = settingsInt;
        }

        public static void SetFloat(string name, float value)
        {
            if (SpettroResources.Settings.Float == null)
            {
                SpettroResources.Settings.Float = new Dictionary<string, float>();
            }
          
            var settingsFloat = SpettroResources.Settings.Float;
            settingsFloat[name] = value;
            SpettroResources.Settings.Float = settingsFloat;

        }
        public static void SetString(string name, string value)
        {
            if (SpettroResources.Settings.String == null)
            {
                SpettroResources.Settings.String = new Dictionary<string, string>();
            }
            var settingsString = SpettroResources.Settings.String;
            settingsString[name] = value;
            SpettroResources.Settings.String = settingsString;
        }
        public static void SetBool(string name, bool value)
        {
            if (SpettroResources.Settings.Bool == null)
            {
                SpettroResources.Settings.Bool = new Dictionary<string, bool>();
            }
           
            var settingsBool = SpettroResources.Settings.Bool;
            settingsBool[name] = value;
            SpettroResources.Settings.Bool = settingsBool;
        }
        #endregion
        #region Delete
        public static void DeleteAll()
        {
            var settings = SpettroResources.Settings;

            settings.String.Clear();
            settings.Int.Clear();
            settings.Float.Clear();
            SpettroResources.Settings = settings;
        }
        #endregion

        #region Has
        public static bool HasInt(string key)
        {
            var settings = SpettroResources.Settings.Int;
            int val;
            return settings.TryGetValue(key, out val);

        }
        public static bool HasFloat(string key)
        {
            var settings = SpettroResources.Settings.Float;
            float val;
            return settings.TryGetValue(key, out val);

        }
        public static bool HasString(string key)
        {
            var settings = SpettroResources.Settings.String;
            string val;
            return settings.TryGetValue(key, out val);

        }
        #endregion

        ///This doesnt work yet vvv
    }

}