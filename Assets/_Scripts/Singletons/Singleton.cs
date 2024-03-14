using UnityEngine;

namespace DesignPatterns
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        protected static T _instance;

        // Getters/Setters
        

        public static T Instance
        { 
            get
            {
                // An instance exists, return it.
                if (_instance != null) return _instance;

                // Check to see if an instance exists is any GameObject in all open scenes.
                _instance = FindAnyObjectByType<T>();
                if (_instance != null) return _instance;

                // No instance found, so a new one will be made.
                GameObject go = new GameObject($"Auto-Generated Singleton: {typeof(T).Name}");
                _instance = go.AddComponent<T>();
                return _instance;
            }
        }

        protected void Awake()
        {
            
        }
    }
}
