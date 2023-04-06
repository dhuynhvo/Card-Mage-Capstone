using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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
    int randCard;
    int priceOfCard;
    public TMP_Text price;
    public int CardNumber;


    // Start is called before the first frame update
    void Start()
    {
        thisID = gameObject.GetInstanceID();
        GameEvents.current.OnShopBuy += Current_OnShopBuy;
        if(CardNumber == 1) //Hard Coded until i find a better solution to overlapping cards
        {
            randCard = Random.Range(0, 3);
        }

        else if (CardNumber == 2)
        {
            randCard = Random.Range(3, 5);
        }

        else if (CardNumber == 3)
        {
            randCard = Random.Range(5, 7);
        }

        priceOfCard = CardPool.cards[randCard].GetComponent<Connected_Spell>().SpellInfo.SpellPrice;
        price.text = priceOfCard.ToString();
        GetComponent<SpriteRenderer>().sprite = CardPool.cards[randCard].GetComponent<SpriteRenderer>().sprite;
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
            
            //Destroy(gameObject);
        }
    }

    public void Current_OnShopBuy(int ID, GameObject player)
    {
        if(thisID == ID)
        {
            //int randCard = Random.Range(0, CardPool.cards.Count);
            //int priceOfCard = CardPool.cards[randCard].GetComponent<Connected_Spell>().SpellInfo.SpellPrice;

            if (priceOfCard < player.GetComponent<Player_Currency>().Steves.money)
            {
                player.GetComponent<Player_Currency>().Steves.money -= priceOfCard;
                var randPosition = new Vector3(Random.Range(-3.0f, 3.0f), 0, Random.Range(-3.0f, 3.0f));
                GameObject Card = Instantiate(CardPool.cards[randCard], gameObject.transform.position + randPosition, Quaternion.Euler(90, 0, 0));
                Debug.Log("PURCHASE SUCCESSFUL");
                gameObject.SetActive(false);
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
