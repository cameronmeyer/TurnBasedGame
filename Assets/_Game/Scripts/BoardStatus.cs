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

    public Action action;
    public int actionsRemaining;
    public int maxActions = 4;

    public bool isSelectingMove = false;
    public bool isMoving = false;
    public Piece movingPiece;
    public List<GridSpace> movingPath;
    public GameObject pieceIndicator;
    public MeshRenderer pieceIndicatorMR;
    private float pieceIndicatorOffset;

    [HideInInspector] public GridSpace[,] board;

    public void Setup(GridSpace[,] map)
    {
        board = map;
        action = Action.NONE;
        actionsRemaining = maxActions;
        pieceIndicatorMR.enabled = false;
        pieceIndicatorOffset = pieceIndicator.transform.position.y;
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
    }

    public GridSpace GetGridClick()
    {
        RaycastHit h;
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(r, out h))
        {
            if (h.transform.gameObject.GetComponent<Tile>() != null)
            {
                Vector2Int pos = h.transform.gameObject.GetComponent<Tile>().tileLocation;
                return board[pos.x, pos.y];
            }
        }

        return null;
    }

    private void Update()
    {
        if (movingPiece != null)
        {
            Vector3 pos = movingPiece.transform.position;
            pos.y += pieceIndicatorOffset;
            pieceIndicator.transform.position = pos;
            pieceIndicatorMR.enabled = true;
        }
        else
        {
            pieceIndicatorMR.enabled = false;
        }
    }

    public bool pieceSelection(bool team) // true = squid, false = kid
    {
        GridSpace gs = GetGridClick();

        if (gs != null)
        {
            if (gs.piece != null)
            {
                //isSelectingMove = true;
                if (gs.piece.team == team)
                {
                    movingPiece = gs.piece;
                    Pathfinding.pathfinding.ShowWalkableArea(gs.piece);
                    return true;
                }
            }
            else
            {
                Pathfinding.pathfinding.HideWalkAbleArea();
            }
        }
        else
        {
            Pathfinding.pathfinding.HideWalkAbleArea();
        }

        movingPiece = null;
        return false;
    }

    public bool destinationSelection()
    {
        GridSpace gs = GetGridClick();

        if (gs != null)
        {
            if (movingPiece != null)
            {
                if (gs.tile.highlightRenderer.enabled == true)
                {
                    movingPath = Pathfinding.pathfinding.FindPath(movingPiece.pieceLocation, gs.location);  // set path to move along
                    board[movingPiece.pieceLocation.x, movingPiece.pieceLocation.y].piece = null;           // reset starting point's piece ref
                    gs.piece = movingPiece;                                                                 // set end point's piece ref
                    //movingPiece.pieceLocation = gs.location;                                                // set piece's new grid location
                    action = Action.MOVE;                                                                   // action move
                    isMoving = true;                                                                        // moving piece anim
                    Pathfinding.pathfinding.HideWalkAbleArea();
                    isSelectingMove = false;
                    return true;
                }
            }
        }

        Pathfinding.pathfinding.HideWalkAbleArea();
        isSelectingMove = false;
        return false;
    }

    public void PaintSquare(Piece paintSource)
    {
        if (paintSource.team) // painting for Squid Team
        {
            GridSpace paintedSpace = board[paintSource.pieceLocation.x, paintSource.pieceLocation.y];
            if (paintedSpace != null)
            {
                if (paintedSpace.tile != null)
                {
                    paintedSpace.tile.UpdatePaint(TilePaint.SQUID_PAINT);
                }
            }
        }
        else
        {
            GridSpace paintedSpace = board[paintSource.pieceLocation.x, paintSource.pieceLocation.y];
            if (paintedSpace != null)
            {
                if (paintedSpace.tile != null)
                {
                    paintedSpace.tile.UpdatePaint(TilePaint.KID_PAINT);
                }
            }
        }
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
                GridSpace paintedSpace = board[paintedTile.x, paintedTile.y];
                if (paintedSpace != null)
                {
                    if (paintedSpace.tile != null)
                    {
                        paintedSpace.tile.UpdatePaint(TilePaint.SQUID_PAINT);
                    }
                }
            }
        }
        else
        {
            paintSourceIndex = TeamKid.IndexOf(paintSource);
            paintSplatter = TeamKid[paintSourceIndex].getPaintPattern(dir);

            foreach (Vector2Int paintedTile in paintSplatter)
            {
                GridSpace paintedSpace = board[paintedTile.x, paintedTile.y];
                if (paintedSpace != null)
                {
                    if (paintedSpace.tile != null)
                    {
                        paintedSpace.tile.UpdatePaint(TilePaint.KID_PAINT);
                    }
                }
            }
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

public enum Action
{
    NONE,
    MOVE,
    PAINT,
    PASS
}