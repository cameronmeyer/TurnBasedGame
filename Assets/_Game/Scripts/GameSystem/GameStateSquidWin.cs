using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateSquidWin : State
{
    GameStateFSM _stateMachine;

    public GameStateSquidWin(GameStateFSM stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("STATE: Squid Team Win");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }
}
