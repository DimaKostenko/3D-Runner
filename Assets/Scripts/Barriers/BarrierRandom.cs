using UnityEngine;

public class BarrierRandom : BarrierBase
{
    public override void OnPlayerCollisionEnter()
    {
        float fRand = Random.Range(0.0f,1.0f);
        if (fRand <= 0.5f){
            GameStorage.Instance.GameState.AddTimeToTimer(GameStorage.Instance.GameState.reducedTimeFromBarrier);
            Destroy(gameObject);
        } else {
            base.OnPlayerCollisionEnter();
        }
    }
}
