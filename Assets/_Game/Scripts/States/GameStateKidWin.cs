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

        StatePrinter.current.printState("STATE: Kid Team Win\nPress ESC for Main Menu");
        Debug.Log("STATE: Kid Team Win");

        AudioHelper.PlayClip2D(Sounds.current.lose, 0.8f);
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
