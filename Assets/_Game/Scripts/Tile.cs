using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TilePaint
{
    UNPAINTED,
    SQUID_PAINT,
    KID_PAINT
}

public class Tile : MonoBehaviour
{
    public TilePaint paint;
    public bool isRespawnPoint;
    //public bool isOccupied;

    public Tile()
    {
        paint = TilePaint.UNPAINTED;
        isRespawnPoint = false;
    }

    public Tile(bool isRespawnPoint)
    {
        paint = TilePaint.UNPAINTED;
        this.isRespawnPoint = isRespawnPoint;
    }
}
