using System;
using System.Collections.Generic;
using UnityEngine;

public class HeatManager : MonoBehaviour
{
    static private Dictionary<HeatColorType, Color> HeatColorRelation = new();

    [SerializeField] private colorValueDict[] _barColorValues;
    private GradientColorKey[] _barColors = new GradientColorKey[5];

    private float _heatValue;
    private Gradient _heatGradient = new();
    

    void Awake()
    {
        InitializeHeatColorRelation();
        InitializeHeatBar();
    }

    private void InitializeHeatColorRelation()
    {
        HeatColorRelation.Add(HeatColorType.Blue, Color.blue);
        HeatColorRelation.Add(HeatColorType.Green, Color.green);
        HeatColorRelation.Add(HeatColorType.Yellow, Color.yellow);
        HeatColorRelation.Add(HeatColorType.Orange, Color.orange);
        HeatColorRelation.Add(HeatColorType.Red, Color.red);
    }

    private void InitializeHeatBar()
    {
        int i = 0;
        foreach (var color in _barColorValues)
        {
            _barColors[i].color = GetHeatColor(color.color);
            _barColors[i].time = color.value;

            i++;
        }

        _heatGradient.mode = GradientMode.Fixed;
        _heatGradient.SetColorKeys(_barColors);
    }

    public void UpdateHeatBar(HeatColorType color)
    {

    }

    public Color GetHeatColor(HeatColorType type)
    {
        return HeatColorRelation[type];
    }
}

[Serializable]
public struct colorValueDict
{
    public HeatColorType color;
    public float value;
}

public enum HeatColorType
{
    Blue,
    Green,
    Yellow,
    Orange,
    Red
}
