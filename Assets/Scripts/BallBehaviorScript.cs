using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private float startSpeed = 0;
    private int localShotChoice = 0;

    //private Rigidbody2D _rb2d;

    public GameObject pineapple;
    public SpriteRenderer pineapplespriteRend;
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

    public List<GameObject> upTargets;
    public List<GameObject> downTargets;
    private int targNum = 0;

    public float rotationSpeed = 50f;

    

    

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
                if (targ != null && (upTargets.Contains(targ)))
                {
                    Up();
                }
                if (targ != null && (downTargets.Contains(targ)))
                {
                    Down();
                }
            }
            if (speed > maxSpeed)
            {
                speed = maxSpeed;
            }
            if (canHit)
            {
                GetComponent<Outline>().enabled = true;
            }
            else
            {
                GetComponent<Outline>().enabled = false;
            }
        }

        float speedconv = speed;
        float t = Mathf.InverseLerp(minSpeedd, maxSpeedd, speedconv);
        MainSoundtrack.pitch = Mathf.Lerp(minPitch, maxPitch, t);



        transform.Rotate(new Vector3(0,0, rotationSpeed * Time.deltaTime));
    }


    public void ApplySpeedBoost(float amount, float duration, int delboost)
    {
        StartCoroutine(SpeedBoostRoutine(amount, duration, delboost));
    }

    private IEnumerator SpeedBoostRoutine(float amount, float duration, int delboost)
    {
        speed = Mathf.Min(speed + amount, maxSpeed);
        yield return new WaitForSeconds(duration);
        if (delboost > 0)
        {
            speed = Mathf.Max(speed - amount, minSpeed);
        }
        
    }

    public void SetAlpha(SpriteRenderer spriteRenderer, float alpha, float duration)
    {
        StartCoroutine(InvisAbil(spriteRenderer,  alpha,  duration));
    }

    private IEnumerator InvisAbil(SpriteRenderer spriteRenderer, float alpha, float duration)
    {
        Color c = spriteRenderer.color;
        c.a = alpha;
        spriteRenderer.color = c;
        yield return new WaitForSeconds(duration);
        c.a = 1;
        spriteRenderer.color = c;
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

    private void missedHit()
    {
        if (!is1PlayerMode)
        {
            if (speed > minSpeed)
            {
                StartCoroutine(DelayedWhileLoop(-0.5f));
            }
            else
            {
                speed = minSpeed;
            }
        }
        _canvas.GetComponent<ScoreScript>().poorScore(isOnPlayer1Side, speed);
        StartCoroutine(Tweening(1.5f));
        TriggerEffect();
        Sqelch.Play();
        MissedHit.Play();
    }


    public bool testForHit(bool isPlayer1, int shotChoice)
    {
        if (isPlayer1 && isOnPlayer1Side)
        {
            if (canHit)
            {
                if (canScore)
                {
                    if (hitType == 1)
                    {
                        missedHit();
                        popupManagerP1.ShowPopup("Too Early");
                        
                    }
                    if (hitType == 2)
                    {
                        perfectHit();
                        popupManagerP1.ShowPopup("Perfect");
                    }
                    if (hitType == 3)
                    {
                        missedHit();
                        popupManagerP1.ShowPopup("Too Late");
                    }
                }
                
                
                    
                
                canHit = false;
                canScore = true;
                _canvas.GetComponent<ScoreScript>().play();
                _canvas.GetComponent<ScoreScript>().timeRun = true;
                Player1 = isPlayer1;
                setTarg(shotChoice);
                
                return true;
            }
        }
        if (!isPlayer1 && !isOnPlayer1Side)
        {
            if (canHit)
            {
                if (canScore)
                {
                    switch (hitType)
                    {
                        case 1:
                            missedHit();
                            popupManagerP2.ShowPopup("Too Early");
                            break;
                        case 2:
                            perfectHit();
                            popupManagerP2.ShowPopup("Perfect");
                            break;
                        case 3:
                            missedHit();
                            popupManagerP2.ShowPopup("Too Late");
                            break;
                    }
                }
                
                
                Player1 = isPlayer1;
                canHit = false;
                
                setTarg(shotChoice);
                return true;
            }
        }
        return false;
    }
    public void setTarg(int shotChoice)
    {
        switch (Player1)
        {
            case true: 
                targNum = 0;
                break;
            case false:
                targNum = 11;
                break;
        }
        switch (shotChoice)
        {
            case 1:
                targ = GameObject.FindGameObjectWithTag(isOnPlayer1Side ? "Target1" : "Target2");
                break;
            case 2:
                foreach (GameObject downTarget in downTargets)
                {
                    if (downTargets.IndexOf(downTarget) == targNum)
                    {
                        targ = downTarget;
                    }
                }
                break;
            case 3:
                foreach (GameObject upTarget in upTargets)
                {
                    if (upTargets.IndexOf(upTarget) == targNum)
                    {
                        targ = upTarget;
                    }
                }
                break;
        }
        localShotChoice = shotChoice;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag) // changed this into a switch 
        {
            case "HitZone":
                canHit = true;
                break;
            case "Early":
                hitType = 1;
                break;
            case "Perfect":
                hitType = 2;
                break;
            case "Late":
                hitType = 3;
                break;
            case "Wall":
                if (playing)
                {
                    canHit = true;
                    hitType = 2;
                    int roll = Random.Range(1, 100);
                    int shotChoice = roll <= 50 ? 1 : roll <= 74 ? 2 : 3;
                    testForHit(isOnPlayer1Side, shotChoice);
                }
                break;
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
        rotationSpeed = 0;
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
        TriggerEffect();
        if (playing)
        {
            //Debug.Log("Stopped");
            targ = null;
            canScore = false;
            speed = minSpeed;
            //_rb2d.velocity = Vector3.zero;
            startSpeed = minSpeed;
        }
        Sqelch.Play();
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
        rotationSpeed = 100;
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


    //Changed this to using a List so that it is easier to visuallise

    public void Down()
    {
        if (Player1)
        {
            if (Dis == 0 && targNum != 11)
            {
                targNum++;
            }
            if (Dis == 0 && targNum == 11)
            {
                targ = GameObject.FindGameObjectWithTag("Target1");
            }
        }
        if (!Player1)
        {
            if (Dis == 0 && targNum != 0)
            {
                targNum--;
            }
            if (Dis == 0 && targNum == 0)
            {
                targ = GameObject.FindGameObjectWithTag("Target2");
            }
        }
        if (downTargets.Contains(targ))
        {
            foreach (GameObject downTarget in downTargets)
            {
                if (downTargets.IndexOf(downTarget) == targNum)
                {
                    targ = downTarget;
                }
            }
        }
    }

    public void Up()
    {
        if (Player1)
        {
            if (Dis == 0 && targNum != 11)
            {
                targNum++;
            }
            if (Dis == 0 && targNum == 11)
            {
                targ = GameObject.FindGameObjectWithTag("Target1");
            }
        }
        if (!Player1)
        {
            if (Dis == 0 && targNum != 0)
            {
                targNum--;
            }
            if (Dis == 0 && targNum == 0)
            {
                targ = GameObject.FindGameObjectWithTag("Target2");
            }
        }
        if (upTargets.Contains(targ))
        {
            foreach (GameObject upTarget in upTargets)
            {
                if (upTargets.IndexOf(upTarget) == targNum)
                {
                    targ = upTarget;
                }
            }
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
            yield return new WaitForSeconds(0.1f); // Delay for 0.1 second
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
