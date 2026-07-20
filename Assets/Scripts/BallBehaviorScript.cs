using System.Collections;
using UnityEngine;

public class BallBehaviorScript : MonoBehaviour
{
    public int targX;
    public int targY;

    public float speed = 5f;
    public float maxSpeed = 15f; 
    public float minSpeed = 5f;

    public float Dis;
    private GameObject targ;

    public bool isOnPlayer1Side = true;
    public bool canHit = false;

    public int hitType = 0;

    private GameObject _canvas;

    private bool canScore = false;

    public bool playing = true;

    private bool Player1 = true;
    private bool hasSpedUp = false;
    private float startSpeed = 0;
    private int localShotChoice = 0;

    //private Rigidbody2D _rb2d;

    public GameObject pineapple;
    public bool is1PlayerMode = true;

    public PopupManager popupManagerP1;
    public PopupManager popupManagerP2;

    public ParticleSystem ParticleBurst;

    public AudioSource Sqelch;
    public AudioSource MissedHit;
    public AudioSource PerfectHit;
    public AudioSource MainSoundtrack;

    public float minPitch = 1f;
    public float maxPitch = 1.08f;
    public float minSpeedd = 0f;
    public float maxSpeedd = 20f;

    public float testValue = 0f;


    // Start is called before the first frame update
    void Start()
    {
        //targ = GameObject.FindGameObjectWithTag("Target1");
        _canvas = GameObject.FindGameObjectWithTag("Canvas");
        //_rb2d = GetComponent<Rigidbody2D>();
        MainSoundtrack.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (targ != null)
        {
            Dis = Vector2.Distance(transform.position, targ.transform.position);
            
            if (Dis > 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, targ.transform.position, speed * Time.deltaTime);
            }
            if (/*Dis < 0.1f*/ Dis == 0 && targ != null)
            {
                if (targ.tag == "Target1" || targ.tag == "Target2")
                {
                    if (playing && !is1PlayerMode)
                    {
                        _canvas.GetComponent<ScoreScript>().loseScore(isOnPlayer1Side, localShotChoice, speed);
                    }
                    if (playing && is1PlayerMode)
                    {
                        _canvas.GetComponent<ScoreScript>().loseLife();
                    }
                    StartCoroutine(splatDelay());
                }
                if (targ != null && (targ.tag == "Loop1" || targ.tag == "Loop2" || targ.tag == "Loop3" || targ.tag == "Loop4" || targ.tag == "Loop5" || targ.tag == "Loop6" || targ.tag == "Loop7" || targ.tag == "Loop8" || targ.tag == "Loop9" || targ.tag == "Loop10" || targ.tag == "Loop11" || targ.tag == "Loop12"))
                {
                    Loop();
                }
                if (targ != null && (targ.tag == "Down1" || targ.tag == "Down2" || targ.tag == "Down3" || targ.tag == "Down4" || targ.tag == "Down5" || targ.tag == "Down6" || targ.tag == "Down7" || targ.tag == "Down8" || targ.tag == "Down9" || targ.tag == "Down10" || targ.tag == "Down11" || targ.tag == "Down12"))
                {
                    Down();
                }
            }
            
        }

