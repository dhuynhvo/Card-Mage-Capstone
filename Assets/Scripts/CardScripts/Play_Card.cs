using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Play_Card : MonoBehaviour
{
    public Hand PlayerHand;
    public Hand EnemyHand; //Currently unused
    public Deck PlayerDeck;
    public Deck EnemyDeck; //Currently unused
    [SerializeField]
    private List<GameObject> CardQueue;
    [SerializeField]
    private int MaxCardsInQueue;
    public string Card1Bind, Card2Bind, Card3Bind, Card4Bind;
    public GameObject SpellSpawnArea;
    string[] binds = {"1", "2", "3", "4"};

    // Start is called before the first frame update
    void Start()
    {
        CardQueue = new List<GameObject>(new GameObject[MaxCardsInQueue]);
    }

    // Update is called once per frame
    void Update()
    {
        PlayCard();
    }

    public void PlayCard()
    {

        if (PlayerHand.HandEmptyCheck() && AnyKeyDown(binds))
        {
            Debug.Log("No more cards!");
            for (int i = 0; i < PlayerDeck.GraveIndex; i++)
            {
                PlayerDeck.Cards[i] = PlayerDeck.Graveyard[i];
            }

            for (int i = 0; i < PlayerDeck.GraveIndex; i++)
            {
                PlayerDeck.Graveyard[i] = null;
            }
            PlayerDeck.GraveIndex = 0;
            PlayerDeck.DeckReload();
            Debug.Log("Deck Reload");
        }

        else if (Input.GetKeyDown(Card1Bind) && PlayerHand.CardsInHand[0] != null)
        {
            Debug.Log("Played: " + PlayerHand.CardsInHand[0].name);
            Instantiate(PlayerHand.CardsInHand[0], SpellSpawnArea.transform.position, SpellSpawnArea.transform.rotation);
            PlayerDeck.GraveTheCard(PlayerHand.CardsInHand[0], ref PlayerDeck.GraveIndex);
            PlayerHand.CardsInHand[0] = null;
        }

        else if (Input.GetKeyDown(Card2Bind) && PlayerHand.CardsInHand[1] != null)
        {
            Debug.Log("Played: " + PlayerHand.CardsInHand[1].name);
            Instantiate(PlayerHand.CardsInHand[1], SpellSpawnArea.transform.position, SpellSpawnArea.transform.rotation);
            PlayerDeck.GraveTheCard(PlayerHand.CardsInHand[1], ref PlayerDeck.GraveIndex);
            PlayerHand.CardsInHand[1] = null;
        }

        else if (Input.GetKeyDown(Card3Bind) && PlayerHand.CardsInHand[2] != null)
        {
            Debug.Log("Played: " + PlayerHand.CardsInHand[2].name);
            Instantiate(PlayerHand.CardsInHand[2], SpellSpawnArea.transform.position, SpellSpawnArea.transform.rotation);
            PlayerDeck.GraveTheCard(PlayerHand.CardsInHand[2], ref PlayerDeck.GraveIndex);
            PlayerHand.CardsInHand[2] = null;
        }

        else if (Input.GetKeyDown(Card4Bind) && PlayerHand.CardsInHand[3] != null)
        {
            Debug.Log("Played: " + PlayerHand.CardsInHand[3].name);
            Instantiate(PlayerHand.CardsInHand[3], SpellSpawnArea.transform.position, SpellSpawnArea.transform.rotation);
            PlayerDeck.GraveTheCard(PlayerHand.CardsInHand[3], ref PlayerDeck.GraveIndex);
            PlayerHand.CardsInHand[3] = null;
        }

        else if(!PlayerHand.HandEmptyCheck() && AnyKeyDown(binds))
        {
            Debug.Log("That hand slot is empty");
        }
    }

    public bool AnyKeyDown(IEnumerable<string> keys)
    {
        foreach (string key in keys)
        {
            if (Input.GetKeyDown(key)) return true;
        }
        return false;
    }

    private void PushQueueforward()
    {
        bool QueueIsEmpty = CardQueue?.Any() != true;
        if (CardQueue.Count < MaxCardsInQueue)
        {
            if(!QueueIsEmpty)
            {
                for(int i = MaxCardsInQueue - 2; i >= 0; i--)
                {
                    while(true)
                    {
                        //lol this will literally crash don't run this code dummy
                    }
                }
            }
        };
    }

    private void GetOutQueue()
    {
        bool QueueIsEmpty = CardQueue?.Any() != true;
        if(!QueueIsEmpty)
        {
            
        }

    }
}
