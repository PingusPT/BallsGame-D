using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBall : Balls
{

    //--------------------------------------------------------------- inherits from Balls Script ----------------------------------------------------


    Rigidbody2D rgb3g;
    Vector3 Lastvelocity;
    Vector3 spawnposition;
    int rand;
    float distance;
    
    
    // Start is called before the first frame update
    void Start()
    {

        
        DontDestroyOnLoad(gameObject); // Keeps the ball on the Scenes
        rgb3g = gameObject.GetComponent<Rigidbody2D>();
        rgb3g.AddForce(new Vector3(90.8f, 90.8f, 0f));
    }

    


    // Update is called once per frame
    void Update()
    {
        Lastvelocity = rgb3g.velocity; // Update the last Velocity of the GreenBall
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Mid")//Cheks if the object spawns on the Scene2 Obstacle
        {
            InMid = true;
        }
        else
        {
            InMid = false;
        }
        if (collision.gameObject.tag == "Player")// checks the collision with the player
        {
            ScoreManager.instance.IncrementScore(); // Adds 1 to the Score
            ChangeSpot(collision);
            SpawnNew(collision);
        }
        Bounce(collision, Lastvelocity, rgb3g);   
    }

    public void ChangeSpot(Collision2D collision) // Change the Spot of the Green Ball while keeps distance and dont spawn in the obstacle
    {
        
        
            spawnposition = new Vector3(Random.Range(-8f, 8f), Random.Range(-4f, 4f), 0);
            distance = Vector3.Distance(collision.transform.position, spawnposition);

            while (distance < 3f || gameObject.GetComponent<Balls>().RedIsInMid())
            {
                spawnposition = new Vector3(Random.Range(-8f, 8f), Random.Range(-4f, 4f), 0);
                distance = Vector3.Distance(collision.transform.position, spawnposition);
            }

        gameObject.transform.position = spawnposition;
        ForceAdd();



        
    }

    virtual public void SpawnNew(Collision2D collision) // Create a random number 0 or 1 and spawn a Normal Red Ball if 0 and if 1 spawn a Focus Red Ball
    {
        rand = Random.Range(0, 2);
        switch (rand)
        {
            case 0:
                {
                    BallManager.instance.SpawnRed(collision);
                    return;
                }
            case 1:
                {
                    BallManager.instance.SpawnFocus(collision);
                    return;
                }
        }
    }


    public void ForceAdd() // Adds inicial force for the Green Ball avoiding 0 vallue
    {
        Vector3 flag = new Vector3(Random.Range(-80f, 80f), Random.Range(-80f, 80f), 0f);
        while(flag.x == 0 || flag.y == 0)
        {
            flag = new Vector3(Random.Range(-80f, 80f), Random.Range(-80f, 80f), 0f);
        }
        rgb3g.AddForce(flag);
       
    }
}
