using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltingPlatformController : PlatformControllerStandard
{
    public float maxTiltAngle = 30f;
    public float tiltSpeed = 1f;

    private float originalZRotation;

    // Start is called before the first frame update
    void Start()
    {
        originalZRotation = transform.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        float angle = Mathf.Sin(Time.time * tiltSpeed) * maxTiltAngle;
        transform.rotation = Quaternion.Euler(0, 0, originalZRotation + angle);
    }
}


