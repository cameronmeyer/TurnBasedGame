using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardStatus : MonoBehaviour
{
    public static BoardStatus current;
    [SerializeField] private Material TeamSquid;
    [SerializeField] private Material TeamKid;

    [HideInInspector] public GridSpace[,] board;

    public void Setup(GridSpace[,] map)
    {
        board = map;
    }

    void Start()
    {
        current = this;
        ChooseTeamColors();
    }

    void ChooseTeamColors()
    {
        ColorPair teamColors = gameObject.GetComponent<Colors>().GetColorPair();

        // 50% chance to swap team colors

        TeamSquid.color = teamColors.color1;
        TeamKid.color = teamColors.color2;
    }
}

public struct GridSpace
{
    public Piece piece;
    public Tile tile;

    public GridSpace(Piece piece, Tile tile)
    {
        this.piece = piece;
        this.tile = tile;
    }
}