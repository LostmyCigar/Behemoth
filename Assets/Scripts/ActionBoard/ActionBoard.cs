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

    [Header("For ability usage")]
    public int tokensOnTurnStart;
    public bool attackCalled;
    public bool mutationCalled;
    public bool recruitmentCalled;
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


        if (playerManager.GetCurrentPlayerAction() != PlayerAction.ChooseAction)
        {
            StartCoroutine(ResetBools());
        }
    }

    public void MutationPointButton()
    {
        if (playerManager.GetCurrentPlayerAction() == PlayerAction.ChooseAction)
        {
            tokensOnTurnStart = tokensOnMutation;
            mutationCalled = true;
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
            tokensOnTurnStart = tokensOnRecruitment;
            recruitmentCalled = true; 
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
            tokensOnTurnStart = tokensOnAttack;
            attackCalled = true;
            playerManager.GivePlayerAttackPoints(tokensOnAttack);
            tokensOnAttack = 1;
            tokensOnMutation++;
            tokensOnRecruitment++;
            playerManager.SetCurrentPlayerAction(PlayerAction.AttackPhase);
           

        }
    }

    IEnumerator ResetBools()
    {
        yield return new WaitForSeconds(0.1f);
        attackCalled = false;
        mutationCalled = false;
        recruitmentCalled = false;
    }
}
