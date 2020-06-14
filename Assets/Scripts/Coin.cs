using UnityEngine;

public class Coin : MonoBehaviour
{
    void OnTriggerEnter(Collider col){
        if (col.gameObject.tag == "Player"){
            Destroy(gameObject);
        }
    }
}
