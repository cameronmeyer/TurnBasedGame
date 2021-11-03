using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBotFSM : StateMachineMB
{
    [SerializeField]
    private LightBotController _controller;

    public LightBotInactiveState InactiveState { get; private set; }
    public LightBotGreenState GreenState { get; private set; }
    public LightBotRedState RedState { get; private set; }

    private void Awake()
    {
        // create my states
        InactiveState = new LightBotInactiveState
            (this, _controller, _controller.Light);
        GreenState = new LightBotGreenState
            (this, _controller.Light, _controller.GreenLightColor);
        RedState = new LightBotRedState
            (this, _controller.Light, _controller.RedLightColor, 
            _controller.DetectionCollider);
    }

    private void Start()
    {
        ChangeState(InactiveState);
    }
}
