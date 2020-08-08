using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private int m_lastCoinLocation = int.MaxValue;

    private float m_spawnRate = 5f;
    private float m_elapsedTime = 0f;

    private GameObject[] m_platforms;
    private GameManager m_gameManager;

    [SerializeField]
    private Coin m_coin;
    [SerializeField]
    private Rain m_rain; 

    // Start is called before the first frame update
    void Start()
    {
        m_platforms = GameObject.FindGameObjectsWithTag("Platform");
        if (m_platforms.Length == 0)
        {
            Debug.LogError("Platforms not found");
        }

        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (!m_gameManager)
        {
            Debug.LogError("Game Manager not found");
        }

        if (!m_coin)
        {
            Debug.LogError("Coin GameObject is not defined");
        }

        SpawnCoin();

        m_spawnRate = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        //m_elapsedTime += Time.deltaTime;
        //if (m_elapsedTime > m_spawnRate)
        //{
        //    m_spawnRate = Time.time + m_gameManager.GetSpawnRate();
        //}
    }

    public void SpawnRain(float spawnRate)
    {
        StartCoroutine(SpawnRainCoroutine(spawnRate));
    }

    IEnumerator SpawnRainCoroutine(float spawnRate)
    {
        yield return new WaitForSeconds(spawnRate);
        while (true)
        {
            float xPosition = Random.Range(-8.7f, 8.7f);
            Instantiate(m_rain, new Vector3(xPosition, 5, 0), Quaternion.identity);
            yield return new WaitForSeconds(3f);
        }
    }


    public void SpawnCoin()
    {
        // Spawn a coin on any platform.
        System.Random random = new System.Random();
        int platform = random.Next(m_platforms.Length);

        // We do not want to spawn the coin in the same location twice in a row.
        while (m_lastCoinLocation == platform)
        {
            platform = random.Next(m_platforms.Length);
        }
        m_lastCoinLocation = platform;

        Platform surface = m_platforms[platform].GetComponent<Platform>();
        Transform location = surface.transform.Find("SpawnLocation");
        if (!location)
        {
            Debug.LogError("Spawn location not found");
        }

        Instantiate(m_coin, location.transform.position, Quaternion.identity);
    }
}
