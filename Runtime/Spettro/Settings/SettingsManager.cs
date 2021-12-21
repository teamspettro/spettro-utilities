using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Spettro.Settings
{
    /// <summary>
    /// Script that manages settings with d2dyno's Setting System.
    /// </summary>
    public static class SettingsManager
    {
        #region Get
        public static int GetInt(string name, int defaultValue = 0)
        {
            if (CommonResources.Settings.Int == null)
            {
                CommonResources.Settings.Int = new Dictionary<string, int>();
            }
            var settingsInt = CommonResources.Settings.Int;
            DLog.Log($"SEM: GET INT {name} DEF {defaultValue}");
            if (settingsInt.ContainsKey(name))
            {
                int keyVal;
                settingsInt.TryGetValue(name, out keyVal);
                return keyVal;
            }
            else
            {
                settingsInt.Add(name, defaultValue);
                CommonResources.Settings.Int = settingsInt;
                return defaultValue;
            }
        }
        public static float GetFloat(string name, float defaultValue = 0)
        {
            if (CommonResources.Settings.Float == null)
            {
                CommonResources.Settings.Float = new Dictionary<string, float>();
            }
            DLog.Log($"SEM: GET FLOAT {name} DEF {defaultValue}");
            var settingsFloat = CommonResources.Settings.Float;
            if (settingsFloat.ContainsKey(name))
            {
                float keyVal;
                settingsFloat.TryGetValue(name, out keyVal);
                return keyVal;
            }
            else
            {
                settingsFloat.Add(name, defaultValue);
                CommonResources.Settings.Float = settingsFloat;
                return defaultValue;
            }
        }

        public static string GetString(string name, string defaultValue = "", bool decode64 = false)
        {
            if (CommonResources.Settings.String == null)
            {
                CommonResources.Settings.String = new Dictionary<string, string>();
            }
            DLog.Log($"SEM: GET STRING {name} DEF {defaultValue} D64 {decode64}");
            var settingsString = CommonResources.Settings.String;
            if (settingsString.ContainsKey(name))
            {
                string keyVal;
                settingsString.TryGetValue(name, out keyVal);
                if (EncodingManager.IsBase64String(keyVal) || decode64)
                {
                    keyVal = (string)EncodingManager.DecodeB64(keyVal);
                }
                return keyVal;
            }
            else
            {
                settingsString.Add(name, defaultValue);
                CommonResources.Settings.String = settingsString;
                return defaultValue;
            }
        }
        public static bool GetBool(string name, bool defaultValue = false)
        {
            if (CommonResources.Settings.Bool == null)
            {
                CommonResources.Settings.Bool = new Dictionary<string, bool>();
            }
            DLog.Log($"SEM: GET BOOL {name} DEF {defaultValue}");
            var settingsBool = CommonResources.Settings.Bool;
            if (settingsBool.ContainsKey(name))
            {
                bool keyVal;
                settingsBool.TryGetValue(name, out keyVal);
                return keyVal;
            }
            else
            {
                settingsBool.Add(name, defaultValue);
                CommonResources.Settings.Bool = settingsBool;
                return defaultValue;
            }
        }
        public static object EnumerateSettings()
        {
            return CommonResources.Settings.ExportSettings();
        }
        #endregion
        #region Set
        public static void SetInt(string name, int value)
        {
            if (CommonResources.Settings.Int == null)
            {
                CommonResources.Settings.Int = new Dictionary<string, int>();
            }
            DLog.Log($"SEM: SET INT {name} VAL {value}");
            var settingsInt = CommonResources.Settings.Int;
            settingsInt[name] = value;
            CommonResources.Settings.Int = settingsInt;
        }

        public static void SetFloat(string name, float value)
        {
            if (CommonResources.Settings.Float == null)
            {
                CommonResources.Settings.Float = new Dictionary<string, float>();
            }
            DLog.Log($"SEM: SET FLOAT {name} VAL {value}");
            var settingsFloat = CommonResources.Settings.Float;
            settingsFloat[name] = value;
            CommonResources.Settings.Float = settingsFloat;

        }
        public static void SetString(string name, string value, bool encode64 = false)
        {
            if (CommonResources.Settings.String == null)
            {
                CommonResources.Settings.String = new Dictionary<string, string>();
            }
            DLog.Log($"SEM: SET STRING {name} VAL {value} E64 {encode64}");
            if (encode64)
            {
                value = EncodingManager.EncodeB64(value);
            }
            var settingsString = CommonResources.Settings.String;
            settingsString[name] = value;
            CommonResources.Settings.String = settingsString;
        }
        public static void SetBool(string name, bool value)
        {
            if (CommonResources.Settings.Bool == null)
            {
                CommonResources.Settings.Bool = new Dictionary<string, bool>();
            }
            DLog.Log($"SEM: SET BOOL {name} VAL {value}");
            var settingsBool = CommonResources.Settings.Bool;
            settingsBool[name] = value;
            CommonResources.Settings.Bool = settingsBool;
        }
        #endregion
        #region Delete
        public static void DeleteAll()
        {
            var settings = CommonResources.Settings;

            settings.String.Clear();
            settings.Int.Clear();
            settings.Float.Clear();
            DLog.LogWarning("Reset all dictionaries for settings.");
            CommonResources.Settings = settings;
        }
        #endregion

        #region Has
        public static bool HasInt(string key)
        {
            var settings = CommonResources.Settings.Int;
            int val;
            return settings.TryGetValue(key, out val);

        }
        public static bool HasFloat(string key)
        {
            var settings = CommonResources.Settings.Float;
            float val;
            return settings.TryGetValue(key, out val);

        }
        public static bool HasString(string key)
        {
            var settings = CommonResources.Settings.String;
            string val;
            return settings.TryGetValue(key, out val);

        }
        #endregion

        ///This doesnt work yet vvv
    }

}