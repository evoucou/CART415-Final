using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse : MonoBehaviour
{
    private Transform pulseTransform;
    private float range = 0.5f;
    private float rangeMax = 1.1f;

    // Start is called before the first frame update
    void Start()
    {
        pulseTransform = transform.Find("Pulse");
    }

    // Update is called once per frame
    void Update()
    {
        float rangeSpeed = 0.5f;
        range += rangeSpeed * Time.deltaTime;
        if (range > rangeMax) range = 0.5f;
        pulseTransform.localScale = new Vector3(range, range);
    }
}
