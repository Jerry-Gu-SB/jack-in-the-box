using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformControllerStandard : MonoBehaviour
{
    [SerializeField]
    private PhysicsMaterial2D physicsMaterial2D;

    public float platformFriction = 0.7f;
    public float platformBounciness = 0f;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        physicsMaterial2D.friction = platformFriction;
        physicsMaterial2D.bounciness = platformBounciness;

        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.sharedMaterial = null; // force refresh to load custom values
            col.sharedMaterial = physicsMaterial2D;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
