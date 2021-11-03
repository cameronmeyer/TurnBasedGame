using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateFinish : State
{
    GameStateFSM _stateMachine;

    public GameStateFinish(GameStateFSM stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("STATE: Finish Game");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        //decrement team action counter

        if (StateDuration >= 1.5f) //check if squid team won
        {
            _stateMachine.ChangeState(_stateMachine.SquidWinState);
        }
        //check if kid team won
        //_stateMachine.ChangeState(_stateMachine.KidWinState);

        //draw/tie state??
    }
}
