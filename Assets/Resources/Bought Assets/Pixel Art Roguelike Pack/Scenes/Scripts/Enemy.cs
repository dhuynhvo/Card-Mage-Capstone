using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Item Drop Settings")]
    [Tooltip("The object that the enemy will drop")]
    [SerializeField] private GameObject dropItem = null;
    [Tooltip("The animation of the enemy, do not change")]
    [SerializeField] private GameObject DeathAnimationPrefab = null;

    [Header("Hit/Damage Settings")]
    [Tooltip("The object that countains the text, do not change")]
    [SerializeField] private GameObject floatingtext = null;
    [Tooltip("what the text says")]
    [SerializeField] private string text = "Hit";

    [Header("Life Settings")]
    [Tooltip("the initial life")]
    [SerializeField] private int initialLife = 5;
    private int Life = 1;

    [Header("Audio settings")]
    [SerializeField] private AudioClip audio_death = null;
    [SerializeField] private AudioClip audio_damage = null;
    [SerializeField] private AudioSource audio_source = null;

    [Header("Animation Settings")]
    [Tooltip("flip the sprite when the enemy move for right or left")]
    public bool flipSprite = true;
    const string IDDLE = "iddle";
    const string WALK = "walk";
    const string ATTACK = "attack";
    const string DAMAGE = "damage";
    private bool EnemyIsMovingAnimation = false;
    private bool EnemyDamageAnimation = false;
    private bool EnemyAttackAnimation = false;
    [Tooltip("The sprite renderer of the enemy")]
    public SpriteRenderer spriteRenderer = null;
    private Animator animator = null;
    private string currentState;
    private string dirFlip = "right";



    [Header("Advanced Settings")]
    [Tooltip("The layer that the enemy will not walk in")]
    public LayerMask BlockLayer;
    [Tooltip("The layer of the player")]
    public LayerMask PlayerLayer;
    [Tooltip("The layer of the enemy")]
    public LayerMask EnemyLayer;
    [Tooltip("This point checks if the enemy can move")]
    public Transform movePoint = null;
    [Tooltip("This action is what 'attacks' the player")]
    public Transform actionPoint = null;
    //public Transform checkPoint = null;
    //public PolygonCollider2D AttackArea = null;
    [Tooltip("The area that checks when the player is close")]
    [SerializeField] private GameObject AreaEnemy = null;
    [Tooltip("The Player")]
    [SerializeField] private GameObject Player = null;
    public float waitTime = 1f;
    public float waitTime2 = 0.5f;
    //public float turnTime = 0.4f;
    //private float waitTimeMoveRandom = 0.1f;
    //private float waitTimeMoveRandomMAX = 0.1f;
    public bool EnemyAttack = false;
    public bool prepareToMove = false;
    public static bool moveEnemy = false;
    public bool moveEnemyshow = false;
    private int moveRandom = 5;
    public float moveSize = 1f;
    private int direction = 0;
    public float moveSpeed = 5f;
    [SerializeField] private float timer = 0.0f;
    public bool hitEnemy = false;
    private bool deleteEnemy = false;
    public float TimeTodeleteEnemy = 0.5f;


    //--------------------------------------------------

    //private AreaEnemyScript;
    // Start is called before the first frame update
    // [ContextMenu("EnemyMove")]

    private void Start()
    {
        audio_source = GetComponent<AudioSource>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        // animator = GetComponent<Animator>();
        movePoint.parent = null;
        Life = initialLife;
    }


    private void Update()
    {
        moveEnemyshow = moveEnemy;
        if (deleteEnemy)
        {
            timer += Time.deltaTime;
            DeleteEnemy();
            return;
        }
        EnemyAnimation();
        if (hitEnemy)
        {
            Hit();
            hitEnemy = false;
        }
        if (EnemyAttack == false)
        {
            EnemyAreaDetection EnemyAreaDetection = AreaEnemy.GetComponent<EnemyAreaDetection>();
            if (EnemyAreaDetection.PlayerOnArea)
            {
                EnemyAttack = true;
            }
        }
        EnemycheckTurn();
    }
    private void EnemyAnimation()
    {
        if (EnemyIsMovingAnimation)
        {
            ChangeAnimationState(WALK);
        }
        else if (EnemyDamageAnimation)
        {
            ChangeAnimationState(DAMAGE);
        }
        else if (EnemyAttackAnimation)
        {
            ChangeAnimationState(ATTACK);
        }
        else
        {
            ChangeAnimationState(IDDLE);
        }

        //----------------------------------------------------
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("walk") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            if (EnemyIsMovingAnimation)
            {

                EnemyIsMovingAnimation = false;
            }
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("damage") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            if (EnemyDamageAnimation)
            {
                EnemyDamageAnimation = false;
            }
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            if (EnemyAttackAnimation)
            {
                EnemyAttackAnimation = false;
            }
        }
        //SPRITER FLIT//------------------------------------------------------------------------------------------------------------------------------
        if (dirFlip == "right")
        {
            // if the variable isn't empty (we have a reference to our SpriteRenderer
            if (spriteRenderer != null)
            {
                // flip the sprite
                if (flipSprite)
                {
                    spriteRenderer.flipX = true;
                }

            }
        }
        if (dirFlip == "left")
        {
            if (spriteRenderer != null)
            {
                // flip the sprite
                if (flipSprite)
                {
                    spriteRenderer.flipX = false;
                }

            }

        }
        //------------------------------------------------------------------------------------------------------

    }
    private void EnemycheckTurn()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, movePoint.position) == 0f)
        {
            if (PlayerController.PlayerTurn == true)
            {

                if (moveEnemy == false && prepareToMove == false)
                {
                    prepareToMove = true;
                    timer = 0;
                }
            }
            if (PlayerController.PlayerTurn == false)
            {

                if (prepareToMove == true && moveEnemy == false)
                {

                    moveEnemy = true;
                    timer = 0;

                }
            }
            //movePoint.position += new Vector3(0f, moveSize, 0f);
            timer += Time.deltaTime;
            if (timer > waitTime)
            {
                //EnemyIsMoving = false;
                if (moveEnemy == true && prepareToMove)
                {


                    EnemyTurn();
                }
            }
        }
        else
        {
            EnemyIsMovingAnimation = true;
        }

    }
    public void EnemyTurn()
    {

        if (Life < 1)
        {
            EnemyDeath();
        }
        else if (EnemyAttack)
        {
            EnemyAttacking();

        }
        else
        {
            EnemyMove();
        }




    }
    public void EnemyMove()
    {
        actionPoint.transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        direction = Random.Range(1, moveRandom); //randomize the direction


        if (direction == 1)
        {
            //cima	
            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, moveSize, 0f), .2f, BlockLayer) && !Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, moveSize, 0f), .2f, PlayerLayer))
            {
                movePoint.position += new Vector3(0f, moveSize, 0f);
            }
            else
            {
                actionPoint.position += new Vector3(0f, moveSize, 0f);
            }
        }
        else if (direction == 2)
        {
            //baixo	
            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, -Mathf.Abs(moveSize), 0f), .2f, BlockLayer) && !Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, -Mathf.Abs(moveSize), 0f), .2f, PlayerLayer))
            {
                movePoint.position += new Vector3(0f, -Mathf.Abs(moveSize), 0f);
            }
            else
            {
                actionPoint.position += new Vector3(0f, -Mathf.Abs(moveSize), 0f);
            }
        }
        else if (direction == 3)
        {
            //right
            dirFlip = "right";
            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(moveSize, 0f, 0f), .2f, BlockLayer) && !Physics2D.OverlapCircle(movePoint.position + new Vector3(moveSize, 0f, 0f), .2f, PlayerLayer))
            {
                movePoint.position += new Vector3(moveSize, 0f, 0f);
            }
            else
            {
                actionPoint.position += new Vector3(moveSize, 0f, 0f);
            }

        }
        else if (direction == 4)
        {
            //left
            dirFlip = "left";
            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(-Mathf.Abs(moveSize), 0f, 0f), .2f, BlockLayer) && !Physics2D.OverlapCircle(movePoint.position + new Vector3(-Mathf.Abs(moveSize), 0f, 0f), .2f, PlayerLayer))
            {
                movePoint.position += new Vector3(-Mathf.Abs(moveSize), 0f, 0f);

            }
            else
            {
                actionPoint.position += new Vector3(-Mathf.Abs(moveSize), 0f, 0f);
            }

        }
        else
        {
            moveEnemy = false;
            timer = 0;
        }
        moveEnemy = false;
        timer = 0;
        prepareToMove = false;
    }
    void Hit()
    {
        EnemyDamageAnimation = true;
        GameObject prefab = Instantiate(floatingtext, transform.position, Quaternion.identity);
        prefab.GetComponentInChildren<TextMesh>().text = text;
        Life -= 1;
        PlayerController playerController = Player.GetComponent<PlayerController>();
        PlayerController.PlayerIsAttacking = true;

        if (audio_source == null)
        {
            Debug.Log("The audio source is NULL");
        }
        else
        {
            audio_source.clip = audio_damage;
            audio_source.Play();
        }


        EnemyAttacking();
        //PlayerController PlayerController = Player.GetComponent<PlayerController>();
        // PlayerController.PlayerIsAttacking = true;

    }
    void EnemyDeath()
    {
        if (audio_source == null)
        {
            Debug.Log("The audio source is NULL");
        }
        else
        {

            audio_source.clip = audio_death;
            audio_source.Play();
        }
        PlayerController.PlayerWait = true;
        //Destroy(gameObject);
        deleteEnemy = true;
        spriteRenderer.color = new Color(0, 0, 0, 0);
        GameObject prefab2 = Instantiate(dropItem, transform.position, Quaternion.identity);
        GameObject prefab3 = Instantiate(DeathAnimationPrefab, transform.position, Quaternion.identity);
    }
    void EnemyAttacking()
    {

        EnemyAttackAnimation = true;
        actionPoint.transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        direction = Random.Range(1, 5);
        if (direction == 1)
        {
            actionPoint.position += new Vector3(0f, moveSize, 0f);

        }
        else if (direction == 2)
        {
            actionPoint.position += new Vector3(0f, -Mathf.Abs(moveSize), 0f);
            //baixo	
        }
        else if (direction == 3)
        {
            //right
            actionPoint.position += new Vector3(moveSize, 0f, 0f);



        }
        else if (direction == 4)
        {
            //left
            actionPoint.position += new Vector3(-Mathf.Abs(moveSize), 0f, 0f);



        }
        else
        {
            actionPoint.position += new Vector3(0f, -Mathf.Abs(moveSize), 0f);
            //baixo	
        }
        moveEnemy = false;
        prepareToMove = false;
        timer = 0;
        EnemyAttack = false;
    }
    void ChangeAnimationState(string newState)
    {

        //stop the same animation from interrupting itself
        if (currentState == newState) return;
        //okay the animation
        animator.Play(newState);
        //reassign the current state animation
        currentState = newState;
    }
    void EndAnimation()
    {
        if (EnemyIsMovingAnimation)
        {
            EnemyIsMovingAnimation = false;
        }
    }
    private void DeleteEnemy()
    {

        if (timer >= TimeTodeleteEnemy)
        {
            Destroy(gameObject);
        }
    }


}