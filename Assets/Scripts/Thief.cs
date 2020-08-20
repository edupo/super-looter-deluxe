using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ThieveStatus
{
    following,
    escaping,
}

public class Thief : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float stealChance = 0.4f;
    public float escapeChance = 0.5f;
    public ThieveStatus state = ThieveStatus.following;
    public Transform target = null;
    public float minEscapeTime = 3f;
    public float maxEscapeTime = 15f;

    [Header("Appearance")]
    public List<Sprite> sprites;

    [Header("References")]
    public Rigidbody2D rigidBody;
    public GlobalEvent onStolen;
    public SpriteRenderer spriteRenderer;

    public UnityEvent onStoleLocal;

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
            case ThieveStatus.following:
                if (target) moveDirection = target.position - transform.position;
                else moveDirection = Vector2.zero;
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
    }

    public void RemoveTarget(Transform target)
    {
        targets.Remove(target);
        if (this.target == target) this.target = null;

        if (this.target == null && targets.Count != 0)
            this.target = targets[Random.Range(0, targets.Count - 1)];
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
                    onStoleLocal.Invoke();
                    if (Random.value < escapeChance)
                        Scare();
                }
                else
                    Scare();
            } else
            {
                Scare();
            }
        }
        
    }

    public void Scare()
    {
        state = ThieveStatus.escaping;
        Invoke("Prosecute", Random.Range(minEscapeTime, maxEscapeTime));
    }

    public void Prosecute()
    {
        state = ThieveStatus.following;
    }

    private void OnValidate()
    {
        if (!rigidBody)
            rigidBody = GetComponent<Rigidbody2D>();
    }
}
