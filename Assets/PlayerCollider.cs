using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    void OnTriggerEnter(Collider col){
        if (col.gameObject.tag == "Coin"){
            GameStorage.Instance.GameManager.CoinCollected();
        }
        if (col.gameObject.tag == "Barrier"){
            col.gameObject.GetComponent<IBarrier>().OnPlayerCollisionEnter();
        }
    }
}
