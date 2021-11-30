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
    private Pathfinding pathfinding;

    [HideInInspector] public GridSpace[,] board;

    public void Setup(GridSpace[,] map)
    {
        board = map;
        pathfinding = new Pathfinding();
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

    public void InitTeams(List<Vector2Int> squidTeamRespawns, List<Vector2Int> kidTeamRespawns)
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

            Vector2Int squidRespawnSpace = new Vector2Int(squidTeamRespawns[squidRespawnIndex].x, squidTeamRespawns[squidRespawnIndex].y);
            Vector2Int kidRespawnSpace = new Vector2Int(kidTeamRespawns[kidRespawnIndex].x, kidTeamRespawns[kidRespawnIndex].y);

            if (teamSize % 3 == 2) // spawn 1 roller for every 2 shooters
            {
                // spawn rollers

                // squid team
                GameObject squidRoller = Instantiate(Roller, board[squidRespawnSpace.x, squidRespawnSpace.y].tile.transform.position, Quaternion.LookRotation(Vector3.right));
                Piece squidRollerPiece = squidRoller.GetComponent<Piece>();
                squidRollerPiece.Init(true, maxPieceHP, squidRespawnSpace, squidRespawnSpace);
                TeamSquid.Add(squidRollerPiece);
                board[squidRespawnSpace.x, squidRespawnSpace.y].piece = squidRollerPiece;

                // kid team
                GameObject kidRoller = Instantiate(Roller, board[kidRespawnSpace.x, kidRespawnSpace.y].tile.transform.position, Quaternion.LookRotation(Vector3.left));
                Piece kidRollerPiece = kidRoller.GetComponent<Piece>();
                kidRollerPiece.Init(false, maxPieceHP, kidRespawnSpace, kidRespawnSpace);
                TeamKid.Add(kidRollerPiece);
                board[kidRespawnSpace.x, kidRespawnSpace.y].piece = kidRollerPiece;
            }
            else
            {
                // spawn shooters

                // squid team
                GameObject squidShooter = Instantiate(Shooter, board[squidRespawnSpace.x, squidRespawnSpace.y].tile.transform.position, Quaternion.LookRotation(Vector3.right));
                Piece squidShooterPiece = squidShooter.GetComponent<Piece>();
                squidShooterPiece.Init(true, maxPieceHP, squidRespawnSpace, squidRespawnSpace);
                TeamSquid.Add(squidShooterPiece);
                board[squidRespawnSpace.x, squidRespawnSpace.y].piece = squidShooterPiece;

                // kid team
                GameObject kidShooter = Instantiate(Shooter, board[kidRespawnSpace.x, kidRespawnSpace.y].tile.transform.position, Quaternion.LookRotation(Vector3.left));
                Piece kidShooterPiece = kidShooter.GetComponent<Piece>();
                kidShooterPiece.Init(false, maxPieceHP, kidRespawnSpace, kidRespawnSpace);
                TeamKid.Add(kidShooterPiece);
                board[kidRespawnSpace.x, kidRespawnSpace.y].piece = kidShooterPiece;
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
        int pieceIndex = (int)Random.Range(0, TeamSquid.Count);
        Piece piece = TeamKid[pieceIndex];

        if (isPaintBoard)
        {
            PaintBoard(piece, Direction.LEFT);
            isPaintBoard = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPosition = GetMouseWorldPosition();
            Debug.Log(mouseWorldPosition);

            List<GridSpace> path = pathfinding.FindPath(piece.pieceLocation, new Vector2Int(20, 3));
            if (path != null)
            {
                for (int i = 0; i < path.Count; i++)
                {
                    path[i].tile.UpdatePaint(TilePaint.SQUID_PAINT);
                }
            }
        }
    }

    public static Vector3 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void PaintBoard(Piece paintSource, Direction dir)
    {
        List<Vector2Int> paintSplatter = new List<Vector2Int>();
        int paintSourceIndex;

        if (paintSource.team) // painting for Squid Team
        {
            paintSourceIndex = TeamSquid.IndexOf(paintSource);
            paintSplatter = TeamSquid[paintSourceIndex].getPaintPattern(dir);

            foreach (Vector2Int paintedTile in paintSplatter)
            {
                board[paintedTile.x, paintedTile.y].tile.UpdatePaint(TilePaint.SQUID_PAINT);
                //Debug.Log("Paint Tile: (" + paintedTile.x + ", " + paintedTile.y + ")");
            }
        }
        else
        {
            paintSourceIndex = TeamKid.IndexOf(paintSource);
            paintSplatter = TeamKid[paintSourceIndex].getPaintPattern(dir);

            foreach (Vector2Int paintedTile in paintSplatter)
            {
                board[paintedTile.x, paintedTile.y].tile.UpdatePaint(TilePaint.KID_PAINT);
                //Debug.Log("Paint Tile: (" + paintedTile.x + ", " + paintedTile.y + ")");
            }
        }

        foreach (Vector2Int paintedTile in paintSplatter)
        {
            board[paintedTile.x, paintedTile.y].tile.UpdatePaint(TilePaint.SQUID_PAINT);
            //Debug.Log("Paint Tile: (" + paintedTile.x + ", " + paintedTile.y + ")");
        }
    }
}

public class GridSpace
{
    public Piece piece;
    public Tile tile;
    public Vector2Int location;
    public Vector3Int pathfindingCosts;  // (fCost, gCost, hCost)
    public GridSpace pathfindingPrevNode;


    public GridSpace(Piece piece, Tile tile, Vector2Int location)
    {
        this.piece = piece;
        this.tile = tile;
        this.location = location;
        pathfindingCosts = Vector3Int.zero;
        pathfindingPrevNode = null;
    }

    public void pathfindingCalcFCost()
    {
        pathfindingCosts[0] = pathfindingCosts[1] + pathfindingCosts[2]; // fCost = gCost + hCost
    }
}