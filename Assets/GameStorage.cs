using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStorage : MonoBehaviour
{
    public static GameStorage Instance { get; private set; } // static singleton
    [SerializeField]
    private GameManager gameManager;

    // Start is called before the first frame update
    void Awake() {
        if (Instance == null) {
            Instance = this;  
        } else { 
            Destroy(gameObject); 
        }
        DontDestroyOnLoad(gameObject);
    }

    
    public GameManager GameManager 
    {
        get
        {
            return gameManager;
        }
    }
}
