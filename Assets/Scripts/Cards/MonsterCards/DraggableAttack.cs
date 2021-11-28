using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableAttack : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    MonsterCard monsterCard;
    List<RaycastResult> Targeting = new List<RaycastResult>();

    /*
    public GameObject arrowImage;
    private Vector2 startPoint;
    private List<GameObject> Arrows = new List<GameObject>();
    [SerializeField] private float spaceBetweenArrows;
    */
    public void Start()
    {
        monsterCard = this.gameObject.GetComponent<MonsterCard>();

        /*
        for (int i = 0; i < 5; i++)
        {
            Arrows.Add(arrowImage);
        }
        for (int i = 0; i < Arrows.Count; i++)
        {
            Arrows[i].SetActive(false);
        }
        */
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Transform parent = this.gameObject.transform.parent;

        /*
        startPoint = gameObject.transform.position;
        for (int i = 0; i < Arrows.Count; i++)
        {
            Instantiate(Arrows[i], transform.position, Quaternion.identity);
            Arrows[i].SetActive(true);
        }
        */
    }


    public void OnDrag(PointerEventData eventData)
    {
        /*
        for (int i = 0; i < Arrows.Count; i++)
        {
            Arrows[i].transform.position = Vector2.Lerp(startPoint, eventData.position, i * 0.1f);
        }
        */
    }



    public void OnEndDrag(PointerEventData eventData)
    {
        //arrowImage.SetActive(false);

        Targeting = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, Targeting);
        for (int i = 0; i < Targeting.Count; i++)
        {
            MonsterCard monsterCardEnemy = Targeting[i].gameObject.GetComponentInParent<MonsterCard>();
            if (monsterCardEnemy != null)
            {
                monsterCard.TryAttacking(monsterCardEnemy.gameObject);
                break;
            }
        }
    }
}
