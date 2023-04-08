using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Add_Card_Storage : MonoBehaviour
{

    [SerializeField]
    private Premade_Decks CardStorage;
    [SerializeField]
    private GameObject card;
    [SerializeField]

    public void addCardsToStorage()
    {
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }

        for (int i = 0; i < CardStorage.CardNames.Count; i++)
        {
            string cardToLoad = "Prefabs/" + CardStorage.CardNames[i] + " Card";
            GameObject Card = Resources.Load<GameObject>(cardToLoad);
            
            GameObject newCardSlot = Instantiate(card, new Vector3(0, 0, 0), Quaternion.identity, gameObject.transform);

            newCardSlot.tag = "Storage";
            //newCardSlot.transform.GetChild(0).tag = "Storage";
            newCardSlot.transform.GetChild(0).gameObject.GetComponent<Connected_Spell>().spell = Card.GetComponent<Connected_Spell>().spell;
            newCardSlot.transform.GetChild(0).gameObject.GetComponent<Connected_Spell>().SpellInfo = Card.GetComponent<Connected_Spell>().SpellInfo;
            newCardSlot.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Card.GetComponent<SpriteRenderer>().sprite;

            newCardSlot.GetComponent<Inventory_Slot>().startSpell = Card;
        };
    }
}
