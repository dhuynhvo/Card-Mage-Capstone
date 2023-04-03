using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Door Type")]
    [SerializeField] private bool DoorIsStoneType = false;
    [Header("Text")]
    [SerializeField] private GameObject floatingtext = null;
    [SerializeField] private string text = "Open!";
    [SerializeField] private string text2 = "Closed!";
    [SerializeField] private bool showtext = false;
    [Header("Audio settings")]
    [SerializeField] private AudioClip audio_doorLock = null;
    [SerializeField] private AudioClip audio_doorUnlock = null;
    [SerializeField] private AudioSource audio_source = null;
    [Header("Other")]
    [SerializeField] private float secondsToDestroy = 1f;
    [SerializeField] private float waitTimeText = 1f;
    private float timer;
    public int AddKeys = 0;
    [SerializeField] private string currentState = "closed_end";
    public bool ChangeDoorState = false;
    public Animator animator = null;

    void Start()
    {
        audio_source = GetComponent<AudioSource>();

    }
    // Update is called once per frame
    void Update()
    {
        if (showtext)
        {
            timer += Time.deltaTime;
            if (timer > waitTimeText)
            {
                showtext = false;
            }
        }
        if (ChangeDoorState)
        {
            if (DoorIsStoneType)
            {
                ChangeState();
            }

        }


    }



    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && DoorIsStoneType == false)
        {
            if (PlayerController.Keys >= 1)
            {
                AddKeys = PlayerController.Keys - 1;
                PlayerController.Keys = AddKeys;


                if (floatingtext)
                {
                    if (showtext == false)
                    {
                        Door_unlock();
                    }

                }

            }
            else
            {

                if (floatingtext)
                {
                    if (showtext == false)
                    {
                        Door_lock();
                    }

                }



            }

        }

    }
    public void ChangeState()
    {

        if (currentState == "open_end")
        {
            currentState = "close";
            

        }
        else if (currentState == "close_end")
        {
            currentState = "open";
        }
        
        if (audio_source == null)
        {
            Debug.Log("The audio source is NULL");
        }
        else
        {
            audio_source.clip = audio_doorUnlock;
            audio_source.Play();
        }
        ChangeDoorState = false;
        animator.Play(currentState);

    }
    void EndAnimation()
    {
        if (currentState == "open")
        {
            currentState = "open_end";
            GetComponent<BoxCollider2D>().enabled = false;
        }
        else if (currentState == "close")
        {
            currentState = "close_end";
            GetComponent<BoxCollider2D>().enabled = true;
        }
        animator.Play(currentState);

    }
    private void Door_lock()
    {
        GameObject prefab = Instantiate(floatingtext, transform.position, Quaternion.identity);
        prefab.GetComponentInChildren<TextMesh>().text = text2;
        timer += Time.deltaTime;
        showtext = true;
        timer = 0;
        if (audio_source == null)
        {
            Debug.Log("The audio source is NULL");
        }
        else
        {
            audio_source.clip = audio_doorLock;
            audio_source.Play();
        }
    }
    private void Door_unlock()
    {
        GameObject prefab = Instantiate(floatingtext, transform.position, Quaternion.identity);
        prefab.GetComponentInChildren<TextMesh>().text = text;
        timer += Time.deltaTime;
        showtext = true;
        timer = 0;
        PlayerController.PlayerWait = true;
        Destroy(gameObject, secondsToDestroy);
        if (audio_source == null)
        {
            Debug.Log("The audio source is NULL");
        }
        else
        {
            audio_source.clip = audio_doorUnlock;
            audio_source.Play();
        }
    }

}