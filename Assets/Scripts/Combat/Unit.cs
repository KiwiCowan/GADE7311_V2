using System.Collections;
using System.Collections.Generic;
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

    

    //public int unitLevel;
    //public int unitHealing;

    //public int damage;

    public int maxHP;
    public int currentHP;

    public void Awake()
    {
        maxHP = unitStats.hp;
        currentHP = unitStats.hp;
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
}
