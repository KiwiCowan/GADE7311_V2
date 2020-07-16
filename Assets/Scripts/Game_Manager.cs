using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Turn
{
    PLAYER1,
    PLAYER2
}

public class Game_Manager : MonoBehaviour
{
    [SerializeField]
    GameObject player1;
    [SerializeField]
    GameObject player2;
    public Turn turn;

    public int Turns { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Turns = 10;
        turn = Turn.PLAYER1;
        Player1Turn();
    }
    void Update()
    {
        if (Turns == 0)
        {
            NextTurn();
            //turn = Turn.PLAYER2;
            //transform.position = player1.transform.position;

        }
        //else
        //{
        //    //turn = Turn.PLAYER1;
        //    //transform.position = player2.transform.position;

        //}
    }
    public void NextTurn()
    {
        Turns = 10;
        if (turn == Turn.PLAYER1)
        {
            Debug.Log("p1");
            turn = Turn.PLAYER2;
            Player2Turn();

        }
        else
        {
            Debug.Log("p2");
            turn = Turn.PLAYER1;
            Player1Turn();
        }
    }

    public void Player1Turn()
    {
        player1.SetActive(true);
        player2.SetActive(false);

    }

    public void Player2Turn()
    {
        player2.SetActive(true);
        player1.SetActive(false);

    }
}
