using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierPunch : Barrier
{
    public override void OnPlayerCollisionEnter()
    {
        GameStorage.Instance.GameState.ContinueGame();
    }
}
