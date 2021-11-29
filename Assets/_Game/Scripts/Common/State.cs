using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public bool CommenceTransition = false;
    protected float StateDuration = 0;

    public virtual void Enter()
    {
        CommenceTransition = false;
        StateDuration = 0;
    }

    public virtual void Exit()
    {
        CommenceTransition = false;
    }

    public virtual void Update()
    {
        StateDuration += Time.deltaTime;
    }

    public virtual void FixedUpdate()
    {

    }
}
