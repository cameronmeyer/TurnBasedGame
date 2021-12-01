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
                BoardStatus.current.pieceSelection(true);
                isSelectingPiece = true;
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
    }
}
