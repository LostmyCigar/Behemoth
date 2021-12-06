using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class TwiceThePower : MutationCardEffect
{
    MutationCard thisCard;
    public bool alreadyActivated;
    void Start()
    {
        thisCard = gameObject.GetComponent<MutationCard>();
    }



    public override void CheckState()
    {
        if (thisCard._cardPosition == MutationCardPosition.onMonsterCard)
        {
            Activate();
            alreadyActivated = true;
        }
    }

    public override void Activate()
    {
        if (alreadyActivated == false)
        {
            MonsterCard monsterCard = GetComponentInParent<MonsterCard>();
            monsterCard.currentAttack *= 2;
        }
    }

    public override void Deactivate()
    {
        if (alreadyActivated == true)
        {
            MonsterCard monsterCard = GetComponentInParent<MonsterCard>();
            monsterCard.currentAttack /= 2;
        }
    }
}
