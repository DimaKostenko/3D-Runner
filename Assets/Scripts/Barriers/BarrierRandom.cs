using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierRandom : Barrier
{
    public override void OnPlayerCollisionEnter()
    {
        float fRand = Random.Range(0.0f,1.0f);
        if (fRand <= 0.5f){
            GameStorage.Instance.GameState.ContinueGame();
            GameStorage.Instance.GameState.ReduceTime();
            Destroy(gameObject);
        } else {
            base.OnPlayerCollisionEnter();
        }
    }
}
