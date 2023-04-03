using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    [Header("Life Settings")]
    public int initialLife;
    public static int life;

    [Header("text/hit Settings")]
    [SerializeField] private GameObject floatingtext = null;
    [SerializeField] private string textHit = "hit";

    [Header("item Settings")]
    public int InitialKeys = 0;
    public int InitialMoney = 0;
    public static int Keys = 0;
    public static int Money = 0;

    [Header("movement Settings")]
    public float moveSpeed = 5f;

    [Header("Audio settings")]
    [SerializeField] private AudioClip audio_damage = null;
     [SerializeField] private AudioClip audio_step = null;
    [SerializeField] private AudioSource audio_source = null;

    [Header("Advanced Settings")]
    public LayerMask BlockLayer;
    public LayerMask EnemyLayer;
    public Transform checkPoint = null;
    public Transform movePoint = null;
    public Transform actionPoint = null;
    //private float waitTime = 1f;
    public float timeTurn = 0.1f;

    [SerializeField] private float timer = 0.0f;
    public static bool PlayerTurn = false;
    public static bool PlayerIsmoving = false;
    public static bool PlayerDamage = false;
    public static bool PlayerAction = false;
    public static bool PlayerIsAttacking = false;
    public static bool PlayerIsDEAD = false;
    public static bool PlayerIsDEAD_end_Animation = false;
    public static bool PlayerWait = false;
    public float PlayerWaitTime = 0.05f;
    public float PlayerWaitTimeDEFAULT = 0.05f;

    void Start()
    {
        audio_source = GetComponent<AudioSource>();
        Keys = InitialKeys;
        Money = InitialMoney;
        //PlayerWaitTime = PlayerWaitTimeDEFAULT;
        life = initialLife;
        movePoint.parent = null;
        PlayerIsDEAD = false;
        PlayerIsDEAD_end_Animation = false;

    }

    // Update is called once per frame

    void Update()
    {
        if (PlayerWait)
        {
            Player_Wait();
        }
        if (PlayerIsDEAD_end_Animation)
        {

            Player_Delete();
        }
        if (life < 1)
        {
            PlayerDeath();
        }


        //Make the player follow the movePoint

        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        //if the player gets to the movePoint
        if (Vector3.Distance(transform.position, movePoint.position) == 0f)
        {
            PlayerTurn = false;
            timer += Time.deltaTime;
            checkInput();

        }


    }
    private void checkInput()
    {

        checkPoint.transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        actionPoint.transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        //Move the MovePoint VERTICAL
        if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
        {
            if (!PlayerWait && !PlayerIsAttacking)
            {
                inputVertical();
            }



        }
        //Move the MovePoint HORIZONTAL
        else if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
        {
            if (!PlayerWait && !PlayerIsAttacking)
            {
                inputHorizontal();

            }



        }
        else
        {
            PlayerIsmoving = false;
            PlayerAction = false;
        }


    }
    private void inputVertical()
    {
        checkPoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
        if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, BlockLayer) && !Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, EnemyLayer))
        {
            movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
            PlayerTurn = true;
            timer = 0;
            PlayerIsmoving = true;
            StepSound();
        }
        else
        {

            actionPoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
            PlayerAction = true;
            PlayerIsmoving = false;
            PlayerTurn = true;
        }
    }
    private void inputHorizontal()
    {

        checkPoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
        //aqui se não tiver nada na frente, ele anda
        if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, BlockLayer) && !Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, EnemyLayer))
        {


            movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
            timer = 0;

            PlayerTurn = true;
            PlayerIsmoving = true;
            StepSound();
        }
        else
        {

            //se tiver algo na frente, ele move o ponto de ação, pra abrir porta etc
            actionPoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
            PlayerIsmoving = false;
            PlayerTurn = true;
            PlayerAction = true;
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "AttackEnemy" && PlayerDamage == false)
        {
            PlayerHit();
        }


    }
    public void PlayerDeath()
    {
        PlayerIsDEAD = true;
        //GameObject prefab = Instantiate(floatingtext, transform.position, Quaternion.identity);
        //prefab.GetComponentInChildren<TextMesh>().text = "GAME OVER";
    }
    public void PlayerHit()
    {
        //Destroy(gameObject);
        PlayerDamage = true;
        life -= 1;
        GameObject prefab = Instantiate(floatingtext, transform.position, Quaternion.identity);
        prefab.GetComponentInChildren<TextMesh>().text = textHit;
        PlayerWait = true;
        if (audio_source == null)
        {
            Debug.Log("The audio source is NULL");
        }
        else
        {
            audio_source.clip = audio_damage;
            audio_source.Play();
        }


    }
    public void Player_Delete()
    {
        //Destroy(gameObject);
        SceneManager.LoadScene("Game_Over");
    }
    private void Player_Wait()
    {
        timer += Time.deltaTime;
        if (timer > PlayerWaitTime)
        {

            PlayerWait = false;
            timer = 0;
        }
    }
    private void StepSound(){
        if (audio_source == null)
        {
            Debug.Log("The audio source is NULL");
        }
        else
        {
            audio_source.clip = audio_step;
            audio_source.Play();
        }
    }
    

}