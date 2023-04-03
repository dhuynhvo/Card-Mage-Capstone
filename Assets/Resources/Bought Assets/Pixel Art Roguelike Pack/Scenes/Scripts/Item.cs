using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Item Settings")]
    [Tooltip("'key', 'money' or 'potion'")]
    [SerializeField] private string item = "key";
    [SerializeField] private int quantity = 1;

    [Header("text Settings")]
    [SerializeField] private string text = "5$";
    [Header("Audio settings")]
    [SerializeField] private AudioClip audio_item = null;
    [SerializeField] private AudioSource audio_source = null;
    [Header("Advanced Settings")]
    public SpriteRenderer sprite;
    [SerializeField] private float secondsToDestroy = 1f;
    [SerializeField] private GameObject floatingtext = null;
    [SerializeField] private bool showtext = false;
    private float timer;
    [SerializeField] private float timerToDelete = 0.5f;
    [SerializeField] private bool deleteObject = false;
    [SerializeField] private int AddItem;
    void start()
    {
        sprite = GetComponent<SpriteRenderer>();
        timer = 0;
        audio_source = GetComponent<AudioSource>();
    }
    void Update()
    {
        
        if(deleteObject){
            //Debug.Log("as");
            timer += Time.deltaTime;
            if(timer >= timerToDelete){
                Destroy(gameObject, secondsToDestroy);
            }
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {


        if (collider.gameObject.tag == "Player")
        {
            if (floatingtext)
            {
                if (showtext == false)
                {

                    //iTEM
                    AddItem += quantity;
                    if (item == "key")
                    {
                        //key
                        PlayerController.Keys += AddItem;
                    }
                    else if (item == "money")
                    {
                        //money
                        PlayerController.Money += AddItem;
                    }
                    else if (item == "potion")
                    {
                        //potion
                        PlayerController.life += AddItem;
                    }


                    //TEXT
                    GameObject prefab = Instantiate(floatingtext, transform.position, Quaternion.identity);
                    prefab.GetComponentInChildren<TextMesh>().text = text;
                    showtext = true;
                    timer += Time.deltaTime;
                    timer = 0;
                    //-
                    //- audio
                    if (audio_source == null)
                    {
                        Debug.Log("The audio source is NULL");
                    }
                    else
                    {
                        audio_source.clip = audio_item;
                        audio_source.Play();
                    }
                    //-
                    deleteObject = true;
                    sprite.color = new Color (0, 0, 0, 0); 
                    //Destroy(gameObject, secondsToDestroy);
                }

            }
        }



    }



}