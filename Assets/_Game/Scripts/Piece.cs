using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    SHOOTER,
    ROLLER,
    BOMB
}

public enum Direction
{
    UP,
    DOWN,
    LEFT,
    RIGHT
}

public class Piece : MonoBehaviour
{
    [SerializeField] private Type pieceType;
    public bool team;  // true = squid, false = kid
    public int currentHP;
    public int maxHP { get; private set; }
    [SerializeField] private MeshRenderer paintRenderer;
    [SerializeField] private int paintRendererMaterialIndex = -1;
    public Material squidMaterial;
    public Material kidMaterial;
    public GridSpace respawnLocation;

    public void Init(bool team, int maxHP, GridSpace respawnLocation)
    {
        this.team = team;
        this.maxHP = maxHP;
        this.respawnLocation = respawnLocation;

        // set paint color based on team
        Material[] pieceMaterialInstances = paintRenderer.materials;
        pieceMaterialInstances[paintRendererMaterialIndex] = team ? squidMaterial : kidMaterial;
        paintRenderer.materials = pieceMaterialInstances;
    }

    public List<Vector2> getPaintPattern(Vector2 gridSpaceLocation, Direction facing)
    {
        List<Vector2> paintPattern = new List<Vector2>();

        switch (pieceType)
        {
            case Type.SHOOTER:
                paintPattern.Add(new Vector2(0, 0));
                paintPattern.Add(new Vector2(0, 1));
                paintPattern.Add(new Vector2(0, 2));
                paintPattern.Add(new Vector2(0, 3));
                paintPattern.Add(new Vector2(-1, 3));
                paintPattern.Add(new Vector2(1, 3));
                break;
            case Type.ROLLER:
                paintPattern.Add(new Vector2(0, 0));
                paintPattern.Add(new Vector2(0, 1));
                paintPattern.Add(new Vector2(-1, 1));
                paintPattern.Add(new Vector2(1, 1));
                paintPattern.Add(new Vector2(0, 2));
                paintPattern.Add(new Vector2(-1, 2));
                paintPattern.Add(new Vector2(1, 2));
                break;
            case Type.BOMB:
                paintPattern.Add(new Vector2(0, 0));
                paintPattern.Add(new Vector2(-1, 0));
                paintPattern.Add(new Vector2(1, 0));
                paintPattern.Add(new Vector2(0, -1));
                paintPattern.Add(new Vector2(-1, -1));
                paintPattern.Add(new Vector2(1, -1));
                paintPattern.Add(new Vector2(0, 1));
                paintPattern.Add(new Vector2(-1, 1));
                paintPattern.Add(new Vector2(1, 1));
                break;
        }

        List<Vector2> relativePaintPattern = new List<Vector2>();

        switch(facing)
        {
            case Direction.UP:
                foreach(Vector2 paintTile in paintPattern)
                {
                    relativePaintPattern.Add(new Vector2(gridSpaceLocation.x + paintTile.x, gridSpaceLocation.y + paintTile.y));
                }
                break;
        }

        return relativePaintPattern;
    }
}
