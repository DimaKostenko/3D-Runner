using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private State startState;
    private State currentState;
    public static GameManager Instance { get; private set; }
    private GameManager gameManager;
    [SerializeField]
    private State[] states;
    private Dictionary<string, State> stateDict = new Dictionary<string, State>();
    private List<State> stateStack = new List<State>();

    private void Awake() {
        for(int i = 0; i < states.Length; ++i)
        {
            states[i].manager = this;
            stateDict.Add(states[i].GetName(), states[i]);
        }
        stateStack.Add(startState);
        DontDestroyOnLoad(gameObject);
    }
    
    public void SwitchState(string newState)
    {
        State state = FindState(newState);
        if (state == null)
        {
            Debug.LogError("Can't find the state named " + newState);
            return;
        }

        stateStack[stateStack.Count - 1].Exit(state);
        state.Enter(stateStack[stateStack.Count - 1]);
        stateStack.RemoveAt(stateStack.Count - 1);
        stateStack.Add(state);
    }

	public State FindState(string stateName)
	{
		State state;
		if (!stateDict.TryGetValue(stateName, out state))
		{
			return null;
		}

		return state;
	}

}

public abstract class State : MonoBehaviour
{
    [HideInInspector]
    public GameManager manager;
    public abstract void Enter(State from);
    public abstract void Exit(State to);
    public abstract string GetName();
}
