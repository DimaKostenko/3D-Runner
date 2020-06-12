using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlatformSetting", menuName = "Settings/PlatformBarriersSetting", order = 50)]
public class PlatformBarriersSetting : ScriptableObject {
    [SerializeField]
    private List<GameObject> barrierBlocks;
    [SerializeField]
    private float distanceBetweenBarrierBlocks;

    public List<GameObject> BarrierBlocksList
    {
        get
        {
            return barrierBlocks;
        }
    }

    public float DistanceBetweenBarrierBlocks
    {
        get
        {
            return distanceBetweenBarrierBlocks;
        }
    }
}
