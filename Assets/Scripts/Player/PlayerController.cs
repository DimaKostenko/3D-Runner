using UnityEngine;

public enum LineDirection
    {
        Left = -1,
        Middle = 0,
        Right = 1
    }

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private TrackManager _trackManager = null;
    [SerializeField]
    private float _laneChangeSpeed = 1.0f;
    private int _currentLane = 1;
    [SerializeField]
    private GameObject _characterCollider = null;
    private Vector3 _targetPosition;

    void Update()
    {
        if(!GameStorage.Instance.GameState.gameStarted){
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeLane(LineDirection.Left);
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeLane(LineDirection.Right);
        }
        _characterCollider.transform.localPosition = Vector3.MoveTowards(_characterCollider.transform.localPosition, _targetPosition, _laneChangeSpeed * Time.deltaTime);
    }

    public void PutPlayerToLine(LineDirection line){
        int direction = (int)line;
        _currentLane = direction + 1;
        _characterCollider.transform.localPosition = _targetPosition = new Vector3((direction) * _trackManager.laneOffset, 0f, 0f);
    }

    public void ChangeLane(LineDirection line)
    {
        int direction = (int)line;
        int targetLane = _currentLane + direction;
        if (targetLane < 0 || targetLane > 2)
            return;

        _currentLane = targetLane;
        _targetPosition = new Vector3((_currentLane - 1) * _trackManager.laneOffset, 0f, 0f);
    }
}
