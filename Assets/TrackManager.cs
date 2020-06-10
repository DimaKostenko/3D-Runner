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
    private float maxDistanceOfPlayerFromZeroPoint;
    [SerializeField]
    private float platformCreateDistanceZ;
    [SerializeField]
    private float platformDestroyDistanceZ;
    [SerializeField]
    private GameObject platformsArea;
    private Transform playerTransform;
    public float laneOffset;
    private List<Platform> createdPlatforms = new List<Platform>();
    [SerializeField]
    private List<Platform> platforms;
    Vector3 frontPointOfFrontPlatform =  new Vector3 (0f, 0f, 0f); //координата, где заканчивается впереди стоящая платформа

    private void Start() {
        playerTransform = playerController.transform;
        Init();
    }

    private void Init(){
        AddPlatforms();
    } 

    void Update()
    {

        playerTransform.Translate(Vector3.forward * playerSpeed * Time.deltaTime);
        //Возвращаем объекты в начало координат
        if(playerTransform.position.z > maxDistanceOfPlayerFromZeroPoint){
            //игрок
            Vector3 deltaPos = GetMaxDistanceOfPlayerFromZeroPointVector();
            playerTransform.position -= deltaPos;
            //платформа
            for(int i = 0; i < createdPlatforms.Count; i++){
                createdPlatforms[i].transform.position -= deltaPos;
            }
            frontPointOfFrontPlatform -= deltaPos;
            AddPlatforms(); //создаем новые платформы
            RemovePlatforms(); // удаляем старые
        }
    }

    private void AddPlatforms(){
        Vector3 pointForCreatingNewPlatform =  new Vector3 (0f, 0f, 0f);
        while (frontPointOfFrontPlatform.z < platformCreateDistanceZ){
            int randomIndex = Random.Range(0, platforms.Count);
            Platform newPlatform = platforms[randomIndex];
            pointForCreatingNewPlatform = frontPointOfFrontPlatform + new Vector3 (0f, 0f, newPlatform.platformLenth / 2);
            Platform createdNewPlatform = Instantiate(newPlatform.gameObject, pointForCreatingNewPlatform, Quaternion.identity, platformsArea.transform).GetComponent<Platform>();
            createdPlatforms.Add(createdNewPlatform);
            frontPointOfFrontPlatform += new Vector3 (0f, 0f, newPlatform.platformLenth);
        }
    }

    private void RemovePlatforms(){
        for (int i = 0; i < createdPlatforms.Count; i++){
            float platformFrontPointZ = createdPlatforms[i].transform.position.z + createdPlatforms[i].platformLenth / 2;
            if (platformFrontPointZ < platformDestroyDistanceZ){
                Destroy(createdPlatforms[i].gameObject);
                createdPlatforms.RemoveAt(i);
                i--;
            }
        }
    }

    private Vector3 GetMaxDistanceOfPlayerFromZeroPointVector(){
        return new Vector3(0f, 0f, maxDistanceOfPlayerFromZeroPoint);
    }
}
