using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public GameObject plateform;

    public GameObject gameover;
    public GameObject mainMenu;
    public static UI instance;
    public int Lchance = 2;
    public int tempp = 0;

    public Text GameOverPoints;
    public Text HighScore;
    public AudioSource BgMusic;

    // Start is called before the first frame update
    void Start()
    {
        //GameOver UI set to False
        gameover.gameObject.SetActive(false);
        instance = this;
        //High Score set to HighScore Text Box
        HighScore.text = "HIGH SCORES : " + PlayerPrefs.GetInt("HighScore").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //Display GamePlay UI when Escape Button Pressed
        if (Input.GetKey(KeyCode.Escape)) { 
            SceneManager.LoadScene("GamePlay");
        }
    }

    //PlayButton 
    public void PlayButton() { 
        Time.timeScale = 1f;
        //GameOver Ui Set to be False
        gameover.gameObject.SetActive(false);
        //MainMenu Ui Set to be False
        mainMenu.gameObject.SetActive(false);

    }
    //Exit Button
    public void ExitButton() {
        //Quit Game when Exit button Pressed
        Application.Quit();
    }

    //Restart
    public void Restart()
    {
        //Check Life 
        if (playerScript.instance.LifeInt > 0)
        {
            //Game Over UI set to false if life remaining
            gameover.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
            if (PlayerPrefs.GetInt("Sound") == 0)
            {
                BgMusic.mute = true;
            }
            else
            {
                BgMusic.mute = false;
            }
            //BgMusic.mute = false;
            //Instantiate(plateform);
            playerScript.instance.Reset();
            playerBound.instance.out_of_bounds = false;
            playerScript.instance.LifeInt--;
            playerScript.instance.Life.text = playerScript.instance.LifeInt.ToString();
        }

        //if No Life remaining, Game Play UI set to True
        else {
            SceneManager.LoadScene("GamePlay");
        }
    }
    //Game Life
    public void life()
    {
        //Stop Game and Back to GamePlay Scene
        Time.timeScale = 0f;
            SceneManager.LoadScene("GamePlay");
        }

    //Game Over
    public void Gameover() {
        //When No Life Remaining, Game OVer UI Set to be True
        if (playerScript.instance.LifeInt <= 0)
            UnityAds.instance.GameOver();
        //BacGround Music Stopping
        BgMusic.mute = true;
        //For vibration
        Handheld.Vibrate();
        //Stop Game
        Time.timeScale = 0.0f;
        //Game Over UI Showing
        gameover.gameObject.SetActive(true);
        //Score Showing of last game played
        GameOverPoints.text = playerScript.instance.scoreInt.ToString();
        //check if Score > Previous Score then LAst Score set in Highest Score TextFiels.
        if (playerScript.instance.scoreInt > PlayerPrefs.GetInt("HighScore")) { 
            PlayerPrefs.SetInt("HighScore", playerScript.instance.scoreInt);
        }
        HighScore.text =  "HIGH SCORES : " + PlayerPrefs.GetInt("HighScore").ToString();
    }
    
}
