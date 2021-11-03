using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateKidSelect : State
{
    GameStateFSM _stateMachine;

    public GameStateKidSelect(GameStateFSM stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("STATE: Kid Team Select Action");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (StateDuration >= 1.5f)
        {
            _stateMachine.ChangeState(_stateMachine.KidActionState);
        }
    }
}