        float speedconv = speed;
        float t = Mathf.InverseLerp(minSpeedd, maxSpeedd, speedconv);
        MainSoundtrack.pitch = Mathf.Lerp(minPitch, maxPitch, t);
    }
    public void perfectHit()
    {
        //Debug.Log("Perfect!");
        if (speed < maxSpeed && !is1PlayerMode)
        {
            //speed += 1f;
            StartCoroutine(DelayedWhileLoop(0.5f));
        }
        if (speed > maxSpeed && !is1PlayerMode)
        {
            speed = maxSpeed;
        }
        _canvas.GetComponent<ScoreScript>().perfectScore(isOnPlayer1Side, speed);
        StartCoroutine((Tweening(2f)));
        TriggerEffect();
        Sqelch.Play();
        PerfectHit.Play();
    }
    public void lateHit()
    {
        //Debug.Log("Too late!");
        if (speed > minSpeed && !is1PlayerMode)
        {
            //speed -= 1f;
            StartCoroutine(DelayedWhileLoop(-0.5f));
        }
        if (speed < minSpeed && !is1PlayerMode)
        {
            speed = minSpeed;
        }
        _canvas.GetComponent<ScoreScript>().poorScore(isOnPlayer1Side, speed);
        StartCoroutine((Tweening(1.5f)));
        TriggerEffect();
        Sqelch.Play();
        MissedHit.Play();
    }
    public void earlyHit()
    {
        //Debug.Log("Too early!");
        if (speed > minSpeed && !is1PlayerMode)
        {
            //speed -= 1f;
            StartCoroutine(DelayedWhileLoop(-0.5f));
        }
        if (speed < minSpeed && !is1PlayerMode)
        {
            speed = minSpeed;
        }
        _canvas.GetComponent<ScoreScript>().poorScore(isOnPlayer1Side, speed);
        StartCoroutine((Tweening(1.5f)));
        TriggerEffect();
        Sqelch.Play();
        MissedHit.Play();
    }
    public void testForHit(bool isPlayer1, int shotChoice)
    {
        if (isPlayer1 && isOnPlayer1Side)
        {
            if (canHit)
            {
                if (canScore)
                {
                    if (hitType == 1)
                    {
                        earlyHit();
                        popupManagerP1.ShowPopup("Too Early");
                    }
                    if (hitType == 2)
                    {
                        perfectHit();
                        popupManagerP1.ShowPopup("Perfect");
                    }
                    if (hitType == 3)
                    {
                        lateHit();
                        popupManagerP1.ShowPopup("Too Late");
                    }
                }
                canHit = false;
                canScore = true;
                _canvas.GetComponent<ScoreScript>().play();
                _canvas.GetComponent<ScoreScript>().timeRun = true;
                Player1 = isPlayer1;
                setTarg(shotChoice);
            }
        }
        if (!isPlayer1 && !isOnPlayer1Side)
        {
            if (canHit)
            {
                if (canScore)
                {
                    if (hitType == 1)
                    {
                        earlyHit();
                    }
                    if (hitType == 2)
                    {
                        perfectHit();
                    }
                    if (hitType == 3)
                    {
                        lateHit();
                    }
                }
                Player1 = isPlayer1;
                canHit = false;
                canScore = true;
                setTarg(shotChoice);
            }
        }
    }
    public void setTarg(int shotChoice)
    {
        if (shotChoice == 1)
        {
            if (isOnPlayer1Side)
            {
                targ = GameObject.FindGameObjectWithTag("Target1");
            }
            if (!isOnPlayer1Side)
            {
                targ = GameObject.FindGameObjectWithTag("Target2");
            }
        }
        if (shotChoice == 2)
        {
            if (Player1)
            {
                targ = GameObject.FindGameObjectWithTag("Down1");
            }
            if (!Player1)
            {
                targ = GameObject.FindGameObjectWithTag("Down12");
            }
        }
        if (shotChoice == 3)
        {
            if (Player1)
            {
                targ = GameObject.FindGameObjectWithTag("Loop1");
            }
            if (!Player1)
            {
                targ = GameObject.FindGameObjectWithTag("Loop12");
            }
        }
        localShotChoice = shotChoice;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "HitZone")
        {
            canHit = true;
            hasSpedUp = false;
        }
        if ((collision.gameObject.tag == "Early") /*&& canHit*/)
        {
            hitType = 1;
        }
        if ((collision.gameObject.tag == "Perfect") /*&& canHit*/)
        {
            hitType = 2;
        }
        if ((collision.gameObject.tag == "Late") /*&& canHit*/)
        {
            hitType = 3;
        }
        if (((collision.gameObject.tag == "Wall")))
        {
            if (playing)
            {
                canHit = true;
                int localHitType = Random.Range(1, 100);
                hitType = 2;
                if (localHitType > 0 && localHitType < 51)
                {
                    testForHit(isOnPlayer1Side, 1);
                }
                if (localHitType > 50 && localHitType < 75)
                {
                    testForHit(isOnPlayer1Side, 2);
                }
                if (localHitType > 74 && localHitType < 100)
                {
                    testForHit(isOnPlayer1Side, 3);
                }
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border1")
        {
            isOnPlayer1Side = true;
        }
        if (collision.gameObject.tag == "Border2")
        {
            isOnPlayer1Side = false;
        }
    }
    public void restart()
    {
        _canvas.GetComponent<ScoreScript>().timeRun = false;
        canHit = true;
        canScore = false;
        if (isOnPlayer1Side)
        {
            transform.position = new Vector2(-5.58f, -1.96f);
        }
        if (!isOnPlayer1Side)
        {
            transform.position = new Vector2(5.58f, -1.96f);
        }
        if (is1PlayerMode && !isOnPlayer1Side)
        {
            StartCoroutine(autoRestart());
        }
    }
    public IEnumerator splatDelay()
    {
        canHit = false;
        if (playing)
        {
            //Debug.Log("Stopped");
            targ = null;
            canScore = false;
            speed = minSpeed;
            //_rb2d.velocity = Vector3.zero;
            startSpeed = minSpeed;
        }
        yield return new WaitForSeconds(1);
        splat();
        if (playing)
        {
            restart();
        }
    }
    public IEnumerator autoRestart()
    {
        yield return new WaitForSeconds(1);
        hitType = Random.Range(1, 4);
        testForHit(isOnPlayer1Side, hitType);
        Debug.Log("StartAgain");
    }

    public void splat()
    {
        if (pineapple != null)
        {
            Instantiate(pineapple, transform.position, Quaternion.identity);
        }
        if (!playing)
        {
            this.gameObject.SetActive(false);
        }
    }
    public void player1SpeedUp(int multiplier)
    {
        if (is1PlayerMode && speed < maxSpeed)
        {
            speed = 7.5f + (2.5f * multiplier);
        }
    }



    //Everything below here is a long-winded way to make the pineapple follow a path of objects for the different shots

    public void Down()
    {
        if (!hasSpedUp)
        {
            startSpeed = speed;
            hasSpedUp = true;
        }
        if (Player1 && targ.tag == "Down1")
        {
            targ = GameObject.FindGameObjectWithTag("Down2");
            //speed = speed * 1.25f;
        }
        else if (Player1 && targ.tag == "Down2")
        {
            targ = GameObject.FindGameObjectWithTag("Down3");
        }
        else if (Player1 && targ.tag == "Down3")
        {
            targ = GameObject.FindGameObjectWithTag("Down4");
        }
        else if (Player1 && targ.tag == "Down4")
        {
            targ = GameObject.FindGameObjectWithTag("Down5");
        }
        else if (Player1 && targ.tag == "Down5")
        {
            targ = GameObject.FindGameObjectWithTag("Down6");
        }
        else if (Player1 && targ.tag == "Down6")
        {
            targ = GameObject.FindGameObjectWithTag("Down7");
        }
        else if (Player1 && targ.tag == "Down7")
        {
            targ = GameObject.FindGameObjectWithTag("Down8");
        }
        else if (Player1 && targ.tag == "Down8")
        {
            targ = GameObject.FindGameObjectWithTag("Down9");
        }
        else if (Player1 && targ.tag == "Down9")
        {
            targ = GameObject.FindGameObjectWithTag("Down10");
        }
        else if (Player1 && targ.tag == "Down10")
        {
            targ = GameObject.FindGameObjectWithTag("Down11");
        }
        else if (Player1 && targ.tag == "Down11")
        {
            targ = GameObject.FindGameObjectWithTag("Down12");
        }
        else if (Player1 && targ.tag == "Down12")
        {
            targ = GameObject.FindGameObjectWithTag("Target1");
            //speed = startSpeed;
        }


        if (!Player1 && targ.tag == "Down12")
        {
            targ = GameObject.FindGameObjectWithTag("Down11");
            //speed = speed * 1.25f;
        }
        else if (!Player1 && targ.tag == "Down11")
        {
            targ = GameObject.FindGameObjectWithTag("Down10");
        }
        else if (!Player1 && targ.tag == "Down10")
        {
            targ = GameObject.FindGameObjectWithTag("Down9");
        }
        else if (!Player1 && targ.tag == "Down9")
        {
            targ = GameObject.FindGameObjectWithTag("Down8");
        }
        else if (!Player1 && targ.tag == "Down8")
        {
            targ = GameObject.FindGameObjectWithTag("Down7");
        }
        else if (!Player1 && targ.tag == "Down7")
        {
            targ = GameObject.FindGameObjectWithTag("Down6");
        }
        else if (!Player1 && targ.tag == "Down6")
        {
            targ = GameObject.FindGameObjectWithTag("Down5");
        }
        else if (!Player1 && targ.tag == "Down5")
        {
            targ = GameObject.FindGameObjectWithTag("Down4");
        }
        else if (!Player1 && targ.tag == "Down4")
        {
            targ = GameObject.FindGameObjectWithTag("Down3");
        }
        else if (!Player1 && targ.tag == "Down3")
        {
            targ = GameObject.FindGameObjectWithTag("Down2");
        }
        else if (!Player1 && targ.tag == "Down2")
        {
            targ = GameObject.FindGameObjectWithTag("Down1");
        }
        else if (!Player1 && targ.tag == "Down1")
        {
            targ = GameObject.FindGameObjectWithTag("Target2");
            //speed = startSpeed;
        }
    }

    public void Loop()
    {
        if (!hasSpedUp)
        {
            startSpeed = speed;
            hasSpedUp = true;
        }
        if (Player1 && targ.tag == "Loop1")
        {
            targ = GameObject.FindGameObjectWithTag("Loop2");
            //speed = speed * 1.25f;
        }
        else if (Player1 && targ.tag == "Loop2")
        {
            targ = GameObject.FindGameObjectWithTag("Loop3");
        }
        else if(Player1 && targ.tag == "Loop3")
        {
            targ = GameObject.FindGameObjectWithTag("Loop4");
        }
        else if (Player1 && targ.tag == "Loop4")
        {
            targ = GameObject.FindGameObjectWithTag("Loop5");
        }
        else if (Player1 && targ.tag == "Loop5")
        {
            targ = GameObject.FindGameObjectWithTag("Loop6");
        }
        else if (Player1 && targ.tag == "Loop6")
        {
            targ = GameObject.FindGameObjectWithTag("Loop7");
        }
        else if (Player1 && targ.tag == "Loop7")
        {
            targ = GameObject.FindGameObjectWithTag("Loop8");
        }
        else if (Player1 && targ.tag == "Loop8")
        {
            targ = GameObject.FindGameObjectWithTag("Loop9");
        }
        else if (Player1 && targ.tag == "Loop9")
        {
            targ = GameObject.FindGameObjectWithTag("Loop10");
        }
        else if (Player1 && targ.tag == "Loop10")
        {
            targ = GameObject.FindGameObjectWithTag("Loop11");
        }
        else if (Player1 && targ.tag == "Loop11")
        {
            targ = GameObject.FindGameObjectWithTag("Loop12");
        }
        else if (Player1 && targ.tag == "Loop12")
        {
            targ = GameObject.FindGameObjectWithTag("Target1");
            //speed = startSpeed;
        }


        if (!Player1 && targ.tag == "Loop12")
        {
            targ = GameObject.FindGameObjectWithTag("Loop11");
            //speed = speed * 1.25f;
        }
        else if (!Player1 && targ.tag == "Loop11")
        {
            targ = GameObject.FindGameObjectWithTag("Loop10");
        }
        else if (!Player1 && targ.tag == "Loop10")
        {
            targ = GameObject.FindGameObjectWithTag("Loop9");
        }
        else if (!Player1 && targ.tag == "Loop9")
        {
            targ = GameObject.FindGameObjectWithTag("Loop8");
        }
        else if (!Player1 && targ.tag == "Loop8")
        {
            targ = GameObject.FindGameObjectWithTag("Loop7");
        }
        else if (!Player1 && targ.tag == "Loop7")
        {
            targ = GameObject.FindGameObjectWithTag("Loop6");
        }
        else if (!Player1 && targ.tag == "Loop6")
        {
            targ = GameObject.FindGameObjectWithTag("Loop5");
        }
        else if (!Player1 && targ.tag == "Loop5")
        {
            targ = GameObject.FindGameObjectWithTag("Loop4");
        }
        else if (!Player1 && targ.tag == "Loop4")
        {
            targ = GameObject.FindGameObjectWithTag("Loop3");
        }
        else if (!Player1 && targ.tag == "Loop3")
        {
            targ = GameObject.FindGameObjectWithTag("Loop2");
        }
        else if (!Player1 && targ.tag == "Loop2")
        {
            targ = GameObject.FindGameObjectWithTag("Loop1");
        }
        else if (!Player1 && targ.tag == "Loop1")
        {
            targ = GameObject.FindGameObjectWithTag("Target2");
            //speed = startSpeed;
        }
    }


    public IEnumerator Tweening(float TweenAmount)
    {
        iTween.ScaleTo(gameObject, iTween.Hash("x", TweenAmount, "y", TweenAmount, "time", 0.1));
        yield return new WaitForSeconds(0.1f);
        iTween.ScaleTo(gameObject, iTween.Hash("x", 1, "y", 1, "time", 0.1));
    }

    public void TriggerEffect()
    {
        if (ParticleBurst != null)
        {
            // Stops the effect and clears any leftover particles
            ParticleBurst.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

            // Plays the effect from the beginning
            ParticleBurst.Play();
        }
    }


    private IEnumerator DelayedWhileLoop(float SpeedDel)
    {
        int Delayvalue = 0;
        while (Delayvalue < 5)
        {

            speed = speed + SpeedDel;
            Delayvalue += 1;
            yield return new WaitForSeconds(0.1f); // Delay for 1 second
        }
    }
}



//Vector3 targ = _player.transform.position;
//targ.z = 0f;

//        Vector3 objectPos = transform.position;
//targ.x = targ.x - objectPos.x;
//        targ.y = targ.y - objectPos.y;

//        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
//transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));


//Dis = Vector2.Distance(transform.position, _player.transform.position);
//if (Dis > 0 && _isMoving)
//{
//    transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, speed* Time.deltaTime);
//}
