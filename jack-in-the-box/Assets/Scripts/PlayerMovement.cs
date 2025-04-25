using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private static readonly int FullCharge = Animator.StringToHash("FullCharge");
    private static readonly int Charge = Animator.StringToHash("Charge");
    private static readonly int YVelocity = Animator.StringToHash("yVelocity");
    private static readonly int Grounded = Animator.StringToHash("Grounded");

    [Header("Movement Settings")]
    public float maxJump;
    public float minJump;
    public float jumpMultiplier;
    public float angleMultiplier;

    [Header("References")]
    public Transform arrow;
    public Animator animator;
    public AudioSource sfxBoxOpenAudioSource;
    public AudioSource sfxJumpBoingAudioSource;
    public AudioSource sfxGroundCollisionAudioSource;
    public AudioSource bgmPopGoesTheWeaselAudioSource;

    [Header("Charge State")]
    [SerializeField]
    private float charger;
    [SerializeField]
    private bool discharge;
    [SerializeField]
    private float angle;

    [Header("Ground Check")]
    private Rigidbody2D rb;
    private bool isGrounded;
    private int groundContacts;

    // charged jump taken from https://discussions.unity.com/t/long-press-for-charged-jump/202543

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 2.5f;
        rb.centerOfMass = new Vector2(0, -1.5f);
        maxJump = 20f;
        minJump = 10f;
        jumpMultiplier = 30f;
        angleMultiplier = 5f;
    }

    private void Update()
    {
        animator.SetFloat(YVelocity, rb.velocity.y);
        animator.SetBool(Grounded, isGrounded);
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
            if (!bgmPopGoesTheWeaselAudioSource.isPlaying)
            {
                bgmPopGoesTheWeaselAudioSource.Play();
            }
            charger += Time.deltaTime;
            animator.SetTrigger(jumpMultiplier * charger < maxJump ? Charge : FullCharge);
        }

        if (Input.GetKeyUp(KeyCode.Space) && isGrounded)
        {
            animator.ResetTrigger("Charge");
            animator.ResetTrigger("FullCharge");
            discharge = true;
        }
    }

    private void FixedUpdate()
    {
        if (discharge)
        {
            bgmPopGoesTheWeaselAudioSource.Stop();
            sfxJumpBoingAudioSource.Play();
            sfxBoxOpenAudioSource.Play();
            
            rb.velocity = new Vector2(rb.velocity.x+angle, Math.Min(minJump + jumpMultiplier * charger, maxJump));
            discharge = false;
            charger = 0f;
            angle = 0;
        }

        var zRot = rb.rotation;
        zRot = Mathf.Clamp(zRot, -45f, 45f);
        rb.MoveRotation(zRot);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Ground")) return;
        groundContacts++;
        isGrounded = true;
        sfxGroundCollisionAudioSource.Play();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Ground")) return;
        groundContacts--;
        if (groundContacts <= 0)
        {
            isGrounded = false;
        }
    }
}
