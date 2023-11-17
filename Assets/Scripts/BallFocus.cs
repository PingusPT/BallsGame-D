using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFocus : Balls
{
    GameObject player;
    Rigidbody2D rgb2d;
    float BounceForce = 3; // Set the force of the Bounce
    
    // Start is called before the first frame update
    private void Awake()
    {
        //player = GameObject.Find("Player"); // GetPlayer
        rgb2d = gameObject.GetComponent<Rigidbody2D>();

    }
    

    public override void Bounce(Collision2D collision, Vector3 lastVel, Rigidbody2D rgb) // Override the Bounce To go directly to the player
    {

        player = BallManager.instance.GetPlayer(); // Get the player

        Vector2 Direction = (player.transform.position - transform.position).normalized; // Direction of the player

        if(collision.gameObject.tag == "Mid")//Cheks if the object spawns on the Scene2 Obstacle
        {
            InMid = true;
        }
        else
        {
            InMid = false;
        }
        

        if (gameObject.tag == "RedFocus" && collision.gameObject.tag == "Player")
        {

            collision.gameObject.GetComponent<Player>().DeathAnimation();
            
            TimeManager.instance.StopTimer();
            TimeManager.instance.TimeDestroi();
            Scenemanager.instance.GoToEnd();

            BallManager.instance.TurnOffGreen();


        }
        else
        {
            rgb2d.velocity = Direction * BounceForce;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Bounce(collision, lastVel, rgb2d);
    }

    
}
