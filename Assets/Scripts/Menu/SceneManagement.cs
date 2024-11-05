using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public GameObject Controls;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToMainMenu()
    {
        AudioManager.instance.Play("Button");
        PreviousSceneTracker.Instance.prevScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1f;

        if (PreviousSceneTracker.Instance.prevScene != "How to play")
        {
            AudioManager.instance.Play("MainMenuTheme");
            AudioManager.instance.StopPlaying("Theme");
        }
    }

    public void ToInGame()
    {
        AudioManager.instance.Play("Button");
        PreviousSceneTracker.Instance.prevScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Difficulty");
    }

    public void ToHowToPlay()
    {
        AudioManager.instance.Play("Button");
        PreviousSceneTracker.Instance.prevScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("How to play");
    }

    public void ToPrevScene()
    {
        AudioManager.instance.Play("Button");
        if (SceneManager.GetActiveScene().name == "How to play")
        {
            if(SceneManager.GetActiveScene().buildIndex == 0)
            {
                SceneManager.LoadScene("Main Menu");
            }
            else
            {
                SceneManager.LoadScene("Game");
            }
        }

        PreviousSceneTracker.Instance.prevScene = SceneManager.GetActiveScene().name;
    }

    public void ButtonSound()
    {
        AudioManager.instance.Play("Button");
    }

    public void ToExitGame()
    {
        AudioManager.instance.Play("Button");
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
