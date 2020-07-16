using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public enum UnitRole
{
    UNASSIGNED,
    PLAYER1,
    PLAYER2,
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

    public void Awake()
    {
        maxHP = unitStats.hp;
        currentHP = unitStats.hp;
        moveCooldownText = new int[4];
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
        for(int i = 0; i <= 3; i++)
        {
            if(moveCooldownText[i] > 0)
            {
                moveCooldownText[i]--;

                if (moveCooldownText[i] == 0)
                {
                    unitStats.moves[i].onCooldown = false;
                }
            }
            
        }
    }
}
