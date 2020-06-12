using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class GameState : State
{
    [SerializeField]
    private TrackManager trackManager;
    [SerializeField]
    private float timeFromCoin;
    [SerializeField]
    private float scoreFromCoin;
    public float time;
    private float savedTimeSetting;
    [SerializeField]
    private Text timerText; 
    public float score;
    [SerializeField]
    private Text scoreText; 
    public bool gameStarted = true;
    private Coroutine timerCoroutine;
    [SerializeField]
    private GameObject gameCanvasContainer;

    private void Awake() {
        savedTimeSetting = time;
    }

    public override void Enter(State from)
    {
        gameStarted = true;
        trackManager.Init();
        ResetTimer();
        ResetScore();
        timerCoroutine = StartCoroutine(StartTimer());
        gameCanvasContainer.SetActive(true);
        Debug.Log("GameState-Enter");
    }

	public override void Exit(State to)
    {
        gameCanvasContainer.SetActive(false);
        Debug.Log("GameState-Exit");
    }

    public void EndGame(){
        trackManager.StopSpawnCoinsCoroutine();
        gameStarted = false;
        StopTimer();
        manager.SwitchState("GameOver");
    }

    private void ResetScore(){
        score = 0f;
        scoreText.text = score.ToString();
    }

    private void ResetTimer(){
        time = savedTimeSetting;
    }

    public override string GetName()
    {
        return "Game";
    }
    
    float seconds, minutes, milliseconds;
    /*
    private void Awake() {
        savedTimeSetting = time;
        gameStarted = true;
        timerCoroutine = StartCoroutine(StartTimer());
    }*/

    public void IncreaseScore(){
        score += scoreFromCoin;
        scoreText.text = score.ToString();
    }

    public void AddTimeToTimer(){
        time += timeFromCoin;
        timerText.text = SetTimerFormat(time);
    }

    IEnumerator StartTimer()
    {   
        while(time > 0f){
            time -= Time.deltaTime;
            timerText.text = SetTimerFormat(time);
            yield return new WaitForFixedUpdate();
        }
    }

    private string SetTimerFormat(float time){
        float _time = time;
        minutes = (int)(_time / 60f);
        seconds = (int)(_time % 60f);
        milliseconds = (int)(_time * 100f) % 100;
        StringBuilder sb = new StringBuilder();
        sb.Append(minutes.ToString("00"));
        sb.Append(":");
        sb.Append(seconds.ToString("00"));
        sb.Append(":");
        sb.Append(milliseconds.ToString("00"));
        return sb.ToString();
    }

    public void StopTimer(){
        if(timerCoroutine != null){
            StopCoroutine(timerCoroutine);
        }
    }

    public void ContinueGame(){
        Debug.Log("ContinueGame");
    }
}
