using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerBound : MonoBehaviour
{
    public static playerBound instance;

    public float min_X = -2.6f, 
        max_X = 2.6f, 
        min_Y = -5.6f, max_Y = 5.6f;
    public bool out_of_bounds;
    public int Lchance = 2;

    private void Start()
    {
        //Setting Camera Postion For minX and maxX
        min_X = -Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z)).x;
        max_X = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z)).x;
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        //CheckBound Function Calling
        checkBounds();
    }
    void checkBounds()
    {

        Vector2 temp = transform.position;
        //Check X Postion for minX and maxX
        if (temp.x > max_X)
            temp.x = max_X;
        if (temp.x < min_X)
            temp.x = min_X;
        transform.position = temp;

        //Condition for Y Postion
        if(temp.y <= min_Y)
        {
            if (!out_of_bounds)
            {
                out_of_bounds = true;
                //Death Sound 
                SoundManager.instance.DeathSound();
                //gameover Ui Displaying
                UI.instance.Gameover();
                //GameManager.instance.RestartGame();
            }
        }
    }

    //OnTrigger Function
    private void OnTriggerEnter2D(Collider2D target)
    {
        //Check if player touch Top Spikes
        if (target.tag == "TopSpike")
        {
            transform.position = new Vector2(1000f, 1000f);
            //Death sound
            SoundManager.instance.DeathSound();
            //gameover Ui Displaying
            UI.instance.Gameover();
            //GameManager.instance.RestartGame();
        }
    }
}
