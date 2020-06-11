using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour, IBarrier
{
    public virtual void OnPlayerCollisionEnter()
    {
        GameStorage.Instance.GameManager.EndGame();
    }
}
