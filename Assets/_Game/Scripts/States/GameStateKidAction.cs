using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateKidAction : State
{
    GameStateFSM _stateMachine;

    public GameStateKidAction(GameStateFSM stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        StatePrinter.current.printState("STATE: Kid Team Perform Action");
        Debug.Log("STATE: Kid Team Perform Action");

        if (BoardStatus.current.action == Action.MOVE)
        {
            BoardStatus.current.movingPiece.isMoving = true;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        
        if (BoardStatus.current.action == Action.MOVE && !BoardStatus.current.isMoving)
        {
            BoardStatus.current.action = Action.NONE;
            _stateMachine.ChangeState(_stateMachine.KidSelectState);
        }
    }
}
