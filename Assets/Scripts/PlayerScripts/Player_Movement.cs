//Dan Huynhvo
//UNR
//CS 425

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [SerializeField]
    private string UpKey, DownKey, LeftKey, RightKey, Dash;
    [SerializeField]
    private Vector3 NW, NE, SW, SE;
    [SerializeField]
    private Vector3 MoveDir;
    [SerializeField]
    public float PlayerSpeed, DashSpeed;
    [SerializeField]
    private GameObject PlayerAvatar;
    [SerializeField]
    private string FacingWhat; // Unused currently
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    float DashCooldown;
    [SerializeField]
    float DashTimer;
    [SerializeField]
    GameObject DashSphere;
    [SerializeField]
    private bool IsDashing = false;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private SpriteRenderer sprite;
    [SerializeField]
    private Play_Card attacking;

    public float SpeedBuff;



    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        rb = gameObject.GetComponent<Rigidbody>();
        NW = new Vector3(-1, 0, 1).normalized;
        NE = new Vector3(1, 0, 1).normalized;
        SW = new Vector3(-1, 0, -1).normalized;
        SE = new Vector3(1, 0, -1).normalized;
        SpeedBuff = 1f;
    }

    public void RollAnim()
    {
        if (MoveDir == Vector3.right)
        {
            sprite.flipX = false;
            anim.SetTrigger("DR");
        }

        else if(MoveDir == Vector3.left)
        {
            sprite.flipX = true;
            anim.SetTrigger("DR");
        }

        else if(MoveDir == Vector3.forward)
        {
            sprite.flipX = false;
            anim.SetTrigger("DU");
        }

        else if(MoveDir == Vector3.back)
        {
            sprite.flipX = false;
            anim.SetTrigger("DD");
        }

        else if(MoveDir == NW)
        {
            sprite.flipX = true;
            anim.SetTrigger("DUD");
        }

        else if (MoveDir == NE)
        {
            sprite.flipX = false;
            anim.SetTrigger("DUD");
        }

        else if(MoveDir == SW)
        {
            sprite.flipX = true;
            anim.SetTrigger("DDD");
        }

        else if (MoveDir == SE)
        {
            sprite.flipX = false;
            anim.SetTrigger("DDD");
        }
    }

    void SwitchRunAnimations(string direction)
    {
        switch (direction)
        {
            case "NW":
                anim.SetBool("RR", false);
                anim.SetBool("RU", false);
                anim.SetBool("RD", false);
                anim.SetBool("RNW", true);
                anim.SetBool("RNE", false);
                anim.SetBool("RSW", false);
                anim.SetBool("RSE", false);
                break;
            case "NE":
                anim.SetBool("RR", false);
                anim.SetBool("RU", false);
                anim.SetBool("RD", false);
                anim.SetBool("RNW", false);
                anim.SetBool("RNE", true);
                anim.SetBool("RSW", false);
                anim.SetBool("RSE", false);
                break;
            case "SW":
                anim.SetBool("RR", false);
                anim.SetBool("RU", false);
                anim.SetBool("RD", false);
                anim.SetBool("RNW", false);
                anim.SetBool("RNE", false);
                anim.SetBool("RSW", true);
                anim.SetBool("RSE", false);
                break;
            case "SE":
                anim.SetBool("RR", false);
                anim.SetBool("RU", false);
                anim.SetBool("RD", false);
                anim.SetBool("RNW", false);
                anim.SetBool("RNE", false);
                anim.SetBool("RSW", false);
                anim.SetBool("RSE", true);
                break;
            case "U":
                anim.SetBool("RR", false);
                anim.SetBool("RU", true);
                anim.SetBool("RD", false);
                anim.SetBool("RNW", false);
                anim.SetBool("RNE", false);
                anim.SetBool("RSW", false);
                anim.SetBool("RSE", false);
                break;
            case "D":
                anim.SetBool("RR", false);
                anim.SetBool("RU", false);
                anim.SetBool("RD", true);
                anim.SetBool("RNW", false);
                anim.SetBool("RNE", false);
                anim.SetBool("RSW", false);
                anim.SetBool("RSE", false);
                break;
            case "L":
                anim.SetBool("RR", true);
                anim.SetBool("RU", false);
                anim.SetBool("RD", false);
                anim.SetBool("RNW", false);
                anim.SetBool("RNE", false);
                anim.SetBool("RSW", false);
                anim.SetBool("RSE", false);
                break;
            case "R":
                anim.SetBool("RR", true);
                anim.SetBool("RU", false);
                anim.SetBool("RD", false);
                anim.SetBool("RNW", false);
                anim.SetBool("RNE", false);
                anim.SetBool("RSW", false);
                anim.SetBool("RSE", false);
                break;
            default:
                // code block
                break;
        }
    }
    
    void FixedUpdate()
    {
        
        if(MoveDir == Vector3.right || MoveDir == Vector3.left)
        {
            SwitchRunAnimations("R");
        }

        if (IsDashing)
        {
            DashTimer += Time.fixedDeltaTime;
            if(DashTimer >= DashCooldown)
            {
                DashTimer = 0;
                IsDashing = false;
                //DashSphere.SetActive(false);
            }
        }
        //rigid body add force
        if(Input.GetKey(UpKey) && Input.GetKey(LeftKey))
        {
            MoveDir = NW;
            if (attacking.NotSpamming == true)
            {
                sprite.flipX = true;
            }
            SwitchRunAnimations("NW");
            //rb.moveposition(transform.position + nw * time.deltatime * playerspeed * .7f);
            //playeravatar.transform.rotation = quaternion.euler(90, -45, 0);
            FacingWhat = "ul";
        }

        else if (Input.GetKey(UpKey) && Input.GetKey(RightKey))
        {
            MoveDir = NE;
            if (attacking.NotSpamming == true)
            {
                sprite.flipX = false;
            }
            SwitchRunAnimations("NE");
            //rb.MovePosition(transform.position + NE * Time.deltaTime * PlayerSpeed * .7f);
            //PlayerAvatar.transform.rotation = Quaternion.Euler(90, 45, 0);
            FacingWhat = "ur";
        }

        else if (Input.GetKey(DownKey) && Input.GetKey(LeftKey))
        {
            MoveDir = SW;
            if (attacking.NotSpamming == true)
            {
                sprite.flipX = true;
            }
            SwitchRunAnimations("SW");
            //rb.MovePosition(transform.position + SW * Time.deltaTime * PlayerSpeed * .7f);
            //PlayerAvatar.transform.rotation = Quaternion.Euler(90, -135, 0);
            FacingWhat = "dl";
        }

        else if (Input.GetKey(DownKey) && Input.GetKey(RightKey))
        {
            MoveDir = SE;
            if (attacking.NotSpamming == true)
            {
                sprite.flipX = false;
            }
            SwitchRunAnimations("SE");
            //rb.MovePosition(transform.position + SE * Time.deltaTime * PlayerSpeed * .7f);
            //PlayerAvatar.transform.rotation = Quaternion.Euler(90, 135, 0);
            FacingWhat = "dr";
        }

        else if (Input.GetKey(UpKey))
        {
            MoveDir = Vector3.forward;
            SwitchRunAnimations("U");
            //rb.MovePosition(transform.position + Vector3.forward * Time.deltaTime * PlayerSpeed);
            //PlayerAvatar.transform.rotation = Quaternion.Euler(90, 0, 0);
            FacingWhat = "u";
        }

        else if (Input.GetKey(DownKey))
        {
            MoveDir = Vector3.back;
            SwitchRunAnimations("D");
            //rb.MovePosition(transform.position + Vector3.back * Time.deltaTime * PlayerSpeed);
            //PlayerAvatar.transform.rotation = Quaternion.Euler(90, 180, 0);
            FacingWhat = "d";
        }

        else if (Input.GetKey(LeftKey))
        {
            if(attacking.NotSpamming == true)
            {
                sprite.flipX = true;
            }
            
            MoveDir = Vector3.left;
            SwitchRunAnimations("R");
            //rb.MovePosition(transform.position + Vector3.left * Time.deltaTime * PlayerSpeed);
            //PlayerAvatar.transform.rotation = Quaternion.Euler(90, 270, 0);
            FacingWhat = "l";
        }
        else if (Input.GetKey(RightKey))
        {
            if (attacking.NotSpamming == true)
            {
                sprite.flipX = false;
            }

            MoveDir = Vector3.right;
            SwitchRunAnimations("R");
            //rb.MovePosition(transform.position + Vector3.right * Time.deltaTime * PlayerSpeed);
            //PlayerAvatar.transform.rotation = Quaternion.Euler(90, 90, 0);
            FacingWhat = "r";
        }

        else
        {
            MoveDir = Vector3.zero;
            //sprite.flipX = false;
            anim.SetBool("RR", false);
            anim.SetBool("RU", false);
            anim.SetBool("RD", false);
            anim.SetBool("RNW", false);
            anim.SetBool("RNE", false);
            anim.SetBool("RSW", false);
            anim.SetBool("RSE", false);
        };
        
        if(Input.GetKeyDown(Dash) && (!IsDashing))
        {
            Vector3 dash = DashDirection(FacingWhat);
            if (dash == NW || dash == NE || dash == SW || dash == SE)
            {
                RollAnim();
                rb.velocity = MoveDir * PlayerSpeed * DashSpeed * SpeedBuff * (1 + attacking.Creativity);
                IsDashing = true;
                //DashSphere.SetActive(true);
            }

            else
            {
                RollAnim();
                rb.velocity = MoveDir * PlayerSpeed * DashSpeed * SpeedBuff * (1 + attacking.Creativity);
                IsDashing = true;
                //DashSphere.SetActive(true);
            }
        }
        
        if(!IsDashing)
        {
            rb.velocity = MoveDir * PlayerSpeed * SpeedBuff * (1 + attacking.Creativity);
        }

       // rb.AddForce(MoveDir*PlayerSpeed, ForceMode.VelocityChange);
    }
    private void OnDrawGizmos()
    {
        //Gizmos.DrawRay(new Ray(transform.position + new Vector3(0, 1, 0), MoveDir * 3));
    }
    private Vector3 DashDirection(string direction)
    {
        switch (direction)

        {
            case "ul":
                return NW;

            case "ur":
                return NE;

            case "dl":
                return SW;

            case "dr":
                return SE;

            case "u":
                return Vector3.forward;

            case "d":
                return Vector3.back;

            case "l":
                return Vector3.left;

            case "r":
                return Vector3.right;
        }
        
        return new Vector3(0, 0, 0);
    }

}