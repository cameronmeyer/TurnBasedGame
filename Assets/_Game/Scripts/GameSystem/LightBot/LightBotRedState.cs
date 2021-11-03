 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBotRedState : State
{
    LightBotFSM _stateMachine;
    Light _light;
    Color _color;
    Collider _detectionCollider;

    public LightBotRedState(LightBotFSM stateMachine, 
        Light light, Color color, Collider detectionCollider)
    {
        _stateMachine = stateMachine;
        _light = light;
        _color = color;
        _detectionCollider = detectionCollider;
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("STATE: Red");
        _light.enabled = true;
        _light.color = _color;
        _detectionCollider.enabled = true;
    }

    public override void Exit()
    {
        base.Exit();
        _detectionCollider.enabled = false;
    }

    public override void Update()
    {
        base.Update();
        //TODO Determine if colliders in range have moved
        Debug.Log("TODO Calculate collider movement to determine failure");
        if(StateDuration >= 1.5f)
        {
            _stateMachine.ChangeState(_stateMachine.GreenState);
        }
    }
}
