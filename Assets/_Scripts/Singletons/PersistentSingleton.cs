using UnityEngine;

namespace DesignPatterns
{
    /// <summary>
    /// A persistent singleton that persists between scene changes.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PersistentSingleton<T> : MonoBehaviour where T : Component
    {
        protected static T _instance;
        public bool _autoUnparentOnAwake = true; // If set to false and the game object is a child, the caller will need to unparent the object themselves
                                                 // or else this will instead act like a normal, non-persistent, generic singleton.
        
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
            if (_autoUnparentOnAwake) transform.SetParent(null);

            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                if (_instance != this) Destroy(gameObject);
            }

        }
    }
}
