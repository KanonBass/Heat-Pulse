using UnityEngine;

public class CombatManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerMoveManager.Instance.OnMovesetGenerated += MovesGenerated;
        MoveButtons.instance.OnButtonPressed.AddListener(MoveSelected);

        PlayerMoveManager.Instance.DefaultMoveSelection();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MovesGenerated()
    {
        MoveButtons.instance.UpdateMoveButtonText();
    }

    public void MoveSelected(CombatMove move)
    {
        PlayerMoveManager.Instance.MoveSelected(move);
        HeatManager.instance.UpdateHeatBar(move.preferredColor);
    }
}
