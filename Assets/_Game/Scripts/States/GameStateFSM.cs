using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    
    public int MaxTurns = 10;
    [HideInInspector] public int TurnNumber = 0;
    public bool squidTeamWin { get; private set; }
    [HideInInspector] public bool gameOver = false;

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

    void LateUpdate()
    {
        base.Update();

        if(gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
