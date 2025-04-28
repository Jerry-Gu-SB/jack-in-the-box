using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryFlagManager : MonoBehaviour
{
    public AudioSource victoryJingleAudioSource;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        StartCoroutine(Victory());
        
    }

    private IEnumerator Victory()
    {
        victoryJingleAudioSource.Play();
        yield return new WaitForSeconds(victoryJingleAudioSource.clip.length);
        SceneManager.LoadScene("Scenes/LevelSelectorScreen");
    }
}
