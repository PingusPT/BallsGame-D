using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    static public ScoreBoard instance;
    TextMeshProUGUI Text;
    // Start is called before the first frame update
    void Start()
    {
        
        Text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        
        DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        if(ScoreManager.instance != null) // Print the Final Score 
        {
            if (gameObject.tag == "end")
            {
                ScoreManager.instance.PrintFinalScore();
            }
        }
        
        
    }

    

    public void PrintGameScore(int Score)
    {
        Text.text = "Score : " + Score;
    }

    public void PrintEndScore(int Score, int HighScore)
    {
        Text.text = "Score : " + Score + "\nHighScore : " + HighScore;
    }

    public void Destroi()
    {
        Destroy(gameObject);
    }
}
