using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] GameObject PowerUpGame;
    GameObject AvoidStuck;
    GameObject powerUp;
    Rigidbody2D rgdPowerUp;

    public static TimeManager instance;
    bool SpawnPowerUp = true;
    bool GetPowerUp = false;
    bool CanGoScene2 = true;
    bool Leave = true;
    
    float GeneralTime = 0;
    float DelayPowerUp = 15;

    // Start is called before the first frame update
    void Start()
    {
        
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            if(powerUp == null)
            {
                powerUp = Instantiate(PowerUpGame, new Vector3(0f, 0f, 0f), Quaternion.identity);
                rgdPowerUp = powerUp.GetComponent<Rigidbody2D>();
                DontDestroyOnLoad(powerUp);
                powerUp.SetActive(false);
            }


            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        AvoidStuck = GameObject.FindGameObjectWithTag("Avoid");
    }

    

    // Update is called once per frame
    void Update()
    {
        


        if(Leave) //Stop Counting the Time if ordered 
        {
            GeneralTime += Time.deltaTime;
        }
        

        if(GeneralTime > 10 && SpawnPowerUp && Leave) // Spawn powerUp
        {
            if(powerUp != null)
            {
                powerUp.SetActive(true);
                rgdPowerUp.AddForce(new Vector3(90.8f, 90.8f, 0f));
                SpawnPowerUp = false;
            }
            
        }
        if(GeneralTime > 29.5f && Leave && CanGoScene2)
        {
            if(AvoidStuck != null)
            {
                AvoidStuck.SetActive(true);
            }
            

        }
        if(GeneralTime > 30 && CanGoScene2 && Leave) // Go To Scene 2 using SceneManager
        {
            CanGoScene2 = false;
            Scenemanager.instance.PlayScene2();
        }
        
        if(GeneralTime > 60 && Leave) // Go to End Sne and Destroi All the balls and player in the game
        {
            
            Leave = false;
            BallManager.instance.TurnOffGreen();
            TimeDestroi();
            Scenemanager.instance.GoToEnd();
            
        }


        if(GetPowerUp) // Delay time to make apear again the power up 
        {
            DelayPowerUp -= Time.deltaTime;

            if(DelayPowerUp < 0)
            {
                SpawnPowerUp = true;
                GetPowerUp = false;
                DelayPowerUp = 15;
            }
        }
    }


    public void PowerUpCaugth() // Get the powerUp
    {
        GetPowerUp = true;
    }

    public void ResetTimeManager() // Reset TimerScript
    {
        SpawnPowerUp = true;
        GetPowerUp = false;
        CanGoScene2 = true;
        GeneralTime = 0;
        Leave = true;
        
    }

    public void StopTimer() // Stop The Timer 
    {
        Leave = false;
    }

    public void TimeDestroi() // Destroi Balls
    {
        BallManager.instance.DestroiList(powerUp);
    }
}
