using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateStart : State
{
    GameStateFSM _stateMachine;

    public GameStateStart(GameStateFSM stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("STATE: Start Game");
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
            _stateMachine.ChangeState(_stateMachine.SquidSelectState);
        }
    }
}
