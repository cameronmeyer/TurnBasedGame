using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TilePaint
{
    UNPAINTED,
    SQUID_PAINT,
    KID_PAINT
}

public enum TileSpawn
{
    NO_SPAWN,
    RESPAWN_SQUID,
    RESPAWN_KID
}

public class Tile : MonoBehaviour
{
    public TilePaint paint;
    public TileSpawn respawn;
    //public bool isOccupied;
    [HideInInspector] public MeshRenderer tileRenderer;

    [SerializeField] Material RespawnMaterial;
    private Material SquidMaterial;
    private Material KidMaterial;

    //public Tile() { Init(TileSpawn.NO_SPAWN); }
    //public Tile(TileSpawn respawn) { Init(respawn); }

    private void Start()
    {
        tileRenderer = gameObject.GetComponent<MeshRenderer>();
    }

    public void Init(TileSpawn respawn)
    {
        Start();

        paint = TilePaint.UNPAINTED;
        this.respawn = respawn;

        if(respawn != TileSpawn.NO_SPAWN)
            tileRenderer.material = RespawnMaterial;
    }

    private void UpdatePaint(TilePaint newPaint)
    {
        paint = newPaint;
        tileRenderer.material = (paint == TilePaint.SQUID_PAINT) ? SquidMaterial : KidMaterial;
    }
}
