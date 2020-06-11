using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierPunch : Barrier
{
    protected override void OnPlayerCollisionEnter(){
        GameStorage.Instance.GameManager.ContinueGame();
    }
}
