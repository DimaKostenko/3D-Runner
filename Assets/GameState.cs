using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class GameState : State
{
    [SerializeField]
    private float timeFromCoin;
    [SerializeField]
    private float scoreFromCoin;
    public float time;
    [SerializeField]
    private Text timerText; 
    public float score;
    [SerializeField]
    private Text scoreText; 

    public override void Enter(State from)
    {
        Debug.Log("GameState-Enter");
    }

	public override void Exit(State to)
    {
        Debug.Log("GameState-Exit");
    }

    public override string GetName()
    {
        return "Game";
    }

    float seconds, minutes, milliseconds;
    private void Start() {
        StartCoroutine(StartTimer());
        //timerText.text = time.ToString();
    }

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
        StopCoroutine(StartTimer());
    }

    public void EndGame(){
        manager.SwitchState("GameOver");
    }

    public void ContinueGame(){
        Debug.Log("ContinueGame");
    }

    public void CoinCollected(){
        Debug.Log("CoinCollected");
    }
}
