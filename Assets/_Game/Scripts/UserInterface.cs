using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterface : MonoBehaviour
{
    public static UserInterface current;
    [SerializeField] GameStateFSM fsm;
    [SerializeField] GameObject selector;
    [SerializeField] GameObject gameplayUI;
    [SerializeField] GameObject finishUI;
    private float selectorOffset;
    private MeshRenderer selectorMR;

    private void Start()
    {
        current = this;
        selectorOffset = selector.transform.position.y;
        selectorMR = selector.GetComponent<MeshRenderer>();
        ShowGameplayUI();
        HideFinishUI();
    }

    public void CommenceTransition()
    {
        fsm.CurrentState.CommenceTransition = true;
    }

    public void ShowGameplayUI()
    {
        gameplayUI.SetActive(true);
    }

    public void HideGameplayUI()
    {
        gameplayUI.SetActive(false);
    }

    public void ShowFinishUI()
    {
        finishUI.SetActive(true);
    }

    public void HideFinishUI()
    {
        finishUI.SetActive(false);
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
