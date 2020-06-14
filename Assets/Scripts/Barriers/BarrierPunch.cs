using UnityEngine;

public class BarrierPunch : BarrierBase
{
    public override void OnPlayerCollisionEnter()
    {
        GameStorage.Instance.GameState.AddTimeToTimer(GameStorage.Instance.GameState.reducedTimeFromBarrier);
        Destroy(gameObject);
    }
}
