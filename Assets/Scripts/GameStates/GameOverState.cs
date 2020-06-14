using UnityEngine;
using UnityEngine.UI;

public class GameOverState : State
{
    [SerializeField]
    private GameObject _gameOverCanvasContainer = null;
    [SerializeField]
    private Text _scoreText = null; 
    [SerializeField]
    private Button _restartButton = null;
    [SerializeField]
    private Button _mainMenuButton = null;
    [SerializeField]
    private Text _bestScoreText = null;

    public override string GetName()
    {
        return "GameOver";
    }

    public override void Enter(State from)
    {
        _bestScoreText.text = GameStorage.Instance.DataManager.BestScore.ToString();
        _scoreText.text = GameStorage.Instance.GameState.score.ToString();
        _gameOverCanvasContainer.SetActive(true);
        Debug.Log("GameOverState-Enter");
    }

	public override void Exit(State to)
    {
        _gameOverCanvasContainer.SetActive(false);
        Debug.Log("GameOverState-Exit");
    }

    void OnEnable()
    {
        _restartButton.onClick.AddListener(OnRestartButton);
        _mainMenuButton.onClick.AddListener(OnMainMenuButton);
    }

    private void OnDisable() {
        _restartButton.onClick.RemoveListener(OnRestartButton);
        _mainMenuButton.onClick.RemoveListener(OnMainMenuButton);
    }

    private void OnRestartButton(){
        manager.SwitchState("Game");
    }

    private void OnMainMenuButton(){
        manager.SwitchState("PreGame");
    }
}
