using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ChestItem : MonoBehaviour
{
    public float delay = 1f;
    public Rigidbody2D rb;

    private void Start()
    {
        Invoke("Destroy", delay);
    }

    private void Destroy()
    {
        Destroy(rb);
        Destroy(this);
    }

    private void OnValidate()
    {
        if (!rb)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }
}
