using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    SHOOTER,
    ROLLER,
    BOMB
}

public class Piece : MonoBehaviour
{
    public Type pieceType;
    public bool team;
    public int currentHP;
    public int maxHP { get; private set; }

    public Piece(Type pieceType, bool team, int maxHP)
    {
        this.pieceType = pieceType;
        this.team = team;
        this.maxHP = maxHP;
    }

    public List<Vector2> getPaintPattern()
    {
        switch (pieceType)
        {
            case Type.SHOOTER:
                break;
            case Type.ROLLER:
                break;
            case Type.BOMB:
                break;
        }

        return null;
    }
}
