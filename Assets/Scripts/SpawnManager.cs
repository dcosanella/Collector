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
    private Coroutine m_spawnCoroutine;

    [SerializeField]
    private Coin m_coin;
    [SerializeField]
    private Rain m_rain; 

    // Start is called before the first frame update
    void Start()
    {
        m_platforms = GameObject.FindGameObjectsWithTag(SceneAssets.Platform);
        if (m_platforms.Length == 0)
        {
            Debug.LogError(ErrorMessages.PlatformsNotFound);
        }

        m_gameManager = GameObject.Find(SceneManagers.GameManager).GetComponent<GameManager>();
        if (!m_gameManager)
        {
            Debug.LogError(ErrorMessages.GameMgrNotFound);
        }

        if (!m_coin)
        {
            Debug.LogError(ErrorMessages.CoinNotFound);
        }

        SpawnCoin();

        m_spawnRate = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnRain(float spawnRate)
    {
        m_spawnCoroutine = StartCoroutine(SpawnRainCoroutine(spawnRate));
    }

    public void UpdateSpawnCoroutine(float spawnRate)
    {
        if (m_spawnCoroutine != null)
        {
            StopCoroutine(m_spawnCoroutine);
        }

        SpawnRain(spawnRate);
    }

    IEnumerator SpawnRainCoroutine(float spawnRate)
    {
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            float xPosition = Random.Range(-8.7f, 8.7f);
            Instantiate(m_rain, new Vector3(xPosition, 5, 0), Quaternion.identity);
            yield return new WaitForSeconds(3f - spawnRate);
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
        Transform location = surface.transform.Find(SceneAssets.SpawnLocation);
        if (!location)
        {
            Debug.LogError(ErrorMessages.SpawnLocationNotFound);
        }

        Instantiate(m_coin, location.transform.position, Quaternion.identity);
    }
}
