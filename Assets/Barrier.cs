using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    // Start is called before the first frame update
    protected virtual void OnPlayerCollisionEnter()
    {
        GameStorage.Instance.GameManager.EndGame();
    }

    void OnCollisionEnter(Collision col){
        if (col.gameObject.tag == "Player"){
            OnPlayerCollisionEnter();
        }
 }
}
