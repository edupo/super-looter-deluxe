using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[Serializable]
public class UnityEventTransform : UnityEvent<Transform> { }

public class Trigger2DEvents : MonoBehaviour
{
    public UnityEventTransform onTriggerEnter;
    public UnityEventTransform onTriggerExit;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        onTriggerEnter.Invoke(collision.transform);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        onTriggerExit.Invoke(collision.transform);
    }
}
