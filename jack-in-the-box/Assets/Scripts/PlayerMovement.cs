using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float jumpForce = 20f;
    public float angleMultiplier = 10f;

    [Header("References")]
    public Transform arrow;
    public AudioSource SFXJump;
    public AudioSource SFXJackInTheBoxOpen;

    private float angle;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 2.5f;
    }

    void Update()
    {
        HandleInput();
    }

    
    public void TriggerJump()
    {
        ApplyJumpForce();
        PlayJumpSounds();
    }

    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            angle -= Time.deltaTime * angleMultiplier;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            angle += Time.deltaTime * angleMultiplier;
        }
        if (arrow != null)
        {
            arrow.rotation = Quaternion.Euler(0, 0, -angle * angleMultiplier);
        }
    }

    private void ApplyJumpForce()
    {
        rb.velocity = new Vector2(rb.velocity.x + angle, jumpForce);
        angle = 0f;
    }

    private void PlayJumpSounds()
    {
        if (SFXJump != null && !SFXJump.isPlaying)
        {
            SFXJump.Play();
        }
        if (SFXJackInTheBoxOpen != null && !SFXJackInTheBoxOpen.isPlaying)
        {
            SFXJackInTheBoxOpen.Play();
        }
    }
}

