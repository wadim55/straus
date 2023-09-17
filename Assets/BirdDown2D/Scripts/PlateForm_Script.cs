using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateForm_Script : MonoBehaviour
{

// public variables of the class
    public static PlateForm_Script Instance;
    public float move_speed;
    public float bound_y = 6f;
    public bool moving_platform_left, moving_platform_right, is_breakable, is_spike, is_platform;
    private Animator anim;
    public int scorer;



    private void Awake()
    {
        //Instance = this;
        //Access Breakable Animator
        if (is_breakable)
        {
            anim = GetComponent<Animator>();
        }      
    }

    private void Start()
    {
       
        move_speed = playerScript.instance.plateformmoveSpeed;

    }

    void Update()
    {
        //calling Move Function
        Move();
    }

    //move
     void Move()
    {
        //assign position to temp
        Vector2 temp = transform.position;
        //add speed in temp.y position
        temp.y += move_speed * Time.deltaTime;
        transform.position = temp;
        //condition checking for Y
        if(temp.y >= bound_y)
        {
            gameObject.SetActive(false);
        }
    }//move

    //Breakable tile
    void breakableDeactivate()
    {
        //Deactivate breakable tile
        Invoke("DeactivateGameObject", 0.5f);
    }
    //break sound deactivating
    void DeactivateGameObject()
    {
        //icebreak sound set to false
        SoundManager.instance.iceBreakSound();
        gameObject.SetActive(false);


    }

     void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Player")
        {
            //Condition for top Spikes
            if (is_spike)
            {
                target.transform.position = new Vector2(1000f, 1000f);
                //deathsound
                SoundManager.instance.DeathSound();
                //gameover UI Displaying
                UI.instance.Gameover();
            }
        }
    }
     void OnCollisionEnter2D(Collision2D target)
    {
        if(target.gameObject.tag == "Player")
        {

            SoundManager.instance.landSound();
            if (is_breakable)
            {
                //SoundManager.instance.landSound();
                anim.SetBool("Break", true);
                Destroy(this.gameObject, 1.5f);
            }
            if (is_platform)
            {
                if (this.transform.position.y > playerBound.instance.min_Y)
                {
                    playerScript.instance.OnPlateform = this.gameObject;
                }
                //SoundManager.instance.landSound();

            }
            
        }

    }

     void OnCollisionStay2D(Collision2D target)
    {
        if(target.gameObject.tag == "Player"){
            if (moving_platform_left)
            {
                target.gameObject.GetComponent<playerScript>().platFormMove(-1f);
            }
            if (moving_platform_right)
            {
                target.gameObject.GetComponent<playerScript>().platFormMove(1f);
            }
        }
    }
}
