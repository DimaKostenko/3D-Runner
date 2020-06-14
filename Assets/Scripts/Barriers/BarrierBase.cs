using UnityEngine;

public class BarrierBase : MonoBehaviour, IBarrier
{
    public virtual void OnPlayerCollisionEnter()
    {
        GameStorage.Instance.GameState.EndGame();
    }
}
