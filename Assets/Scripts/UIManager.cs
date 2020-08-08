using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameManager m_gameManager = null;
    [SerializeField]
    private Text m_scoreText;
    [SerializeField]
    private Text m_gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        m_scoreText.text = "Score: 0";
        m_gameOverText.gameObject.SetActive(false);

        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (!m_gameManager)
        {
            Debug.LogError("Game Manager not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        m_gameOverText.gameObject.SetActive(true);
        m_gameManager.GameOver();
    }

    public void UpdateScore(int score)
    {
        m_scoreText.text = "Score: " + score.ToString();
    }
}
