using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : State
{
    public override void Enter(State from)
    {
        Debug.Log("GameOverState-Enter");
    }

	public override void Exit(State to)
    {
        Debug.Log("GameOverState-Exit");
    }

    public override string GetName()
    {
        return "GameOver";
    }
}
