using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    delegate void MultiDelegate();
    MultiDelegate myDelegate;
    MultiDelegate myDelegateV2;

    [SerializeField] GameObject RedBall;
    [SerializeField] GameObject RedFocus;
    [SerializeField] GameObject GreenBallPrefab;
    [SerializeField] GameObject PlayerPrefab;
    List<GameObject> BallsList = new List<GameObject>();

    GameObject BallAdd;
    GameObject GreenBall;
    GameObject player;
    public static BallManager instance;

    float distance = 0;
    bool Small = false;
    float SmallTime = 10f;
    

    // Start is called before the first frame update
    void Start()
    {



        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
            
            if (GreenBall == null) //Ensure that there are not two green balls and if doesent exist one spawn one
            {

                GreenBall = Instantiate(GreenBallPrefab, new Vector3(Random.Range(-8f, 8f), Random.Range(-3.5f, 3.5f), 0), Quaternion.identity);
                
            }
            else
            {
                GreenBall.SetActive(true);
            }

            if(player == null)//Ensure that there are not two players and if doesent exist one spawn one
            {
                player = Instantiate(PlayerPrefab, new Vector3(Random.Range(-8f, 8f), Random.Range(-4f, 4f), 0), Quaternion.identity);

            }
            else
            {
                player.SetActive(true);
            }
            
        }
        else
        {
            Destroy(gameObject);
            
            
        }

    }
        

    // Update is called once per frame
    void Update()
    {
        if(Small) //Count the time of the balls being small
        {
            SmallTime -= Time.deltaTime;

            if(SmallTime < 0 )
            {
                myDelegateV2();
                Small = false;
                SmallTime = 10;
            }
        }
    }

    public void SpawnFocus(Collision2D collision)
    {
        Vector3 spawnposition = new Vector3(Random.Range(-8f, 8f), Random.Range(-3f, 3f), 0); // Create a new spawn Position for the red ball to spawn
        distance = Vector3.Distance(collision.transform.position, spawnposition); // Get the distance between this spawn and the player
        BallAdd = Instantiate(RedFocus, spawnposition, Quaternion.identity);// Add the ball to the game
       
        while (distance < 2f || BallAdd.GetComponent<Balls>().RedIsInMid()) // Change the ball position until is a good distance from the player 
       {
          spawnposition = new Vector3(Random.Range(-8f, 8f), Random.Range(-3f, 3f), 0);
          distance = Vector3.Distance(collision.transform.position, spawnposition);
       }
        
        BallAdd.transform.position = spawnposition; // change to final position


        // Add the spawned ball to the delegates, and add the ball to a list for later be destroid 

        myDelegate += BallAdd.GetComponent<Balls>().SrinkBall;
        myDelegateV2 += BallAdd.GetComponent<Balls>().GrowBalls;
        BallsList.Add(BallAdd);
        DontDestroyOnLoad(BallAdd);
    }
    public void SpawnRed(Collision2D collision)
    {
        Vector3 spawnposition = new Vector3(Random.Range(-8f, 8f), Random.Range(-3f, 3f), 0);// Create a new spawn Position for the red ball to spawn
        distance = Vector3.Distance(collision.transform.position, spawnposition);// Get the distance between this spawn and the player
        BallAdd = Instantiate(RedBall, spawnposition, Quaternion.identity);// Add the ball to the game

        while (distance < 2f || BallAdd.GetComponent<Balls>().RedIsInMid())// Change the ball position until is a good distance from the player 
        {
          spawnposition = new Vector3(Random.Range(-8f, 8f), Random.Range(3f, -3f), 0);
          distance = Vector3.Distance(collision.transform.position, spawnposition);
       }
        
        BallAdd.transform.position = spawnposition;// change to final position

        // Add the spawned ball to the delegates, and add the ball to a list for later be destroid 

        myDelegate += BallAdd.GetComponent<Balls>().SrinkBall;
        myDelegateV2 += BallAdd.GetComponent<Balls>().GrowBalls;
        BallsList.Add(BallAdd);
        DontDestroyOnLoad(BallAdd);
    }

    public void DestroiList(GameObject PowerUp)
    {
        
        
        if(PowerUp != null) //if the power up exist turn off
        {
            
            PowerUp.SetActive(false);
        }
       
        
        
        
        foreach (GameObject obj in BallsList) // Destroi every ball on the list and removing from the delegates
        {
            if(obj != null)
            {
                myDelegate -= obj.GetComponent<Balls>().SrinkBall;
                myDelegateV2 -= obj.GetComponent<Balls>().GrowBalls;
                Small = false;
                SmallTime = 10;
                
                Destroy(obj);
            }
            
        }
        BallsList.Clear(); //Clears the list
    }

    public void PowerUpTaked() //activate de delegate when the powerUp is taken
    {
        
        myDelegate();
        Small = true;
    }

    public bool CheckBalls() // check is the list of red balls is empty
    {
        if(BallsList.Count.Equals(0))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void TurnOffGreen()//turn of green xD
    {
        GreenBall.SetActive(false);
    }

    public void TurnOnGreenAndPlayer()// Activate the player and the greenBall
    {

        GreenBall.SetActive(true);
        player.SetActive(true);
        GreenBall.GetComponent<GreenBall>().ForceAdd();

    }

    public GameObject GetPlayer()// Give acess to the player to the others scripts
    {
        return player;
    }
}
