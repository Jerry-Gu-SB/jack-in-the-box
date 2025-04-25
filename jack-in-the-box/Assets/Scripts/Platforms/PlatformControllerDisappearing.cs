using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

namespace Platforms
{
    public class PlatformControllerDisappearing : MonoBehaviour
    {
        public float disappearDelay = 3f;
        
        public TilemapRenderer tilemapRenderer;
        public BoxCollider2D boxCollider;
        public AudioSource explosionAudioSource;
        
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag("Player")) return;
            StartCoroutine(DisappearAfterDelay());
        }

        private IEnumerator DisappearAfterDelay()
        {
            yield return new WaitForSeconds(disappearDelay);
            boxCollider.enabled = false;
            tilemapRenderer.enabled = false;
            explosionAudioSource.Play();
            StartCoroutine(ReappearAfterDelay());
        }

        private IEnumerator ReappearAfterDelay()
        {
            yield return new WaitForSeconds(disappearDelay);
            boxCollider.enabled = true;
            tilemapRenderer.enabled = true;
        }
    }
}

