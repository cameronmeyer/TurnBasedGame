using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateKidAction : State
{
    GameStateFSM _stateMachine;

    public GameStateKidAction(GameStateFSM stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        StatePrinter.current.printState("STATE: Kid Team Perform Action");
        Debug.Log("STATE: Kid Team Perform Action");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        //decrement team action counter

        if (CommenceTransition)//(StateDuration >= 1.5f) //check if team can perform more actions
        {
            //commented out so we can show off all the states
            if(_stateMachine.TurnNumber < _stateMachine.MaxTurns)
            {
                _stateMachine.ChangeState(_stateMachine.SquidSelectState);
            }
            else
            {
                _stateMachine.ChangeState(_stateMachine.FinishState);
            }
            //_stateMachine.ChangeState(_stateMachine.KidSelectState);
        }
        //no more actions are available

        //check if more turns are available
        //_stateMachine.ChangeState(_stateMachine.SquidSelectState);

        //else
        //
    }
}
