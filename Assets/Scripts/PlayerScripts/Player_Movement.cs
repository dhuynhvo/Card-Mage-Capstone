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
    private float PlayerSpeed, DashSpeed;
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



    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        rb = gameObject.GetComponent<Rigidbody>();
        NW = new Vector3(-1, 0, 1).normalized;
        NE = new Vector3(1, 0, 1).normalized;
        SW = new Vector3(-1, 0, -1).normalized;
        SE = new Vector3(1, 0, -1).normalized;
    }


    void FixedUpdate()
    {
        MoveDir = Vector3.zero;
        if(IsDashing)
        {
            DashTimer++;
            if(DashTimer >= DashCooldown)
            {
                DashTimer = 0;
                IsDashing = false;
                DashSphere.SetActive(false);
            }
        }
        //rigid body add force
        if(Input.GetKey(UpKey) && Input.GetKey(LeftKey))
        {
            MoveDir = NW;
            //rb.moveposition(transform.position + nw * time.deltatime * playerspeed * .7f);
            //playeravatar.transform.rotation = quaternion.euler(90, -45, 0);
            FacingWhat = "ul";
        }

        else if (Input.GetKey(UpKey) && Input.GetKey(RightKey))
        {
            MoveDir = NE;

            //rb.MovePosition(transform.position + NE * Time.deltaTime * PlayerSpeed * .7f);
            //PlayerAvatar.transform.rotation = Quaternion.Euler(90, 45, 0);
            FacingWhat = "ur";
        }

        else if (Input.GetKey(DownKey) && Input.GetKey(LeftKey))
        {
            MoveDir = SW;
            //rb.MovePosition(transform.position + SW * Time.deltaTime * PlayerSpeed * .7f);
            //PlayerAvatar.transform.rotation = Quaternion.Euler(90, -135, 0);
            FacingWhat = "dl";
        }

        else if (Input.GetKey(DownKey) && Input.GetKey(RightKey))
        {
            MoveDir = SE;
            //rb.MovePosition(transform.position + SE * Time.deltaTime * PlayerSpeed * .7f);
            //PlayerAvatar.transform.rotation = Quaternion.Euler(90, 135, 0);
            FacingWhat = "dr";
        }

        else if (Input.GetKey(UpKey))
        {
            MoveDir = Vector3.forward;
            //rb.MovePosition(transform.position + Vector3.forward * Time.deltaTime * PlayerSpeed);
            //PlayerAvatar.transform.rotation = Quaternion.Euler(90, 0, 0);
            FacingWhat = "u";
        }

        else if (Input.GetKey(DownKey))
        {
            MoveDir = Vector3.back;
            //rb.MovePosition(transform.position + Vector3.back * Time.deltaTime * PlayerSpeed);
            //PlayerAvatar.transform.rotation = Quaternion.Euler(90, 180, 0);
            FacingWhat = "d";
        }

        else if (Input.GetKey(LeftKey))
        {
            MoveDir = Vector3.left;
            //rb.MovePosition(transform.position + Vector3.left * Time.deltaTime * PlayerSpeed);
            //PlayerAvatar.transform.rotation = Quaternion.Euler(90, 270, 0);
            FacingWhat = "l";
        }
        else if (Input.GetKey(RightKey))
        {
            MoveDir = Vector3.right;
            //rb.MovePosition(transform.position + Vector3.right * Time.deltaTime * PlayerSpeed);
            //PlayerAvatar.transform.rotation = Quaternion.Euler(90, 90, 0);
            FacingWhat = "r";
        }
        
        if(Input.GetKeyDown(Dash) && (!IsDashing))
        {
            Vector3 dash = DashDirection(FacingWhat);
            if (dash == NW || dash == NE || dash == SW || dash == SE)
            {
                rb.velocity = MoveDir * PlayerSpeed * DashSpeed;
                IsDashing = true;
                DashSphere.SetActive(true);
            }

            else
            {
                rb.velocity = MoveDir * PlayerSpeed * DashSpeed;
                IsDashing = true;
                DashSphere.SetActive(true);
            }
        }
        
        if(!IsDashing)
        {
            rb.velocity = MoveDir * PlayerSpeed;
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