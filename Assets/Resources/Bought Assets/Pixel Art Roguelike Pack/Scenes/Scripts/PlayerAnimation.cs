using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour

{
    // Start is called before the first frame update

    private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject Player = null;
    public Animator animator = null;
    private string currentState;
    const string PLAYER_ATTACK = "Player_attack";
    const string PLAYER_IDDLE = "Player_iddle";
    const string PLAYER_WALK = "Player_walk";
    const string PLAYER_DAMAGE = "Player_Damage";
    const string PLAYER_DEATH = "Player_Death";
    const string EMPITY = "EMPITY";

    //-
    //-
    void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        currentState = "Player_iddle";
    }

    // Update is called once per frame
    void Update()
    {
        //SPRITER FLIT//------------------------------------------------------------------------------------------------------------------------------
        //SpriteRenderer.flipX;
        if (Input.GetAxisRaw("Horizontal") == -1f)
        {
            // if the variable isn't empty (we have a reference to our SpriteRenderer
            if (spriteRenderer != null)
            {
                // flip the sprite
                spriteRenderer.flipX = true;
            }
        }
        else if (Input.GetAxisRaw("Horizontal") == 1f)
        {
            if (spriteRenderer != null)
            {
                // flip the sprite
                spriteRenderer.flipX = false;
            }

        }
        //------------------------------------------------------------------------------------------------------
        // ANIMATION
        PlayerController PlayerController = Player.GetComponent<PlayerController>();
        if (PlayerController.PlayerIsDEAD_end_Animation)
        {

            //ChangeAnimationState(EMPITY);
            spriteRenderer.color = new Color(1, 1, 1, 0f);
        }
        else if (PlayerController.PlayerIsDEAD)
        {
            ChangeAnimationState(PLAYER_DEATH);
        }
        else if (PlayerController.PlayerIsAttacking)
        {
            ChangeAnimationState(PLAYER_ATTACK);
        }
        else if (PlayerController.PlayerDamage)
        {
            ChangeAnimationState(PLAYER_DAMAGE);

        }
        else if (PlayerController.PlayerIsmoving == true)
        {
            ChangeAnimationState(PLAYER_WALK);
        }
        else
        {
            ChangeAnimationState(PLAYER_IDDLE);
        }





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
        PlayerController PlayerController = Player.GetComponent<PlayerController>();
        if (PlayerController.PlayerIsDEAD)
        {
            PlayerController.PlayerIsDEAD_end_Animation = true;
        }
        else if (PlayerController.PlayerIsAttacking)
        {
            PlayerController.PlayerIsAttacking = false;
        }
        else if (PlayerController.PlayerDamage)
        {
            PlayerController.PlayerDamage = false;
        }


    }
}
