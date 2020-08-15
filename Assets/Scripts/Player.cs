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

    private Vector2 moveDirection;

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
}
