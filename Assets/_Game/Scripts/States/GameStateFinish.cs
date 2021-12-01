using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateFinish : State
{
    GameStateFSM _stateMachine;
    bool squidTeamWin = false;

    public GameStateFinish(GameStateFSM stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        _stateMachine.gameOver = true;
        StatePrinter.current.printState("STATE: Finish Game");
        Debug.Log("STATE: Finish Game");

        int squidSpaces = 0;
        int kidSpaces = 0;

        foreach (GridSpace gs in BoardStatus.current.board)
        {
            if (gs.tile != null)
            {
                if (gs.tile.paint == TilePaint.SQUID_PAINT)
                {
                    squidSpaces++;
                }
                else if (gs.tile.paint == TilePaint.KID_PAINT)
                {
                    kidSpaces++;
                }
            }
        }

        if (squidSpaces > kidSpaces)
        {
            squidTeamWin = true;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (squidTeamWin)
        {
            _stateMachine.ChangeState(_stateMachine.SquidWinState);
        }
        else
        {
            _stateMachine.ChangeState(_stateMachine.KidWinState);
        }
    }
}
