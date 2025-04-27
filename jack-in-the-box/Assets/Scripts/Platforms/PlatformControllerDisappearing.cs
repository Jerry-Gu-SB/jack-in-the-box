using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Platforms
{
    public class PlatformControllerDisappearing : MonoBehaviour
    {
        public float disappearReappearDelay = 3f;

        public AudioSource sfxExplosionAudioSource;
        [SerializeField] 
        private BoxCollider2D platformBoxCollider2D;
        [SerializeField]
        private TilemapRenderer platformTilemapRenderer;
    
        private bool triggered = false;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (triggered || !collision.gameObject.CompareTag("Player")) return;
            triggered = true;
            StartCoroutine(DisappearAndReappearAfterDelay());
        }

        private IEnumerator DisappearAndReappearAfterDelay()
        {
            yield return new WaitForSeconds(disappearReappearDelay);
            platformBoxCollider2D.enabled = false;
            platformTilemapRenderer.enabled = false;
            sfxExplosionAudioSource.Play();
        
            yield return new WaitForSeconds(disappearReappearDelay);
            platformBoxCollider2D.enabled = true;
            platformTilemapRenderer.enabled = true;
        }
    }
}

