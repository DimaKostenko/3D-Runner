using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    private List<MeshRenderer> meshes; 
    public float platformLenth;

    void Start()
    {
        Color platformColor = Random.ColorHSV();
        for(int i = 0; i < meshes.Count; i++){
            meshes[i].material.color = platformColor;
        }
        //GetComponent<Renderer>().material.color = Random.ColorHSV();
    }
}
