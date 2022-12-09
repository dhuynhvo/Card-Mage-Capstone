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
    private int count;
    private bool QueueEmpty;
    private bool DeQueuePlaying;
    [SerializeField]
    public List<GameObject> CardQueue;
    [SerializeField]
    private int MaxCardsInQueue;
    public string Card1Bind, Card2Bind, Card3Bind, Card4Bind;
    public GameObject SpellSpawnArea;
    string[] binds = { "1", "2", "3", "4" };

    void Start()
    {
        CardQueue = new List<GameObject>(new GameObject[MaxCardsInQueue]);
        StartCoroutine(GetOutQueue());
    }

    void Update()
    {
        count = CardQueue.Count(x => x != null);
        PlayCard();
        PushQueueforward();
    }

    private void FixedUpdate()
    {
        GetOutQueue();
    }

    public void PlayCard()
    {

        if (PlayerHand.HandEmptyCheck() && AnyKeyDown(binds) && count == 0)
        {
            PlayerDeck.GraveIndex = 0;
            PlayerDeck.DeckReload();
        }

        else if (Input.GetKeyDown(Card1Bind) && PlayerHand.CardsInHand[0] != null && count < MaxCardsInQueue)
        {
            Debug.Log("Played: " + PlayerHand.CardsInHand[0].name);
            GameObject newSpell_0 = Instantiate(PlayerHand.CardsInHand[0], SpellSpawnArea.transform.position, SpellSpawnArea.transform.rotation) as GameObject;
            CardQueue[0] = newSpell_0;
            PlayerHand.CardsInHand[0] = null;
        }

        else if (Input.GetKeyDown(Card2Bind) && PlayerHand.CardsInHand[1] != null && count < MaxCardsInQueue)
        {
            Debug.Log("Played: " + PlayerHand.CardsInHand[1].name);
            GameObject newSpell_1 = Instantiate(PlayerHand.CardsInHand[1], SpellSpawnArea.transform.position, SpellSpawnArea.transform.rotation) as GameObject;
            CardQueue[0] = newSpell_1;
            PlayerHand.CardsInHand[1] = null;
        }

        else if (Input.GetKeyDown(Card3Bind) && PlayerHand.CardsInHand[2] != null && count < MaxCardsInQueue)
        {
            Debug.Log("Played: " + PlayerHand.CardsInHand[2].name);
            GameObject newSpell_2 = Instantiate(PlayerHand.CardsInHand[2], SpellSpawnArea.transform.position, SpellSpawnArea.transform.rotation) as GameObject;
            CardQueue[0] = newSpell_2;
            PlayerHand.CardsInHand[2] = null;
        }

        else if (Input.GetKeyDown(Card4Bind) && PlayerHand.CardsInHand[3] != null && count < MaxCardsInQueue)
        {
            Debug.Log("Played: " + PlayerHand.CardsInHand[3].name);
            GameObject newSpell_3 = Instantiate(PlayerHand.CardsInHand[3], SpellSpawnArea.transform.position, SpellSpawnArea.transform.rotation) as GameObject;
            CardQueue[0] = newSpell_3;
            PlayerHand.CardsInHand[3] = null;
        }

        else if (!PlayerHand.HandEmptyCheck() && AnyKeyDown(binds))
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
        int j;
        if (count < MaxCardsInQueue)
        {
            if(CardQueue[MaxCardsInQueue - 1] != null)
            {
                for(int i = MaxCardsInQueue - 2; i >= 0; i--)
                {
                    j = i + 2;
                    if(CardQueue[i + 1] == null)
                    {
                        CardQueue[i + 1] = CardQueue[i];
                        CardQueue[i] = null;
                        while(CardQueue[j] == null)
                        {
                            CardQueue[j] = CardQueue[j - 1];
                            CardQueue[j - 1] = null;
                            j++;
                        }
                    }
                }
            }

            else if(CardQueue[MaxCardsInQueue - 1] == null)
            {
                for(int i = MaxCardsInQueue - 2; i >= 0; i--)
                {
                    if(CardQueue[i] != null)
                    {
                        CardQueue[MaxCardsInQueue - 1] = CardQueue[i];
                        CardQueue[i] = null;
                        break;
                    }
                }
            }    
        }
    }

    private IEnumerator GetOutQueue()
    {
        while(true)
        {
            for(int i = MaxCardsInQueue - 1; i > 0; i--)
            {
                if(count > 0 && CardQueue[i] != null)
                {
                    CardQueue[i].GetComponent<Spell_Info>().CooldownTimer++;
                    Debug.Log(CardQueue[i].GetComponent<Spell_Info>().CooldownTimer);
                    if (CardQueue[i].GetComponent<Spell_Info>().CooldownTimer >= CardQueue[i].GetComponent<Spell_Info>().cooldown)
                    {
                        PlayerDeck.GraveTheCard(CardQueue[i], ref PlayerDeck.GraveIndex);
                        CardQueue[i] = null;
                    }

                }
            }
            yield return new WaitForSeconds(1f);
        }
    }

}