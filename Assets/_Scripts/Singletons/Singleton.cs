using UnityEngine;

namespace DesignPatterns
{
    /// <summary>
    /// A generic singleton. Only one instance exists and it is destroyed upon changing scenes.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        protected static T _instance;
        
        // Helper Properties / Methods
        public static bool HasInstance = _instance != null;
        public static T TryGetInstance() => HasInstance ? _instance : null;  // If there is no instance, don't try to make a new one.
        
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
                GameObject go = new GameObject($"Auto-Generated Singleton: {typeof(T).Name}");
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
            _instance = this as T;
        }
    }
}
