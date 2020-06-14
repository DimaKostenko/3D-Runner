using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    void OnTriggerEnter(Collider col){
        if (col.gameObject.tag == "Coin"){
            GameStorage.Instance.GameState.AddTimeToTimer(GameStorage.Instance.GameState.timeFromCoin);
            GameStorage.Instance.GameState.IncreaseScore(GameStorage.Instance.GameState.scoreFromCoin);
        }
        if (col.gameObject.tag == "Barrier"){
            col.gameObject.GetComponent<IBarrier>()?.OnPlayerCollisionEnter();
        }
    }
}
