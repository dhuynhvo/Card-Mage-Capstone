//Dan Huynhvo
//UNR
//CS 425
//Outside Resource referenced for event handling

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField]
    private Premade_Decks PlayerCardPool;
    public int thisID;

    void Start()
    {
        thisID = gameObject.GetInstanceID();
        GameEvents.current.NearDroppedCard += PickUpCard;
        PlayerCardPool = Resources.Load<Premade_Decks>("Player_Card_Pool");
    }

    void Update()
    {

    }

    private void PickUpCard(int ID)
    {
        if(thisID == ID)
        {
            string cardName = gameObject.GetComponent<Connected_Spell>().spell.GetComponent<Spell_Info>().SpellName;
            bool isInList = false;

            if (PlayerCardPool.CardNames.Count == 0)
            {
                PlayerCardPool.CardNames.Add(cardName);

                return;
            }

            else if (PlayerCardPool.CardNames.Count > 0)
            {
                for (int i = 0; i < PlayerCardPool.CardNames.Count; i++)
                {
                    if (PlayerCardPool.CardNames[i] == cardName)
                    {
                        isInList = true;
                    }
                }
            }

            if (isInList == false)
            {
                PlayerCardPool.CardNames.Add(cardName);
            }

            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Untagged" || collision.gameObject.tag == "Untagged")
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }

        if (collision.gameObject.tag == "Player")
        {
            GameEvents.current.PickUpCard_E(thisID);
            AudioManager.instance.Play("PickupCardPlayerEvent");
        }
    }

    public void OnDestroy()
    {
        GameEvents.current.NearDroppedCard -= PickUpCard;
    }
}
