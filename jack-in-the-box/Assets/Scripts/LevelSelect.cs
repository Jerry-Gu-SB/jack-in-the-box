using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
   public void levelSelect(int levelID) 
   {
        string levelName = "level "+levelID;
        SceneManager.LoadScene(levelName);
   }
}
