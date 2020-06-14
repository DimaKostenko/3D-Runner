using UnityEngine;
using UnityEngine.UI;

public class PreGameState : State
{
    [SerializeField]
    private Button _startButton = null;
    [SerializeField]
    private GameObject _pregameScene = null;
    [SerializeField]
    private GameObject _preGameCanvasContainer = null;
    [SerializeField]
    private TextMesh _coinScoreCount = null;
    [SerializeField]
    private TextMesh _coinAddTimeCount = null;
    [SerializeField]
    private TextMesh[] _barriersReduceTimeCount = null;

    public override string GetName()
    {
        return "PreGame";
    }

    public override void Exit(State to)
    {
        _pregameScene.SetActive(false);
        _preGameCanvasContainer.SetActive(false);
        Debug.Log("PreGameState-Exit");
    }

    public override void Enter(State from)
    {
        SetCoinDescriptionText();
        SetBarriersReduceTimeCountText();
        _pregameScene.SetActive(true);
        _preGameCanvasContainer.SetActive(true);
        Debug.Log("PreGameState-Enter");
    }

    private void Start() {
        SetCoinDescriptionText();
        SetBarriersReduceTimeCountText();
    }

    void OnEnable()
    {
        _startButton.onClick.AddListener(OnStartButton);
    }

    private void OnDisable() {
        _startButton.onClick.RemoveListener(OnStartButton);
    }

    private void OnStartButton(){
        manager.SwitchState("Game");
    }

    private void SetBarriersReduceTimeCountText(){
        float time = Mathf.Abs(GameStorage.Instance.GameState.reducedTimeFromBarrier);
        for (int i = 0; i < _barriersReduceTimeCount.Length; i++)
        {
            _barriersReduceTimeCount[i].text = GameStorage.Instance.GameState.SetTimerFormat(time);
        }
    }

    private void SetCoinDescriptionText(){
        _coinScoreCount.text = GameStorage.Instance.GameState.scoreFromCoin.ToString();
        _coinAddTimeCount.text = GameStorage.Instance.GameState.SetTimerFormat(GameStorage.Instance.GameState.timeFromCoin);
    }
}
