using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatePrinter : MonoBehaviour
{
    public static StatePrinter current;
    [SerializeField] Text stateText;

    void Start()
    {
        current = this;
    }

    public void printState(string state)
    {
        stateText.text = state;
    }
}
