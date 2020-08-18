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
        rigidBody.velocity = moveDirection * moveSpeed;
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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var item = collision.gameObject.GetComponentInParent<Item>();
        if (item)
        {
            item.Picked();
            lootBag.Picked(item);
            picked.Raise(item);
        }
    }
}
