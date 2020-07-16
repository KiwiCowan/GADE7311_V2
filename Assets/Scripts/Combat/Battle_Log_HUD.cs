using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Battle_Log_HUD : MonoBehaviour
{
    public TextMeshProUGUI BattleLogText;
    //public TextMeshProUGUI currentPlayerText;
    // public TextMeshProUGUI lastMoveText;
    public TextMeshProUGUI move1Text;
    public TextMeshProUGUI move2Text;
    public TextMeshProUGUI move3Text;
    public TextMeshProUGUI move4Text;

    public Button[] movesButtonsArray;


    public void SetHUD(Unit unit)
    {
        BattleLogText.text = unit.unitStats.name + " choose an action:";
        //currentPlayerText.text = unit.unitStats.role.ToString();
        //lastMoveText.text = unit.unitStats.lastMove.moveName;
        move1Text.text = unit.unitStats.moves[0].moveName;
        move2Text.text = unit.unitStats.moves[1].moveName;
        move3Text.text = unit.unitStats.moves[2].moveName;
        move4Text.text = unit.unitStats.moves[3].moveName;

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
