using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnImpact : MonoBehaviour
{
    public static int layerAfter;

    private void Awake()
    {
         layerAfter = LayerMask.NameToLayer("Interactuable");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var thief = collision.gameObject.GetComponent<Thief>();
        if (thief)
            thief.Scare();
        Destroy(this);
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        gameObject.layer = layerAfter;
    }
}
