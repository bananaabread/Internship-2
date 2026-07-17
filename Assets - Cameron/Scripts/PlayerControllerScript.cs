using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerScript : MonoBehaviour
{
    public bool isPlayer1 = true;
    private GameObject Ball;

    private bool isOnCooldown = false;
    public float cooldownTime = 1f;

    public bool playing = true;
    public bool is1PlayerScene;

    public bool canRestart = false;

    // Start is called before the first frame update
    void Start()
    {
        Ball = GameObject.FindGameObjectWithTag("Ball");
        Debug.Log("Got the ball");
    }

    // Update is called once per frame
    void Update()
    {
        if (playing)
        {
            if (isPlayer1 && Input.GetKeyDown(KeyCode.D) && !isOnCooldown)
            {
                Ball.GetComponent<BallBehaviorScript>().testForHit(isPlayer1, 1);
                isOnCooldown = true;
                StartCoroutine(cooldown());

            }
            if (!isPlayer1 && Input.GetKeyDown(KeyCode.LeftArrow) && !isOnCooldown)
            {
                Ball.GetComponent<BallBehaviorScript>().testForHit(isPlayer1, 1);
                isOnCooldown = true;
                StartCoroutine(cooldown());
            }
            if (isPlayer1 && Input.GetKeyDown(KeyCode.S) && !isOnCooldown)
            {
                Ball.GetComponent<BallBehaviorScript>().testForHit(isPlayer1, 2);
                isOnCooldown = true;
                StartCoroutine(cooldown());

            }
            if (!isPlayer1 && Input.GetKeyDown(KeyCode.DownArrow) && !isOnCooldown)
            {
                Ball.GetComponent<BallBehaviorScript>().testForHit(isPlayer1, 2);
                isOnCooldown = true;
                StartCoroutine(cooldown());
            }
            if (isPlayer1 && Input.GetKeyDown(KeyCode.W) && !isOnCooldown)
            {
                Ball.GetComponent<BallBehaviorScript>().testForHit(isPlayer1, 3);
                isOnCooldown = true;
                StartCoroutine(cooldown());

            }
            if (!isPlayer1 && Input.GetKeyDown(KeyCode.UpArrow) && !isOnCooldown)
            {
                Ball.GetComponent<BallBehaviorScript>().testForHit(isPlayer1, 3);
                isOnCooldown = true;
                StartCoroutine(cooldown());
            }
        }
        if (!playing && canRestart)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (is1PlayerScene)
                {
                    SceneManager.LoadScene("1PlayerScene");
                }
                if (!is1PlayerScene)
                {
                    SceneManager.LoadScene("2PlayerScene");
                }
            }
        }
    }
    public IEnumerator cooldown()
    {
        yield return new WaitForSeconds(0.5f);
        isOnCooldown = false;
    }
}
