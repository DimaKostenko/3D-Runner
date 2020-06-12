using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;
    public float playerSpeed;
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
    [Header("Coins spawn settings")]
    [SerializeField]
    private int coinsCount;
    [SerializeField]
    private float distanceToCoinsSpawn;
    [SerializeField]
    private float distanceBetweenCoins;
    [SerializeField]
    private GameObject coinPrefab;
    [SerializeField]
    private Transform coinsContainer;
    [SerializeField]
    private float coinsSpawnDeltaTime;
    private List<GameObject> spawnedCoins = new List<GameObject>();
    [SerializeField]
    private bool gameStarted;
    private Coroutine spawnCoinsCoroutine;
    [SerializeField]
    private Vector3 playerSpawnPosition;

    private void Start() {
        playerTransform = playerController.transform;
        Init();
    }

    public void Init(){
        playerTransform.position = playerSpawnPosition;
        RemoveAllObjects();
        frontPointOfFrontPlatform = new Vector3 (0f, 0f, 0f);
        AddPlatforms();
        spawnCoinsCoroutine = StartCoroutine(SpawnCoinsCoroutine());
    } 

    private void RemoveAllObjects(){
        //удаляем все платформы
        for (int i = 0; i < createdPlatforms.Count; i++){
            Destroy(createdPlatforms[i].gameObject);
            createdPlatforms.RemoveAt(i);
            i--;
        }
        //удаляем все монеты
        for (int i = 0; i < spawnedCoins.Count; i++){
            Destroy(spawnedCoins[i].gameObject);
            spawnedCoins.RemoveAt(i);
            i--;
        }
    }

    IEnumerator SpawnCoinsCoroutine()
    {   
        while(GameStorage.Instance.GameState.gameStarted){
            SpawnCoins();
            yield return new WaitForSeconds(coinsSpawnDeltaTime);
        }
    }

    public void StopSpawnCoinsCoroutine(){
        StopCoroutine(spawnCoinsCoroutine);
    }

    private void SpawnCoins(){
        int randomLine = Random.Range(-1,1);
        Vector3 coinsSpawnPosition = new Vector3 (randomLine * laneOffset, 0f, distanceToCoinsSpawn);
        for (int i = 0; i < coinsCount; i++)
        {
            GameObject coin = Instantiate(coinPrefab, coinsContainer);
            coin.transform.localPosition = coinsSpawnPosition;
            spawnedCoins.Add(coin);
            coinsSpawnPosition += new Vector3 (0f, 0f, distanceBetweenCoins);
        }
    }

    void Update()
    {
        if(!GameStorage.Instance.GameState.gameStarted){
            return;
        }
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
            //монеты
            for(int i = 0; i < spawnedCoins.Count; i++){
                if(spawnedCoins[i] == null){
                    spawnedCoins.RemoveAt(i);
                    i--;
                } else {
                    spawnedCoins[i].transform.position -= deltaPos;
                }
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
