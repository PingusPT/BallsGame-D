using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidStuckBallsScript : MonoBehaviour
{
    GameObject Player;
    float distance;
    // Start is called before the first frame update
    void Start()
    {
        Player = BallManager.instance.GetPlayer();
        gameObject.SetActive(false);

    }

   

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag != "Player")
        {
            Vector3 spawnposition = new Vector3(Random.Range(-7f, 7f), Random.Range(-3f, 3f), 0); // Create a new spawn Position for the red ball to spawn
            distance = Vector3.Distance(Player.transform.position, spawnposition); // Get the distance between this spawn and the player

            while (distance < 2)
            {
                spawnposition = new Vector3(Random.Range(-8f, 8f), Random.Range(-4f, 4f), 0);
                distance = Vector3.Distance(Player.transform.position, spawnposition);
            }

            collision.transform.position = spawnposition;
        }
        
    }
}
