//Worked on by Dan Huynhvo
//CS426

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Black_Screen_Fade : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve _FadeInCurve;
    public Level_Counter Counter;

    public IEnumerator FadeToBlack(float fadeinDuration, bool ReloadScene = false)
    {
        Time.timeScale = 0f;
        float currentTime = 0;
        while (currentTime < fadeinDuration)
        {
            float output = 0 + ((1 - 0) / (fadeinDuration - 0)) * (currentTime - 0);
            currentTime += Time.unscaledDeltaTime;
            float t = _FadeInCurve.Evaluate(output);
            GetComponent<Image>().color = new Color(0, 0, 0, t);
            //BlackText.GetComponent<Text>().color = new Color(170, 0, 0, t);
            yield return null;
        }
        if (ReloadScene == true)
        {
            Time.timeScale = 1f;
            Counter.Level += 1;
            SceneManager.LoadScene("GameStage");
        }

        if (ReloadScene == false)
        {
            GetComponent<Image>().color = new Color(0, 0, 0, 0);
            Time.timeScale = 1f;
        }



        //yield return new WaitForSeconds(3f);
        //BlackScreen.SetActive(false);
        //DefeatText.SetActive(false);
        //SceneManager.LoadScene("GameStage");


    }

    public IEnumerator FadeFromBlack(float fadeinDuration)
    {
        float currentTime = 0;
        while (currentTime < fadeinDuration)
        {
            float output = 0 + ((1 - 0) / (fadeinDuration - 0)) * (currentTime - 0);
            currentTime += Time.unscaledDeltaTime;
            float t = _FadeInCurve.Evaluate(output);
            GetComponent<Image>().color = new Color(0, 0, 0, 1-t);
            //BlackText.GetComponent<Text>().color = new Color(170, 0, 0, t);
            yield return null;
        }
        GetComponent<Image>().color = new Color(0, 0, 0, 0);
    }

    public void CallFadeToBlack(float duration)
    {
        StartCoroutine(FadeToBlack(duration, false));
    }

    public void CallFadeFromBlack(float duration)
    {
        StartCoroutine(FadeFromBlack(duration));
    }
}
