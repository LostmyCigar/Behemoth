using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class SoftenThemUp : MutationCardEffect
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

        if (monsterCard._statusEffectApplies == StatusEffectStates.None)
        {
            alreadyActivated = false;
        }

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
            monsterCard._statusEffectApplies = StatusEffectStates.Weak;
            monsterCard.appliesTimer += 1;
        }
    }

    public override void Deactivate()
    {
        if (alreadyActivated == true)
        {
            MonsterCard monsterCard = GetComponentInParent<MonsterCard>();
            monsterCard._statusEffectApplies = StatusEffectStates.None;
            monsterCard.appliesTimer -= 1;
            alreadyActivated = false;
        }
    }
}
