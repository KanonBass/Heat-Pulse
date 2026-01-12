using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMoveManager : MonoBehaviour
{
    [SerializeField] private List<CombatMove> _moveList = new();

    public static List<CombatMove> _currentMoves = new();
    public static List<MoveType> _emptyTypeList = new();

    private int _maxMoves = 3;

    void Start()
    {
        DefaultMoveSelection();
        foreach (CombatMove move in _currentMoves)
        {
            Debug.Log(move.moveName);
        }
    }

    public void GenerateMoveList()
    {
        _currentMoves.Clear();
        NewMoveSelection(_emptyTypeList);
        foreach (CombatMove move in _currentMoves)
        {
            Debug.Log(move.moveName);
        }
    }

    public void NewMoveSelection(List<MoveType> types)
    {
        if (_currentMoves.Count >= _maxMoves)
        {
            return;
        }

        if (types.Count == 0)
        {
            DefaultMoveSelection();
            return;
        }

        List<CombatMove> possibleMoves = new();

        possibleMoves.AddRange(FindMoveByType(types));

        int index = UnityEngine.Random.Range(0, possibleMoves.Count);

        while (_currentMoves.Contains(possibleMoves[index]))
        {
            possibleMoves.RemoveAt(index);
            if (possibleMoves.Count == 0)
            {
                DefaultMoveSelection();
                return;
            }
            index = UnityEngine.Random.Range(0, possibleMoves.Count);
        }

        _currentMoves.Add(possibleMoves[index]);

        if (_maxMoves - _currentMoves.Count > types.Count)
        {
            types.Remove(possibleMoves[index].moveType);
        }

        NewMoveSelection(types);
    }

    public void DefaultMoveSelection()
    {
        int index = UnityEngine.Random.Range(0, _moveList.Count);
        _currentMoves.Add(_moveList[index]);

        List<MoveType> moveTypes = Enum.GetValues(typeof(MoveType)).Cast<MoveType>().ToList();
        moveTypes.Remove(_moveList[index].moveType);

        NewMoveSelection(moveTypes);
    }

    public List<CombatMove> FindMoveByType(MoveType type)
    {
        List<CombatMove> newList = new();

        newList = _moveList.FindAll(CombatMove => CombatMove.moveType == type);
        return newList;
    }

    public List<CombatMove> FindMoveByType(List<MoveType> types)
    {
        List<CombatMove> newList = new();

        foreach (MoveType type in types) 
        {
            newList.AddRange(_moveList.FindAll(CombatMove => CombatMove.moveType == type));
        }

        return newList;
    }
}
