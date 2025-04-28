using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelSelect : MonoBehaviour, IPointerEnterHandler
{
    public AudioSource buttonClickAudioSource;
    public AudioSource buttonHoverAudioSource;

    public void levelSelect(int levelID)
    {
        StartCoroutine(PlaySoundAndLoad(levelID));
    }

    private IEnumerator PlaySoundAndLoad(int levelID)
    {
        buttonClickAudioSource.Play();
        yield return new WaitForSeconds(buttonClickAudioSource.clip.length);

        string levelName = "level " + levelID;
        SceneManager.LoadScene(levelName);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonHoverAudioSource != null && buttonHoverAudioSource.clip != null)
        {
            buttonHoverAudioSource.Play();
        }
    }
}