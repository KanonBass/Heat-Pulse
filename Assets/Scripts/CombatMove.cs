using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CombatMove", menuName = "CombatMove")]
public class CombatMove : ScriptableObject
{
    public string moveName;
    public MoveType moveType;
    public Dictionary<Status, int> statuses;
    public TargetType targetType;
    public int damage;
    public bool isAOE;
    public MoveType[] combos;
    public HeatColorType preferredColor;
}

public enum MoveType
{
    None,
    Attack,
    Spell,
    Buff,
    Debuff,
}

public enum Status
{
    Anger,
    Shocked
}

public enum TargetType
{
    Friendly,
    Enemy
}
