using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class DraggableBuy : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Vector2 originalPosition;
    MutationCard mutationCard;
    List<RaycastResult> Targeting = new List<RaycastResult>();

    private void Start()
    {
        mutationCard = gameObject.GetComponent<MutationCard>();
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
            MonsterCard monsterCard = Targeting[i].gameObject.GetComponentInParent<MonsterCard>();
            if (monsterCard != null)
            {
                mutationCard.TryPlacingOn(monsterCard.gameObject);
                break;
            }
        }
        gameObject.transform.position = originalPosition;
    }

}
