using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlatformSetting", menuName = "Settings/PlatformBarriersSetting", order = 50)]
public class PlatformBarriersSetting : ScriptableObject {
    [SerializeField]
    private List<GameObject> _barrierBlocks = null;
    [SerializeField]
    private float _distanceBetweenBarrierBlocks = 0f;
    public List<GameObject> BarrierBlocksList { get { return _barrierBlocks; } }
    public float DistanceBetweenBarrierBlocks { get { return _distanceBetweenBarrierBlocks; } }
}
