using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStorage : MonoBehaviour
{
    public static GameStorage Instance { get; private set; } // static singleton
    [SerializeField]
    private GameState gameState;
    [SerializeField]
    private DataManager dataManager;

    // Start is called before the first frame update
    void Awake() {
        if (Instance == null) {
            Instance = this;  
        } else { 
            Destroy(gameObject); 
        }

        if(dataManager == null){
            dataManager = new DataManager();
        }
        DontDestroyOnLoad(gameObject);
    }
    
    public GameState GameState 
    {
        get
        {
            return gameState;
        }
    }

    public DataManager DataManager 
    {
        get
        {
            return dataManager;
        }
    }
}
