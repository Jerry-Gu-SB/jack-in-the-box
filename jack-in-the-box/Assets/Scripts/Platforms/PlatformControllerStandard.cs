using UnityEngine;

namespace Platforms
{
    public class PlatformControllerStandard : MonoBehaviour
    {
        [SerializeField]
        private PhysicsMaterial2D physicsMaterial2D;

        public float platformFriction = 0.7f;
        public float platformBounciness = 0f;

        // Start is called before the first frame update
        private void Start()
        {
            physicsMaterial2D.friction = platformFriction;
            physicsMaterial2D.bounciness = platformBounciness;

            var col = GetComponent<Collider2D>();
            if (col == null) return;
            col.sharedMaterial = null; // force refresh to load custom values
            col.sharedMaterial = physicsMaterial2D;
        }
    }
}
