using System.Collections.Generic;
using UnityEngine;

//Класс отвечает за переключение между состояниями игры (стартовое меню, ингейм и ендгейм)
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private State _startState = null;
    [SerializeField]
    private State[] _states = null;
    private Dictionary<string, State> _stateDict = new Dictionary<string, State>();
    private List<State> _stateStack = new List<State>();

    private void Awake() {
        for(int i = 0; i < _states.Length; ++i)
        {
            _states[i].manager = this;
            _stateDict.Add(_states[i].GetName(), _states[i]);
        }
        _stateStack.Add(_startState);
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

        _stateStack[_stateStack.Count - 1].Exit(state);
        state.Enter(_stateStack[_stateStack.Count - 1]);
        _stateStack.RemoveAt(_stateStack.Count - 1);
        _stateStack.Add(state);
    }

	public State FindState(string stateName)
	{
		State state;
		if (!_stateDict.TryGetValue(stateName, out state))
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
    public abstract string GetName();
    public abstract void Enter(State from);
    public abstract void Exit(State to);
}
