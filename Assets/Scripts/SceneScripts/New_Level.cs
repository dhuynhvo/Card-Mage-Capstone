//Worked on by Dan Huynhvo
//CS426

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class New_Level : MonoBehaviour
{
    public Black_Screen_Fade BlackScreen;
    public float duration;
    public Level_Counter Counter;

    public void OnCollisionEnter(Collision collision)   // fade to black
    {
        if(collision.gameObject.tag == "Player")
        {
            StartCoroutine(BlackScreen.FadeToBlack(duration, true));
            //StartCoroutine(WaitForEndBlackScreen(duration));
        }
    }

    public IEnumerator WaitForEndBlackScreen(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
