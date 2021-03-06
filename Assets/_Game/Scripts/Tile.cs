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
    public MeshRenderer respawnRenderer;
    public MeshRenderer highlightRenderer;
    public Vector2Int tileLocation;

    [SerializeField] Material RespawnMaterial;
    [SerializeField] private Material SquidMaterial;
    [SerializeField] private Material KidMaterial;

    private void Start()
    {
        tileRenderer = gameObject.GetComponent<MeshRenderer>();
    }

    public void Init(TileSpawn respawn, Vector2Int tileLocation)
    {
        Start();

        paint = TilePaint.UNPAINTED;
        this.respawn = respawn;
        this.tileLocation = tileLocation;

        if(respawn != TileSpawn.NO_SPAWN)
            respawnRenderer.enabled = true;
    }

    public void UpdatePaint(TilePaint newPaint)
    {
        paint = newPaint;
        tileRenderer.material = (paint == TilePaint.SQUID_PAINT) ? SquidMaterial : KidMaterial;
    }
}
