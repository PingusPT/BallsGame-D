using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balls : MonoBehaviour
{
    // Start is called before the first frame update
    protected Rigidbody2D rbi;
    protected Vector3 lastVel;
    protected bool InMid;


    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        rbi = gameObject.GetComponent<Rigidbody2D>();
        rbi.AddForce(new Vector3(90.8f, 90.8f, 0f));

    }
    private void Update()
    {
        RefreshVell();
    }

    public virtual void RefreshVell()
    {
        lastVel = rbi.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Bounce(collision, lastVel, rbi);

        if(collision.gameObject.tag == "Player" && gameObject.tag == "Red")
        {

            collision.gameObject.GetComponent<Player>().DeathAnimation();
            BallManager.instance.TurnOffGreen();
            TimeManager.instance.TimeDestroi();
            TimeManager.instance.StopTimer();
            Scenemanager.instance.GoToEnd();
            
        }

        if (collision.gameObject.tag == "Mid")//Cheks if the object spawns on the Scene2 Obstacle
        {
            InMid = true;
        }
        else
        {
            InMid = false;
        }
    }

    public virtual void Bounce(Collision2D collision, Vector3 latVel, Rigidbody2D rgb)
    {
        if(collision.gameObject.tag != "Player") // Bounce on the walls
        {
            var speed = latVel.magnitude;
            var direction = Vector3.Reflect(latVel.normalized, collision.contacts[0].normal);
            rgb.velocity = direction * Mathf.Max(speed, 0f);
        }
        

    }

    public void SrinkBall()
    {
        //Set the scale of the red balls for 0.5
        Vector2 flag = new Vector2(0.5f, 0.5f);
        transform.localScale = flag;
    }

    public void GrowBalls()
    {
        //Stor eu sei que o tamanho esta 0.8 mas este é o tamanho fixe para ficar na tela
        //Grows the red balls to the original Size
        Vector2 flag = new Vector2(0.8f, 0.8f);
        transform.localScale = flag;
    }

    public bool RedIsInMid()
    {
        return InMid;
    }
}
