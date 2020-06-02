using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum BattleState
{
    START,
    PLAYER1,
    PLAYER2,
    WON,
    LOST
}

public class Battle_System : MonoBehaviour
{

    public GameObject player1Prefab;
    public GameObject player2Prefab;

    public Transform player1BattleStation;
    public Transform player2BattleStation;

    Unit player1Unit;
    Unit player2Unit;

    public TextMeshProUGUI dialogueText;

    public Battle_HUD player1HUD;
    public Battle_HUD player2HUD;

    public BattleState state;
    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject player1GO = Instantiate(player1Prefab, player1BattleStation);
        player1Unit = player1GO.GetComponent<Unit>();

        GameObject player2GO = Instantiate(player2Prefab, player2BattleStation);
        player2Unit = player2GO.GetComponent<Unit>();

        dialogueText.text = "A wild " + player2Unit.unitName + " approaches";

        player1HUD.SetHUD(player1Unit);
        player2HUD.SetHUD(player2Unit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYER1;
        Player1Turn();
    }

    void Player1Turn()
    {
        dialogueText.text = player1Unit.unitName + " choose an action:";
    }

    void Player2Turn()
    {
        dialogueText.text = player2Unit.unitName + " choose an action:";
    }

    //IEnumerator Player2Turn()
    //{
    //    dialogueText.text = player2Unit.unitName + " attacks!";

    //    yield return new WaitForSeconds(1f);



    //    bool isDead = player1Unit.TakeDamage(player2Unit.damage);

    //    player1HUD.SetHP(player1Unit.currentHP);

    //    dialogueText.text = "The attack was successful";

    //    yield return new WaitForSeconds(2f);

    //    if (isDead)
    //    {
    //        state = BattleState.LOST;
    //        EndBattle();
    //    }
    //    else
    //    {
    //        state = BattleState.PLAYER1;
    //        Player1Turn();
    //    }
    //}

    public void OnAttackButton()
    {
        if (state == BattleState.PLAYER1)
        {
            StartCoroutine(PlayerAttack(player1Unit, player2Unit));
        }
        else if (state == BattleState.PLAYER2)
        {
            StartCoroutine(PlayerAttack(player2Unit, player1Unit));
        }

        //if (state != BattleState.PLAYER1)
        //{
        //    return;
        //}

        //StartCoroutine(PlayerAttack());
    }

    IEnumerator PlayerAttack(Unit  currentUnit,Unit enemyUnit)
    {
        bool isDead = enemyUnit.TakeDamage(currentUnit.damage);

        player2HUD.SetHP(player2Unit.currentHP);
        player1HUD.SetHP(player1Unit.currentHP);
        dialogueText.text = "The attack was successful";

        yield return new WaitForSeconds(2f);

        if (isDead && state == BattleState.PLAYER1)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else if(isDead && state == BattleState.PLAYER2)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            NextTurn();
           // StartCoroutine(Player2Turn());
        }
    }

    void NextTurn()
    {
        if(state == BattleState.PLAYER1)
        {
            state = BattleState.PLAYER2;
            Player2Turn();
        }
        else
        {
            state = BattleState.PLAYER1;
            Player1Turn();
        }
    }
    public void OnHealButton()
    {
        if (state == BattleState.PLAYER1)
        {
            StartCoroutine(PlayerHeal(player1Unit));
        }
        else if (state == BattleState.PLAYER2)
        {
            StartCoroutine(PlayerHeal(player2Unit));
        }

       // StartCoroutine(PlayerHeal());
    }

    IEnumerator PlayerHeal(Unit unit)
    {
        unit.Heal();
        dialogueText.text = unit.unitName + " healed themselves";
        player1HUD.SetHP(player1Unit.currentHP);
        player2HUD.SetHP(player2Unit.currentHP);

        yield return new WaitForSeconds(2f);

        NextTurn();
    }

    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = player1Unit.unitName + " wins the battle";
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = player2Unit.unitName + " wins the battle";
        }
    }
}
