using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggleBP : MonoBehaviour
{
    private GameObject bp;
    private GameObject Curs;
    // Start is called before the first frame update
    void Start()
    {
        Curs = GameObject.Find("Cursor");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BossPointereToggle()
    {
        if (Curs.GetComponent<Animator>().enabled == true)
        {
            Curs.GetComponent<Animator>().enabled = false;
        }
        else if (Curs.GetComponent<Animator>().enabled == false)
        {
            Curs.GetComponent<Animator>().enabled = true;
        }
    }
}
