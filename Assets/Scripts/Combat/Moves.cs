using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MoveType
{
    DAMAGE,
    BUFF,
    DEFAULT
}

[CreateAssetMenu(fileName = "New Move", menuName = "Move")]
public class Moves : ScriptableObject
{
    public string moveName;
    public MoveType moveType;
    public int damage;
    public int buff;
    public int cooldown;
    public bool onCooldown = false;

    public bool hasEffect;
    public string effectName;
    public int effectDamage;
    public int effectDuration;
    public Sprite moveIcon;


}
