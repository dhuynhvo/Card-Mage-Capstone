//Author: Robert and Grant Davis
//CS 426 Senior Project: Card Mage
//EnemyShootingRed.cs

using System.Collections;
using UnityEngine;

public class EnemyShootingRed : MonoBehaviour
{
    [SerializeField]
    private Level_Counter levels;
    public bool singleShot = false;
    public GameObject bullet;
    public Transform bulletPos;
    private float timer;
    public float cooldown;
    private bool firstStrike;
    public float initialAttack;
    public float enemyRange;
    float additionalDuration;
    private GameObject player;
    private Enemy_Info ei;
    [SerializeField]
    private Animator anim;

    public float shootDuration = 5f; // The duration for shooting
    
    public float firerate = 0.3f; // The time between each shot

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ei = GetComponent<Enemy_Info>();
        anim = GetComponent<Animator>();
        additionalDuration = (float)levels.Level / 7; //NOTE, IMPORTANT TO DIFFICULTY, HIGHER NUBMER LESS IMPACT
    }

    // Update is called once per frame
    void Update()
    {
        if (ei.health <= 0)
        {
            return;
        }
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance < enemyRange)
        {
            timer += Time.deltaTime;
            if (timer > initialAttack && firstStrike == true)
            {
                firstStrike = false;
                timer = 0;
                StartCoroutine(ShootWithAnimation());

            }
            if (timer > cooldown  && firstStrike==false)
            {
                timer = 0;
                StartCoroutine(ShootWithAnimation());
            }
        }
    }

    void shoot()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
        AudioManager.instance.Play("EnemyProjectile");
    }

    IEnumerator ShootWithAnimation()
    {
        anim.SetBool("Attack", true);
        float durationTimer = 0;
        float fireTimer = 0;
        if (singleShot != true)
        {
            while (durationTimer < shootDuration + additionalDuration)
            {
                if (fireTimer > firerate)
                {
                    // Check if the enemy is alive before shooting
                    if (ei.health > 0)
                    {
                        shoot();
                    }
                    fireTimer = 0;
                }

                durationTimer += Time.deltaTime;
                fireTimer += Time.deltaTime;
                yield return null;
            }
        }
        else
        {
            // Check if the enemy is alive before shooting
            if (ei.health > 0)
            {
                shoot();
            }
        }
        anim.SetBool("Attack", false);
    }
}
