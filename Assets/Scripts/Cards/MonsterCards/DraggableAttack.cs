using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableAttack : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Vector2 originalPosition;
    MonsterCard monsterCard;
    List<RaycastResult> Targeting = new List<RaycastResult>();
    PlayerManager playerManager;

    public void Start()
    {
        monsterCard = this.gameObject.GetComponent<MonsterCard>();
        playerManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerManager>();

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
            MonsterCard monsterCardEnemy = Targeting[i].gameObject.GetComponentInParent<MonsterCard>();
            if (monsterCardEnemy != null)
            {
                if (playerManager.MonsterCardInOtherHand(monsterCardEnemy.gameObject))
                {
                    monsterCard.TryAttacking(monsterCardEnemy.gameObject);
                    break;
                }
            }
        }
        gameObject.transform.position = originalPosition;
    }
}
