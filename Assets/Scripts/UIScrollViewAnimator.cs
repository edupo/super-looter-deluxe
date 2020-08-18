using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIScrollViewAnimator : MonoBehaviour
{
    public ScrollRect scroll;
    public float speed;
    // Update is called once per frame
    private void Start()
    {
        scroll.verticalNormalizedPosition= 0f;
    }
    void Update()
    {
        scroll.velocity = Vector2.down * speed;
    }
    private void OnValidate()
    {
        if (scroll == null)
            scroll = GetComponent<ScrollRect>();
    }
}
