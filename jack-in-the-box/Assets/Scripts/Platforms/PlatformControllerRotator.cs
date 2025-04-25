using UnityEngine;

namespace Platforms
{
    public class TiltingPlatformController : MonoBehaviour
    {
        public float maxTiltAngle = 30f;
        public float tiltSpeed = 1f;

        [SerializeField]
        private Transform platformTransform;

        private float originalZRotation;

        // Start is called before the first frame update
        void Start()
        {
            originalZRotation = platformTransform.eulerAngles.z;
        }

        // Update is called once per frame
        void Update()
        {
            float angle = Mathf.Sin(Time.time * tiltSpeed) * maxTiltAngle;
            platformTransform.rotation = Quaternion.Euler(0, 0, originalZRotation + angle);
        }
    }
}


