using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class HighScore : MonoBehaviour
{
    public float Score; //link to the score system of the player
    public TextMeshProUGUI ScoreText; //wuses TMP to show the score
    public GameObject NameEntryPanel; //UI panel 
    public GameObject ScoreboardPanel;  //UI panel
    public TMP_InputField NameInputField; //where you eneter your name
    public TextMeshProUGUI[] ScoreboardEntries; //scoreboard

    private const int MaxScores = 5; //this is how many scores will be listed on the leaderboards

    private string ScoreKey(int i) => SceneManager.GetActiveScene().name + "_Score_" + i;
    private string NameKey(int i) => SceneManager.GetActiveScene().name + "_Name_" + i;

    void Update()
    {
        ScoreText.text = "Score: " + (Score).ToString("0");
    }

    public void GameOver() //when game ends it shows the scoreboard
    {
        NameEntryPanel.SetActive(true);
        ScoreboardPanel.SetActive(true);
    }

    public void OnConfirmName()
    {
        string name = NameInputField.text;
        if (string.IsNullOrWhiteSpace(name)) name = "Anonymous";

        SaveScore(name);
        DisplayScoreboard();
        NameEntryPanel.SetActive(false);
    }

    public List<float> LoadScores()
    {
        List<float> scores = new List<float>();
        for (int i = 0; i < MaxScores; i++)
        {
            if (PlayerPrefs.HasKey(ScoreKey(i)))
                scores.Add(PlayerPrefs.GetFloat(ScoreKey(i)));
        }
        return scores;
    }

    public List<string> LoadNames()
    {
        List<string> names = new List<string>();
        for (int i = 0; i < MaxScores; i++)
        {
            if (PlayerPrefs.HasKey(NameKey(i)))
                names.Add(PlayerPrefs.GetString(NameKey(i)));
        }
        return names;
    }

    public void SaveScore(string playerName)
    {
        List<float> scores = LoadScores();
        List<string> names = LoadNames();

        scores.Add(Score);
        names.Add(playerName);

        var combined = new List<(float score, string name)>();
        for (int i = 0; i < scores.Count; i++)
            combined.Add((scores[i], names[i]));

        combined.Sort((a, b) => b.score.CompareTo(a.score));
        if (combined.Count > MaxScores) combined.RemoveRange(MaxScores, combined.Count - MaxScores);

        for (int i = 0; i < combined.Count; i++)
        {
            PlayerPrefs.SetFloat(ScoreKey(i), combined[i].score);
            PlayerPrefs.SetString(NameKey(i), combined[i].name);
        }

        PlayerPrefs.Save();
    }

    public void DisplayScoreboard()
    {
        List<float> scores = LoadScores();
        List<string> names = LoadNames();

        for (int i = 0; i < ScoreboardEntries.Length; i++)
        {
            ScoreboardEntries[i].text = i < scores.Count
                ? $"{i + 1}. {names[i]} - {scores[i]:0}"
                : $"{i + 1}. ---";
        }
    }
}
