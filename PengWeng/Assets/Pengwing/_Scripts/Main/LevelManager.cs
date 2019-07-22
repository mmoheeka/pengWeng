using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<PengwingManager>
{
    public int nextLevel;

    // Use this for initialization
    void Start()
    {
        nextLevel = PlayerPrefs.GetInt("CurrentLevel");
        StartCoroutine(LoadNextLevel());
    }


    IEnumerator LoadNextLevel()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextLevel);

        while (!asyncLoad.isDone)
        {
            SceneManager.LoadScene(nextLevel);
            yield return null;
        }
    }
}
