using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    public int score1 = 0;
    public int score2 = 0;

    public int bonusScore1 = 0;
    public int bonusScore2 = 0;

    public TextMeshProUGUI player1Score;
    public TextMeshProUGUI player2Score;

    public float time = 60f;
    public int timeWhole = 60;

    public TextMeshProUGUI timeText;
    public bool timeRun = false;

    private GameObject ball;

    public GameObject prompt;
    public GameObject promptPanel;
    public GameObject restartPrompt;

    public GameObject lifePanel;
    public GameObject life1;
    public GameObject life2;
    public GameObject life3;

    public GameObject redLife1;
    public GameObject redLife2;
    public GameObject redLife3;

    public int lives = 3;

    private GameObject audioControl;
    private bool hasPlayedSound = false;

    public bool is1PlayerMode = true;

    public ParticleSystem p1VictoryParticles;
    public ParticleSystem p2VictoryParticles;

    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
        audioControl = GameObject.FindGameObjectWithTag("AudioCtrl");
        restartPrompt.SetActive(false);
        if (!ball.GetComponent<BallBehaviorScript>().is1PlayerMode)
        {
            lifePanel.SetActive(false);
            life1.SetActive(false);
            life2.SetActive(false);
            life3.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRun && !ball.GetComponent<BallBehaviorScript>().is1PlayerMode)
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
            }
            timeWhole = (int)time;
        }
        if (lives < 3)
        {
            life3.SetActive(false);
            redLife3.SetActive(true);
        }
        if (lives < 2)
        {
            life2.SetActive(false);
            redLife2.SetActive(true);
        }
        if (lives < 1)
        {
            life1.SetActive(false);
            redLife1.SetActive(true);
        }
        if (ball.GetComponent<BallBehaviorScript>().is1PlayerMode)
        {
            timeText.text = "";
        }
        if (!ball.GetComponent<BallBehaviorScript>().is1PlayerMode)
        {
            timeText.text = timeWhole.ToString();
        }
        if (timeWhole == 0 || lives == 0)
        {
            GameObject[] players;
            players = GameObject.FindGameObjectsWithTag("Player");
            if (ball != null)
            {
                ball.GetComponent<BallBehaviorScript>().playing = false;
            }
            foreach (GameObject player in players)
            {
                player.GetComponent<PlayerControllerScript>().playing = false;
            }


            if (audioControl != null && !hasPlayedSound)
            {
                if (score1 > 10000 && is1PlayerMode) //Replace 100000 with highscore
                {
                    audioControl.GetComponent<AudioManager>().PlayCelebration();
                }
                if (score1 < 10000 && is1PlayerMode) //Replace 100000 with highscore
                {
                    audioControl.GetComponent<AudioManager>().PlayFail();
                }
                if (!is1PlayerMode)
                {
                    audioControl.GetComponent<AudioManager>().PlayCelebration();
                    if (score1 > score2)
                    {
                        p1VictoryParticles.Play();
                    }
                    if (score1 < score2)
                    {
                        p2VictoryParticles.Play();
                    }
                }
                hasPlayedSound = true;
            }



            StartCoroutine(waitForSceneFinish());
        }
        if (ball != null)
        {
            ball.GetComponent <BallBehaviorScript>().player1SpeedUp((int)Mathf.Floor(score1 / 1000));
        }
            SetScoreText();
    }
    public void SetScoreText()
    {
        if (score1 < 10)
        {
            player1Score.text = "Score: 0000" + score1;
        }
        else if (score1 < 100 && score1 >= 10)
        {
            player1Score.text = "Score: 000" + score1;
        }
        else if (score1 < 1000 && score1 >= 100)
        {
            player1Score.text = "Score: 00" + score1;
        }
        else if (score1 < 10000 && score1 >= 1000)
        {
            player1Score.text = "Score: 0" + score1;
        }
        else
        {
            player1Score.text = "Score: " + score1;
        }


        if (score2 < 10)
        {
            player2Score.text = "Score: 0000" + score2;
        }
        else if (score2 < 100 && score2 >= 10)
        {
            player2Score.text = "Score: 000" + score2;
        }
        else if (score2 < 1000 && score2 >= 100)
        {
            player2Score.text = "Score: 00" + score2;
        }
        else if (score2 < 10000 && score2 >= 1000)
        {
            player2Score.text = "Score: 0" + score2;
        }
        else
        {
            player2Score.text = "Score: " + score2;
        }
    }
    public void perfectScore(bool isPlayer1, float currentSpeed)
    {
        currentSpeed = currentSpeed / 5;
        //int scoreMultiplier = (int)currentSpeed;
        //if (scoreMultiplier < 1)
        //{
        //    scoreMultiplier = 1;
        //}
        if (isPlayer1)
        {
            score1 += 100 + bonusScore1 + (100 * (int)currentSpeed);
            bonusScore1 += 100 + (10 * (int)currentSpeed);
        }
        if (!isPlayer1)
        {
            score2 += 100 + bonusScore2 + (100 * (int)currentSpeed);
            bonusScore2 += 100 + (10 * (int)currentSpeed);
        }
    }
    public void poorScore(bool isPlayer1, float currentSpeed)
    {
        currentSpeed = currentSpeed / 5;
        //int scoreMultiplier = (int)currentSpeed;
        //if (scoreMultiplier < 1)
        //{
        //    scoreMultiplier = 1;
        //}
        if (isPlayer1)
        {
            score1 = score1 + 10 + (10 * (int)currentSpeed);
            bonusScore1 = 0;
        }
        if (!isPlayer1)
        {
            score2 = score2 + 10 + (10 * (int)currentSpeed);
            bonusScore2 = 0;
        }
    }
    public void loseScore(bool isPlayer1, int shotChoice, float currentSpeed)
    {
        currentSpeed = currentSpeed / 5;
        //int scoreMultiplier = (int)currentSpeed;
        //if (scoreMultiplier < 1)
        //{
        //    scoreMultiplier = 1;
        //}
        if (shotChoice == 1)
        {
            if (isPlayer1 && score1 != 0)
            {
                score1 -= 10 + (10 * (int)currentSpeed);
            }
            if (!isPlayer1 && score2 != 0)
            {
                score2 -= 10 + (10 * (int)currentSpeed);
            }
        }
        if (shotChoice == 2 || shotChoice == 3)
        {
            if (isPlayer1)
            {
                if (score1 != 0)
                {
                    score1 -= 10 + (10 * (int)currentSpeed);
                }
                score2 += 100 + (10 * (int)currentSpeed);
            }
            if (!isPlayer1)
            {
                if (score2 != 0)
                {
                    score2 -= 10 + (10 * (int)currentSpeed);
                }
                score1 += 100 + (10 * (int)currentSpeed);
            }
        }
    }

    public void loseLife()
    {
        lives--;
    }
    public void play()
    {
        prompt.SetActive(false);
        promptPanel.SetActive(false);
    }

    public IEnumerator waitForSceneFinish()
    {
        GameObject[] _players = GameObject.FindGameObjectsWithTag("Player");
        yield return new WaitForSeconds(2f);
        foreach (GameObject player in _players)
        {
            player.GetComponent<PlayerControllerScript>().canRestart = true;
        }
        restartPrompt.SetActive(true);
        promptPanel.SetActive(true);
    }
}
