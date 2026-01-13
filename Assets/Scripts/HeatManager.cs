using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class HeatManager : MonoBehaviour
{
    public static HeatManager instance;

    static private Dictionary<HeatColorType, Color> HeatColorRelation = new();

    [SerializeField] private float _heatMultiplier = 1f;
    [SerializeField] private float _startingHeat = 0f;

    [SerializeField] private GameObject _heatMarkerUI;
    [SerializeField] private GameObject _heatBarUI;
    private RectTransform _heatMarkerRect;
    private RectTransform _heatBarRect;

    [SerializeField] private colorValueDict[] _barColorValues;
    private GradientColorKey[] _barColors = new GradientColorKey[5];

    private float _heatValue;
    private Gradient _heatGradient = new();

    public event Action<float> OnHeatUpdate;

    void Awake()
    {
        HeatManager.instance = this;
        InitializeHeatColorRelation();
        InitializeHeatBar();
    }

    private void Start()
    {
        OnHeatUpdate += MoveHeatMarker;

        _heatMarkerRect = _heatMarkerUI.GetComponent<RectTransform>();
        _heatBarRect = _heatBarUI.GetComponent<RectTransform>();

        SetHeatBar(_startingHeat);
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
        var stepDifference = .2f;

        if (_heatGradient.Evaluate(_heatValue) == GetHeatColor(color))
        {
            _heatValue += stepDifference * _heatMultiplier;
            _heatValue = Mathf.Clamp(_heatValue, 0f, 1f);
            OnHeatUpdate?.Invoke(_heatValue);

            return;
        }        

        int difference = color - GetHeatColor(_heatGradient.Evaluate(_heatValue));

        _heatValue += stepDifference * difference * _heatMultiplier;
        _heatValue = Mathf.Clamp(_heatValue, 0f, 1f);

        OnHeatUpdate?.Invoke(_heatValue);
    }

    public void SetHeatBar(float value)
    {
        _heatValue = Mathf.Clamp(value, 0, 1);

        OnHeatUpdate?.Invoke(_heatValue);
    }

    public void MoveHeatMarker(float value)
    {
        Debug.Log(value);

        var newX = _heatBarUI.transform.localPosition.x + _heatBarRect.rect.width * value;

        Debug.Log(newX);

        _heatMarkerRect.localPosition = new Vector3(newX, _heatMarkerRect.localPosition.y, _heatMarkerRect.localPosition.z);

        Debug.Log(_heatMarkerUI.transform.localPosition.x);
    }

    public Color GetHeatColor(HeatColorType type)
    {
        return HeatColorRelation[type];
    }

    public HeatColorType GetHeatColor(Color color) 
    {
        if (color == Color.blue)
        {
            return HeatColorType.Blue;
        }
        else if (color == Color.green)
        { 
            return HeatColorType.Green;
        }
        else if (color == Color.yellow)
        {
            return HeatColorType.Yellow;
        }
        else if (color == Color.orange)
        {
            return HeatColorType.Orange;
        }
        else if (color == Color.red)
        {
            return HeatColorType.Red;
        }

        return HeatColorType.Blue;
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
