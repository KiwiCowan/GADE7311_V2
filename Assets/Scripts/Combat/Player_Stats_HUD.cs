using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player_Stats_HUD : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    
    public TextMeshProUGUI move1CooldownText;
    public TextMeshProUGUI move2CooldownText;
    public TextMeshProUGUI move3CooldownText;
    public TextMeshProUGUI move4CooldownText;
    public TextMeshProUGUI hpText;
    public GameObject currentEffect;

    public Image playerIcon;

    public Slider hpSlider;
    private string hpString;
    private int maxHP;

    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitStats.role.ToString() + " " + unit.unitStats.name;
        playerIcon.sprite = unit.unitStats.unitSprite;
        move1CooldownText.text = unit.moveCooldownText[0].ToString();
        move2CooldownText.text = unit.moveCooldownText[1].ToString();
        move3CooldownText.text = unit.moveCooldownText[2].ToString();
        move4CooldownText.text = unit.moveCooldownText[3].ToString();
        if (unit.hasEffect)
        {
            currentEffect.SetActive(true);
            currentEffect.GetComponentInChildren<TextMeshProUGUI>().text = "-" + unit.currentEffectDamage;
        }
        else
        {
            currentEffect.SetActive(false);
        }
        
        //currentEffect.GetComponentInChildren<Text>().text = "-" + unit.currentEffectDamage.ToString();

        //levelText.text = "Lvl " + unit.unitLevel;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
        maxHP = unit.maxHP;
        hpString =unit.currentHP + "/" + maxHP;
        hpText.text = hpString;
    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
        hpString = hp + "/" + maxHP;
        hpText.text = hpString;
    }

}
