using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailIntensity : MonoBehaviour
{
    TrailRenderer trail;
    Rigidbody body;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        trail = GetComponent<TrailRenderer>();
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        speed = body.velocity.magnitude;
        trail.time = speed;
    }
}
