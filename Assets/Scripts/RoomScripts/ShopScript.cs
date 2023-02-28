using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// implemented by Robert Bothne & Dan Huynhvo
public class ShopScript : MonoBehaviour
{
    [SerializeField]
    public float DropChance;
    [SerializeField]
    public float Cost;
    [SerializeField]
    private Premade_Decks CardPool;
    [SerializeField]
    private int thisID;
    // Start is called before the first frame update
    void Start()
    {
        thisID = gameObject.GetInstanceID();
        GameEvents.current.OnShopBuy += Current_OnShopBuy;
    }
    private void ShopErr()
    {
        throw new System.NotImplementedException();
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("PURCHASE ATTTEMPTED");
            GameEvents.current.DropCard_S(thisID, collision.gameObject);
            Destroy(gameObject);
        }
    }

    public void Current_OnShopBuy(int ID, GameObject player)
    {
        if(thisID == ID)
        {
            int randCard = Random.Range(0, CardPool.cards.Count);
            int priceOfCard = CardPool.cards[randCard].GetComponent<Connected_Spell>().SpellInfo.SpellPrice;

            if (priceOfCard < player.GetComponent<Player_Currency>().Steves.money)
            {
                player.GetComponent<Player_Currency>().Steves.money -= priceOfCard;
                var randPosition = new Vector3(Random.Range(-3.0f, 3.0f), 0, Random.Range(-3.0f, 3.0f));
                GameObject Card = Instantiate(CardPool.cards[randCard], gameObject.transform.position + randPosition, Quaternion.Euler(90, 0, 0));
                Debug.Log("PURCHASE SUCCESSFUL");
            }

            else
            {
                Debug.Log("PURCHASE FAILED");
            }
        }
    }

    public void OnDestroy()
    {
        GameEvents.current.OnShopBuy -= Current_OnShopBuy;
    }
}
