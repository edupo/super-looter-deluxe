using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Chest : MonoBehaviour
{
    public GameObject itemPrefab;
    public int minItems;
    public int maxItems;
    public AnimationCurve probability;
    public float radius;

    public UnityEvent beforeOpen;

    public void Open()
    {
        int numItems = Mathf.RoundToInt(probability.Evaluate(Random.value) * (maxItems - minItems)) + minItems;
        for (int i = 0; i < numItems; i++)
        {
            ThrowItem(Random.insideUnitCircle* radius);
        }
    }

    public void ThrowItem(Vector2 force)
    {
        var go = Instantiate(itemPrefab, transform.position, Quaternion.identity);
        var rb = go.GetComponent<Rigidbody2D>();
        rb.AddForce(force);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        beforeOpen.Invoke();
        Open();
    }
}
