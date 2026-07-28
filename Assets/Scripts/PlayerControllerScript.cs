using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerScript : MonoBehaviour
{
    [Header ("Animation")]
    public Animator P1Anim;
    public Animator P2Anim;

    [Header ("Player checks")]
    public bool isPlayer1 = true;
    public bool is1PlayerScene;

    [Header ("Misc")]
    public bool playing = true;
    public bool canRestart = false;
    public AbilityManager abilityManager;
    public float cooldownTime = 1f;

    private bool isOnCooldown = false;
    private bool canEmulate = true;
    private GameObject Ball;


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
            if (isPlayer1 && Input.GetKeyDown(KeyCode.W) && !isOnCooldown)
            {
                Ball.GetComponent<BallBehaviorScript>().testForHit(isPlayer1, 3);
                isOnCooldown = true;
                StartCoroutine(cooldown());

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
            if (isPlayer1 && Input.GetKeyDown(KeyCode.S) && !isOnCooldown)
            {
                Ball.GetComponent<BallBehaviorScript>().testForHit(isPlayer1, 2);
                isOnCooldown = true;
                StartCoroutine(cooldown());

            }
            if (isPlayer1 && Input.GetKeyDown(KeyCode.D) && !isOnCooldown)
            {
                Ball.GetComponent<BallBehaviorScript>().testForHit(isPlayer1, 1);
                isOnCooldown = true;
                StartCoroutine(cooldown());
                //P1Anim.SetTrigger("punch");


            }
            if (!isPlayer1 && Input.GetKeyDown(KeyCode.LeftArrow) && !isOnCooldown)
            {
                P2Anim.ResetTrigger("punchup");
                P2Anim.SetTrigger("punch");
                Ball.GetComponent<BallBehaviorScript>().testForHit(isPlayer1, 1);
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
            if (!isPlayer1 && Input.GetKeyDown(KeyCode.UpArrow) && !isOnCooldown)
            {
                Ball.GetComponent<BallBehaviorScript>().testForHit(isPlayer1, 3);
                isOnCooldown = true;
                StartCoroutine(cooldown());
                P2Anim.ResetTrigger("punch");
                P2Anim.SetTrigger("punchup");

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
    public void Celebration(bool player1)
    {
        if (player1 == isPlayer1)
        {
            if (player1 == true)
            {
                //P1Anim.Play("Win");
            }
            if (player1 == false)
            {
                P2Anim.Play("Win");
            }
        }
    }
    public IEnumerator cooldown()
    {
        yield return new WaitForSeconds(0.5f);
        isOnCooldown = false;
    }

    public void emulateAnim(int emulationType)
    {
        if (canEmulate)
        {
            switch (emulationType)
            {
                case 1:
                    P2Anim.ResetTrigger("punchup");
                    P2Anim.SetTrigger("punch");
                    Debug.Log("Punch");
                    break;
                case 2:
                    P2Anim.ResetTrigger("punchup");
                    P2Anim.SetTrigger("punch");
                    Debug.Log("Punch");
                    break;
                case 3:
                    P2Anim.ResetTrigger("punch");
                    P2Anim.SetTrigger("punchup");
                    Debug.Log("UpperCrust!");
                    break;
            }
            canEmulate = false;
        }
    }
    public void emulateAgain()
    {
        canEmulate = true;
    }
}
