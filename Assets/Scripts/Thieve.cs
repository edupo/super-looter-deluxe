using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ThieveStatus
{
    idle,
    following,
    escaping,
}

public class Thieve : MonoBehaviour
{
    public float moveSpeed = 8f;
    public static float stealChance = 0.4f;
    public float escapeChance = 0.5f;
    public ThieveStatus state = ThieveStatus.idle;
    public Transform target = null;

    [Header("Appearance")]
    public List<Sprite> sprites;

    [Header("References")]
    public Rigidbody2D rigidBody;
    public GlobalEvent onStolen;
    public SpriteRenderer spriteRenderer;

    private Vector2 moveDirection;
    private List<Transform> targets = new List<Transform>();

    private void Start()
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Count - 1)];
    }

    void Update()
    {
        switch (state)
        {
            case ThieveStatus.idle:
                moveDirection = Vector2.zero;
                break;
            case ThieveStatus.following:
                if (target) moveDirection = target.position - transform.position;
                break;
            case ThieveStatus.escaping:
                if (target) moveDirection = transform.position - target.position;
                break;
        }
        rigidBody.velocity = moveDirection.normalized * moveSpeed;
    }

    public void AddTarget(Transform target)
    {
        targets.Add(target);
        if (!this.target || this.target.tag != "Player")
            this.target = target;
        state = ThieveStatus.following;
    }

    public void RemoveTarget(Transform target)
    {
        targets.Remove(target);
        if (this.target == target) this.target = null;

        if (this.target == null && targets.Count != 0)
            this.target = targets[Random.Range(0, targets.Count - 1)];

        if(this.target == null)
            state = ThieveStatus.idle;
    }

    private void OnDisable()
    {
        moveDirection = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var actuable = collision.gameObject.GetComponent<Interactive>();
        if (actuable)
        {
            actuable.Actuate();
        }
        var player = collision.gameObject.GetComponent<Player>();
        if (player)
        {
            var loot = player.lootBag.loot;

            if (loot.Count > 0)
            {
                if (Random.value < stealChance)
                {
                    int i = Random.Range(0, loot.Count - 1);
                    var item = loot[i];
                    loot.RemoveAt(i);
                    onStolen.Raise(item);
                    if (Random.value < escapeChance)
                        state = ThieveStatus.escaping;
                }
                else
                    state = ThieveStatus.escaping;
            } else
            {
                state = ThieveStatus.escaping;
            }
        }
        
    }

    private void OnValidate()
    {
        if (!rigidBody)
            rigidBody = GetComponent<Rigidbody2D>();
    }
}
