using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Current_Player_Moves : MonoBehaviour
{
    //public TextMeshProUGUI move1Text;
    //public TextMeshProUGUI move2Text;
    //public TextMeshProUGUI move3Text;
    //public TextMeshProUGUI move4Text;
    //public TextMeshProUGUI move5Text;
    //public TextMeshProUGUI move6Text;

    //public TextMeshProUGUI[] moveInfo;

    public Button[] movesButtonsArray;
    public Moves[] currentPlayersMoves;

    public void SetHUD(Unit unit)
    {
        for (int i = 0; i <= 5; i++)
        {
            movesButtonsArray[i].transform.Find("Move_Icon").GetComponent<Image>().sprite = unit.unitStats.moves[i].moveIcon;
            if (unit.unitStats.moves[i].cooldown > 0)
            {
                float cooldownFillAmount = (float)unit.moveCooldownText[i] / (float)unit.unitStats.moves[i].cooldown;
                Debug.Log("Cooldown Text is " + unit.moveCooldownText[i]);
                Debug.Log("Move Cooldown is " + unit.unitStats.moves[i].cooldown);
                Debug.Log(unit.unitStats.moves[i].moveName + " has cooldown fill amount = " + cooldownFillAmount);
                movesButtonsArray[i].transform.Find("Move_Cooldown").GetComponent<Image>().fillAmount = cooldownFillAmount;
            }
            
        }
        //BattleLogText.text = unit.unitStats.name + " choose an action:";
        //currentPlayerText.text = unit.unitStats.role.ToString();
        //lastMoveText.text = unit.unitStats.lastMove.moveName;
        //move1Text.text = unit.unitStats.moves[0].moveName;
        //move2Text.text = unit.unitStats.moves[1].moveName;
        //move3Text.text = unit.unitStats.moves[2].moveName;
        //move4Text.text = unit.unitStats.moves[3].moveName;

    }
    // Start is called before the first frame update
    public void ShowMoveInfo(int index)
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
