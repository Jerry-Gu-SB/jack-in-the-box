using UnityEngine;

namespace Platforms
{
    public class TiltingPlatformController : MonoBehaviour
    {
        public float maxTiltAngle = 30f;
        public float tiltSpeed = 1f;

        [SerializeField]
        private Transform platformTransform;
        public AudioSource platformLandingAudioSource;

        private float originalZRotation;

        // Start is called before the first frame update
        private void Start()
        {
            originalZRotation = platformTransform.eulerAngles.z;
        }

        // Update is called once per frame
        private void Update()
        {
            float angle = Mathf.Sin(Time.time * tiltSpeed) * maxTiltAngle;
            platformTransform.rotation = Quaternion.Euler(0, 0, originalZRotation + angle);
        }
    
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                platformLandingAudioSource.Play();
            }
        }
    }
}


