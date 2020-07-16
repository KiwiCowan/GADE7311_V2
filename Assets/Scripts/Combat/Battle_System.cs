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

    public Transform currentPlayerBattleStation;
    public Transform nextPlayerBattleStation;

    Unit currentPlayer;
    Unit nextPlayer;

    GameObject player1GO;
    GameObject player2GO;

    Moves currentMove;
    public TextMeshProUGUI dialogueText;

    public Battle_HUD currentPlayerHUD;
    public Battle_HUD nextPlayerHUD;
    public Battle_Log_HUD battleLogHUD;

    public BattleState state;
    public MoveType moveType;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        player1GO = Instantiate(player1Prefab, currentPlayerBattleStation);
        currentPlayer = player1GO.GetComponent<Unit>();

        player2GO = Instantiate(player2Prefab, nextPlayerBattleStation);
        nextPlayer = player2GO.GetComponent<Unit>();

        dialogueText.text = "A wild " + nextPlayer.unitStats.name + " approaches";

        currentPlayerHUD.SetHUD(currentPlayer);
        nextPlayerHUD.SetHUD(nextPlayer);


        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYER1;
        Player1Turn();
    }

    void Player1Turn()
    {
        dialogueText.text = currentPlayer.unitStats.name + " choose an action:";

        currentPlayer = player1GO.GetComponent<Unit>();
        nextPlayer = player2GO.GetComponent<Unit>();

        UpdateHUDs();
        //currentPlayerHUD.SetHUD(currentPlayer);
        //nextPlayerHUD.SetHUD(nextPlayer);
        //battleLogHUD.SetHUD(currentPlayer);

        UpdateHUDs();
        player1GO.transform.position = currentPlayerBattleStation.position;
        player2GO.transform.position = nextPlayerBattleStation.position;
    }

    void Player2Turn()
    {
        dialogueText.text = nextPlayer.unitStats.name + " choose an action:";

        currentPlayer = player2GO.GetComponent<Unit>();
        nextPlayer = player1GO.GetComponent<Unit>();

        //currentPlayerHUD.SetHUD(nextPlayer);
        //nextPlayerHUD.SetHUD(currentPlayer);
        //battleLogHUD.SetHUD(nextPlayer);
        UpdateHUDs();

        player2GO.transform.position = currentPlayerBattleStation.position;
        player1GO.transform.position = nextPlayerBattleStation.position;
    }

    public void UpdateHUDs()
    {
        currentPlayerHUD.SetHUD(currentPlayer);
        nextPlayerHUD.SetHUD(nextPlayer);
        battleLogHUD.SetHUD(currentPlayer);
    }

    public void OnMoveButton(int move)
    {
        currentMove = currentPlayer.unitStats.moves[move];

       

        if (moveType == MoveType.DAMAGE)
        {
            StartCoroutine(DamageMove()); ;
        }
        else if (moveType == MoveType.BUFF)
        {
            StartCoroutine(BuffMove());
        }
    }

    IEnumerator DamageMove()
    {
        bool isDead = nextPlayer.TakeDamage(currentMove.damage);
        //enemyUnit.TakeDamage(currentUnit.damage);

        //nextPlayerHUD.SetHP(nextPlayer.currentHP);
        //currentPlayerHUD.SetHP(currentPlayer.currentHP);
        


        dialogueText.text = "The attack was successful";

        yield return new WaitForSeconds(2f);

        if (isDead && state == BattleState.PLAYER1)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else if (isDead && state == BattleState.PLAYER2)
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

    IEnumerator BuffMove()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("move");
        Player2Turn();
    }

    //IEnumerator Player2Turn()
    //{
    //    dialogueText.text = nextPlayer.unitName + " attacks!";

    //    yield return new WaitForSeconds(1f);



    //    bool isDead = currentPlayer.TakeDamage(nextPlayer.damage);

    //    currentPlayerHUD.SetHP(currentPlayer.currentHP);

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
            StartCoroutine(PlayerAttack(currentPlayer, nextPlayer));
        }
        else if (state == BattleState.PLAYER2)
        {
            StartCoroutine(PlayerAttack(nextPlayer, currentPlayer));
        }

        //if (state != BattleState.PLAYER1)
        //{
        //    return;
        //}

        //StartCoroutine(PlayerAttack());
    }

    IEnumerator PlayerAttack(Unit currentUnit, Unit enemyUnit)
    {
        bool isDead = false;
        //enemyUnit.TakeDamage(currentUnit.damage);

        nextPlayerHUD.SetHP(nextPlayer.currentHP);
        currentPlayerHUD.SetHP(currentPlayer.currentHP);
        dialogueText.text = "The attack was successful";

        yield return new WaitForSeconds(2f);

        if (isDead && state == BattleState.PLAYER1)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else if (isDead && state == BattleState.PLAYER2)
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
        if (state == BattleState.PLAYER1)
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
            StartCoroutine(PlayerHeal(currentPlayer));
        }
        else if (state == BattleState.PLAYER2)
        {
            StartCoroutine(PlayerHeal(nextPlayer));
        }

        // StartCoroutine(PlayerHeal());
    }

    IEnumerator PlayerHeal(Unit unit)
    {
        //unit.Heal();
        dialogueText.text = unit.unitStats.name + " healed themselves";
        currentPlayerHUD.SetHP(currentPlayer.currentHP);
        nextPlayerHUD.SetHP(nextPlayer.currentHP);

        yield return new WaitForSeconds(2f);

        NextTurn();
    }



    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = currentPlayer.unitStats.name + " wins the battle";
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = nextPlayer.unitStats.name + " wins the battle";
        }
    }
}
