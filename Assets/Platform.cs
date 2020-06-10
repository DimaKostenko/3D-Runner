using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    private List<MeshRenderer> meshes; 
    public float platformLenth;
    [SerializeField]
    private Transform barriers;
    [SerializeField]
    private List<GameObject> barrierBlocks;
    [SerializeField]
    private float distanceBetweenBarrierBlocks;

    void Start()
    {
        // рандомный цвет для элементов платформы
        Color platformColor = Random.ColorHSV();
        for(int i = 0; i < meshes.Count; i++){
            meshes[i].material.color = platformColor;
        }
        // создаем препятствия
        Vector3 spawnBarrierPoint = new Vector3(0f, 0f, -platformLenth/2 + distanceBetweenBarrierBlocks); 
        while (spawnBarrierPoint.z < platformLenth/2) //Спавним барриер если его z координата меньше чем координата где платформа заканчивается
        {
            int randomIndex = Random.Range(0, barrierBlocks.Count);
            GameObject barrier = Instantiate(barrierBlocks[randomIndex], barriers);
            barrier.transform.localPosition = spawnBarrierPoint;
            spawnBarrierPoint += new Vector3(0f, 0f, distanceBetweenBarrierBlocks);
        }

    }
}
