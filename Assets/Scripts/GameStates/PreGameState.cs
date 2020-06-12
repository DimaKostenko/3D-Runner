using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreGameState : State
{
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private GameObject pregameScene;
    [SerializeField]
    private GameObject preGameCanvasContainer;
    [SerializeField]
    private TextMesh coinScoreCount;
    [SerializeField]
    private TextMesh coinAddTimeCount;
    [SerializeField]
    private TextMesh[] barriersReduceTimeCount;

    private void Start() {
        SetCoinDescriptionText();
        SetBarriersReduceTimeCountText();
    }

    void OnEnable()
    {
        startButton.onClick.AddListener(OnStartButton);
    }

    private void OnDisable() {
        startButton.onClick.RemoveListener(OnStartButton);
    }

    private void OnStartButton(){
        manager.SwitchState("Game");
    }

    public override void Enter(State from)
    {
        SetCoinDescriptionText();
        SetBarriersReduceTimeCountText();
        pregameScene.SetActive(true);
        preGameCanvasContainer.SetActive(true);
        Debug.Log("PreGameState-Enter");
    }

    private void SetBarriersReduceTimeCountText(){
        for (int i = 0; i < barriersReduceTimeCount.Length; i++)
        {
            barriersReduceTimeCount[i].text = GameStorage.Instance.GameState.SetTimerFormat(GameStorage.Instance.GameState.reducedTimeFromBarrier);
        }
    }

    private void SetCoinDescriptionText(){
        coinScoreCount.text = GameStorage.Instance.GameState.scoreFromCoin.ToString();
        coinAddTimeCount.text = GameStorage.Instance.GameState.SetTimerFormat(GameStorage.Instance.GameState.timeFromCoin);
    }

	public override void Exit(State to)
    {
        pregameScene.SetActive(false);
        preGameCanvasContainer.SetActive(false);
        Debug.Log("PreGameState-Exit");
    }

    public override string GetName()
    {
        return "PreGame";
    }
}
