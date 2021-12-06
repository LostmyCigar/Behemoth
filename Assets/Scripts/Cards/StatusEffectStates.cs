using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum StatusEffectStates
{
    None,
    Stunned,
    Poisioned,
    Weak
}


public class Statuseffects : MonoBehaviour
{



    public void Poisioned(GameObject card, int turns)
    {
        MonsterCard monsterCard = card.GetComponent<MonsterCard>();

    }



}

