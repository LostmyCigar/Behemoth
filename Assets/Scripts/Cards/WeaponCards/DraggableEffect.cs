using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class DraggableEffect : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Vector2 originalPosition;
    public WeaponCard weaponCard;
    List<RaycastResult> Targeting = new List<RaycastResult>();
    void Start()
    {
        weaponCard = gameObject.GetComponent<WeaponCard>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = gameObject.transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        gameObject.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Targeting = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, Targeting);
        for (int i = 0; i < Targeting.Count; i++)
        {
            weaponCard.TryTargeting(Targeting[i].gameObject);
        }
        gameObject.transform.position = originalPosition;
    }

}
