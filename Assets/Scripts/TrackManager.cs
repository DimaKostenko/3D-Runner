using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    [Header("Player forward move setting")]
    [SerializeField]
    private PlayerController _playerController = null;
    private Transform _playerTransform = null;
    [SerializeField]
    private float _playerStartSpeed = 0f;    
    [SerializeField]
    public float _playerMaxSpeed = 0f;
    [SerializeField]
    private float _playerSpeedModifier = 0f;
    private float _currentPlayerSpeed;
    [SerializeField]
    private LineDirection _playerDefaultLine = LineDirection.Middle;
    [SerializeField]
    private Vector3 _playerSpawnPosition = new Vector3(0f, 0f, 0f);
    [Header("Path settings")]
    [SerializeField]
    private float _platformCreateDistanceZ = 0f;
    [SerializeField]
    private float _platformDestroyDistanceZ = 0f;
    [SerializeField]
    private float _maxDistanceOfPlayerFromZeroPoint = 0f;
    [SerializeField]
    private GameObject _platformsArea = null;
    public float laneOffset;
    [SerializeField]
    private List<Platform> _platforms = null;
    private List<Platform> _createdPlatforms = new List<Platform>();
    Vector3 _frontPointOfFrontPlatform =  new Vector3 (0f, 0f, 0f); //координата, где заканчивается впереди стоящая платформа
    [Header("Coins spawn settings")]
    [SerializeField]
    private int _coinsCount = 0;
    [SerializeField]
    private float _distanceToCoinsSpawn = 0f;
    [SerializeField]
    private float _distanceBetweenCoins = 0f;
    [SerializeField]
    private GameObject _coinPrefab = null;
    [SerializeField]
    private Transform _coinsContainer = null;
    [SerializeField]
    private float _coinsSpawnDeltaTime = 0f;
    private List<GameObject> spawnedCoins = new List<GameObject>();
    private Coroutine spawnCoinsCoroutine;


    public void Init(){
        _currentPlayerSpeed = _playerStartSpeed;
        _frontPointOfFrontPlatform = new Vector3 (0f, 0f, 0f);
        SetPlayerSpawnPosition();
        RemoveAllPlatforms();
        RemoveAllCoins();
        AddPlatforms();
        spawnCoinsCoroutine = StartCoroutine(SpawnCoinsCoroutine());
    } 

    private void SetPlayerSpawnPosition(){
        SetPlayerToDefaultLine();
        if (_playerTransform == null){
            return;
        }
        _playerTransform.position = _playerSpawnPosition;
    }

    private void SetPlayerToDefaultLine(){
        if(_playerController == null){
            return;
        }
        _playerTransform = _playerController.transform;
        _playerController.PutPlayerToLine(_playerDefaultLine);
    }

    private void Update()
    {
        if(!GameStorage.Instance.GameState.gameStarted || _playerTransform == null){
            return;
        }
        if(_currentPlayerSpeed < _playerMaxSpeed){
            _currentPlayerSpeed += _playerSpeedModifier;
        }
        _playerTransform.Translate(Vector3.forward * _currentPlayerSpeed * Time.deltaTime);
        // Возвращаем объекты в начало координат
        if(_playerTransform.position.z > _maxDistanceOfPlayerFromZeroPoint){
            // игрок
            Vector3 deltaPos = new Vector3(0f, 0f, _maxDistanceOfPlayerFromZeroPoint);
            _playerTransform.position -= deltaPos;
            // платформы
            for(int i = 0; i < _createdPlatforms.Count; i++){
                _createdPlatforms[i].transform.position -= deltaPos;
            }
            // монеты
            for(int i = 0; i < spawnedCoins.Count; i++){
                if(spawnedCoins[i] == null){
                    spawnedCoins.RemoveAt(i);
                    i--;
                } else {
                    spawnedCoins[i].transform.position -= deltaPos;
                }
            }
            _frontPointOfFrontPlatform -= deltaPos;
            // Создаем новые платформы, удаляем ненужные
            AddPlatforms();
            RemovePlatforms();
        }
    }

    // Добавляет новые платформы, если в этом есть необходимость
    private void AddPlatforms(){ 
        Vector3 pointForCreatingNewPlatform =  new Vector3 (0f, 0f, 0f);
        while (_frontPointOfFrontPlatform.z < _platformCreateDistanceZ){
            int randomIndex = Random.Range(0, _platforms.Count);
            Platform newPlatform = _platforms[randomIndex];
            pointForCreatingNewPlatform = _frontPointOfFrontPlatform + new Vector3 (0f, 0f, newPlatform.PlatformLength / 2);
            Platform createdNewPlatform = Instantiate(newPlatform.gameObject, pointForCreatingNewPlatform, Quaternion.identity, _platformsArea.transform).GetComponent<Platform>();
            _createdPlatforms.Add(createdNewPlatform);
            _frontPointOfFrontPlatform += new Vector3 (0f, 0f, newPlatform.PlatformLength);
        }
    }

    // Удаляет платформы, которые игрок уже пробежал
    private void RemovePlatforms(){
        for (int i = 0; i < _createdPlatforms.Count; i++){
            float platformFrontPointZ = _createdPlatforms[i].transform.position.z + _createdPlatforms[i].PlatformLength / 2;
            if (platformFrontPointZ < _platformDestroyDistanceZ){
                Destroy(_createdPlatforms[i].gameObject);
                _createdPlatforms.RemoveAt(i);
                i--;
            }
        }
    }

    private void RemoveAllPlatforms(){
        for (int i = 0; i < _createdPlatforms.Count; i++){
            Destroy(_createdPlatforms[i].gameObject);
            _createdPlatforms.RemoveAt(i);
            i--;
        }
    }

    IEnumerator SpawnCoinsCoroutine()
    {   
        while(GameStorage.Instance.GameState.gameStarted){
            SpawnCoins();
            yield return new WaitForSeconds(_coinsSpawnDeltaTime);
        }
    }

    public void StopSpawnCoinsCoroutine(){
        StopCoroutine(spawnCoinsCoroutine);
    }

    private void RemoveAllCoins(){
        for (int i = 0; i < spawnedCoins.Count; i++){
            Destroy(spawnedCoins[i].gameObject);
            spawnedCoins.RemoveAt(i);
            i--;
        }
    }

    private void SpawnCoins(){
        if(_coinPrefab == null || _coinsContainer == null){
            return;
        }
        int randomLine = Random.Range(-1,1);
        Vector3 coinsSpawnPosition = new Vector3 (randomLine * laneOffset, 0f, _distanceToCoinsSpawn);
        for (int i = 0; i < _coinsCount; i++)
        {
            GameObject coin = Instantiate(_coinPrefab, _coinsContainer);
            coin.transform.localPosition = coinsSpawnPosition;
            spawnedCoins.Add(coin);
            coinsSpawnPosition += new Vector3 (0f, 0f, _distanceBetweenCoins);
        }
    }
}
