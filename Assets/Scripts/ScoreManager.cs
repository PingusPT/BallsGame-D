using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{

    // This ScoreManager is a Singleton and can be acessed by enveryone

    public static ScoreManager instance;
    int Score = 0;
    int HighScore = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        HighScore = SaveScript.instance.GetBinarySave();  // Get The HighScore from the FileStream

        DontDestroyOnLoad(gameObject);
        if (instance == null)// Let only one time of this gameobject exist
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
    


    public void PrintScore() // Give a order to the ScoreBoard to print the Score
    {
        ScoreBoard.instance.PrintGameScore(Score);
    }

    public void IncrementScore() // Increment Score and if is Higher from the actual HighScore set a new HighScore and Refresh the Score
    {
        Score++;
        if(Score > HighScore)
        {
            HighScore = Score;
            SaveScript.instance.SaveBinaryGame(HighScore);
        }
        PrintScore();
    }

    public void PrintFinalScore() // Give a order to the ScoreBoard to print the Final Score 
    {
        HighScore = SaveScript.instance.GetBinarySave();
        ScoreBoard.instance.PrintEndScore(Score, HighScore);
    }

    public void ResetScore() // Reset the Score
    {
        Score = 0;
    }
    
}
