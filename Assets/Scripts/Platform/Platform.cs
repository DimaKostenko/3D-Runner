using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    private List<MeshRenderer> _meshes = null; 
    [SerializeField]
    private float _platformLength = 0f;
    [SerializeField]
    private Transform _barriersContainer = null;
    [SerializeField]
    private PlatformBarriersSetting _platformBarriersSetting = null;
    public float PlatformLength { get { return _platformLength; } }

    private void Start()
    {
        SetPlatformColor();
        SpawnBarriers();
    }

    // рандомный цвет для платформы
    private void SetPlatformColor(){
        if(_meshes == null){
            return;
        }
        Color platformColor = Random.ColorHSV();
        for(int i = 0; i < _meshes.Count; i++){
            _meshes[i].material.color = platformColor;
        }
    }

    // создаем препятствия
    private void SpawnBarriers(){
        if(_barriersContainer == null || _platformBarriersSetting == null){
            return;
        }
        Vector3 spawnBarrierPoint = new Vector3(0f, 0f, -PlatformLength / 2 + _platformBarriersSetting.DistanceBetweenBarrierBlocks); 
        while (spawnBarrierPoint.z < PlatformLength / 2) //Спавним препятствия в пределах платформы
        {
            int randomIndex = Random.Range(0, _platformBarriersSetting.BarrierBlocksList.Count);
            GameObject barrier = Instantiate(_platformBarriersSetting.BarrierBlocksList[randomIndex], _barriersContainer);
            barrier.transform.localPosition = spawnBarrierPoint;
            spawnBarrierPoint += new Vector3(0f, 0f, _platformBarriersSetting.DistanceBetweenBarrierBlocks);
        }
    }
}
