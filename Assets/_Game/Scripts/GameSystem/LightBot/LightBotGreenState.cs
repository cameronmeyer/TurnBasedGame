using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBotGreenState : State
{
    LightBotFSM _stateMachine;
    Light _light;
    Color _color;

    public LightBotGreenState(LightBotFSM stateMachine, 
        Light light, Color color)
    {
        _stateMachine = stateMachine;
        _light = light;
        _color = color;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("STATE: Green State");

        //TODO determine random green duration
        // turn on the light
        _light.enabled = true;
        // change light color to green
        _light.color = _color;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        // wait for X seconds
        if(StateDuration >= 1.5f)
        {
            _stateMachine.ChangeState(_stateMachine.RedState);
        }
    }
}
