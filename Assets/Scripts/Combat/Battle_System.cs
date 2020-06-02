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

    IEnumerator PlayerAttack()
    {
        bool isDead = player2Unit.TakeDamage(player1Unit.damage);

        player2HUD.SetHP(player2Unit.currentHP);
        dialogueText.text = "The attack was successful";

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYER2;
            StartCoroutine(Player2Turn());
        }
    }

    IEnumerator Player2Turn()
    {
        dialogueText.text = player2Unit.unitName + " attacks!";

        yield return new WaitForSeconds(1f);



        bool isDead = player1Unit.TakeDamage(player2Unit.damage);

        player1HUD.SetHP(player1Unit.currentHP);

        dialogueText.text = "The attack was successful";

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYER1;
            Player1Turn();
        }
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

    void Player1Turn()
    {
        dialogueText.text = player1Unit.unitName + " choose an action:";
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYER1)
        {
            return;
        }

        StartCoroutine(PlayerAttack());
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYER1)
        {
            return;
        }

        StartCoroutine(PlayerHeal());
    }

    IEnumerator PlayerHeal()
    {
        player1Unit.Heal(5);
        player1HUD.SetHP(player1Unit.currentHP);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYER2;
        StartCoroutine(Player2Turn());
    }
}
