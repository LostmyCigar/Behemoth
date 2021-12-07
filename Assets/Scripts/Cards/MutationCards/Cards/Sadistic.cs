using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Sadistic : MutationCardEffect
{
    MutationCard thisCard;
    public bool alreadyActivated;
    void Start()
    {
        thisCard = gameObject.GetComponent<MutationCard>();
    }

    public override void CheckState()
    {
        MonsterCard monsterCard = GetComponentInParent<MonsterCard>();

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
            monsterCard.healOnAttack += 2;
        }
    }

    public override void Deactivate()
    {
        if (alreadyActivated == true)
        {
            MonsterCard monsterCard = GetComponentInParent<MonsterCard>();
            monsterCard.healOnAttack -= 2;
            alreadyActivated = false;
        }
    }
}
