using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Hippolia : MonoBehaviour
{
    public int hpThreshold;
    public int reduction;
    public int bonusAttack;
    public MonsterCard monsterCard;
    public GameObject gameManager;
    public PlayerManager playerManager;
    public bool notActivated;
    void Start()
    {
        monsterCard = GetComponent<MonsterCard>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        playerManager = gameManager.GetComponent<PlayerManager>();
        notActivated = true;
    }


    void Update()
    {
        CheckAbility();
    }

    private void CheckAbility()
    {
        if (monsterCard.currentHp <= hpThreshold && notActivated)
        {
            ability();
            notActivated = false;
        }
    }

    private void ability()
    {
        monsterCard.currentAttack += bonusAttack;
        monsterCard.flatDamageReduction += reduction;
    }
}
