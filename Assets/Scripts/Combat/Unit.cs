using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public enum UnitRole
{
    UNASSIGNED,
    P1,
    P2,
    ENEMY
}
public class Unit : MonoBehaviour
{
    public Unit_Stats unitStats;
    //public string unitName;
    //public string unitRole;
    //public string unitLastMove;



    int cooldownTimer;

    public int[] moveCooldownText;
    
    //public int unitHealing;

    //public int damage;

    public int maxHP;
    public int currentHP;

    public bool hasEffect = false;
    public String currentEffectName;
    public int currentEffectDuration;
    public int currentEffectDamage;

    public void Awake()
    {
        maxHP = unitStats.hp;
        currentHP = unitStats.hp;
        
        moveCooldownText = new int[6];
    }

    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Heal(int heal)
    {
        currentHP += heal;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }

    public void CurrentEffect(Moves move)
    {
        hasEffect = true;
        currentEffectName = move.effectName;
        currentEffectDamage = move.effectDamage;
        currentEffectDuration = move.effectDuration;
    }

    public void PutOnCooldown(Moves _move)
    {
        
        
        foreach (Moves move in unitStats.moves)
        {
            if(move == _move)
            {
                move.onCooldown = true;
                int moveIndex = Array.IndexOf(unitStats.moves, move);
                moveCooldownText[moveIndex] = move.cooldown;
            }
        }
    }

    public void UpdateCooldowns()
    {
        for(int i = 0; i <= 5; i++)
        {
            if(moveCooldownText[i] > 0)
            {
                moveCooldownText[i]--;

                if (moveCooldownText[i] <= 0)
                {
                    unitStats.moves[i].onCooldown = false;
                }
            }
            
        }
    }

    public void UpdateEffectDuration()
    {
        TakeDamage(currentEffectDamage);
        currentEffectDuration--;

        if (currentEffectDuration <= 0)
        {
            currentEffectDamage = default;
            currentEffectDuration = default;
            currentEffectName = default;
            hasEffect = false;
        }

        
    }
}
