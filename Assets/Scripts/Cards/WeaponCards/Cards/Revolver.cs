using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
public class Revolver : WeaponCard
{
    public override void TryTargeting(GameObject target)
    {
        MonsterCard monsterCardEnemy = target.GetComponentInParent<MonsterCard>();
        if (monsterCardEnemy != null)
        {
            if (playerManager.MonsterCardInOtherHand(monsterCardEnemy.gameObject))
            {
                Effect(target);
            }
        }
    }

    public override void Effect(GameObject target)
    {
        if (canUse)
        {
            MonsterCard monsterCard = target.GetComponentInParent<MonsterCard>();
            monsterCard.currentHp -= 2;
            canUse = false;
        }
    }
}
