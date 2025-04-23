using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float maxJump;
    public float minJump;
    public float jumpMultiplier;
    public float angleMultiplier;
    
    // charged jump taken from https://discussions.unity.com/t/long-press-for-charged-jump/202543
    private float charger;
    private bool discharge;
    private float angle;

    private Rigidbody2D rb;
    public Transform arrow;
    public Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 2.5f;
        maxJump = 20f;
        minJump = 10f;
        jumpMultiplier = 30f;
        angleMultiplier = 5f;
    }

    
    void Update()
    {   
        animator.SetFloat("yVelocity", rb.velocity.y);
        if(Input.GetKey(KeyCode.LeftArrow)) 
        {
            angle -= Time.deltaTime * angleMultiplier;
            angle = Math.Max(angle, -20f);
        }
        if(Input.GetKey(KeyCode.RightArrow)) 
        {
            angle += Time.deltaTime * angleMultiplier;
            angle = Math.Min(angle, 20f);
        }
        arrow.rotation = Quaternion.Euler(0,0,-angle*angleMultiplier);
        if (Input.GetKey(KeyCode.Space))
        {
            charger += Time.deltaTime;
            if (jumpMultiplier * charger < maxJump)
            {
                animator.SetTrigger("Charge");
            }
            else
            {
                animator.SetTrigger("FullCharge");
            }
            
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            discharge = true;
            animator.SetTrigger("Jumping");
        }
    }

    private void FixedUpdate()
    {
        if (discharge)
        {
            rb.velocity = new Vector2(rb.velocity.x+angle, Math.Min(minJump + jumpMultiplier * charger, maxJump));
            discharge = false;
            charger = 0f;
            angle = 0;
        }
    }

}
