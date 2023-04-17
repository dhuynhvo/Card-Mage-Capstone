using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossDeathEvents : MonoBehaviour
{
    public Enemy_Info info;
    public bool HasWon;
    public GameObject WinScreen;
    public GameObject WinText;
    [SerializeField] private AnimationCurve _FadeInCurve;
    public GameObject BossBar;
    public GameObject NextRoom;

    void Start()
    {
        
    }

    void Update()
    {

        if(info != null)
        {
            BossBar.GetComponent<Boss_Hp_Bar>().BossHP = info;
        }

        if (info != null && info.health <= 0 && HasWon == false)
        {
            BossBar.SetActive(false);
            HasWon= true;
            NextRoom.SetActive(true);
            //WinScreen.SetActive(true);
            //WinText.SetActive(true);
            //StartCoroutine(ShowWinScreen(5));


        }

    }

    IEnumerator ShowWinScreen(float fadeinDuration)
    {
        float currentTime = 0;
        while (currentTime < fadeinDuration)
        {
            float output = 0 + ((1 - 0) / (fadeinDuration - 0)) * (currentTime - 0);
            currentTime += Time.unscaledDeltaTime;
            float t = _FadeInCurve.Evaluate(output);
            WinScreen.GetComponent<Image>().color = new Color(0, 0, 0, t);
            WinText.GetComponent<Text>().color = new Color(170, 0, 0, t);
            yield return null;
        }
        WinScreen.GetComponent<Image>().color = new Color(0, 0, 0, 1);
        WinText.GetComponent<Text>().color = new Color(170, 0, 0, 1);
        Time.timeScale = 1f;
        yield return new WaitForSeconds(3f);
        WinScreen.SetActive(false);
        WinText.SetActive(false);
        SceneManager.LoadScene("GameStage");
    }
}
