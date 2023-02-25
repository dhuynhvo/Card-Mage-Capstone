using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform DragParent;
    public Image image;
    public GameObject SelectedCard;
    public bool IsSelected;
    public string DeleteButton;

    public void OnMouseDown()
    {
        
        if (IsSelected == false)
        {
            SelectedCard = gameObject;
            IsSelected = true;
            image.color = new Color32(100, 100, 100, 255);
        }

        else if (IsSelected == true)
        {
            SelectedCard = gameObject;
            IsSelected = false;
            image.color = new Color32(255, 255, 255, 255);
        };
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        IsSelected = true;
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
