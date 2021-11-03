using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateFSM : StateMachineMB
{
    // TODO: Serialized reference to MatchController. Sets team colors before the match and # turns. 

    public GameStateStart StartState { get; private set; }
    public GameStateSquidSelect SquidSelectState { get; private set; }
    public GameStateSquidAction SquidActionState { get; private set; }
    public GameStateKidSelect KidSelectState { get; private set; }
    public GameStateKidAction KidActionState { get; private set; }
    public GameStateFinish FinishState { get; private set; }
    public GameStateSquidWin SquidWinState { get; private set; }
    public GameStateKidWin KidWinState { get; private set; }

    public int TurnNumber { get; private set; }
    public int MaxTurns = 10;

    private void Awake()
    {
        // create my states
        StartState = new GameStateStart(this);
        SquidSelectState = new GameStateSquidSelect(this);
        SquidActionState = new GameStateSquidAction(this);
        KidSelectState = new GameStateKidSelect(this);
        KidActionState = new GameStateKidAction(this);
        FinishState = new GameStateFinish(this);
        SquidWinState = new GameStateSquidWin(this);
        KidWinState = new GameStateKidWin(this);
    }

    private void Start()
    {
        ChangeState(StartState);
    }
}
