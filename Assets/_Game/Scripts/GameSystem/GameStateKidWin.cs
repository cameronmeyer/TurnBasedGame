using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateKidWin : State
{
    GameStateFSM _stateMachine;

    public GameStateKidWin(GameStateFSM stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("STATE: Kid Team Win");
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
