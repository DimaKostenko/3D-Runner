using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private float bestScore;
    private readonly string bestScoreKey = "bestScore";

    public float BestScore
    {
        get
        {
            return GetDataKey(bestScoreKey, bestScore);
        }
 
        set
        {
            SaveDataKey(bestScoreKey, value);
        }
    }

    private void SaveDataKey(string key, float value){
        PlayerPrefs.SetFloat(key, value);
    }

    private void SaveDataKey(string key, int value){
        PlayerPrefs.SetInt(key, value);
    }

    private void SaveDataKey(string key, string value){
        PlayerPrefs.SetString(key, value);
    }

    private float GetDataKey(string key, float value){
        return PlayerPrefs.GetFloat(key, value);
    }

    private int GetDataKey(string key, int value){
        return PlayerPrefs.GetInt(key, value);
    }

    private string GetDataKey(string key, string value){
        return PlayerPrefs.GetString(key, value);
    }
}
