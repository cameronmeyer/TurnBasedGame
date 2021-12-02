using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatePrinter : MonoBehaviour
{
    public static StatePrinter current;
    public UserInterface ui;
    [SerializeField] Text stateText;
    [SerializeField] Text turnText;
    [SerializeField] Text actionText;
    [SerializeField] Text winnerText;
    [SerializeField] Text tipText;

    void Start()
    {
        current = this;
    }

    public void printState(string state)
    {
        stateText.text = state;
    }

    public void printTurn(int turn, int maxTurns)
    {
        turnText.text = "Current Turn: " + turn + " / " + maxTurns;
    }

    public void printAction(int action, int maxActions)
    {
        actionText.text = "Actions Left: " + action + " / " + maxActions;
    }

    public void printWinner(bool team)
    {
        if (team)
        {
            winnerText.text = "Squid Team Win! ";
        }
        else
        {
            winnerText.text = "Kid Team Win! ";
        }
    }

    public void printTip()
    {
        tipText.text = "Press [ESC] to return to the menu...";
    }
}
