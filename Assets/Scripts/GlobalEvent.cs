using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Global Event")]
public class GlobalEvent : ScriptableObject
{
    private List<GlobalEventListener> listeners = new List<GlobalEventListener>();

    public void Raise(object obj = null)
    {
        for (int i = listeners.Count - 1; i >= 0 ; i--)
        {
            listeners[i].OnEventRaise(obj);
        }
    }

    public void RegisterListener(GlobalEventListener listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterListener(GlobalEventListener listener)
    {
        listeners.Remove(listener);
    }
}
