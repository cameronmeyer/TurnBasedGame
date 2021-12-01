using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateSquidSelect : State
{
    GameStateFSM _stateMachine;
    bool isSelectingPiece = false;
    bool isMovingPiece = false;
    float timeExecuted = 0f;

    public GameStateSquidSelect(GameStateFSM stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        _stateMachine.TurnNumber++;

        StatePrinter.current.printTurn(_stateMachine.TurnNumber, _stateMachine.MaxTurns);
        StatePrinter.current.printState("STATE: Squid Team Select Action");
        Debug.Log("STATE: Squid Team Select Action");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (CommenceTransition)
        {
            _stateMachine.ChangeState(_stateMachine.SquidActionState);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time == timeExecuted)
                return;

            timeExecuted = Time.time;

            if (!isSelectingPiece)
            {
                BoardStatus.current.pieceSelection(true);
                isSelectingPiece = true;
            }
            else
            {
                isSelectingPiece = false;
                if (BoardStatus.current.destinationSelection())
                {
                    _stateMachine.ChangeState(_stateMachine.SquidActionState);
                }
            }
        }
    }
}
