using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rgd;
    Animator anim;

    float Speed = 4f;
    float horizontal;
    float vertical;

    // Start is called before the first frame update
    void Start()
    {
        

        if(gameObject != null) // Permit only One Player GameObject in the Scene
        {

        }
        else
        {
            Destroy(gameObject);
        }

        anim = gameObject.GetComponent<Animator>();
        DontDestroyOnLoad(gameObject);
        rgd = gameObject.GetComponent<Rigidbody2D>();
    }

    
    

    private void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, vertical, 0);
        movement.Normalize();
        rgd.velocity = movement * Speed; // The Code does the movement HERE 
        
        if(rgd.velocity.x != 0 || rgd.velocity.y != 0) // Set the animations 
        {
            anim.SetBool("Walkin", true);
        }
        else
        {
            anim.SetBool("Walkin", false);
        }
        
    }

   

    public void DeathAnimation() // Trigger DeathAnimation
    {
        anim.SetTrigger("Death");
    }

    public void Death() // Turn Off GameObject
    {
        gameObject.SetActive(false);
    }
}
