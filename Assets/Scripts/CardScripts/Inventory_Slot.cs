//Worked on by Dan Huynhvo

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory_Slot : MonoBehaviour, IDropHandler
{
    public GameObject startSpell;

        public void OnDrop(PointerEventData eventData)  //used YouTube tutorial for this: changes parent gameobjects to simulate card swapping
    {
        if (transform.childCount == 0 && gameObject.tag == "Deck")
        {
            GameObject dropped = eventData.pointerDrag;
            DragCard draggableItem = dropped.GetComponent<DragCard>();
            draggableItem.DragParent = transform;
        }

        else if (transform.childCount == 1 && gameObject.tag == "Deck")
        {
            GameObject dropped = eventData.pointerDrag;
            DragCard draggableItem = dropped.GetComponent<DragCard>();
            gameObject.transform.GetChild(0).transform.SetParent(draggableItem.DragParent);
            draggableItem.DragParent = transform;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.tag == "Storage" && gameObject.transform.childCount > 0 && gameObject.transform.GetChild(0).GetComponent<Connected_Spell>().spell != startSpell.GetComponent<Connected_Spell>().spell)
        {
            transform.GetChild(0).gameObject.GetComponent<Connected_Spell>().spell = startSpell.GetComponent<Connected_Spell>().spell;
            transform.GetChild(0).gameObject.GetComponent<Connected_Spell>().SpellInfo = startSpell.GetComponent<Connected_Spell>().SpellInfo;
            transform.GetChild(0).gameObject.GetComponent<Image>().sprite = startSpell.GetComponent<SpriteRenderer>().sprite;
        }
    }
}
