using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterface : MonoBehaviour
{
    [SerializeField] GameStateFSM fsm;
    [SerializeField] GameObject selector;
    private float selectorOffset;
    private MeshRenderer selectorMR;

    private void Start()
    {
        selectorOffset = selector.transform.position.y;
        selectorMR = selector.GetComponent<MeshRenderer>();
    }

    public void CommenceTransition()
    {
        fsm.CurrentState.CommenceTransition = true;
    }

    private void Update()
    {
        GridSpace gs = BoardStatus.current.GetGridClick();
        
        if (gs != null)
        {
            if (gs.tile != null)
            {
                Vector3 pos = gs.tile.transform.position;
                pos.y += selectorOffset;
                selector.transform.position = pos;
                selectorMR.enabled = true;
            }
            else
            {
                selectorMR.enabled = false;
            }
        }
        else
        {
            selectorMR.enabled = false;
        }
    }
}
