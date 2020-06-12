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
    [SerializeField]
    private Button mainMenuButton;
    [SerializeField]
    private Text bestScoreText;

    void OnEnable()
    {
        restartButton.onClick.AddListener(OnRestartButton);
        mainMenuButton.onClick.AddListener(OnMainMenuButton);
    }

    private void OnDisable() {
        restartButton.onClick.RemoveListener(OnRestartButton);
        mainMenuButton.onClick.RemoveListener(OnMainMenuButton);
    }

    private void OnRestartButton(){
        manager.SwitchState("Game");
    }

    private void OnMainMenuButton(){
        manager.SwitchState("PreGame");
    }

    public override void Enter(State from)
    {
        bestScoreText.text = GameStorage.Instance.DataManager.BestScore.ToString();
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
