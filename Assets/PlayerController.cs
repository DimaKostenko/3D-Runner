using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private TrackManager trackManager;
    [SerializeField]
    public float laneChangeSpeed = 1.0f;
    private int currentLane = 1;
    [SerializeField]
    private GameObject characterCollider;
    private Vector3 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeLane(-1);
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeLane(1);
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            //Jump();
        }
		else if (Input.GetKeyDown(KeyCode.DownArrow))
		{
            /*
			if(!m_Sliding)
				Slide();
            */
		}
        characterCollider.transform.localPosition = Vector3.MoveTowards(characterCollider.transform.localPosition, targetPosition, laneChangeSpeed * Time.deltaTime);
    }

    public void ChangeLane(int direction)
    {
        int targetLane = currentLane + direction;

        if (targetLane < 0 || targetLane > 2)
            return;

        currentLane = targetLane;
        targetPosition = new Vector3((currentLane - 1) * trackManager.laneOffset, 0f, 0f);
    }
}
