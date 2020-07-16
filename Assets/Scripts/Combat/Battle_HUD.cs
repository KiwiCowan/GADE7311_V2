using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Battle_HUD : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI currentPlayerText;
    public TextMeshProUGUI lastMoveText;
    public TextMeshProUGUI move1CooldownText;
    public TextMeshProUGUI move2CooldownText;
    public TextMeshProUGUI move3CooldownText;
    public TextMeshProUGUI move4CooldownText;

    public Slider hpSlider;

    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitStats.name;
        currentPlayerText.text = unit.unitStats.role.ToString();
      //  lastMoveText.text = unit.unitStats.lastMove.moveName;
        move1CooldownText.text = unit.unitStats.moves[0].cooldown.ToString();
        move2CooldownText.text = unit.unitStats.moves[1].cooldown.ToString();
        move3CooldownText.text = unit.unitStats.moves[2].cooldown.ToString();
        move4CooldownText.text = unit.unitStats.moves[3].cooldown.ToString();

        //levelText.text = "Lvl " + unit.unitLevel;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
    }
}
