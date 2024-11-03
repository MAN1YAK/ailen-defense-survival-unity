using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyMenu : MonoBehaviour
{
    public int difficulty;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startGame()
    {
        int dataToKeep = difficulty;
        StaticData.valueToKeep = dataToKeep;
        PreviousSceneTracker.Instance.prevScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Game");
        AudioManager.instance.Play("Theme");
        AudioManager.instance.StopPlaying("MainMenuTheme");

        Time.timeScale = 1f;

    }

    public void easy()
    {
        AudioManager.instance.Play("Button");
        difficulty = 0;
        startGame();
        Debug.Log("The game is: " + difficulty);

    }
    public void normal()
    {
        AudioManager.instance.Play("Button");
        difficulty = 1;
        startGame();
        Debug.Log("The game is: " + difficulty);
    }
    public void hard()
    {
        AudioManager.instance.Play("Button");
        difficulty = 2;
        startGame();
        Debug.Log("The game is: " + difficulty);
    }


}
