using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverState : State
{
    [SerializeField]
    private GameObject gameOverCanvasContainer;
    [SerializeField]
    private Text scoreText; 
    [SerializeField]
    private Button restartButton;

    void OnEnable()
    {
        restartButton.onClick.AddListener(OnRestartButton);
    }

    private void OnDisable() {
        restartButton.onClick.RemoveListener(OnRestartButton);
    }

    private void OnRestartButton(){
        manager.SwitchState("Game");
    }

    public override void Enter(State from)
    {
        scoreText.text = GameStorage.Instance.GameState.score.ToString();
        gameOverCanvasContainer.SetActive(true);
        Debug.Log("GameOverState-Enter");
    }

	public override void Exit(State to)
    {
        gameOverCanvasContainer.SetActive(false);
        Debug.Log("GameOverState-Exit");
    }

    public override string GetName()
    {
        return "GameOver";
    }
}
