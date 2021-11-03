using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBotInactiveState : State
{
    private LightBotFSM _stateMachine;
    private Light _light;
    private float _timeUntilStart = 1.5f;
    private Collider _detectionCollider;

    // create a constructor definition
    public LightBotInactiveState(LightBotFSM stateMachine, 
        LightBotController controller, Light light)
    {
        _stateMachine = stateMachine;
        _detectionCollider = controller.DetectionCollider;
        _light = light;
    }
    // define enter, exit, update/etc.
    public override void Enter()
    {
        base.Enter();

        Debug.Log("STATE: Inactive");
        _light.enabled = false;
        _detectionCollider.enabled = false;
    }

    public override void Update()
    {
        base.Update();

        if(StateDuration >= _timeUntilStart)
        {
            _stateMachine.ChangeState(_stateMachine.GreenState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("BEGIN GAME");
    }

    // add whatever code we want
}
