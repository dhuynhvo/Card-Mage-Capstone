using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Black_Screen_Fade : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve _FadeInCurve;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator FadeToBlack(float fadeinDuration)
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
        GetComponent<Image>().color = new Color(0, 0, 0, 0);
        //BlackText.GetComponent<Text>().color = new Color(170, 0, 0, 1);
        Time.timeScale = 1f;
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
}
