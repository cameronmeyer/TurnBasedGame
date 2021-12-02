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

        StatePrinter.current.printState("STATE: Squid Team Win\nPress ESC for Main Menu");
        Debug.Log("STATE: Squid Team Win");

        AudioHelper.PlayClip2D(Sounds.current.win, 0.8f);
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
