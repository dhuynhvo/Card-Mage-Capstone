using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    private float timer;
    private GameObject player;
    private Enemy_Info ei;
    [SerializeField]
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ei = GetComponent<Enemy_Info>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ei.health <= 0)
        {
            return;
        }
        float distance = Vector3.Distance(transform.position, player.transform.position);
        //Debug.Log(distance);

        if (distance < 6)
        {
            timer += Time.deltaTime;
            if(timer > 3)
            {
                    timer = 0;
                    StartCoroutine(ShootWithAnimation());
            } 
        }
    }
    
    void shoot(){
        Instantiate(bullet, transform.position, Quaternion.identity);
        AudioManager.instance.Play("EnemyProjectile");
    }
    IEnumerator ShootWithAnimation()
    {
        anim.SetBool("Attack", true);
        yield return new WaitForSeconds(0.5f); // Adjust this value based on the time in the animation when the bullet should be fired
        shoot();
        yield return new WaitForSeconds(0.5f); // Adjust this value based on the remaining time of the animation
        anim.SetBool("Attack", false);
    }
}
