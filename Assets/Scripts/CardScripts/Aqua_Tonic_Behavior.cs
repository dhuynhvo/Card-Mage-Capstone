//Worked on by Dan Huynhvo

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Aqua_Tonic_Behavior : MonoBehaviour
{
    public PlayerHealth Health;
    public GameObject Player;
    public float HealAmount;
    public int HealIterations;
    public float HealSpacing;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        Health = Player.GetComponent<PlayerHealth>();
        transform.rotation = Quaternion.Euler(90, 0, 0);
        StartCoroutine(AquaTonicHeal());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.transform.position;
    }

    public IEnumerator AquaTonicHeal()  // main behavior for aqua tonic spell
    {
        for(int i = 0; i < HealIterations; i++)
        {
            Health.UpdateHealth(HealAmount);
            yield return new WaitForSeconds(HealSpacing);
        }
    }
}
