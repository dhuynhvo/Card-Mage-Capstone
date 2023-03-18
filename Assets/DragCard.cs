using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public Transform DragParent;
    public Image image;
    public GameObject SelectedCard;
    public bool IsSelected;
    public string DeleteButton;
    public GameObject SlotHolder;
    public GameObject card;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (gameObject.transform.parent.gameObject.tag != "Storage")
            {
                Destroy(gameObject);
            }
        }

        else
        {
            if (gameObject.transform.parent.gameObject.tag == "Storage")
            {
                SlotHolder = gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.GetChild(0).gameObject;

                for (int i = 0; i < SlotHolder.transform.childCount; i++)
                {
                    if (SlotHolder.transform.GetChild(i).gameObject.transform.childCount == 0)
                    {
                        GameObject newCardSlot = Instantiate(card, new Vector3(0, 0, 0), Quaternion.identity, SlotHolder.transform.GetChild(i).gameObject.transform);
                        newCardSlot.GetComponent<Connected_Spell>().spell = gameObject.GetComponent<Connected_Spell>().spell;
                        newCardSlot.GetComponent<Connected_Spell>().SpellInfo = gameObject.GetComponent<Connected_Spell>().SpellInfo;
                        newCardSlot.GetComponent<Image>().sprite = gameObject.GetComponent<Image>().sprite;
                        return;
                    }
                }
            }
        };
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        DragParent = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(DragParent);
        image.raycastTarget = true;
    }

    void Update()
    {
        if(IsSelected == true && Input.GetKeyDown(KeyCode.E))
        {
            //gameObject.GetComponent<Connected_Spell>().spell = null;
            //gameObject.GetComponent<Connected_Spell>().SpellInfo = null;
            //gameObject.GetComponent<Image>().sprite = 
            Destroy(gameObject);
        };
    }
}
