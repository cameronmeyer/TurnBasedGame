using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateSquidSelect : State
{
    GameStateFSM _stateMachine;
    bool isSelectingPiece = false;
    float timeExecuted = 0f;

    public GameStateSquidSelect(GameStateFSM stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        BoardStatus.current.movingPiece = null;

        StatePrinter.current.printTurn(_stateMachine.TurnNumber, _stateMachine.MaxTurns);
        StatePrinter.current.printState("Squid Team's Turn");
        StatePrinter.current.printAction(BoardStatus.current.actionsRemaining, BoardStatus.current.maxActions);
        Debug.Log("STATE: Squid Team Select Action");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (CommenceTransition || BoardStatus.current.actionsRemaining <= 0)
        {
            BoardStatus.current.actionsRemaining = BoardStatus.current.maxActions;
            _stateMachine.ChangeState(_stateMachine.KidSelectState);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time == timeExecuted)
                return;

            timeExecuted = Time.time;

            if (!isSelectingPiece)
            {
                isSelectingPiece = BoardStatus.current.pieceSelection(true);
            }
            else
            {
                isSelectingPiece = false;
                if (BoardStatus.current.destinationSelection())
                {
                    BoardStatus.current.actionsRemaining--;
                    _stateMachine.ChangeState(_stateMachine.SquidActionState);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (isSelectingPiece)
            {
                isSelectingPiece = false;
                Pathfinding.pathfinding.HideWalkAbleArea();
                BoardStatus.current.actionsRemaining--;
                AudioHelper.PlayClip2D(Sounds.current.paintSplatter, 0.8f);

                Direction dir = Direction.RIGHT;
                GridSpace gs = BoardStatus.current.GetGridClick();
                Vector2Int gsLocation = new Vector2Int(0, 0);

                if (gs != null)
                {
                    gsLocation = gs.location;
                }

                if (Mathf.Abs(gsLocation.y - BoardStatus.current.movingPiece.pieceLocation.y) > Mathf.Abs(gsLocation.x - BoardStatus.current.movingPiece.pieceLocation.x))
                {
                    if (gsLocation.y - BoardStatus.current.movingPiece.pieceLocation.y > 0)
                    {
                        dir = Direction.UP;
                    }
                    else
                    {
                        dir = Direction.DOWN;
                    }
                }
                else
                {
                    if (gsLocation.x - BoardStatus.current.movingPiece.pieceLocation.x < 0)
                    {
                        dir = Direction.LEFT;
                    }
                }

                BoardStatus.current.PaintBoard(BoardStatus.current.movingPiece, dir);
            }
        }
    }
}
