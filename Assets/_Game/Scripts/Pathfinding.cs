using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public static Pathfinding pathfinding;

    private List<GridSpace> openList;   // list of nodes we want to explore
    private List<GridSpace> closedList; // list of already-explored nodes

    private void Awake()
    {
        pathfinding = this;
    }

    public List<GridSpace> GetWalkableArea(Piece movingPiece)
    {
        List<GridSpace> walkableSpaces = new List<GridSpace>();

        // get max move distance from piece
        int maxMovementDistance = movingPiece.maxMovementDistance;

        // set loop boundaries
        int leftBound = Mathf.Max(movingPiece.pieceLocation.x - maxMovementDistance, 0);
        int rightBound = Mathf.Min(movingPiece.pieceLocation.x + maxMovementDistance, BoardStatus.current.board.GetLength(0) - 1);
        int lowerBound = Mathf.Max(movingPiece.pieceLocation.y - maxMovementDistance, 0);
        int upperBound = Mathf.Min(movingPiece.pieceLocation.y + maxMovementDistance, BoardStatus.current.board.GetLength(1) - 1);

        for (int x = leftBound; x <= rightBound; x++)
        {
            for (int y = lowerBound; y <= upperBound; y++)
            {
                if (BoardStatus.current.board[x, y].tile != null)
                {
                    if (BoardStatus.current.board[x, y].piece == null || BoardStatus.current.board[x, y].piece == movingPiece)
                    {
                        List<GridSpace> path = FindPath(movingPiece.pieceLocation, new Vector2Int(x, y));
                        if (path != null)
                        {
                            if (path.Count <= maxMovementDistance)
                            {
                                // path within movement distance
                                walkableSpaces.Add(BoardStatus.current.board[x, y]);
                            }
                        }
                    }
                }
            }
        }

        return walkableSpaces;
    }

    public void HideWalkAbleArea()
    {
        foreach (GridSpace location in BoardStatus.current.board)
        {
            if (location.tile != null)
            {
                location.tile.highlightRenderer.enabled = false;
            }
        }
    }

    public void ShowWalkableArea(Piece movingPiece)
    {
        // disable previous highlight markers
        HideWalkAbleArea();

        // enable highlight markers
        foreach (GridSpace gs in GetWalkableArea(movingPiece))
        {
            gs.tile.highlightRenderer.enabled = true;
        }
    }

    public List<GridSpace> FindPath(Vector2Int startPos, Vector2Int endPos)
    {
        GridSpace startNode = BoardStatus.current.board[startPos.x, startPos.y];
        GridSpace endNode = BoardStatus.current.board[endPos.x, endPos.y];
        Piece movingPiece = startNode.piece;

        openList = new List<GridSpace> { startNode };
        closedList = new List<GridSpace>();

        // initialize an empty path by resetting all pathfinding values in the board
        for (int i = 0; i < BoardStatus.current.board.GetLength(0); i++)
        {
            for (int j = 0; j < BoardStatus.current.board.GetLength(1); j++)
            {
                GridSpace pathNode = BoardStatus.current.board[i, j];
                pathNode.pathfindingCosts[1] = int.MaxValue; // sets gCost to infinity
                pathNode.pathfindingCalcFCost();
                pathNode.pathfindingPrevNode = null;
            }
        }

        startNode.pathfindingCosts[1] = 0; // set gCost to 0
        startNode.pathfindingCosts[2] = CalculateDistanceCost(startNode, endNode); // set hCost
        startNode.pathfindingCalcFCost();

        while (openList.Count > 0)
        {
            GridSpace currentNode = GetLowestFCostNode(openList);
            if (currentNode == endNode)
            {
                // reached final node
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (GridSpace neighbor in GetNeighborList(currentNode))
            {
                if (closedList.Contains(neighbor)) continue;

                // rollers can only move in straight lines
                if (movingPiece.pieceType == Type.ROLLER)
                {
                    if (neighbor.location.x != startNode.location.x && neighbor.location.y != startNode.location.y)
                    {
                        closedList.Add(neighbor);
                        continue;
                    }
                }

                int tentativeGCost = currentNode.pathfindingCosts[1] + CalculateDistanceCost(currentNode, neighbor);

                if (tentativeGCost < neighbor.pathfindingCosts[1])
                {
                    neighbor.pathfindingPrevNode = currentNode;
                    neighbor.pathfindingCosts[1] = tentativeGCost;
                    neighbor.pathfindingCosts[2] = CalculateDistanceCost(neighbor, endNode);
                    neighbor.pathfindingCalcFCost();

                    if (!openList.Contains(neighbor))
                    {
                        openList.Add(neighbor);
                    }
                }
            }
        }
            
        // outside of the openList and there is no path to the end node
        return null;
    }

    private List<GridSpace> CalculatePath(GridSpace endNode)
    {
        List<GridSpace> path = new List<GridSpace>();
        path.Add(endNode);
        GridSpace currentNode = endNode;

        while (currentNode.pathfindingPrevNode != null)
        {
            path.Add(currentNode.pathfindingPrevNode);
            currentNode = currentNode.pathfindingPrevNode;
        }

        path.Reverse();
        return path;
    }

    private List<GridSpace> GetNeighborList(GridSpace currentNode)
    {
        List<GridSpace> neighborList = new List<GridSpace>();
        GridSpace tempSpace;

        // left
        // check that the neighbor location is in bounds of the board array
        if (currentNode.location.x - 1 >= 0 && currentNode.location.x - 1 < BoardStatus.current.board.GetLength(0))
        {
            tempSpace = BoardStatus.current.board[currentNode.location.x - 1, currentNode.location.y];
            if (tempSpace.tile != null && tempSpace.piece == null)
            {
                neighborList.Add(tempSpace);
            }
        }

        // right
        if (currentNode.location.x + 1 >= 0 && currentNode.location.x + 1 < BoardStatus.current.board.GetLength(0))
        {
            tempSpace = BoardStatus.current.board[currentNode.location.x + 1, currentNode.location.y];
            if (tempSpace.tile != null && tempSpace.piece == null)
            {
                neighborList.Add(tempSpace);
            }
        }

        // down
        if (currentNode.location.y - 1 >= 0 && currentNode.location.y - 1 < BoardStatus.current.board.GetLength(1))
        {
            tempSpace = BoardStatus.current.board[currentNode.location.x, currentNode.location.y - 1];
            if (tempSpace.tile != null && tempSpace.piece == null)
            {
                neighborList.Add(tempSpace);
            }
        }

        // up
        if (currentNode.location.y + 1 >= 0 && currentNode.location.y + 1 < BoardStatus.current.board.GetLength(1))
        {
            tempSpace = BoardStatus.current.board[currentNode.location.x, currentNode.location.y + 1];
            if (tempSpace.tile != null && tempSpace.piece == null)
            {
                neighborList.Add(tempSpace);
            }
        }

        return neighborList;
    }

    private int CalculateDistanceCost(GridSpace a, GridSpace b)
    {
        int xDistance = Mathf.Abs(a.location.x - b.location.x);
        int yDistance = Mathf.Abs(a.location.y - b.location.y);
        return Mathf.Abs(xDistance - yDistance);
    }

    private GridSpace GetLowestFCostNode(List<GridSpace> nodeList)
    {
        GridSpace lowestFCostNode = nodeList[0];

        for (int i = 1; i < nodeList.Count; i++)
        {
            // compare fCosts of the nodes
            if (nodeList[i].pathfindingCosts[0] < lowestFCostNode.pathfindingCosts[0])
            {
                lowestFCostNode = nodeList[i];
            }
        }

        return lowestFCostNode;
    }
}
