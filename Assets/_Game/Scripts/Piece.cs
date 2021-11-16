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

    public Vector2Int pieceLocation;
    public Vector2Int respawnLocation;

    public void Init(bool team, int maxHP, Vector2Int pieceLocation, Vector2Int respawnLocation)
    {
        this.team = team;
        this.maxHP = maxHP;
        this.pieceLocation = pieceLocation;
        this.respawnLocation = respawnLocation;

        // set paint color based on team
        Material[] pieceMaterialInstances = paintRenderer.materials;
        pieceMaterialInstances[paintRendererMaterialIndex] = team ? squidMaterial : kidMaterial;
        paintRenderer.materials = pieceMaterialInstances;
    }

    public List<Vector2Int> getPaintPattern(Vector2Int gridSpaceLocation, Direction facing)
    {
        List<Vector2Int> paintPattern = new List<Vector2Int>();

        switch (pieceType)
        {
            case Type.SHOOTER:
                paintPattern.Add(new Vector2Int(0, 0));
                paintPattern.Add(new Vector2Int(0, 1));
                paintPattern.Add(new Vector2Int(0, 2));
                paintPattern.Add(new Vector2Int(0, 3));
                paintPattern.Add(new Vector2Int(-1, 3));
                paintPattern.Add(new Vector2Int(1, 3));
                break;
            case Type.ROLLER:
                paintPattern.Add(new Vector2Int(0, 0));
                paintPattern.Add(new Vector2Int(0, 1));
                paintPattern.Add(new Vector2Int(-1, 1));
                paintPattern.Add(new Vector2Int(1, 1));
                paintPattern.Add(new Vector2Int(0, 2));
                paintPattern.Add(new Vector2Int(-1, 2));
                paintPattern.Add(new Vector2Int(1, 2));
                break;
            case Type.BOMB:
                paintPattern.Add(new Vector2Int(0, 0));
                paintPattern.Add(new Vector2Int(-1, 0));
                paintPattern.Add(new Vector2Int(1, 0));
                paintPattern.Add(new Vector2Int(0, -1));
                paintPattern.Add(new Vector2Int(-1, -1));
                paintPattern.Add(new Vector2Int(1, -1));
                paintPattern.Add(new Vector2Int(0, 1));
                paintPattern.Add(new Vector2Int(-1, 1));
                paintPattern.Add(new Vector2Int(1, 1));
                break;
        }

        List<Vector2Int> relativePaintPattern = new List<Vector2Int>();

        foreach (Vector2Int paintTile in paintPattern)
        {
            switch (facing)
            {
                case Direction.UP:
                    relativePaintPattern.Add(new Vector2Int(gridSpaceLocation.x + paintTile.x, gridSpaceLocation.y + paintTile.y));
                    break;
                case Direction.DOWN:
                    relativePaintPattern.Add(new Vector2Int(gridSpaceLocation.x + paintTile.x, gridSpaceLocation.y - paintTile.y));
                    break;
                case Direction.LEFT:
                    relativePaintPattern.Add(new Vector2Int(gridSpaceLocation.x - paintTile.y, gridSpaceLocation.y + paintTile.x));
                    break;
                case Direction.RIGHT:
                    relativePaintPattern.Add(new Vector2Int(gridSpaceLocation.x + paintTile.y, gridSpaceLocation.y + paintTile.x));
                    break;
            }
        }

        return relativePaintPattern;
    }
}
