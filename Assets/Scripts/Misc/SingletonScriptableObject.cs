using System;
using UnityEngine;

public abstract class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject 
{
    static T _instance = null;
    
    public static T Instance
    {
        get
        {
            if (!_instance)
                _instance = FindObjectOfType<T>();
            
            if(!_instance)
				_instance = CreateInstance<T>();
            
            return _instance;
        }
    }

    public void ResetInstance()
    {
        if(_instance != null)
        {
            _instance = null;
            _instance = ScriptableObject.CreateInstance<T>();
        }
    }
}

