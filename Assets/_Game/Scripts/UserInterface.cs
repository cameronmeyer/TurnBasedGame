using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterface : MonoBehaviour
{
    [SerializeField] GameStateFSM fsm;

    public void CommenceTransition()
    {
        fsm.CurrentState.CommenceTransition = true;
    }
}
