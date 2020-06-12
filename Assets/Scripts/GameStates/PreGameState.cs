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
        pregameScene.SetActive(true);
        preGameCanvasContainer.SetActive(true);
        Debug.Log("PreGameState-Enter");
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
