using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class TwiceThePower : MonoBehaviour
{
    MutationCard thisCard;
    public bool alreadyActivated;
    void Start()
    {
        thisCard = gameObject.GetComponent<MutationCard>();
    }


    void Update()
    {
        CheckState();
    }

    private void CheckState()
    {
        if (thisCard._cardPosition == MutationCardPosition.onMonsterCard)
        {
            Activate();
            alreadyActivated = true;
        }
    }

    private void Activate()
    {
        if (alreadyActivated == false)
        {
            MonsterCard monsterCard = GetComponentInParent<MonsterCard>();
            monsterCard.currentAttack *= 2;
        }
    }


}
