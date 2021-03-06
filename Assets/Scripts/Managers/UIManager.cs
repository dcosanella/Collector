﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameManager m_gameManager = null;

    [SerializeField]
    private Text m_highScoreText;

    [SerializeField]
    private Text m_scoreText;

    [SerializeField]
    private Text m_gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        LoadHighScore();

        m_scoreText.text = Scores.InitialScore;
        m_gameOverText.gameObject.SetActive(false);

        m_gameManager = GameObject.Find(SceneManagers.GameManager).GetComponent<GameManager>();
        if (!m_gameManager)
        {
            Debug.LogError(ErrorMessages.GameMgrNotFound);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameOver()
    {
        m_gameOverText.gameObject.SetActive(true);

        SaveHighScore();

        m_gameManager.GameOver();
    }

    public void UpdateScore(int score)
    {
        m_scoreText.text = Scores.ScoreLabel + score.ToString();

        if (score != 0 && score % 5 == 0)
        {
            m_gameManager.IncreaseSpawnRate();
        }
    }

    private void LoadHighScore()
    {
        string highScore = GetHighScore();
        if (string.IsNullOrEmpty(highScore))
        {
            m_highScoreText.gameObject.SetActive(false);
            return;
        }

        m_highScoreText.text = Scores.HighScoreLabel + highScore;
        m_highScoreText.gameObject.SetActive(true);
    }

    private string GetHighScore()
    {
        string score = PlayerPrefs.GetString(Scores.HighScoreKey);
        if (string.IsNullOrEmpty(score))
        {
            return null;
        }

        return ParseScore(score);
    }

    private string ParseScore(string score)
    {
        var index = score.IndexOf(" ");
        return score.Substring(++index);
    }

    private void SaveHighScore()
    {
        string highScoreText = GetHighScore();
        string currentScore = ParseScore(m_scoreText.text);

        if (string.IsNullOrEmpty(highScoreText) || int.Parse(highScoreText) < int.Parse(currentScore))
        {
            m_highScoreText.text = Scores.HighScoreLabel + currentScore;
            if (!m_highScoreText.gameObject.activeInHierarchy)
            {
                m_highScoreText.gameObject.SetActive(true);
            }

            PlayerPrefs.SetString(Scores.HighScoreKey, currentScore);
        }
    }
}
