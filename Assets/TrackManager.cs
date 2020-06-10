using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private float playerSpeed;
    [SerializeField]
    private float platformLength;
    [SerializeField]
    private GameObject platformsArea;
    [SerializeField]
    private GameObject platform;
    private Transform playerTransform;
    public float laneOffset;
    private List<GameObject> platforms = new List<GameObject>();
    [SerializeField]
    private int platformsCount;

    private void Start() {
        playerTransform = playerController.transform;
        Init();
    }

    private void Init(){
        //Создаем платформы
        Vector3 platformPos = platform.transform.position;
        Vector3 deltaPos = GetAreaLengthVector();
        for (int i = 0; i < platformsCount; i++){
            GameObject newPlatform = Instantiate(platform, platformPos, Quaternion.identity, platformsArea.transform);
            platforms.Add(newPlatform);
            platformPos += deltaPos;
        }
    } 

    void Update()
    {
        playerTransform.Translate(Vector3.forward * playerSpeed * Time.deltaTime);
        //Возвращаем объекты в начало координат
        if(playerTransform.position.z > platformLength){
            //игрок
            Vector3 deltaPos = GetAreaLengthVector();
            playerTransform.position -= deltaPos;
            //платформа
            for(int i = 0; i < platforms.Count; i++){
                platforms[i].transform.position -= deltaPos;
            }
            //создаем новую платформу, удаляем старую
            Vector3 newPlatformPos = platforms[platforms.Count - 1].transform.position + deltaPos;
            GameObject newPlatform = Instantiate(platform, newPlatformPos, Quaternion.identity, platformsArea.transform);
            platforms.Add(newPlatform);
            Destroy(platforms[0]);
            platforms.RemoveAt(0);
        }
    }

    private Vector3 GetAreaLengthVector(){
        return new Vector3(0f, 0f, platformLength);
    }
}
