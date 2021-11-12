using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colors : MonoBehaviour
{
    [SerializeField] private List<ColorPair> teamColors;

    public ColorPair GetColorPair()
    {
        int index = Random.Range(0, teamColors.Count);
        return teamColors[index];
    }
}

[System.Serializable]
public struct ColorPair
{
    public Color color1;
    public Color color2;

    public ColorPair(Color color1, Color color2)
    {
        this.color1 = color1;
        this.color2 = color2;
    }
}