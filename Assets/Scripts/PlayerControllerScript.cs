using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    public bool isPlayer1 = true;
    private GameObject Ball;

    // Start is called before the first frame update
    void Start()
    {
        Ball = GameObject.FindGameObjectWithTag("Ball");
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayer1 && Input.GetKeyDown(KeyCode.W))
        {
            Ball.GetComponent<BallBehaviorScript>().testForHit(isPlayer1);
        }
        if (!isPlayer1 && Input.GetKeyDown(KeyCode.UpArrow))
        {
            Ball.GetComponent<BallBehaviorScript>().testForHit(isPlayer1);
        }
    }
}
