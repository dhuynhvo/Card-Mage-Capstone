using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory_Slot : MonoBehaviour, IDropHandler
{

        public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            DragCard draggableItem = dropped.GetComponent<DragCard>();
            draggableItem.DragParent = transform;
        }

        else if (transform.childCount == 1)
        {
            GameObject dropped = eventData.pointerDrag;
            DragCard draggableItem = dropped.GetComponent<DragCard>();
            gameObject.transform.GetChild(0).transform.SetParent(draggableItem.DragParent);
            draggableItem.DragParent = transform;
        };


    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
