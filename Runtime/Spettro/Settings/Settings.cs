using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Spettro;

namespace Spettro.SettingsSystem
{
    public class Settings : BaseJsonSettingsModel
    {
        public Settings()
        : base(Path.Combine(SpettroResources.UserPath, "config.sg"), false)
        {
        }

        public Dictionary<string, int> Int
        {
            get => Get<Dictionary<string, int>>(new Dictionary<string, int>());
            set => Set(value);
        }
        public Dictionary<string, float> Float
        {
            get => Get<Dictionary<string, float>>(new Dictionary<string, float>());
            set => Set(value);
        }
        public Dictionary<string, string> String
        {
            get => Get<Dictionary<string, string>>(new Dictionary<string, string>());
            set => Set(value);

        }
        public Dictionary<string, bool> Bool
        {
            get => Get<Dictionary<string, bool>>(new Dictionary<string, bool>());
            set => Set(value);

        }
    }
}

