using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardStatus : MonoBehaviour
{
    public static BoardStatus current;

    [HideInInspector] public GridSpace[,] board;

    public void Setup(GridSpace[,] map)
    {
        board = map;
    }

    void Start()
    {
        current = this;
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

