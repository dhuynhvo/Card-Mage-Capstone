using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
// implemented by Robert Bothne & Dan Huynhvo
public class ChestScript : MonoBehaviour
{
    [SerializeField]
    public float DropChance;
    [SerializeField]
    public float Cost;
    [SerializeField]
    private Premade_Decks CardPool;
    [SerializeField]
    private int thisID;
    int CurrentCount;
    public string CurrentCard;
    int randCard;
    int priceOfCard;
    public int CardNumber;
    public SpriteRenderer sprite;
    public GameObject CardSprite;
    public GameObject PriceObject;


    // Start is called before the first frame update
    void Start()
    {
        thisID = gameObject.GetInstanceID();
        GameEvents.current.OnShopBuy += Current_OnChestBuy;
        randCard = Random.Range(0, CardPool.cards.Count);
        if(CardPool.cards.Count > 0)
        {
            priceOfCard = CardPool.cards[randCard].GetComponent<Connected_Spell>().SpellInfo.SpellPrice;
            sprite.sprite = CardPool.cards[randCard].GetComponent<SpriteRenderer>().sprite;
        }
        CurrentCount = CardPool.cards.Count;
        CurrentCard = CardPool.cards[randCard].name;
    }

    private void Update()
    {
        if(CurrentCount != CardPool.cards.Count)
        {
            for(int i = 0; i < CardPool.cards.Count; i++)
            {
                if(CurrentCard == CardPool.cards[i].name)
                {
                    randCard= i;
                    if (CardPool.cards.Count > 0)
                    {
                        priceOfCard = CardPool.cards[randCard].GetComponent<Connected_Spell>().SpellInfo.SpellPrice;
                        sprite.sprite = CardPool.cards[randCard].GetComponent<SpriteRenderer>().sprite;
                    }
                    CurrentCount = CardPool.cards.Count;
                    CurrentCard = CardPool.cards[i].name;
                    return;
                }
            }

            randCard = Random.Range(0, CardPool.cards.Count);
            if (CardPool.cards.Count > 0)
            {
                priceOfCard = CardPool.cards[randCard].GetComponent<Connected_Spell>().SpellInfo.SpellPrice;
                sprite.sprite = CardPool.cards[randCard].GetComponent<SpriteRenderer>().sprite;
                CurrentCount = CardPool.cards.Count;
                CurrentCard = CardPool.cards[randCard].name;
            }

            else
            {
                CurrentCount = 0;
                CurrentCard = "";
                sprite.sprite = null;
            }
        }
    }

    private void ChestErr()
    {
        throw new System.NotImplementedException();
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("PURCHASE ATTTEMPTED");
            Debug.Log(randCard);
            GameEvents.current.DropCard_S(thisID, collision.gameObject);

            //Destroy(gameObject);
        }
    }

    public void Current_OnChestBuy(int ID, GameObject player)   //game events function to spawn a card when touching chest
    {
        if (thisID == ID)
        {
            //int randCard = Random.Range(0, CardPool.cards.Count);
            //int priceOfCard = CardPool.cards[randCard].GetComponent<Connected_Spell>().SpellInfo.SpellPrice;
            //player.GetComponent<Player_Currency>().Steves.money -= priceOfCard;

            var randPosition = new Vector3(Random.Range(-1.5f, 1.5f), 0, Random.Range(-1.5f, 1.5f));
            GameObject Card = Instantiate(CardPool.cards[randCard], gameObject.transform.position + randPosition, Quaternion.Euler(90, 0, 0));
            Debug.Log("PURCHASE SUCCESSFUL");
            gameObject.SetActive(false);
            CardSprite.SetActive(false);
            PriceObject.SetActive(false);
        }
    }

    public void OnDestroy()
    {
        GameEvents.current.OnShopBuy -= Current_OnChestBuy;
    }
}
