using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
	//
    /*
    public LayerMask BlockLayer;
    public static int Keys = 0;
    public float moveSpeed = 5f;
    public Transform movePoint;
    public Transform actionPoint;
    public float waitTime = 1f;
    public static bool moveEnemy = false;
    public float moveSize = 1f;
    public int direction = 0;
    //public PlayerController PlayerControllerScript;
    [SerializeField] private float timer = 0.0f;
    // Start is called before the first frame update
    */
	//-
	
    public float speed;
    public float checkRadius;
    public float attackRadius;
    public LayerMask whatIsPlayer;

	private Transform target;
	private Rigidbody2D rb;
	private Vector2 movement;
	public Vector3 dir;
	
    private bool isChaseRange;
    private bool isAttackRange;
	//-
	
    private void Start()
    {
        //-
        rb = GetComponent<Rigidbody2D>();
		target = GameObject.FindWithTag("Player").transform;
        //-
       // movePoint.parent = null;
    }

    // Update is called once per frame
    private void Update()
    {
        //-
        isChaseRange = 
        isChaseRange = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsPlayer);
        isAttackRange = Physics2D.OverlapCircle(transform.position, attackRadius, whatIsPlayer);

        dir = target.position - transform.position;
        //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        dir.Normalize();
        movement = dir;
        //if should rotate
        





        //-
        /*
        if (PlayerController.PlayerTurn == true)
        {
            if (moveEnemy == false)
            {
                moveEnemy = true;
            }
        }
        //if (PlayerController
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, movePoint.position) == 0f)
        {
            if (moveEnemy == true)
            {
				//direction = Random.Range(0, 4);
				direction = 2;
				if(direction == 1){
				//cima	
				movePoint.position += new Vector3(0f, moveSize, 0f);
				}else if(direction == 2){
				movePoint.position += new Vector3 (0, 1);
				//baixo	
				}else if(direction == 3){
				//esquerda	
				}else if(direction == 4){
				//right
				}
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, moveSize, 0f), .2f, BlockLayer))
                {
                    movePoint.position += new Vector3(0f, moveSize, 0f);
                }
                else
                {
                    Debug.Log("ssss");
                }
                //}				

                moveEnemy = false;
                timer = 0;

                // }
            }
        }
        */

    }
    private void FixedUpdate(){
        
        if(isChaseRange && !isAttackRange){
            Debug.Log("a");
            MoveCharacter(movement);

        }
        if(isAttackRange){
            Debug.Log("b");
            rb.velocity = Vector2.zero;
        }

    }
    private void MoveCharacter(Vector2 dir){
       rb.MovePosition((Vector2)transform.position +(dir*speed*Time.deltaTime));
    }
    public static void MoveEnemy()
    {
       // moveEnemy = true;
        //timer = 15;
        //Debug.Log("moveEnemy0");
    }
}