using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreScript : MonoBehaviour
{
    public int score1 = 0;
    public int score2 = 0;

    public int bonusScore1 = 0;
    public int bonusScore2 = 0;

    public TextMeshProUGUI player1Score;
    public TextMeshProUGUI player2Score;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
    public void perfectScore(bool isPlayer1)
    {
        if (isPlayer1)
        {
            score1 += 100 + bonusScore1;
            bonusScore1 += 100;
        }
        if (!isPlayer1)
        {
            score2 += 100 + bonusScore2;
            bonusScore2 += 100;
        }
    }
    public void poorScore(bool isPlayer1)
    {
        if (isPlayer1)
        {
            score1 = score1 + 10;
            bonusScore1 = 0;
        }
        if (!isPlayer1)
        {
            score2 = score2 + 10;
            bonusScore2 = 0;
        }
    }
}
