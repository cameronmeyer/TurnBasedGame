using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardStatus : MonoBehaviour
{
    public static BoardStatus current;
    public List<Piece> TeamSquid;
    public List<Piece> TeamKid;

    [SerializeField] private Material TeamSquidMat;
    [SerializeField] private Material TeamKidMat;
    [SerializeField] private GameObject Shooter;
    [SerializeField] private GameObject Roller;
    [SerializeField] private GameObject Bomb;
    [SerializeField] private int maxPieceHP = 2;
    [SerializeField] private bool isPaintBoard = false;

    [HideInInspector] public GridSpace[,] board;

    public void Setup(GridSpace[,] map)
    {
        board = map;
    }

    void Awake()
    {
        current = this;
        ChooseTeamColors();
    }

    void ChooseTeamColors()
    {
        ColorPair teamColors = gameObject.GetComponent<Colors>().GetColorPair();

        // 50% chance to swap team colors
        int swapColors = (int) Random.Range(0, 2); // generates either 0 or 1

        if (swapColors < 1)
        {
            TeamSquidMat.color = teamColors.color1;
            TeamKidMat.color = teamColors.color2;
        }
        else
        {
            TeamSquidMat.color = teamColors.color2;
            TeamKidMat.color = teamColors.color1;
        }
    }

    public void InitTeams(List<Vector2> squidTeamRespawns, List<Vector2> kidTeamRespawns)
    {
        // function accepts list of GridSpace locations for each team's respawns

        if(squidTeamRespawns.Count != kidTeamRespawns.Count)
        {
            Debug.LogError("Unequal team sizes");
            return;
        }

        int totalTeammates = squidTeamRespawns.Count;
        for(int teamSize = 0; teamSize < totalTeammates; teamSize++)
        {
            int squidRespawnIndex = Random.Range(0, squidTeamRespawns.Count);
            int kidRespawnIndex = Random.Range(0, kidTeamRespawns.Count);

            GridSpace squidRespawnSpace = board[(int)squidTeamRespawns[squidRespawnIndex].x, (int)squidTeamRespawns[squidRespawnIndex].y];
            GridSpace kidRespawnSpace = board[(int)kidTeamRespawns[kidRespawnIndex].x, (int)kidTeamRespawns[kidRespawnIndex].y];

            if (teamSize % 3 == 2) // spawn 1 roller for every 2 shooters
            {
                // spawn rollers

                // squid team
                GameObject squidRoller = Instantiate(Roller, squidRespawnSpace.tile.transform.position, Quaternion.LookRotation(Vector3.right));
                Piece squidRollerPiece = squidRoller.GetComponent<Piece>();
                squidRollerPiece.Init(true, maxPieceHP, squidRespawnSpace);
                TeamSquid.Add(squidRollerPiece);

                // kid team
                GameObject kidRoller = Instantiate(Roller, kidRespawnSpace.tile.transform.position, Quaternion.LookRotation(Vector3.left));
                Piece kidRollerPiece = kidRoller.GetComponent<Piece>();
                kidRollerPiece.Init(false, maxPieceHP, kidRespawnSpace);
                TeamKid.Add(kidRollerPiece);
            }
            else
            {
                // spawn shooters

                // squid team
                GameObject squidShooter = Instantiate(Shooter, squidRespawnSpace.tile.transform.position, Quaternion.LookRotation(Vector3.right));
                Piece squidShooterPiece = squidShooter.GetComponent<Piece>();
                squidShooterPiece.Init(true, maxPieceHP, squidRespawnSpace);
                TeamSquid.Add(squidShooterPiece);

                // kid team
                GameObject kidShooter = Instantiate(Shooter, kidRespawnSpace.tile.transform.position, Quaternion.LookRotation(Vector3.left));
                Piece kidShooterPiece = kidShooter.GetComponent<Piece>();
                kidShooterPiece.Init(false, maxPieceHP, kidRespawnSpace);
                TeamKid.Add(kidShooterPiece);
            }
            squidTeamRespawns.RemoveAt(squidRespawnIndex);
            kidTeamRespawns.RemoveAt(kidRespawnIndex);
        }

        foreach (Piece piece in TeamSquid)
        {
            Debug.Log("Piece " + TeamSquid.Count);
        }
    }

    private void Update()
    {
        if (isPaintBoard)
        {
            PaintBoard();
            isPaintBoard = false;
        }
    }

    public void PaintBoard()
    {
        List<Vector2> paintSplatter = new List<Vector2>();
        paintSplatter = TeamSquid[0].getPaintPattern(new Vector2(5, 5), Direction.UP);

        foreach (Vector2 paintedTile in paintSplatter)
        {
            if(board[(int)paintedTile.x, (int)paintedTile.y].tile != null)
                board[(int) paintedTile.x, (int) paintedTile.y].tile.tileRenderer.material = TeamSquidMat;
            Debug.Log("Paint Tile: (" + paintedTile.x + ", " + paintedTile.y + ")");
        }
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