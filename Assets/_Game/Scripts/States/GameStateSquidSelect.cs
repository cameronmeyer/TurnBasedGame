using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateSquidSelect : State
{
    GameStateFSM _stateMachine;

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

        if (CommenceTransition)//(StateDuration >= 1.5f)
        {
            _stateMachine.ChangeState(_stateMachine.SquidActionState);
        }
    }
}
