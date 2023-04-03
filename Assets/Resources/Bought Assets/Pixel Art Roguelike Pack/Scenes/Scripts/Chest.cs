using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [Header("Item Settings")]
    [SerializeField] private GameObject dropItem = null;

    [Header("text Settings")]
    [SerializeField] private string text = "Open!";
    [SerializeField] private GameObject floatingtext = null;
    private bool showtext = false;
    [Header("Audio settings")]
    [SerializeField] private AudioClip audio_chestUnlock = null;
    [SerializeField] private AudioSource audio_source = null;

    [Header("Advanced Settings")]
    [SerializeField] private float secondsToDestroy = 1f;
    [SerializeField] private float timeForDrop = 1f;
    private float timer;
    private bool drop = false;
    private bool chestOpened = false;
    // Start is called before the first frame update

    // Update is called once per frame
    void Start()
    {
        audio_source = GetComponent<AudioSource>();

    }
    void Update()
    {
        if (drop == true && chestOpened == false)
        {
            timer += Time.deltaTime;
            if (timer > timeForDrop)
            {
                //ITEM DROP
                GameObject prefab2 = Instantiate(dropItem, transform.position, Quaternion.identity);
                chestOpened = true;
            }
        }


    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (drop == false)
        {
            if (collider.gameObject.tag == "Player")
            {
                if (floatingtext)
                {
                    if (showtext == false)
                    {

                        //TEXT
                        GameObject prefab = Instantiate(floatingtext, transform.position, Quaternion.identity);
                        prefab.GetComponentInChildren<TextMesh>().text = text;
                        //-
                        //ITEM DROP
                        //GameObject prefab2 =Instantiate (dropItem, transform.position, Quaternion.identity);
                        drop = true;
                        //audio
                        if (audio_source == null)
                        {
                            Debug.Log("The audio source is NULL");
                        }
                        else
                        {
                            audio_source.clip = audio_chestUnlock;
                            audio_source.Play();
                        }
                        timer = 0;
                        Destroy(gameObject, secondsToDestroy);
                    }

                }
            }
        }

    }
}
