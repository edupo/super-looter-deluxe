using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class RefUnityEvent : UnityEvent<object> { }
public class GlobalEventListener : MonoBehaviour
{
    public GlobalEvent Event;
    public RefUnityEvent Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);   
    }
    private void OnDisable()
    {
        Event.UnregisterListener(this);   
    }

    public void OnEventRaise(object obj)
    {
        Response.Invoke(obj);
    }
}
