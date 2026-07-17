using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviorScript : MonoBehaviour
{
    public int targX;
    public int targY;

    public float speed = 5f;

    public float Dis;
    public GameObject targ;

    public bool isOnPlayer1Side = true;
    public bool canHit = false;

    public int hitType = 0;

    private GameObject Canvas;

    public PopupManager popupManagerP1;
    public PopupManager popupManagerP2;

    public ParticleSystem ParticleBurst;

    public AudioSource Sqelch;
    public AudioSource MissedHit;
    public AudioSource PerfectHit;
    public AudioSource MainSoundtrack;

    public float minPitch = 1f;
    public float maxPitch = 1.08f;
    public float minSpeed = 0f;
    public float maxSpeed = 20f;
    // Start is called before the first frame update
    void Start()
    {
        targ = GameObject.FindGameObjectWithTag("Target1");
        Canvas = GameObject.FindGameObjectWithTag("Canvas");
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
                transform.position = Vector2.MoveTowards(transform.position, targ.transform.position, speed * Time.deltaTime);
            }
        }



        float speedconv = speed;
        float t = Mathf.InverseLerp(minSpeed, maxSpeed, speedconv);
        MainSoundtrack.pitch = Mathf.Lerp(minPitch, maxPitch, t);
    }
    public void perfectHit()
    {
        Debug.Log("Perfect!");

        if (speed < 14)
        {
            //speed = speed * 1.1f;
            StartCoroutine(DelayedWhileLoop(0.5f));
        }
        if (speed > 20)
        {
            speed = 20f;
        }
        Canvas.GetComponent<ScoreScript>().perfectScore(isOnPlayer1Side);
        StartCoroutine((Tweening(2f)));
        TriggerEffect();
        Sqelch.Play();
        PerfectHit.Play();
    }
    public void lateHit()
    {
        Debug.Log("Too late!");

        if (speed > 5)
        {
            //speed = speed * 0.75f;
            StartCoroutine(DelayedWhileLoop(-0.5f));
        }
        if (speed < 5)
        {
            speed = 5f;
        }
        Canvas.GetComponent<ScoreScript>().poorScore(isOnPlayer1Side);
        StartCoroutine((Tweening(1.5f)));
        TriggerEffect();
        Sqelch.Play();
        MissedHit.Play();
    }
    public void earlyHit()
    {
        Debug.Log("Too early!");

        if (speed > 5)
        {
            //speed = speed * 0.75f;
            StartCoroutine(DelayedWhileLoop(-0.5f));
        }
        if (speed < 5)
        {
            speed = 5f;
            
        }
        Canvas.GetComponent<ScoreScript>().poorScore(isOnPlayer1Side);
        StartCoroutine((Tweening(1.5f)));
        TriggerEffect();
        Sqelch.Play();
        MissedHit.Play();
    }


    public void PlayerAbility()
    {
        /*if (isPlayer1 && isOnPlayer1Side)
        {

        }*/




    }
    
    
    
    public void testForHit(bool isPlayer1)
    {
        if (isPlayer1 && isOnPlayer1Side)
        {
            if (canHit)
            {
                if (hitType == 1)
                {
                    earlyHit();
                    popupManagerP1.ShowPopup("Too early!");
                }
                if (hitType == 2)
                {
                    perfectHit();
                    popupManagerP1.ShowPopup("Perfect!");
                }
                if (hitType == 3)
                {
                    lateHit();
                    popupManagerP1.ShowPopup("Too late!");
                }
                canHit = false;
                setTarg();
            }
        }
        if (!isPlayer1 && !isOnPlayer1Side)
        {
            if (canHit)
            {
                if (hitType == 1)
                {
                    earlyHit();
                    popupManagerP2.ShowPopup("Too early!");
                }
                if (hitType == 2)
                {
                    perfectHit();
                    popupManagerP2.ShowPopup("Perfect!");
                }
                if (hitType == 3)
                {
                    lateHit();
                    popupManagerP2.ShowPopup("Too late!");
                }
                canHit = false;
                setTarg();
            }
        }
    }
    public void setTarg()
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "HitZone")
        {
            canHit = true;
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
            canHit = true;
            hitType = 2;
            testForHit(isOnPlayer1Side);
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
