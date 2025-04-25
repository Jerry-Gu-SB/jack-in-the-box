using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float maxJump;
    public float minJump;
    public float jumpMultiplier;
    public float angleMultiplier;

    [Header("References")]
    public Transform arrow;
    public Animator animator;

    [Header("Charge State")]
    private float charger;
    private bool discharge;
    private float angle;

    [Header("Ground Check")]
    private Rigidbody2D rb;
    private bool isGrounded = false;
    private int groundContacts = 0;

    // charged jump taken from https://discussions.unity.com/t/long-press-for-charged-jump/202543
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 2.5f;
        rb.centerOfMass = new Vector2(0, -1.5f);
        maxJump = 20f;
        minJump = 10f;
        jumpMultiplier = 30f;
        angleMultiplier = 5f;
    }

    void Update()
    {
        animator.SetFloat("yVelocity", rb.velocity.y);
        animator.SetBool("Grounded", isGrounded);
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

        if (Input.GetKeyUp(KeyCode.Space) && isGrounded)
        {
            animator.ResetTrigger("Charge");
            animator.ResetTrigger("FullCharge");
            discharge = true;
        }
    }

    void FixedUpdate()
    {
        if (discharge)
        {
            rb.velocity = new Vector2(rb.velocity.x+angle, Math.Min(minJump + jumpMultiplier * charger, maxJump));
            discharge = false;
            charger = 0f;
            angle = 0;
        }

        float zRot = rb.rotation;
        zRot = Mathf.Clamp(zRot, -45f, 45f);
        rb.MoveRotation(zRot);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            groundContacts++;
            isGrounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            groundContacts--;
            if (groundContacts <= 0)
            {
                isGrounded = false;
            }
        }
    }
}
