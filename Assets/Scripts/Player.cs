using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class Player : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float throwForce = 100f;
    public float throwTorque = 20f;

    [Header("References")]
    public Rigidbody2D rigidBody;
    public Loot lootBag;
    public GameObject itemThrowPrefab;
    public AudioSource audioSource;

    [Header("Events")]
    public GlobalEvent picked;
    public GlobalEvent thrown;

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

    public void Throw(InputAction.CallbackContext context)
    {
        if (context.performed
            && lootBag.loot.Count > 0 
            && moveDirection != Vector2.zero)
        {
            GameObject go = Instantiate(itemThrowPrefab, transform.position, Quaternion.identity);
            var item = go.GetComponent<Item>();
            int i = Random.Range(0, lootBag.loot.Count - 1);
            var itemData = lootBag.loot[i];
            lootBag.loot.RemoveAt(i);

            var rb = go.GetComponent<Rigidbody2D>();
            rb.AddForce(moveDirection.normalized * throwForce);
            rb.AddTorque(throwTorque);

            itemData.value = itemData.value / 2;
            if (!itemData.description.StartsWith("thrown"))
                itemData.description = "thrown " + itemData.description;
            item.data = itemData;
            thrown.Raise(item.data);
        }
    }

    private void OnValidate()
    {
        if (!rigidBody)
            rigidBody = GetComponent<Rigidbody2D>();
        if (!audioSource)
            audioSource = GetComponent<AudioSource>();
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
                PlayAudio(item.data.audio);
            }
            actuable.Actuate();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var thieve = collision.gameObject.GetComponent<Thief>();
        if (thieve) thieve.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var thieve = collision.gameObject.GetComponent<Thief>();
        if (thieve) thieve.enabled = false;
    }

    private void PlayAudio(AudioClip clip)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}

