using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    public Transform parentAfterDrag;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin drag");

        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root); // bring to top
        transform.SetAsLastSibling();

        image.raycastTarget = false; // allow drop detection
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End drag");

        transform.SetParent(parentAfterDrag);
        transform.localPosition = Vector3.zero;

        image.raycastTarget = true; // enable clicking again
    }
}