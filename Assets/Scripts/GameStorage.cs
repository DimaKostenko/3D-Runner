using UnityEngine;

public class GameStorage : MonoBehaviour
{
    public static GameStorage Instance { get; private set; }
    [SerializeField]
    private GameState _gameState = null;
    [SerializeField]
    private DataManager _dataManager = null;
    public GameState GameState { get { return _gameState; } }
    public DataManager DataManager { get { return _dataManager; } }

    void Awake() {
        if (Instance == null) {
            Instance = this;  
        } else { 
            Destroy(gameObject); 
        }

        if(_dataManager == null){
            _dataManager = new DataManager();
        }

        DontDestroyOnLoad(gameObject);
    }
}
