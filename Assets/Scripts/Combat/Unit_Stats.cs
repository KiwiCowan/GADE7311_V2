using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "Unit")]
public class Unit_Stats : ScriptableObject
{
    public new string name;
    public int hp;
    public Moves[] moves;
    public Moves lastMove;
    public UnitRole role;
    public GameObject unitModel;

    
}
