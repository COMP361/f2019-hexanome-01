using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance;
    
    public virtual void Awake()
    {
        if (instance) {
            Debug.LogError("Duplicate subclass of type " + typeof(T) + "! eliminating " + name + " while preserving " + instance.name);
            Destroy(gameObject);
        } else {
            instance = this as T;
        }
    }

    public virtual void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }
}