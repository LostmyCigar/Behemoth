using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionBoard : MonoBehaviour
{
    PlayerManager playerManager;
    GameObject gameManager;
    public int tokensOnAttack;
    public int tokensOnMutation;
    public int tokensOnRecruitment;
    public Text tokensOnAttackText;
    public Text tokensOnMutationText;
    public Text tokensOnRecruitmentText;
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        playerManager = gameManager.GetComponent<PlayerManager>();

        tokensOnAttack = 2;
        tokensOnMutation = 2;
        tokensOnRecruitment = 2;

    }

    private void Update()
    {
        tokensOnAttackText.text = tokensOnAttack.ToString();
        tokensOnMutationText.text = tokensOnMutation.ToString();
        tokensOnRecruitmentText.text = tokensOnRecruitment.ToString();
    }
    public void MutationPointButton()
    {
        if (playerManager.GetCurrentPlayerAction() == PlayerAction.ChooseAction)
        {
            playerManager.GivePlayerMutationPoints(tokensOnMutation);
            tokensOnAttack++;
            tokensOnMutation = 1;
            tokensOnRecruitment++;
            playerManager.MovePlayerToNextPhase();
        }
    }

    public void RecruitmentPointButton()
    {
        if (playerManager.GetCurrentPlayerAction() == PlayerAction.ChooseAction)
        {
            playerManager.GivePlayerRecruitmentPoints(tokensOnRecruitment);
            tokensOnAttack++;
            tokensOnMutation++;
            tokensOnRecruitment = 1;
            playerManager.MovePlayerToNextPhase();
        }
    }

    public void AttackButton()
    {
        if (playerManager.GetCurrentPlayerAction() == PlayerAction.ChooseAction)
        {
            playerManager.GivePlayerAttackPoints(tokensOnAttack);
            tokensOnAttack = 1;
            tokensOnMutation++;
            tokensOnRecruitment++;
            playerManager.SetCurrentPlayerAction(PlayerAction.AttackPhase);
        }
    }
}
