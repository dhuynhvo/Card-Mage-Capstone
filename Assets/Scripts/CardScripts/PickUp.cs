using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField]
    private Premade_Decks PlayerCardPool;

    void Start()
    {
        GameEvents.current.NearDroppedCard += PickUpCard;
        PlayerCardPool = Resources.Load<Premade_Decks>("Player_Card_Pool");
    }

    void Update()
    {

    }

    private void PickUpCard()
    {
        string cardName = gameObject.GetComponent<Connected_Spell>().spell.GetComponent<Spell_Info>().SpellName;
        bool isInList = false;
        if(PlayerCardPool.CardNames.Count == 0)
        {
            PlayerCardPool.CardNames.Add(cardName);
            return;
        }

        else if(PlayerCardPool.CardNames.Count > 0)
        {
            for (int i = 0; i < PlayerCardPool.CardNames.Count; i++)
            {
                if (PlayerCardPool.CardNames[i] == cardName)
                {
                    isInList = true;
                }
            }
        }

        if(isInList == false)
        {
            PlayerCardPool.CardNames.Add(cardName);
        }

        Destroy(gameObject, 1f);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameEvents.current.PickUpCard_E();
        }
    }
}
