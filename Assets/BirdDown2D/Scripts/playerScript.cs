using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerScript : MonoBehaviour
{
    public static playerScript instance;

    public GameObject OnPlateform;

    public Animator SpikeAnim;

    public Text score;
    public int scoreInt;
    public Text Life;
    public int LifeInt;
    private Rigidbody2D mybody;
    public float moveSpeed;
    public float plateformmoveSpeed;
    public float BGmoveSpeed;
    public int playerscore;
    bool left, right;
    void Awake()
    {
        Time.timeScale = 0f;
        instance = this;
        scoreInt = 0;
        LifeInt = 3;
        Life.text = LifeInt.ToString();
        playerscore = 75;
        score.text = scoreInt.ToString();
        StartCoroutine("scoreincrement");
        left = right = false;
        mybody = GetComponent<Rigidbody2D>();
    }

    //Reset Position and Platform
    public void Reset()
    {

        mybody.velocity = Vector2.zero;

        if (OnPlateform != null && OnPlateform.transform.position.y < 2f)
        {
            this.transform.position = new Vector2(OnPlateform.gameObject.transform.position.x, OnPlateform.gameObject.transform.position.y + 0.5f);
        }
        else {
            GameObject [] g= GameObject.FindGameObjectsWithTag("Platform");
            
            GameObject newtile = Instantiate(UI.instance.plateform);
            for (int i = 0; i < g.Length; i++)
            {
                if ((g[i].transform.position.y > newtile.transform.position.y - 1.5 && g[i].transform.position.y <= newtile.transform.position.y) 
                    || 
                    (g[i].transform.position.y < newtile.transform.position.y + 1.5 && g[i].transform.position.y >= newtile.transform.position.y)){
                    Destroy(g[i].gameObject);
                }
            }
            this.transform.position = new Vector2(0f, -3.5f);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //Pause game when Escape Button Pressed
        if (Input.GetKey(KeyCode.Escape)) { 
            Time.timeScale = 0f;
        }
        //Start Game When Space Button Pressed
        if (Input.GetKey(KeyCode.Space))
            {
                SpikeAnim.SetBool("Animate",true);
        }
        //Move
        Move();

        if (scoreInt == playerscore)
        {
            //Back ground Speed Addition
            BGmoveSpeed += 0.05f;
            //Move Speed Addition
            moveSpeed += 0.2f;
            plateformmoveSpeed += 0.1f;
            //Addition in Payer Score
            playerscore += 75;
        }
    }
    private void Move()
    {
        //when tap on right side, move right
        if (Input.GetAxisRaw("Horizontal") > 0f || right)
        {
            mybody.velocity = new Vector2(moveSpeed, mybody.velocity.y);
        }
        //move left when tap on OR left a=key pressed
        if (Input.GetAxisRaw("Horizontal") < 0f || left)
        {
            mybody.velocity = new Vector2(-moveSpeed, mybody.velocity.y);
        }
    }//move
    public void platFormMove(float x)
    {
        mybody.velocity = new Vector2(x, mybody.velocity.y);

    }
    //moving right
    public void moveRight() {
        right = true;
    }
    //moving left
    public void moveLeft() {
        left = true;
    }
    //Right and Left Left false
    public void setfalse() {
        right = false;
        left = false;
    }

    //Score Incrementation
    IEnumerator scoreincrement() {
        yield return new WaitForSeconds(0.2f);
        scoreInt++ ;
        score.text = "Points: " + scoreInt.ToString();
        StartCoroutine("scoreincrement");

    }

}
