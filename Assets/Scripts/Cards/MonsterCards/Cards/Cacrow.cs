using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Cacrow : MonoBehaviour
{
    public MonsterCard monsterCard;
    public GameObject gameManager;
    public PlayerManager playerManager;
    public int currentBonus;

    void Start()
    {
        monsterCard = GetComponent<MonsterCard>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        playerManager = gameManager.GetComponent<PlayerManager>();
    }

    void Update()
    {
        CheckAbility();
    }

    private void CheckAbility()
    {
        if (monsterCard.MutationCards.Count > currentBonus)
        {
            ability();
        }
    }

    private void ability()
    {
        monsterCard.currentAttack++;
        currentBonus++;
    }
}
