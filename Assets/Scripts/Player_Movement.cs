using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public string UpKey, DownKey, LeftKey, RightKey;
    public float PlayerSpeed;
    [SerializeField]
    private GameObject PlayerAvatar;
    private char FacingWhat; // Unused currently
    Rigidbody rb;



    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        if (Input.GetKey(UpKey))
        {
            rb.MovePosition(transform.position + Vector3.forward * Time.deltaTime * PlayerSpeed);
            PlayerAvatar.transform.rotation = Quaternion.Euler(90, 0, 0);
            //FacingWhat = 'u';
        }
        if (Input.GetKey(DownKey))
        {
            rb.MovePosition(transform.position + Vector3.back * Time.deltaTime * PlayerSpeed);
            PlayerAvatar.transform.rotation = Quaternion.Euler(90, 180, 0);
            //FacingWhat = 'd';
        }
        if (Input.GetKey(LeftKey))
        {
            rb.MovePosition(transform.position + Vector3.left * Time.deltaTime * PlayerSpeed);
            PlayerAvatar.transform.rotation = Quaternion.Euler(90, 270, 0);
            //FacingWhat = 'l';
        }
        if (Input.GetKey(RightKey))
        {
            rb.MovePosition(transform.position + Vector3.right * Time.deltaTime * PlayerSpeed);
            PlayerAvatar.transform.rotation = Quaternion.Euler(90, 90, 0);
            //FacingWhat = 'r';
        }
    }
}