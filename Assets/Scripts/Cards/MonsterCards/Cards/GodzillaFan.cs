using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GodzillaFan : MonoBehaviour
{
    public MonsterCard monsterCard;
    public GameObject gameManager;
    public GameObject actionBoard;
    public ActionBoard actionBoardScript;
    public PlayerManager playerManager;

    public bool notActivated;
    void Start()
    {
        monsterCard = GetComponent<MonsterCard>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        playerManager = gameManager.GetComponent<PlayerManager>();
        actionBoard = GameObject.FindGameObjectWithTag("ActionBoard");
        actionBoardScript = actionBoard.GetComponent<ActionBoard>();

        notActivated = true;
    }


    void Update()
    {

        CheckAbility();
    }

    private void CheckAbility()
    {
        if (playerManager.MonsterCardInCurrentPlayerHand(this.gameObject))
        {
            if (actionBoardScript.attackCalled && actionBoardScript.tokensOnTurnStart >= 3)
            {
                ability();
            }
        } 
        else if (playerManager.MonsterCardInCurrentPlayerHand(this.gameObject) == false)
        {
            notActivated = true;
        }
    }

    private void ability()
    {
        if (notActivated)
        {
            monsterCard.currentAttack++;
            monsterCard.originalAttack++;
            notActivated = false;
        }
    }
}
