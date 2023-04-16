using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Teleporter_To_Boss : MonoBehaviour
{
    public GameObject BossRoomTeleport;
    public Point_To_Boss_Room Arrow;
    public GameObject BlackScreen;
    [SerializeField]
    private AnimationCurve _FadeInCurve;

    public void Start()
    {
        BlackScreen = GameObject.Find("Gen Purpose Blackscreen");
        BossRoomTeleport = GameObject.Find("BossTeleport");
        Arrow = GameObject.Find("Player").transform.GetChild(5).GetComponent<Point_To_Boss_Room>();
        Arrow.Target = gameObject.transform.parent.gameObject;
        Arrow.FoundTarget = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        //Debug.Log(other.gameObject.name);
        if(other.gameObject.tag == "Player")
        {
            StartCoroutine(FadeToBlack(2f, other.gameObject));
            GameObject BossArrow = other.gameObject.transform.GetChild(5).gameObject;
            BossArrow.SetActive(false);
            
        }
    }

    IEnumerator FadeToBlack(float fadeinDuration, GameObject player)
    {
        Time.timeScale = 0f;
        float currentTime = 0;
        while (currentTime < fadeinDuration)
        {
            float output = 0 + ((1 - 0) / (fadeinDuration - 0)) * (currentTime - 0);
            currentTime += Time.unscaledDeltaTime;
            float t = _FadeInCurve.Evaluate(output);
            BlackScreen.GetComponent<Image>().color = new Color(0, 0, 0, t);
            //BlackText.GetComponent<Text>().color = new Color(170, 0, 0, t);
            yield return null;
        }
        player.transform.position = new Vector3(BossRoomTeleport.transform.position.x, 1f, BossRoomTeleport.transform.position.z);
        BlackScreen.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        //BlackText.GetComponent<Text>().color = new Color(170, 0, 0, 1);
        Time.timeScale = 1f;
        //yield return new WaitForSeconds(3f);
        //BlackScreen.SetActive(false);
        //DefeatText.SetActive(false);
        //SceneManager.LoadScene("GameStage");

        
    }
}
