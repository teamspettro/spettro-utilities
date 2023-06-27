using System;
using UnityEngine;

//Taken from MyBox https://github.com/Deadcows/MyBox/blob/master/LICENSE.md
namespace Spettro
{
	public abstract class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
	{
		public static T Instance
		{
			get
			{
				if (singleton == null) singleton = FindObjectOfType<T>();
				if (singleton == null && !isMissing)
                {
                    Debug.LogWarning($"Could not find instance of {typeof(T).Name} in the scene. Please set it in the inspector.");
                    isMissing = true;
                }

				return singleton;
			}
		}
		public static string Tag
		{
			get 
			{
				if (Instance != null)
					return Instance.gameObject.tag;
				else
					return null;
			}
		}
		private static bool isMissing;
		private static T singleton;


		/// <summary>
		/// Use this function to cache instance and destroy duplicate objects.
		/// Also use DontDestroyOnLoad if "persistent" is not set to false
		/// </summary>
		protected void InitializeSingleton(bool persistent = true)
		{
			if (singleton == null)
			{
				singleton = (T)Convert.ChangeType(this, typeof(T));
				if (persistent) DontDestroyOnLoad(singleton);
			}
			else
			{
				Debug.LogWarning($"Another instance of \"{typeof(T).Name}\" wasdetected on \"{name}\", so it was removed.", gameObject);
				Destroy(this);
			}
		}
	}
}