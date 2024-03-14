using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns
{
    /// <summary>
    /// A singleton that regulates itself. It will destroy all other older components of the same type,
    /// ensuring that it is the one and only instance.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RegulatorSingleton<T> : MonoBehaviour where T : Component
    {
        protected static T _instance;
        private static float _creationTime;
        
        // Helper Properties / Methods
        public static bool HasInstance = _instance != null;
        public float CreationTime { get { return _creationTime; } }
        
        public static T Instance
        { 
            get
            {
                // An instance already exists.
                if (_instance != null) return _instance;

                // Check to see if an instance already exists is any GameObject in all open scenes.
                _instance = FindAnyObjectByType<T>();
                if (_instance != null) return _instance;

                // No instance found, so a new one will be made.
                GameObject go = new GameObject($"Auto-Generated Singleton: {typeof(T).Name}")
                {
                    // Since regulators destroy other singletons, this will replace other singletons in a scene. 
                    // We don't want to save this to NOW BE the singleton for that scene.
                    // Instead, we won't save it, so as to make sure the previous singleton that this is replacing, stays as the singleton for that scene.
                    hideFlags = HideFlags.HideAndDontSave
                };
                _instance = go.AddComponent<T>();
                return _instance;
            }
        }

        // Called when a new instance is made.
        // Call base.Awake() for any child overriding the Awake function.
        protected virtual void Awake()
        {
            InitializeSingleton();
        }

        protected void InitializeSingleton()
        {
            if (!Application.isPlaying) return;
            _creationTime = Time.time;
            DontDestroyOnLoad(gameObject);

            T[] oldInstances = FindObjectsByType<T>(FindObjectsSortMode.None);
            foreach (T old in oldInstances)
            {
                if (old.GetComponent<RegulatorSingleton<T>>().CreationTime < _creationTime) Destroy(old.gameObject);
            }

            if (_instance == null)  _instance = this as T;
        }
    }
}
