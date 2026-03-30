using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] AudioSource MenuMusic;
    [SerializeField] SaveManager saveManager;

    void Start()
    {
        MenuMusic.Play();
    }

    public void StartGame()
    {
        MenuMusic.Stop();
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        // Load scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Game");

        // Show loading screen or progress bar here
        while (!asyncLoad.isDone)
        {
            float progress = asyncLoad.progress;
            Debug.Log("Loading: " + progress);
            yield return null;
        }
        if(asyncLoad.isDone)
        {
            Debug.Log("Scene loaded successfully!");
            saveManager.LoadGame();
        }
        yield break;
    }
}
