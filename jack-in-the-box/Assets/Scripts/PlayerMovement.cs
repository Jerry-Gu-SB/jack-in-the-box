using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpForce = 10f;
    public float maxJump = 100f;
    
    // charged jump taken from https://discussions.unity.com/t/long-press-for-charged-jump/202543
    private float charger;
    private Boolean discharge;
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            charger += Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            discharge = true;
        }
    }

    private void FixedUpdate()
    {
        if (discharge)
        {
            jumpForce = Math.Min(10 * charger,maxJump);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            discharge = false;
            charger = 0f;
        }
    }

}
