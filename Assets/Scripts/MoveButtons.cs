using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MoveButtons : MonoBehaviour
{
    public static MoveButtons instance;

    [SerializeField] private List<GameObject> _buttons = new();

    private Dictionary<GameObject, TMP_Text> _buttonTextPairs = new();

    public UnityEvent<CombatMove> OnButtonPressed;
    private void Awake()
    {
        instance = this;

        foreach (var button in _buttons)
        {
            _buttonTextPairs.Add(button, button.GetComponentInChildren<TMP_Text>());
        }
    }

    void Start()
    {
        
    }

    public void UpdateMoveButtonText()
    {
        foreach(var button in _buttons)
        {
            _buttonTextPairs[button].text = PlayerMoveManager._currentMoves[_buttons.IndexOf(button)].name;
        }
    }

    public void PressButtion(int buttonNum)
    {
        OnButtonPressed?.Invoke(PlayerMoveManager._currentMoves[buttonNum]);
    }
}
