using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum BattleState
{
    START,
    P1,
    P2,
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

    private Moves currentMove;
    public TextMeshProUGUI[] moveInfo;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI roundText;
    public Player_Stats_HUD currentPlayerHUD;
    public Player_Stats_HUD nextPlayerHUD;
    public Current_Player_Moves currentPlayerMovesHUD;

    public BattleState state;
    public MoveType moveType;
    int turns = 0;
    int rounds = 0;
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

        foreach (Moves move in currentPlayer.unitStats.moves)
        {
            move.onCooldown = false;
        }
        foreach (Moves move in nextPlayer.unitStats.moves)
        {
            move.onCooldown = false;
        }

        dialogueText.text = "A wild " + nextPlayer.unitStats.name + " approaches";

        currentPlayerHUD.SetHUD(currentPlayer);
        nextPlayerHUD.SetHUD(nextPlayer);
        roundText.text = "Round: " + rounds.ToString();


        yield return new WaitForSeconds(2f);

        state = BattleState.P1;
        Player1Turn();
    }

    void Player1Turn()
    {
        turns++;
        if(turns == 2)
        {
            rounds++;
            roundText.text = "Round: " + rounds.ToString();
            turns = 0;
        }

        currentPlayer = player1GO.GetComponent<Unit>();
        nextPlayer = player2GO.GetComponent<Unit>();
        dialogueText.text = currentPlayer.unitStats.name + " choose an action:";

        //currentPlayerHUD.SetHUD(currentPlayer);
        //nextPlayerHUD.SetHUD(nextPlayer);
        //battleLogHUD.SetHUD(currentPlayer);
        UpdateHUDs();
        player1GO.transform.position = currentPlayerBattleStation.position;
        player2GO.transform.position = nextPlayerBattleStation.position;

        //dialogueText.text = currentPlayer.unitStats.name + " choose an action:";
    }

    void Player2Turn()
    {
        turns++;
        if (turns == 2)
        {
            rounds++;
            roundText.text = "Round: " + rounds.ToString();
            turns = 0;
        }
        currentPlayer = player2GO.GetComponent<Unit>();
        nextPlayer = player1GO.GetComponent<Unit>();
        dialogueText.text = currentPlayer.unitStats.name + " choose an action:";
        //currentPlayerHUD.SetHUD(nextPlayer);
        //nextPlayerHUD.SetHUD(currentPlayer);
        //battleLogHUD.SetHUD(nextPlayer);
        UpdateHUDs();

        player2GO.transform.position = currentPlayerBattleStation.position;
        player1GO.transform.position = nextPlayerBattleStation.position;

        //dialogueText.text = nextPlayer.unitStats.name + " choose an action:";
    }

    public void UpdateHUDs()
    {
        currentPlayerHUD.SetHUD(currentPlayer);
        nextPlayerHUD.SetHUD(nextPlayer);
        currentPlayerMovesHUD.SetHUD(currentPlayer);
    }

    public void UpdateHP()
    {
        currentPlayerHUD.SetHP(currentPlayer.currentHP);
        nextPlayerHUD.SetHP(nextPlayer.currentHP);

    }

    public void OnMoveButton(int move)
    {
        if (currentPlayer.unitStats.moves[move].onCooldown == false)
        {
            currentMove = currentPlayer.unitStats.moves[move];
            moveInfo[0].text = "Attack Name: " + currentMove.moveName;
            moveInfo[1].text = "Damage: " + currentMove.damage;
            moveInfo[2].text = "Cooldown: " + currentMove.cooldown;
            moveInfo[3].text = "Stat Effect: " + currentMove.effectName;
            moveInfo[4].text = "Effect Damage: " + currentMove.effectDamage;
            moveInfo[5].text = "Effect Duration: " + currentMove.effectDuration;
        }
        
        

        //if(currentMove.onCooldown == false)
        //{
        //    if (moveType == MoveType.DAMAGE)
        //    {
        //        StartCoroutine(DamageMove()); ;
        //    }
        //    else if (moveType == MoveType.BUFF)
        //    {
        //        StartCoroutine(BuffMove());
        //    }
        //}



    }

    IEnumerator DamageMove()
    {
        bool isDead = nextPlayer.TakeDamage(currentMove.damage);

        if(currentMove.hasEffect)
        {
            nextPlayer.CurrentEffect(currentMove);
        }
        UpdateHP();

        dialogueText.text = "The attack was successful";
        if (currentMove.cooldown > 0)
        {
            currentPlayer.PutOnCooldown(currentMove);
        }


        yield return new WaitForSeconds(2f);

        if (isDead && state == BattleState.P1)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else if (isDead && state == BattleState.P2)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            //EndTurn();
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

    public void EndTurn()
    {
        if (currentMove != null)
        {
            if (moveType == MoveType.DAMAGE)
            {
                StartCoroutine(DamageMove()); ;
            }
            else if (moveType == MoveType.BUFF)
            {
                StartCoroutine(BuffMove());
            }
        }
        //if (currentMove.onCooldown == false)
        //{
        //    if (moveType == MoveType.DAMAGE)
        //    {
        //        StartCoroutine(DamageMove()); ;
        //    }
        //    else if (moveType == MoveType.BUFF)
        //    {
        //        StartCoroutine(BuffMove());
        //    }
        //}

        //dialogueText.text = nextPlayer.unitStats.role + "'s Turn";
        //if(currentMove.cooldown >0)
        //{
        //    currentPlayer.PutOnCooldown(currentMove);
        //}
        //nextPlayer.UpdateCooldowns();
        //nextPlayer.UpdateEffectDuration();
        //currentPlayer.unitStats.lastMove = currentMove;

        //currentMove = null;
        //NextTurn();
    }

    void NextTurn()
    {
        nextPlayer.UpdateCooldowns();
        nextPlayer.UpdateEffectDuration();
        UpdateHUDs();
        currentPlayer.unitStats.lastMove = currentMove;

        currentMove = null;

        if (state == BattleState.P1)
        {
            state = BattleState.P2;
            Player2Turn();
        }
        else
        {
            state = BattleState.P1;
            Player1Turn();
        }
    }

    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = currentPlayer.unitStats.name + " wins the battle";
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = currentPlayer.unitStats.name + " wins the battle";
        }
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
    //        state = BattleState.P1;
    //        Player1Turn();
    //    }
    //}

    public void OnAttackButton()
    {
        if (state == BattleState.P1)
        {
            StartCoroutine(PlayerAttack(currentPlayer, nextPlayer));
        }
        else if (state == BattleState.P2)
        {
            StartCoroutine(PlayerAttack(nextPlayer, currentPlayer));
        }

        //if (state != BattleState.P1)
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

        if (isDead && state == BattleState.P1)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else if (isDead && state == BattleState.P2)
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


    public void OnHealButton()
    {
        if (state == BattleState.P1)
        {
            StartCoroutine(PlayerHeal(currentPlayer));
        }
        else if (state == BattleState.P2)
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




}
