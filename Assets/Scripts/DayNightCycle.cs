using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using System;

public class DayNightCycle : MonoBehaviour
{
    public Light2D light;
    [Range(0f, 1f)]
    public float status = 0f;
    public float duty_cycle = 10f;
    public Gradient gradient;

    private void Update()
    {
        status += Time.deltaTime / duty_cycle;
        light.color = gradient.Evaluate(Mathf.PingPong(status, 1f));
    }

    private void OnValidate()
    {
        if (!light)
            light = GetComponent<Light2D>();
    }
}
