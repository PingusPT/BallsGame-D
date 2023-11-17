using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenemanager : MonoBehaviour
{

    // This SceneManager is a Singleton and can be acessed by enveryone


    static public Scenemanager instance;
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null) // Let only one time of this gameobject exist
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
    

    public void PlayGameScene1() // Play normal game Scene 1
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void PlayScene2() // play normal game Scene 2 
    {
        SceneManager.LoadScene("SampleScene 1");
    }

    public void GoToMenu() // Go to Menu Scene
    {
        SceneManager.LoadScene("Menu");
    }

    public void GoToEnd() // Go to End Scene and destroy itself because of the assosiation with the button on that scene
    {
        ScoreBoard.instance.Destroi();
        SceneManager.LoadScene("End");
        
        Destroy(gameObject);
    }

    public void Exit() // Exit Game
    {
        Application.Quit();
    }

    public void ResetGame() // Reset the game stats
    {

        ScoreBoard.instance.Destroi();
        BallManager.instance.TurnOnGreenAndPlayer();
        ScoreManager.instance.ResetScore();
        TimeManager.instance.ResetTimeManager();
        PlayGameScene1();
    }

    
}
