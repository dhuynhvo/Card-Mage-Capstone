using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    [SerializeField]
    public float DropChance;
    [SerializeField]
    public float Cost;
    [SerializeField]
    private Premade_Decks CardPool;
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.OnShopBuy += Current_OnShopBuy;
    }
    private void Current_OnShopBuy()
    {
        throw new System.NotImplementedException();
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("PURCHASE ATTTEMPTED");
            GameEvents.current.DropCard_S();
            GameEvents.current.OnShopBuy += Current_OnShopBuy;
            Destroy(gameObject);
        }
    }
}
