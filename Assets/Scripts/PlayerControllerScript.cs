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

    public AbilityManager abilityManager;

    public Animator P1Anim;
    public Animator P2Anim;


    // Start is called before the first frame update
    void Start()
    {
        Ball = GameObject.FindGameObjectWithTag("Ball");
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
                P1Anim.SetTrigger("punch");


            }
            if (!isPlayer1 && Input.GetKeyDown(KeyCode.LeftArrow) && !isOnCooldown)
            {
                P2Anim.ResetTrigger("punchup");
                P2Anim.SetTrigger("punch");
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
                P2Anim.ResetTrigger("punchup");
                P2Anim.SetTrigger("punch");

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
                P2Anim.ResetTrigger("punch");
                P2Anim.SetTrigger("punchup");

            }
            if (isPlayer1 && Input.GetKeyDown(KeyCode.A))
            {
                if (abilityManager.RequiresHit(abilityManager.CurrentAbilityP1))
                {
                    if (!isOnCooldown)
                    {
                        if (Ball.GetComponent<BallBehaviorScript>().testForHit(isPlayer1, 1))
                        {
                            abilityManager.RunCurrentAbilityP1();
                        }
                        isOnCooldown = true;
                        StartCoroutine(cooldown());
                    }
                }
                else
                {
                    abilityManager.RunCurrentAbilityP1();
                }
            }
            if (!isPlayer1 && Input.GetKeyDown(KeyCode.RightArrow))
            {

                if (abilityManager.RequiresHit(abilityManager.CurrentAbilityP2))
                {
                    if (!isOnCooldown)
                    {
                        if (Ball.GetComponent<BallBehaviorScript>().testForHit(isPlayer1, 1))
                        {
                            abilityManager.RunCurrentAbilityP2();
                        }
                        isOnCooldown = true;
                        P2Anim.ResetTrigger("punchup");
                        P2Anim.SetTrigger("punch");
                        StartCoroutine(cooldown());
                    }
                }
                else
                {
                    abilityManager.RunCurrentAbilityP2();
                }
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
    public IEnumerator cooldown()
    {
        yield return new WaitForSeconds(0.5f);
        isOnCooldown = false;
    }
}
