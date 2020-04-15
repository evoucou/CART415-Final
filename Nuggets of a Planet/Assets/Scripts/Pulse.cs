using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse : MonoBehaviour
{
    public Transform pulseTransform;
    private float range = 0.5f;
    public float rangeMax = 1.1f;
    public float rangeSpeed = 0.5f;


    // Update is called once per frame
    void Update()
    {
        // float rangeSpeed = 0.5f;
        range += rangeSpeed * Time.deltaTime;
        if (range > rangeMax) range = 0.5f;
        pulseTransform.localScale = new Vector3(range, range);
    }
}
