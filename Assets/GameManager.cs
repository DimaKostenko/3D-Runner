using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void EndGame(){
        Debug.Log("EndGame");
    }

    public void ContinueGame(){
        Debug.Log("ContinueGame");
    }

    public void CoinCollected(){
        Debug.Log("CoinCollected");
    }
}
