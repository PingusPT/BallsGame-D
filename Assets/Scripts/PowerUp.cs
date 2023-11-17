using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : Balls
{

    //--------------------------------------------------------------- inherits from Balls Script ----------------------------------------------------


    Rigidbody2D rd;
    Vector3 spawnposition;
    Vector3 LastVell;
    float distance;
    

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        rd = gameObject.GetComponent<Rigidbody2D>();
        spawnposition = new Vector3(Random.Range(-8f, 8f), Random.Range(-3f, 3f), 0); // Set the position of the powerUp in a random Spot
        
        float distance = Vector3.Distance(spawnposition, BallManager.instance.GetPlayer().transform.position);

        while (distance < 3f) // makes the Power Up dont spawn near the player
        {
            spawnposition = new Vector3(Random.Range(-8f, 8f), Random.Range(3f, -3f), 0);
            distance = Vector3.Distance(BallManager.instance.GetPlayer().transform.position, spawnposition);
        }
        gameObject.transform.position = spawnposition;
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        LastVell = rd.velocity; //Update de lastVelocity of the Ball for the bounce on the walls

    }
    
    public override void RefreshVell()
    {
        LastVell = rd.velocity;
    }
    


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Mid") //Cheks if the object spawns on the Scene2 Obstacle 
        {
            InMid = true;
        }
        else
        {
            InMid = false;
        }
        if (collision.gameObject.tag == "Player") // checks the collision with the player
        {
            spawnposition = new Vector3(Random.Range(-8f, 8f), Random.Range(-4f, 4f), 0); // Change the spot of the powerUp
            distance = Vector3.Distance(collision.transform.position, spawnposition); // Get the distance in between the player and powerUp 

            while (distance < 3f || gameObject.GetComponent<Balls>().RedIsInMid()) // Change the position until the is far from the player and not spawning in the obtacle
            {
                spawnposition = new Vector3(Random.Range(-8f, 8f), Random.Range(-4f, 4f), 0);
                distance = Vector3.Distance(collision.transform.position, spawnposition);
            }

            gameObject.transform.position = spawnposition; // change spot of the powerUp
            
            if(BallManager.instance.CheckBalls()) // only activate power Up habillity if at leat one red ball exist
            {
                TimeManager.instance.PowerUpCaugth();
                BallManager.instance.PowerUpTaked();
                gameObject.SetActive(false);
            }
            
            
        }
        else
        {
            
            Bounce(collision, LastVell, rd); // if the collision isnt with the player Bounce 
        }
    }

    

}
