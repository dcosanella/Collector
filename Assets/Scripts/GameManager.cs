using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private SpawnManager m_spawnManager;

    [SerializeField]
    private float m_spawnRate = 1f;

    private bool m_gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        m_spawnManager = GameObject.Find(SceneManagers.SpawnManager).GetComponent<SpawnManager>();
        if (!m_spawnManager)
        {
            Debug.LogError(ErrorMessages.SpawnMgrNotFound);
        }

        //m_spawnManager.SpawnCoin();
        m_spawnManager.SpawnRain(m_spawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && m_gameOver)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void GameOver()
    {
        m_gameOver = true;
    }

    public void IncreaseSpawnRate()
    {
        m_spawnRate += 0.1f;
        m_spawnManager.UpdateSpawnCoroutine(m_spawnRate);
    }
}
