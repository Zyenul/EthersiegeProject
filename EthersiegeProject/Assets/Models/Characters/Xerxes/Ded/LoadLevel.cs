using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public string levelName; // The name of the level to load

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(2);
    }
}
