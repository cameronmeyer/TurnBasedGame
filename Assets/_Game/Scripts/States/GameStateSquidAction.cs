using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateSquidAction : State
{
    GameStateFSM _stateMachine;

    public GameStateSquidAction(GameStateFSM stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("STATE: Squid Team Perform Action");

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
        //decrement team action counter

        if (BoardStatus.current.action == Action.MOVE && !BoardStatus.current.isMoving)
        {
            BoardStatus.current.action = Action.NONE;
            _stateMachine.ChangeState(_stateMachine.SquidSelectState);
        }

        //if (CommenceTransition)//(StateDuration >= 1.5f) //check if team can perform more actions
        //{
            //commented out so we can show off all the states
            //_stateMachine.ChangeState(_stateMachine.SquidSelectState);
        //    _stateMachine.ChangeState(_stateMachine.KidSelectState);
        //}
        //no more actions are available, next team's turn
        //_stateMachine.ChangeState(_stateMachine.KidSelectState);
    }
}
