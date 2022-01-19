using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Discord;
using System;
using Spettro.DiscordGameSDK;

namespace Spettro.DiscordGameSDK
{
    public class DiscordController : MonoBehaviour
    {
        public bool setDDOLAutomatically = true;
        public DiscordActivityObject[] activities;
        public Int64 appID;
        public Discord.Discord discord;
        public string defaultBigImageKey;

        private ActivityManager activityManager;
        public Activity activity;

        // Use this for initialization
        public static DiscordController discordInstance { get; private set; }

        void Awake()
        {
            if (setDDOLAutomatically)
                DontDestroyOnLoad(this);

            if (discordInstance == null)
            {
                discordInstance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        void Start()
        {
            //NoRequireDiscord : Doesn't need discord to run the game
            //Default : does require discord to run the game, and will try to open it.
            discord = new Discord.Discord(appID, (ulong)Discord.CreateFlags.NoRequireDiscord);

            //Check the scene name and set it accordingly
            SceneManager.sceneLoaded += RunMethodCheck;

            //Get activity manager
            activityManager = discord.GetActivityManager();
            //Make a temporary activity, more likely than not this'll never show.
            activity = new Discord.Activity()
            {
                State = "Discord State not set yet!"
            };
            //Do the same check it would do when a scene changes but when the current one loads
            CheckLevelName(SceneManager.GetActiveScene());
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            try
            {
                discord.RunCallbacks();
            }
            catch (Exception ex)
            {
                Debug.LogError($"DiscordManager lost callbacks!\n{ex}", this);
                this.enabled = false;
            }

        }

        void OnApplicationQuit()
        {
            //Dispose of the rich presence
            try
            {
                discord.Dispose();
            }
            catch (Exception ex)
            {
                Debug.LogError($"DiscordManager was unable to dispose of Discord Activity!\n{ex}", this);
            }
        }

        void RunMethodCheck(Scene scene, LoadSceneMode mode)
        {
            CheckLevelName(scene);
        }
        void CheckLevelName(Scene scene)
        {
            ///Grab the scene name
            string lvlName = scene.name;
            int count = 0;
            long timestamp = 0;
            ///Foreach activity in the activities array, check if any activity's scene value is equal to the scene name
            ///If yes, apply it.
            ///If no, continue until it finds one or doesn't.
            foreach (DiscordActivityObject activityobject in activities)
            {
                if (activityobject.scene == lvlName)
                {
                    count += 1;
                    ///If the big image key is null, set it to the default big icon (instead of leaving it empty)
                    if (string.IsNullOrEmpty(activityobject.bigImageKey))
                    {
                        activityobject.bigImageKey = defaultBigImageKey;
                    }
                    switch (activityobject.timestampMode)
                    {
                        case DiscordActivityObject.TimeStampMode.None:
                            {
                                timestamp = 0;
                                break;
                            }
                        case DiscordActivityObject.TimeStampMode.Elapsed:
                            {
                                timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
                                break;
                            }
                        case DiscordActivityObject.TimeStampMode.Remaining:
                            {
                                Debug.Log("Remaining has not been implemented yet.", this);
                                break;
                            }
                    }

                    ///Parse all the info of the activity in the activity array and actually turn it into...
                    ///- drum roll -
                    ///A discord activity.
                    activity = new Discord.Activity()
                    {
#if UNITY_EDITOR
                        State = $"[EDITOR] {activityobject.state}",
#else
                        State = activityobject.state,
#endif

                        Details = activityobject.details,
                        Timestamps =
                    {
                        Start = timestamp
                    },
                        Assets =
                    {
                        LargeImage = activityobject.bigImageKey,
                        SmallImage = activityobject.smallImageKey,
                        LargeText = activityobject.bigImageKeyText,
                        SmallText = activityobject.smallImageKeyText
                    }

                    };
                    ///Update the activity
                    activityManager.UpdateActivity(activity, (res) =>
                    {
                        if (res == Discord.Result.Ok)
                        {
                            Debug.Log("Status Update Success! [Discord]");
                        }
                        else
                        {
                            Debug.Log("Status Update Failed! [Discord]");
                        }
                    });
                    return;

                }
                else
                {
                    continue;
                }
            }
            if (count == 0)
            {
                activity = new Discord.Activity()
                {
                    State = "No state was found for this scene."
                };
                activityManager.UpdateActivity(activity, (res) =>
                {
                    if (res == Discord.Result.Ok)
                    {
                        Debug.Log("Status Update Success! [Discord]");
                    }
                    else
                    {
                        Debug.Log("Status Update Failed! [Discord]");
                    }
                });
            }

        }




    }
    [System.Serializable]
    public class DiscordActivityObject
    {
        public string scene;
        public string details, state;
        public string bigImageKey, smallImageKey, bigImageKeyText, smallImageKeyText;
        public TimeStampMode timestampMode;
        public enum TimeStampMode
        {
            None,
            Elapsed,
            Remaining
        }
    }
}
