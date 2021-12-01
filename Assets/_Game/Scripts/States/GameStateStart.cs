using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateStart : State
{
    GameStateFSM _stateMachine;
    MapGenerator mg;

    public GameStateStart(GameStateFSM stateMachine)
    {
        _stateMachine = stateMachine;
        mg = stateMachine.GetComponentInParent<MapGenerator>();
    }

    public override void Enter()
    {
        base.Enter();

        _stateMachine.gameOver = false;
        StatePrinter.current.printState("STATE: Start Game");
        Debug.Log("STATE: Start Game");

        mg.GenerateMap();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (mg.doneGenerating)
        {
            _stateMachine.TurnNumber++;
            _stateMachine.ChangeState(_stateMachine.SquidSelectState);
        }
    }
}
