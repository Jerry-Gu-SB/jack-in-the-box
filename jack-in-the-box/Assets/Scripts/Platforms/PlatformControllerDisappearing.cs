using System.Collections;
using UnityEngine;

public class PlatformControllerDisappearing : MonoBehaviour
{
    public float disappearDelay = 3f;
    private bool triggered = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!triggered && collision.gameObject.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(DisappearAfterDelay());
        }
    }

    private IEnumerator DisappearAfterDelay()
    {
        yield return new WaitForSeconds(disappearDelay);
        Destroy(gameObject);
    }
}

