    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject ButttonR;
    public GameObject ButttonL;

    //awake
    void Awake()
    {
        if(instance == null)
           instance = this;
        //check if player is playing first time
        if (!PlayerPrefs.HasKey("First")) {
            //set highest score
            PlayerPrefs.SetInt("HighScore", 0);
            PlayerPrefs.SetInt("Sound",1);
            PlayerPrefs.SetInt("First", 1);
            //set tutorial
            ButttonL.GetComponent<Transform>().GetChild(0).transform.gameObject.SetActive(true);
            ButttonR.GetComponent<Transform>().GetChild(0).transform.gameObject.SetActive(true);
            var tempc = ButttonL.GetComponent<Image>().color;
            tempc.a = 0.5f;
            ButttonL.GetComponent<Image>().color = tempc;
            tempc = ButttonR.GetComponent<Image>().color;
            tempc.a = 0.5f;
            ButttonR.GetComponent<Image>().color = tempc;
            StartCoroutine("startwait");
        }

    }
    //restart
    public void RestartGame()
    {
        //restart game after 2 seconds
        Invoke("restartAfterTime", 2f);
    }
    //restart after time
    void restartAfterTime()
    {
        //load GamePlay Scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("GamePlay");
    }
    IEnumerator startwait() {
        yield return new WaitForSeconds(5f);
        ButttonL.GetComponent<Transform>().GetChild(0).transform.gameObject.SetActive(false);
        ButttonR.GetComponent<Transform>().GetChild(0).transform.gameObject.SetActive(false);
        var tempc = ButttonL.GetComponent<Image>().color;
        tempc.a = 0f;
        ButttonL.GetComponent<Image>().color = tempc;
        tempc = ButttonR.GetComponent<Image>().color;
        tempc.a = 0f;
        ButttonR.GetComponent<Image>().color = tempc;
    }
}
