using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Worked on by Abida Mim
// uses outside websites as reference

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float health = 0f;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField]
    private HealthBar bar;
    [SerializeField] private GameObject mainCamera;
    [SerializeField]
    private GameObject DefeatScreen;
    [SerializeField]
    private GameObject DefeatText;
    [SerializeField] private AnimationCurve _FadeInCurve;
    public bool IsAlive 
    {
        get { return health > 0; }
    }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        bar.SetMaxHealth(maxHealth);
    }
    
    void Awake(){
        gameObject.SetActive(true);
    }

    private void Update()
    {
        bar.SetHealth(health);
    }

    // Update is called once per frame
    public void TakeDamage(float mod)
    {
        if (!IsAlive)
        {
            return;
        };

        health -= mod;
        mainCamera.GetComponent<shaker>().start = true;
        if(!IsAlive)
        {
            Time.timeScale = 0f;
            DefeatScreen.SetActive(true);
            DefeatText.SetActive(true);
            StartCoroutine(ShowDefeatScreen(5));
        }
    }

    public void UpdateHealth(float mod){
        health += mod;
        
        if(health > maxHealth){
            health = maxHealth;
            
        }
        else if(health <= 0f){
            health = 0f;
        }
    }

    IEnumerator ShowDefeatScreen(float fadeinDuration)
    {
        float currentTime = 0;
        float output_start = 0;
        while (currentTime < fadeinDuration)
        {
            float output = 0 + ((1 - 0) / (fadeinDuration - 0)) * (currentTime - 0);
            currentTime += Time.unscaledDeltaTime;
            float t = _FadeInCurve.Evaluate(output);
            DefeatScreen.GetComponent<Image>().color = new Color(0, 0, 0, t);
            DefeatText.GetComponent<Text>().color = new Color(170, 0, 0, t);
            yield return null;
        }
        DefeatScreen.GetComponent<Image>().color = new Color(0, 0, 0, 1);
        DefeatText.GetComponent<Text>().color = new Color(170, 0, 0, 1);
        Time.timeScale = 1f;
        yield return new WaitForSeconds(3f);
        DefeatScreen.SetActive(false);
        DefeatText.SetActive(false);
        SceneManager.LoadScene("GameStage");
    }
}