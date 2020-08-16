using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlacerCircle : MonoBehaviour
{
    public GameObject prefab;
    public int ammount;
    public float radius;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < ammount; i++)
        {
            var pos = Random.insideUnitCircle * radius;
            Instantiate(prefab, pos, Quaternion.identity);
        }        
    }
}
