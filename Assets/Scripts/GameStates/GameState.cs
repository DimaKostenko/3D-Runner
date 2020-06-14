using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class GameState : State
{
    [SerializeField]
    private TrackManager _trackManager = null;
    public float timeFromCoin = 0f;
    public float scoreFromCoin = 0f;
    public float time = 0f;
    private float _savedTimeSetting = 0f;
    [SerializeField]
    private Text _timerText = null; 
    public float score = 0f;
    [SerializeField]
    private Text _scoreText = null; 
    public float bestScore = 0f;
    [SerializeField]
    private Text _bestScoreText = null; 
    public bool gameStarted = true;
    private Coroutine _timerCoroutine;
    [SerializeField]
    private GameObject _gameCanvasContainer = null;
    public float reducedTimeFromBarrier = 0f;
    private float _seconds, _minutes, _milliseconds;

    public override string GetName()
    {
        return "Game";
    }

    public override void Enter(State from)
    {
        gameStarted = true;
        _trackManager.Init();
        ResetTimer();
        ResetScore();
        bestScore = GameStorage.Instance.DataManager.BestScore;
        SetBestScoreText();
        _timerCoroutine = StartCoroutine(StartTimer());
        _gameCanvasContainer.SetActive(true);
        Debug.Log("GameState-Enter");
    }

    public override void Exit(State to)
    {
        _gameCanvasContainer.SetActive(false);
        Debug.Log("GameState-Exit");
    }

    public void EndGame(){
        _trackManager.StopSpawnCoinsCoroutine();
        gameStarted = false;
        StopTimer();
        SaveBestScore();
        manager.SwitchState("GameOver");
    }

    private void ResetScore(){
        score = 0f;
        _scoreText.text = score.ToString();
    }

    public void IncreaseScore(float count){
        score += count;
        _scoreText.text = score.ToString();
        SetBestScore();
    }

    private void SetBestScore(){
        if(score >= bestScore){
            bestScore = score;
            SetBestScoreText();
        }
    }

    private void SetBestScoreText(){
        _bestScoreText.text = bestScore.ToString();
    }

    private void SaveBestScore(){
        if(bestScore > GameStorage.Instance.DataManager.BestScore){
            GameStorage.Instance.DataManager.BestScore = bestScore;
        }
    }

    public void AddTimeToTimer(float t){
        time += t;
        _timerText.text = SetTimerFormat(time);
    }

    IEnumerator StartTimer()
    {   
        while(time > 0f){
            time -= Time.deltaTime;
            _timerText.text = SetTimerFormat(time);
            yield return new WaitForFixedUpdate();
        }
        EndGame();
    }

    public void StopTimer(){
        if(_timerCoroutine != null){
            StopCoroutine(_timerCoroutine);
        }
    }

    private void ResetTimer(){
        if(_savedTimeSetting == 0f){
            _savedTimeSetting = time;
        }
        time = _savedTimeSetting;
    }

    public string SetTimerFormat(float time){
        float _time = time;
        _minutes = (int)(_time / 60f);
        _seconds = (int)(_time % 60f);
        _milliseconds = (int)(_time * 100f) % 100;
        StringBuilder sb = new StringBuilder();
        sb.Append(_minutes.ToString("00"));
        sb.Append(":");
        sb.Append(_seconds.ToString("00"));
        sb.Append(":");
        sb.Append(_milliseconds.ToString("00"));
        return sb.ToString();
    }
}
