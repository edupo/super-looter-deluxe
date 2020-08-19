using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class Player : MonoBehaviour
{
    public float moveSpeed = 10f;

    [Header("References")]
    public Rigidbody2D rigidBody;
    public Loot lootBag;

    [Header("Events")]
    public GlobalEvent picked;

    private Vector2 moveDirection;

    private void Start()
    {
        lootBag.Clean();
    }

    void Update()
    {
        rigidBody.velocity = moveDirection.normalized * moveSpeed;
    }

    public void MovePerformed(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }
    private void OnValidate()
    {
        if (!rigidBody)
            rigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var actuable = collision.gameObject.GetComponent<Interactive>();
        if (actuable)
        {
            if (actuable is Item)
            {
                Item item = actuable as Item;
                lootBag.Picked(item.data);
                picked.Raise(item.data);
            }
            actuable.Actuate();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var thieve = collision.gameObject.GetComponent<Thieve>();
        if (thieve) thieve.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var thieve = collision.gameObject.GetComponent<Thieve>();
        if (thieve) thieve.enabled = false;
    }
}

