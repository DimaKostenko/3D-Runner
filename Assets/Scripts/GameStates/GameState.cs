using System.Collections;
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
    public float bestScore;
    [SerializeField]
    private Text bestScoreText; 
    public bool gameStarted = true;
    private Coroutine timerCoroutine;
    [SerializeField]
    private GameObject gameCanvasContainer;
    [SerializeField]
    private float reducedTimeFromBarrier;

    private void Awake() {
        savedTimeSetting = time;
    }

    public override void Enter(State from)
    {
        gameStarted = true;
        trackManager.Init();
        ResetTimer();
        ResetScore();
        SetBestScore();
        timerCoroutine = StartCoroutine(StartTimer());
        gameCanvasContainer.SetActive(true);
        Debug.Log("GameState-Enter");
    }

    private void SetBestScore(){
        bestScore = GameStorage.Instance.DataManager.BestScore;
        bestScoreText.text = bestScore.ToString();
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
        CheckAndSaveBestScore();
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

    public void IncreaseScore(){
        score += scoreFromCoin;
        scoreText.text = score.ToString();
        CheckAndSetBestScoreText();
    }

    private void CheckAndSetBestScoreText(){
        if(score >= bestScore){
            bestScore = score;
            bestScoreText.text = bestScore.ToString();
        }
    }

    private void CheckAndSaveBestScore(){
        if(bestScore > GameStorage.Instance.DataManager.BestScore){
            GameStorage.Instance.DataManager.BestScore = bestScore;
        }
    }

    public void AddTimeToTimer(){
        time += timeFromCoin;
        timerText.text = SetTimerFormat(time);
    }

    public void ReduceTime(){
        time -= reducedTimeFromBarrier;
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
